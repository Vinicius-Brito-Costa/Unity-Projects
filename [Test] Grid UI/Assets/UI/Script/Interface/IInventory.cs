using Inventory.Slots;
using Inventory.Items;
using Inventory.State;
namespace Inventory
{
    public interface IInventory : ISlotManager, IColorized, IPublisher
    {
        void Action();
        void Action(UIControlEnum pressedButton);
        void Move(UIControlEnum move);
        void AddItem(IItem item);
        IItem GetItem();
        void SetState(IInventoryState state);
        IInventoryState GetState();
    }
}