using UnityEngine;

public abstract class SO_BaseInventoryState : ScriptableObject, IInventoryState {
    protected IUIController _baseUIController;
    protected ISlotManager _slotManager;
    protected IInventoryState _previousState;
    public void SetSlotManager(ISlotManager slotManager){
        _slotManager = slotManager;
    }
    public void SetUIController(IUIController baseUIController){
        _baseUIController = baseUIController;
    }
    public IUIController GetUIController(){
        return _baseUIController;
    }
    public abstract IInventoryState Action(UIControlEnum pressedButton);
    public abstract IInventoryState Action();
    public abstract IInventoryState AddSlot();
    public abstract IInventoryState AddItem(IItem item);
    public abstract InventoryStateEnum GetState();
    public IInventoryState GetPreviousState(){
        return _previousState;
    }
    public void SetPreviousState(IInventoryState state){
        if(state != null){
            _previousState = state;
        }
    }
    public abstract void ExitState();
}
