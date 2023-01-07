using System.Collections.Generic;
namespace Inventory.Slots
{
    public class SlotRow
    {
        public List<ISlot> _slots;
        public SlotRow(List<ISlot> slots)
        {
            _slots = slots;
        }
    }
}