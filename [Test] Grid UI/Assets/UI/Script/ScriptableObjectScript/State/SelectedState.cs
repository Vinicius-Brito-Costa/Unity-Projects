using UnityEngine;
[CreateAssetMenu(fileName = "NavigationState", menuName = "Inventory/States/SelectedState", order = 50)]
public class SelectedState : SO_BaseInventoryState{
    private static InventoryStateEnum _state = InventoryStateEnum.SUBMENU;
    public override IInventoryState Action(){
        return Action(_baseUIController.GetButtonPressed());
    }
    public override IInventoryState Action(UIControlEnum pressedButton){
        IInventoryState returnState = this;
        switch(pressedButton){
            case UIControlEnum.UP:
            case UIControlEnum.DOWN:
            case UIControlEnum.LEFT:
            case UIControlEnum.RIGHT:
                
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

    public override void ExitState()
    {
        // Do nothing
    }
}