using UnityEngine;
public class NavigationState : BaseInventoryState
{
    private static InventoryStateEnum _state = InventoryStateEnum.NAVIGATION;
    private IInventoryManager _inventory;
    public NavigationState(IInventoryManager inventory){
        if(inventory != null){
            this._inventory = inventory;
        }
    }
    public override BaseInventoryState AddSlot()
    {
        //_inventory.GetInventoryMap().AddSlot(_inventory.GetAllSlots()[-1]);
        return this;
    }

    public override BaseInventoryState AddItem(IItem item)
    {
        if (item != null)
        {
            int itemCount = item.GetItemCount();
            _inventory.GetAllSlots().ForEach(slot =>
            {
                if (itemCount <= 0) return;
                if (slot == null) return;
                if (slot.IsActive())
                {
                    IItem slotItem = slot.GetItem();
                    if (slotItem != null)
                    {
                        if (slotItem.GetName().Equals(item.GetName()))
                        {
                            int itemFreeQuantity = slotItem.GetMaxCount() - slotItem.GetItemCount();
                            if (itemFreeQuantity > 0)
                            {
                                slotItem.AddItem(itemFreeQuantity);
                                itemCount -= itemFreeQuantity;
                                item.DropItem(itemFreeQuantity);
                            }
                        }
                    }
                }
            });
            if (itemCount > 0)
            {
                // If item count is above 0, a new slot must be used
                ISlot freeSlot = GetFreeSlot();
                if (freeSlot != null)
                {
                    freeSlot.AddItem(item);
                }
            }
        }
        return this;
    }
    private ISlot GetFreeSlot(){
        ISlot freeSlot = null;
        _inventory.GetAllSlots().ForEach(slot => {
            if(slot != null && freeSlot == null){
                if(slot.IsActive() && slot.GetItem() == null){
                    freeSlot = slot;
                    return;
                }
            }
        });
        return freeSlot;
    }
    public override BaseInventoryState RemoveItem()
    {
        _inventory.GetSelectedSlot().RemoveItem();
        return this;
    }
    public override BaseInventoryState SelectSlot(UIMovementEnum move)
    {
        ISlot selectedSlot = UIMovement(move);
        if (selectedSlot != null)
        {
            if (selectedSlot.IsActive())
            {
                _inventory.GetSelectedSlot().Deselect();
                selectedSlot.MarkAsSelected();
                _inventory.SetSelectedSlot(selectedSlot);
            }
        }
        return this;
    }
    private ISlot UIMovement(UIMovementEnum movementEnum)
    {
        int vert = VerticalMovement(movementEnum);
        int hori = HorizontalMovement(movementEnum);
        Vector2Int currentSlot = _inventory.GetInventoryMap().FindSlotPosition(_inventory.GetSelectedSlot());
        Vector2Int selectedSlot = new Vector2Int(currentSlot.y + hori, currentSlot.x + vert);
        return _inventory.GetInventoryMap().GetSlot(selectedSlot, movementEnum);
    }
    private int VerticalMovement(UIMovementEnum movementEnum)
    {
        return UIMovementEnum.DOWN.Equals(movementEnum) ? 1 : UIMovementEnum.UP.Equals(movementEnum) ? -1 : 0;
    }
    private int HorizontalMovement(UIMovementEnum movementEnum)
    {
        return UIMovementEnum.LEFT.Equals(movementEnum) ? -1 : UIMovementEnum.RIGHT.Equals(movementEnum) ? 1 : 0;
    }
    public override BaseInventoryState MoveItem(ISlot target)
    {
        // do something
        return this;
    }
    public override BaseInventoryState OpenSlotMenu() {
        return this;
    }
    public override InventoryStateEnum GetState(){
        return _state;
    }
}
