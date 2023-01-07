using Inventory.Slots;
using Inventory.Items;

namespace Inventory.State
{
    public interface IInventoryState
    {
        void SetSlotManager(ISlotManager inventoryManager);
        void SetUIController(IUIController baseUIController);
        IUIController GetUIController();
        IInventoryState Action();
        IInventoryState Action(UIControlEnum pressedButton);
        IInventoryState AddSlot();
        IInventoryState AddItem(IItem item);
        InventoryStateEnum GetState();
        IInventoryState GetPreviousState();
        void SetPreviousState(IInventoryState state);
        void ExitState();
    }
}