using UnityEngine;

[CreateAssetMenu(fileName = "NavigationState", menuName = "Inventory/States/NavigationState", order = 50)]
public class NavigationState : SO_BaseInventoryState
{
    private static InventoryStateEnum _state = InventoryStateEnum.NAVIGATION;
    [SerializeField]
    private SO_BaseInventoryState _submenuState;

    public NavigationState() : base() {
        if(_submenuState != null){
            _submenuState.SetUIController(_baseUIController);
        }
    }
    public override IInventoryState Action()
    {
        IInventoryState returnState = this;
        UIControlEnum pressedButton = _baseUIController.GetButtonPressed();
        switch(pressedButton){
            case UIControlEnum.UP:
            case UIControlEnum.DOWN:
            case UIControlEnum.LEFT:
            case UIControlEnum.RIGHT:
                returnState = Move(pressedButton);
                break;
            case UIControlEnum.ACTION:
                // TODO: change state
                ISlot currentSlot = _slotManager.GetSelectedSlot();
                if(currentSlot != null && currentSlot.OpenSubmenu()){
                    _submenuState.SetPreviousState(this);
                    returnState = _submenuState;
                }
                break;
            case UIControlEnum.RETURN:
                // TODO: Exit navigation
                returnState = this;
                break;
            case UIControlEnum.NOT_PRESSED:
            default:
                break;
        }
        return returnState;
    }
    public override IInventoryState AddSlot()
    {
        //_slotManager.GetInventoryMap().AddSlot(_slotManager.GetAllSlots()[-1]);
        return this;
    }

    public override IInventoryState AddItem(IItem item)
    {
        if (item != null)
        {
            int itemCount = item.GetItemCount();
            _slotManager.GetAllSlots().ForEach(slot =>
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
        _slotManager.GetAllSlots().ForEach(slot => {
            if(slot != null && freeSlot == null){
                if(slot.IsActive() && slot.GetItem() == null){
                    freeSlot = slot;
                    return;
                }
            }
        });
        return freeSlot;
    }
    public IInventoryState Move(UIControlEnum move)
    {
        ISlot selectedSlot = UIMovement(move);
        if (selectedSlot != null && selectedSlot.IsActive())
        {
            // if (_slotManager.GetSelectedSlot() != null && _slotManager.GetSelectedSlot().IsSelected())
            // {
            //     _slotManager.GetSelectedSlot().Deselect();
                
            // }
            selectedSlot.MarkAsSelected();
            _slotManager.SetSelectedSlot(selectedSlot);
        }
        return this;
    }
    private ISlot UIMovement(UIControlEnum movementEnum)
    {
        Vector2Int selectedSlot = Vector2Int.zero;
        if(_slotManager.GetSelectedSlot() != null){
            int vert = VerticalMovement(movementEnum);
            int hori = HorizontalMovement(movementEnum);
            Vector2Int currentSlot = _slotManager.GetInventoryMap().FindSlotPosition(_slotManager.GetSelectedSlot());
            selectedSlot = new Vector2Int(currentSlot.y + hori, currentSlot.x + vert);
        }
        return _slotManager.GetInventoryMap().GetSlot(selectedSlot, movementEnum);
    }
    private int VerticalMovement(UIControlEnum movementEnum)
    {
        return UIControlEnum.DOWN.Equals(movementEnum) ? 1 : UIControlEnum.UP.Equals(movementEnum) ? -1 : 0;
    }
    private int HorizontalMovement(UIControlEnum movementEnum)
    {
        return UIControlEnum.LEFT.Equals(movementEnum) ? -1 : UIControlEnum.RIGHT.Equals(movementEnum) ? 1 : 0;
    }
    public override InventoryStateEnum GetState(){
        return _state;
    }
}
