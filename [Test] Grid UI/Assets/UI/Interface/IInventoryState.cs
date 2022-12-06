public interface IInventoryState {
    void SetSlotManager(ISlotManager inventoryManager);
    void SetUIController(IUIController baseUIController);
    IUIController GetUIController();
    IInventoryState Action();
    IInventoryState AddSlot();
    IInventoryState AddItem(IItem item);
    InventoryStateEnum GetState();
    IInventoryState GetPreviousState();
    void SetPreviousState(IInventoryState state);
}