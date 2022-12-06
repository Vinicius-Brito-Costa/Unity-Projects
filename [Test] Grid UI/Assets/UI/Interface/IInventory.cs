public interface IInventory : ISlotManager {
    void Action();
    void Move(UIControlEnum move);
    void AddSlot();
    void AddItem(IItem item);
    IItem GetItem();
    void SetState(IInventoryState state);
    IInventoryState GetState();
}