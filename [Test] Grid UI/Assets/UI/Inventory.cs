using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Inventory : MonoBehaviour, IInventory
{
    // _slots: contains ALL current slots, locked, unlocked, free and used.
    private List<ISlot> _slots = new List<ISlot>();
    private List<ISlot> _lockedSlots = new List<ISlot>();
    private List<ISlot> _unlockedSlots = new List<ISlot>();
    private List<ISlot> _freeSlots = new List<ISlot>();
    private List<ISlot> _usedSlots = new List<ISlot>();

    [SerializeField, Min(2)]
    private int _maxItemsPerRow;
    private ISlot _selected;

    struct InventoryMap
    {
        int rows { get; set; }
        int collumns { get; set; }

        public InventoryMap(int rows, int collumns)
        {
            this.rows = rows;
            this.collumns = collumns;
        }
    }

    void Start()
    {

        ISlot[] childs = GetComponentsInChildren<ISlot>();
        for (int index = 0; index < childs.Length; index++)
        {
            _slots.Add(childs[index]);
            _freeSlots.Add(childs[index]);
            _unlockedSlots.Add(childs[index]);
        }
        _selected = childs[0];
        _selected.MarkAsSelected();
        Debug.Log("Slot count: " + childs.Length);
    }

    public bool AddItem(IItem item)
    {
        bool response = false;
        if (item != null)
        {
            int itemCount = item.GetItemCount();
            _usedSlots.ForEach(slot =>
            {
                if (itemCount <= 0) return;
                if (slot == null) return;
                if (slot.IsActive())
                {
                    IItem slotItem = slot.GetItem();
                    if (slotItem != null)
                    {
                        if (slotItem.GetName().Equals(item.GetName()))
                        {
                            int itemFreeQuantity = slotItem.GetMaxCount() - slotItem.GetItemCount();
                            if (itemFreeQuantity > 0)
                            {
                                slotItem.AddItem(itemFreeQuantity);
                                itemCount -= itemFreeQuantity;
                                item.DropItem(itemFreeQuantity);
                                response = true;
                            }
                        }
                    }
                }
            });
            if (itemCount > 0)
            {
                // If item count is above 0, a new slot must be used(freeSlots list)
                // After the item is added, the slot is marked as a usedSlot and removed from
                // the freeSlot list
                if (_freeSlots.Count > 0)
                {
                    _freeSlots[0].AddItem(item);
                    _usedSlots.Add(_freeSlots[0]);
                    _freeSlots.Remove(_freeSlots[0]);
                    response = true;
                }
            }
        }

        return response;
    }
    public bool RemoveItem()
    {
        _selected.RemoveItem();
        _freeSlots.Add(_selected);
        return _selected.IsEmpty();
    }
    public void SelectSlot(UIMovementEnum move)
    {
        int currentIndex = _slots.IndexOf(_selected);
        int totalSlots = _unlockedSlots.Count - 1;
        int slotIndex = currentIndex;
        
        if (UIMovementEnum.UP.Equals(move))
        {
            int selectedIndex = currentIndex - _maxItemsPerRow;
            slotIndex = VerticalUIMovement(currentIndex, selectedIndex, totalSlots);

        }
        if (UIMovementEnum.DOWN.Equals(move))
        {
            int selectedIndex = currentIndex + _maxItemsPerRow;
            slotIndex = VerticalUIMovement(currentIndex, selectedIndex, totalSlots);

        }
        if (UIMovementEnum.LEFT.Equals(move))
        {
            int selectedIndex = currentIndex - 1;
            slotIndex = HorizontalUIMovement(currentIndex, selectedIndex, totalSlots);

        }
        if (UIMovementEnum.RIGHT.Equals(move))
        {
            int selectedIndex = currentIndex + 1;
            slotIndex = HorizontalUIMovement(currentIndex, selectedIndex, totalSlots);
        }
        ISlot selectedSlot = _slots[slotIndex];
        if (selectedSlot != null)
        {
            if (selectedSlot.IsActive())
            {
                _selected.Deselect();
                selectedSlot.MarkAsSelected();
                _selected = selectedSlot;
            }
        }
    }
    private int GetCurrentRows(){
        float filledRow = ((float)_unlockedSlots.Count) / ((float) _maxItemsPerRow);
        return Mathf.CeilToInt(filledRow);
    }
    private int HorizontalUIMovement(int currentIndex, int selectedIndex, int totalSlots){
        int slotIndex = currentIndex;
        bool listWillBeLooped = selectedIndex < 0 || selectedIndex > totalSlots;
        if (listWillBeLooped)
        {
            // Left?
            if(selectedIndex < 0){
                slotIndex = totalSlots;
            }
            // Then Right
            else { 
                slotIndex = 0;
            }
            
        }
        else
        {
            slotIndex = selectedIndex;
        }
        return slotIndex;
    }
    private int VerticalUIMovement(int currentIndex, int selectedIndex, int totalSlots){
        int slotIndex = currentIndex;
        bool listWillBeLooped = selectedIndex < 0 || selectedIndex > totalSlots;
        int rows = GetCurrentRows();
        if (listWillBeLooped)
        {
            int index;
            // Up?
            if(selectedIndex < 0){
                bool incompleteLastRow = totalSlots + 1 < (rows * _maxItemsPerRow);
                index = selectedIndex + totalSlots;
                if(incompleteLastRow){
                    int slotsCompleteRows = ((totalSlots) - ((rows - 1) * _maxItemsPerRow));
                    if(currentIndex <= slotsCompleteRows){
                        index += _maxItemsPerRow - slotsCompleteRows;
                    }
                    else{
                        index--;
                    }
                }
            }
            // Then Down
            else {
                float rowsWithSelectedIndex = ((float) selectedIndex + 1) / ((float) _maxItemsPerRow);
                int totalRows = Mathf.CeilToInt(rowsWithSelectedIndex);
                int discardRows = (totalRows - 1) * _maxItemsPerRow;
                index = selectedIndex - discardRows;
                slotIndex = index;
                
            }
            
            slotIndex = Mathf.Abs(index);
        }
        else
        {
            slotIndex = selectedIndex;
        }
        return slotIndex;
    }
    private bool ResetListIndex(UIMovementEnum movement){
        bool response = false;

        return response;
    }
    public void MoveItem(ISlot target)
    {
        // do something
        
    }
    public IItem GetItem(){
        return _selected.GetItem();
    }
}
