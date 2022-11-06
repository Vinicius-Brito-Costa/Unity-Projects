using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class ArrayInventory : GridLayoutGroup, IInventory, IInventoryManager
{
    // _slots: contains ALL current slots, locked, unlocked, free and used.
    [SerializeField]
    private List<ISlot> _slots = new List<ISlot>();
    private NavigationState _navigationState;
    [SerializeField]
    private BaseInventoryState _currentState;
    [SerializeField, Min(0)]
    private int _slotCount;
    [SerializeField]
    private ISlot _prefab;
    private ISlot _selected;
    [SerializeField]
    private InventoryMap _inventoryMap;
    
    new void Start()
    {
        base.Start();
        ISlot[] childs = GetComponentsInChildren<ISlot>();
        _selected = childs[0];
        _slots = new List<ISlot>(childs);
        _selected.MarkAsSelected();

        // Set InventoryMap
        _inventoryMap = new InventoryMap(GridLayoutGroupHelper.Size(this), _slots);
        
        _navigationState = new NavigationState(this);
        _currentState = _navigationState;
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
                    if(Application.isPlaying){
                        _inventoryMap.AddSlot(createdSlot);
                        _slots.Add(createdSlot);
                    }
                    
                }
            }
            else if (_slotCount < currentSlotCount)
            {
                for (int removedCount = currentSlotCount - _slotCount; removedCount > 0; removedCount--)
                {
                    if(Application.isPlaying){
                        _slots.RemoveAt(-1);
                        _inventoryMap.RemoveSlot();
                    }
                    DestroyImmediate(childs[removedCount - 1].gameObject);
                }
            }
        }
    }
#endif
    public void AddSlot()
    {
        _slotCount++;
        _currentState = _currentState.AddSlot();
    }
    public void AddItem(IItem item)
    {
        _currentState = _currentState.AddItem(item);
    }
    public void RemoveItem()
    {
        _currentState = _currentState.RemoveItem();
    }
    public void SelectSlot(UIMovementEnum move)
    {
        _currentState = _currentState.SelectSlot(move);
    }
    public void MoveItem(ISlot target)
    {
        _currentState = _currentState.MoveItem(target);
    }
    public IItem GetItem()
    {
        return _selected.GetItem();
    }
    public void OpenSlotMenu() {
        _currentState = _currentState.OpenSlotMenu();
    }
    public GameObject GetGameObject(){
        return this.gameObject;
    }
    public ISlot GetPrefab(){
        return _prefab;
    }
    public ISlot GetSelectedSlot(){
        return _selected;
    }
    public void SetSelectedSlot(ISlot slot){
        _selected = slot;
    }
    public List<ISlot> GetAllSlots(){
        return _slots;
    }
    public InventoryMap GetInventoryMap(){
        return _inventoryMap;
    }
}
