public interface IInventory : ISlotManager {
    void Action();
    void Action(UIControlEnum pressedButton);
    void Move(UIControlEnum move);
    void AddSlot();
    void AddItem(IItem item);
    IItem GetItem();
    void SetState(IInventoryState state);
    IInventoryState GetState();
}