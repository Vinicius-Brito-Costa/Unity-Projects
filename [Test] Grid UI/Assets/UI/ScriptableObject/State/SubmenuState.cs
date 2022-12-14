using UnityEngine;
[CreateAssetMenu(fileName = "SubmenuState", menuName = "Inventory/States/SubmenuState", order = 50)]
public class SubmenuState : SO_BaseInventoryState{
    private Submenu _currentSubmenu;
    private static InventoryStateEnum _state = InventoryStateEnum.SELECTED;

    public override IInventoryState Action(){
        return Action(_baseUIController.GetButtonPressed());
    }
    public override IInventoryState Action(UIControlEnum pressedButton){
        IInventoryState returnState = this;
        ISlot currentSlot = _slotManager.GetSelectedSlot();
        _currentSubmenu = currentSlot.GetSubmenu();
        switch(pressedButton){
            case UIControlEnum.UP:
            case UIControlEnum.DOWN:
            case UIControlEnum.LEFT:
            case UIControlEnum.RIGHT:
                _currentSubmenu.Move(pressedButton);
                break;
            case UIControlEnum.ACTION:
                _currentSubmenu.Action(currentSlot);
                break;
            case UIControlEnum.RETURN:
                currentSlot.CloseSubmenu();
                returnState = _previousState;
                break;
            default:
                break;
        }
        return returnState;
    }
    public override IInventoryState AddSlot(){
        // Do nothing
        return this;
    }
    public override IInventoryState AddItem(IItem item){
        // Do nothing
        return this;
    }
    public override InventoryStateEnum GetState(){
        // Do nothing
        return _state;
    }
}