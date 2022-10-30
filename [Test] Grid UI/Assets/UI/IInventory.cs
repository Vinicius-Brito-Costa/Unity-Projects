public interface IInventory {
    void AddSlot();
    bool AddItem(IItem item);
    bool RemoveItem();
    void SelectSlot(UIMovementEnum move);
    void MoveItem(ISlot target);
    IItem GetItem();
}