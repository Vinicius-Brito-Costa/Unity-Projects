using UnityEngine;
using Inventory.Slots;
using Inventory.Items;
namespace Inventory.State
{
    [CreateAssetMenu(fileName = "SubmenuState", menuName = "Inventory/States/SubmenuState", order = 50)]
    public class SubmenuState : SO_BaseInventoryState
    {
        private static InventoryStateEnum _state = InventoryStateEnum.SUBMENU;
        private ISubmenu _currentSubmenu = null;

        public override IInventoryState Action()
        {
            return Action(_baseUIController.GetButtonPressed());
        }
        public override IInventoryState Action(UIControlEnum pressedButton)
        {
            IInventoryState returnState = this;

            ISlot currentSlot = _slotManager.GetSelectedSlot();
            if (currentSlot == null || currentSlot.GetItem() == null) return returnState;

            if (_currentSubmenu == null)
            {
                if (currentSlot.OpenSubmenu(_slotManager.GetSubmenu()))
                {
                    _currentSubmenu = _slotManager.GetSubmenu();
                }
                else
                {
                    return _previousState;
                }
            }

            switch (pressedButton)
            {
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
                    _currentSubmenu = null;
                    returnState = _previousState;
                    ExitState();
                    break;
                default:
                    break;
            }
            return returnState;
        }
        public override IInventoryState AddSlot()
        {
            // Do nothing
            return this;
        }
        public override IInventoryState AddItem(IItem item)
        {
            // Do nothing
            return this;
        }
        public override InventoryStateEnum GetState()
        {
            // Do nothing
            return _state;
        }

        public override void ExitState()
        {
            _slotManager.GetAllSlots().ForEach(slot => slot.Activate());
        }
    }
}