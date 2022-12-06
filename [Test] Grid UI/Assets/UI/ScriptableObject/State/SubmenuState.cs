using UnityEngine;
[CreateAssetMenu(fileName = "SubmenuState", menuName = "Inventory/States/SubmenuState", order = 50)]
public class SubmenuState : SO_BaseInventoryState{
    private static InventoryStateEnum _state = InventoryStateEnum.SELECTED;

    public override IInventoryState Action(){
        IInventoryState returnState = this;
        ISlot currentSlot = _slotManager.GetSelectedSlot();
        switch(_baseUIController.GetButtonPressed()){
            case UIControlEnum.UP:
            case UIControlEnum.DOWN:
            case UIControlEnum.LEFT:
            case UIControlEnum.RIGHT:
                // TODO: Submenu movement
                break;
            case UIControlEnum.ACTION:
                currentSlot.Activate();
                return this;
            case UIControlEnum.RETURN:
                // TODO: Exit to previous state
                return _previousState;
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