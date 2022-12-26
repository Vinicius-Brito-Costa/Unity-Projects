public interface IInventory : ISlotManager, IColorized {
    void Action();
    void Action(UIControlEnum pressedButton);
    void Move(UIControlEnum move);
    void AddItem(IItem item);
    IItem GetItem();
    void SetState(IInventoryState state);
    IInventoryState GetState();
}