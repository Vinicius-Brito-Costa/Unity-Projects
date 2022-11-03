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
    
    new void Start()
    {
        base.Start();
        ISlot[] childs = GetComponentsInChildren<ISlot>();
        _selected = childs[0];
        _slots = new List<ISlot>(childs);
        _unlockedSlots = new List<ISlot>(childs);
        _freeSlots = new List<ISlot>(childs);
        _selected.MarkAsSelected();

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
            if (selectedSlot.IsActive())
            {
                _selected.Deselect();
                selectedSlot.MarkAsSelected();
                _selected = selectedSlot;
            }
        }
    }
    private ISlot UIMovement(UIMovementEnum movementEnum)
    {
        int vert = VerticalMovement(movementEnum);
        int hori = HorizontalMovement(movementEnum);
        Vector2Int currentSlot = _inventoryMap.FindSlotPosition(_selected);
        Debug.Log("Movement: " + movementEnum);
        Debug.Log("Current Slot Position: " + currentSlot);
        Vector2Int selectedSlot = new Vector2Int(currentSlot.y + hori, currentSlot.x + vert);
        Debug.Log("Selected Slot Position: " + selectedSlot);
        return _inventoryMap.GetSlot(selectedSlot, movementEnum);
    }
    private int VerticalMovement(UIMovementEnum movementEnum)
    {
        return UIMovementEnum.DOWN.Equals(movementEnum) ? 1 : UIMovementEnum.UP.Equals(movementEnum) ? -1 : 0;
    }
    private int HorizontalMovement(UIMovementEnum movementEnum)
    {
        return UIMovementEnum.LEFT.Equals(movementEnum) ? -1 : UIMovementEnum.RIGHT.Equals(movementEnum) ? 1 : 0;
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
