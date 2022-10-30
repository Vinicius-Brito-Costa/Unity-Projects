using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class ArrayInventory : GridLayoutGroup, IInventory
{
    // _slots: contains ALL current slots, locked, unlocked, free and used.
    [SerializeField]
    private List<ISlot> _slots = new List<ISlot>();
    private List<ISlot> _lockedSlots = new List<ISlot>();
    private List<ISlot> _unlockedSlots = new List<ISlot>();
    private List<ISlot> _freeSlots = new List<ISlot>();
    private List<ISlot> _usedSlots = new List<ISlot>();
    [SerializeField, Min(0)]
    private int _slotCount;
    [SerializeField]
    private ISlot _prefab;
    private ISlot _selected;
    public String _teste;
    [SerializeField]
    private InventoryMap _inventoryMap;
    [Serializable]
    public class InventoryMap
    {
        [SerializeField]
        private List<SlotCollumn> _map = new List<SlotCollumn>();
        [SerializeField]
        private int rows;
        [SerializeField]
        private int collumns;

        [Serializable]
        public class SlotCollumn{
            public List<ISlot> slots;
            public SlotCollumn(List<ISlot> slots){
                this.slots = slots;
            }
        }
        public InventoryMap(Vector2Int map, List<ISlot> slots)
        {
            this.rows = map.y;
            this.collumns = map.x;
            for(int rowCount = 0; rowCount < this.rows; rowCount++){
                int index = (this.collumns) > (slots.Count - ((rowCount + 1) * rows)) ? (slots.Count - ((rowCount + 1) * rows)) : this.collumns;
                List<ISlot> rowCollumns = slots.GetRange((rowCount * this.collumns), index);
                _map.Add(new SlotCollumn(rowCollumns));
            }
        }

        public SlotCollumn GetCollumn(int index){
            return _map[index];
        }
        public ISlot GetSlot(Vector2Int position){
            ISlot slot = null;
            int yPos = position.y;
            int xPos = position.x;
            if(yPos >= _map.Count){
                yPos = 0;
            }
            else if(yPos < 0){
                yPos = _map.Count - 1;
            }
            SlotCollumn collumn = _map[yPos];
            if(collumn != null &&
                collumn.slots != null){
                if(xPos >= collumn.slots.Count){
                    xPos = 0;
                }
                else if(xPos < 0){
                    xPos = collumn.slots.Count - 1;
                }
                slot = collumn.slots[xPos];
            }
            return slot;
        }
        public Vector2Int FindSlotPosition(ISlot slot){
            Vector2Int pos = new Vector2Int(0, 0);
            for(int rIndex = 0; rIndex < _map.Count; rIndex++){
                List<ISlot> cSlots = _map[rIndex].slots;
                if(cSlots.Contains(slot)){
                    pos = new Vector2Int(rIndex, cSlots.IndexOf(slot));
                }
            }
            return pos;
        }
    }

    new void Start()
    {
        base.Start();
        Debug.Log(_teste);
        ISlot[] childs = GetComponentsInChildren<ISlot>();
        _selected = childs[0];
        _slots = new List<ISlot>(childs);
        _unlockedSlots = new List<ISlot>(childs);
        _freeSlots = new List<ISlot>(childs);
        _selected.MarkAsSelected();
        Debug.Log("Slot count: " + childs.Length);

        // Set InventoryMap
        _inventoryMap = new InventoryMap(GridLayoutGroupHelper.Size(this), _slots);
    }
// Add Slot prefabs as a child of this element
#if UNITY_EDITOR
    void Update()
    {
        if (_prefab)
        {
            ISlot[] childs = GetComponentsInChildren<ISlot>();
            int currentSlotCount = childs.Length;
            if (_slotCount > currentSlotCount)
            {
                GameObject inventory = this.gameObject;
                for (int index = currentSlotCount; index < _slotCount; index++)
                {
                    ISlot createdSlot = (ISlot)PrefabUtility.InstantiatePrefab(_prefab);
                    createdSlot.gameObject.transform.SetParent(inventory.transform);
                    createdSlot.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                }

            }
            else if (_slotCount < currentSlotCount)
            {
                for (int removedCount = currentSlotCount - _slotCount; removedCount > 0; removedCount--)
                {
                    DestroyImmediate(childs[removedCount - 1].gameObject);
                }
            }
        }
    }
#endif
    public void AddSlot()
    {
        ISlot slot = null;//Instantiate(_slots[_slots.Count - 1], Vector3.zero, Quaternion.identity);
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
        ISlot selectedSlot = UIMovement(move);
        if (selectedSlot != null)
        {
            Debug.Log("Selected Slot is not null");
            if (selectedSlot.IsActive())
            {
                _selected.Deselect();
                selectedSlot.MarkAsSelected();
                _selected = selectedSlot;
            }
        }
    }
    private ISlot UIMovement(UIMovementEnum movementEnum){
        int vert = VerticalMovement(movementEnum);
        int hori = HorizontalMovement(movementEnum);
        Vector2Int currentSlot = _inventoryMap.FindSlotPosition(_selected);
        Debug.Log("Movement: " + movementEnum);
        Debug.Log("Current Slot Position: " + currentSlot);
        Vector2Int selectedSlot = new Vector2Int(currentSlot.y + hori, currentSlot.x + vert);
        Debug.Log("Selected Slot Position: " + selectedSlot);
        return _inventoryMap.GetSlot(selectedSlot);
    }
    private int VerticalMovement(UIMovementEnum movementEnum){
        return UIMovementEnum.DOWN.Equals(movementEnum) ? -1 : UIMovementEnum.UP.Equals(movementEnum) ? 1 : 0;
    }
    private int HorizontalMovement(UIMovementEnum movementEnum){
        return UIMovementEnum.LEFT.Equals(movementEnum) ? -1 : UIMovementEnum.RIGHT.Equals(movementEnum) ? 1 : 0;
    }
    private int GetCurrentRows()
    {
        if (GridLayoutGroup.Constraint.FixedRowCount.Equals(constraint))
        {
            return constraintCount;
        }
        float filledRow = ((float)_unlockedSlots.Count) / ((float)constraintCount);
        return Mathf.CeilToInt(filledRow);
    }
    private int HorizontalUIMovement(int currentIndex, int selectedIndex, int totalSlots)
    {
        int slotIndex = currentIndex;
        bool listWillBeLooped = selectedIndex < 0 || selectedIndex > totalSlots;
        if (listWillBeLooped)
        {
            // Left?
            if (selectedIndex < 0)
            {
                slotIndex = totalSlots;
            }
            // Then Right
            else
            {
                slotIndex = 0;
            }

        }
        else
        {
            slotIndex = selectedIndex;
        }
        return slotIndex;
    }
    private int GetItemsPerRow()
    {
        if (GridLayoutGroup.Constraint.FixedColumnCount.Equals(constraint))
        {
            return constraintCount;
        }
        float slotCount = _slots.Count;
        return Mathf.CeilToInt(slotCount / constraintCount);
    }
    private int VerticalUIMovement(int currentIndex, int selectedIndex, int totalSlots, Vector2Int inventoryMap)
    {
        int slotIndex = currentIndex;
        Debug.Log("SelectedIndex: " + selectedIndex);
        bool listWillBeLooped = selectedIndex < 0 || selectedIndex > totalSlots;
        int rows = inventoryMap.y;
        int itemsPerRow = inventoryMap.x;
        Debug.Log("Items per row: " + itemsPerRow);
        if (listWillBeLooped)
        {
            int index;
            // Up?
            if (selectedIndex < 0)
            {
                bool incompleteLastRow = totalSlots + 1 < (rows * itemsPerRow);
                index = selectedIndex + totalSlots + 1;
                if (incompleteLastRow)
                {
                    int slotsInTheIncompletedRow = ((totalSlots) - ((rows - 1) * itemsPerRow));

                    if (currentIndex <= slotsInTheIncompletedRow)
                    {
                        index += itemsPerRow - slotsInTheIncompletedRow - 1;
                    }
                    else
                    {
                        index -= itemsPerRow - 1;
                    }
                }
            }
            // Then Down
            else
            {
                float rowsWithSelectedIndex = ((float)selectedIndex + 1) / ((float)itemsPerRow);
                int totalRows = Mathf.CeilToInt(rowsWithSelectedIndex);
                int discardRows = (totalRows - 1) * itemsPerRow;
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
    public void MoveItem(ISlot target)
    {
        // do something

    }
    public IItem GetItem()
    {
        return _selected.GetItem();
    }
}
