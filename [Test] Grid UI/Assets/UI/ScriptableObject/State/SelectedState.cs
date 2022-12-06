using UnityEngine;
[CreateAssetMenu(fileName = "NavigationState", menuName = "Inventory/States/SelectedState", order = 50)]
public class SelectedState : SO_BaseInventoryState{
    private static InventoryStateEnum _state = InventoryStateEnum.SELECTED;
    public override IInventoryState Action(){
        IInventoryState returnState = this;
        switch(_baseUIController.GetButtonPressed()){
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
}