using System.Collections.Generic;
public abstract class BaseInventoryState {
    public abstract BaseInventoryState SelectSlot(UIMovementEnum move);
    public abstract BaseInventoryState RemoveItem();
    public abstract BaseInventoryState MoveItem(ISlot target);
    public abstract BaseInventoryState AddSlot();
    public abstract BaseInventoryState AddItem(IItem item);
    public abstract BaseInventoryState OpenSlotMenu();
    public abstract InventoryStateEnum GetState();
}
