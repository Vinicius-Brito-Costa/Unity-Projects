public interface IInventory {
    bool AddItem(IItem item);
    bool RemoveItem();
    void SelectSlot(UIMovementEnum move);
    void MoveItem(ISlot target);
    IItem GetItem();
}