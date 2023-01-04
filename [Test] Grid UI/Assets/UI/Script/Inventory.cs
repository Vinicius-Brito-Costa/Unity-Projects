using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class Inventory : GridLayoutGroup, IInventory
{
    // _slots: contains ALL current slots, locked, unlocked, free and used.
    private List<ISlot> _slots;
    [SerializeField]
    private SO_BaseInventoryState _currentState;
    [SerializeField, Min(0)]
    private int _slotCount;
    [SerializeField]
    private ISlot _prefab;
    private ISlot _selected;
    private ISubmenu _submenu;
    [SerializeField]
    private InventoryMap _inventoryMap;
    [SerializeField]
    private UIColorSchema _colorSchema;

    new void Start()
    {
        base.Start();
        ISlot[] childs = GetComponentsInChildren<ISlot>();
        _selected = childs[0];
        _slots = new List<ISlot>(childs);
        _selected.MarkAsSelected();

        // Set InventoryMap
        _inventoryMap = new InventoryMap(GridLayoutGroupHelper.Size(this), _slots);
        _slots.ForEach(slot => slot.UpdateColor(_colorSchema));
    }
    // Add Slot prefabs as a child of this element
    void Update()
    {
        if(!Application.isPlaying && _colorSchema != null){
            UpdateSlotList();
            UpdateColor(_colorSchema);
        }
    }
    private void UpdateSlotList()
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
                    ISlot createdSlot = (ISlot)PrefabUtility.InstantiatePrefab(_prefab, inventory.transform);
                    createdSlot.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    if (Application.isPlaying)
                    {
                        _inventoryMap.AddSlot(createdSlot);
                        _slots.Add(createdSlot);
                        UpdateColor(_colorSchema);
                    }
                }
            }
            else if (_slotCount < currentSlotCount)
            {
                for (int removedCount = currentSlotCount - _slotCount; removedCount > 0; removedCount--)
                {
                    if (Application.isPlaying)
                    {
                        _slots.RemoveAt(-1);
                        _inventoryMap.RemoveSlot();
                    }
                    DestroyImmediate(childs[removedCount - 1].gameObject);
                }
            }
        }
    }
    public ISlot AddSlot()
    {
        _slotCount++;
        UpdateSlotList();
        return _slots[_slots.Count - 1];
    }
    public void AddItem(IItem item)
    {
        _currentState = (SO_BaseInventoryState)_currentState.AddItem(item);
    }
    public void Action()
    {
        if (_currentState != null)
        {
            Debug.Log("State: " + _currentState.GetState());
            _currentState = (SO_BaseInventoryState)_currentState.Action();
        }
    }
    public void Action(UIControlEnum pressedButton)
    {
        if (_currentState != null)
        {
            _currentState = (SO_BaseInventoryState)_currentState.Action(pressedButton);
        }
    }
    public void Move(UIControlEnum move)
    {
        //_currentState = _currentState.Move(move);
    }
    public void MoveItem(ISlot target)
    {
        //_currentState = _currentState.MoveItem(target);
    }
    public IItem GetItem()
    {
        return _selected.GetItem();
    }
    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
    public ISlot GetPrefab()
    {
        return _prefab;
    }
    public ISlot GetSelectedSlot()
    {
        return _selected;
    }
    public void SetSelectedSlot(ISlot slot)
    {
        if (_selected != null)
        {
            _selected.Deselect();
        }
        _selected = slot;
        _selected.MarkAsSelected();
    }
    public List<ISlot> GetAllSlots()
    {
        return _slots;
    }
    public InventoryMap GetInventoryMap()
    {
        return _inventoryMap;
    }
    public void SetState(IInventoryState state)
    {
        _currentState = (SO_BaseInventoryState)state;
    }
    public IInventoryState GetState()
    {
        return _currentState;
    }

    public void UpdateColor(UIColorSchema colorSchema)
    {
        _colorSchema = colorSchema;
        _submenu?.UpdateColor(_colorSchema);
        _slots?.ForEach(slot => {
            if(slot){
                slot.UpdateColor(_colorSchema);
            }
        });
    }

    public void SetSubmenu(ISubmenu submenu)
    {
        this._submenu = submenu;
    }
    public ISubmenu GetSubmenu()
    {
        return _submenu;
    }
}
