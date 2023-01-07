using System.Collections.Generic;

namespace Inventory
{
    public class MessageType
    {
        public enum MessageTypeEnum
        {
            COLOR_SCHEMA,
            ISLOT,
            IITEM
        }

        private MessageType() { }

        public static List<MessageTypeEnum> ALL_TYPES = new List<MessageTypeEnum>(){
        MessageTypeEnum.COLOR_SCHEMA,
        MessageTypeEnum.ISLOT,
        MessageTypeEnum.IITEM
    };

        public static Dictionary<MessageTypeEnum, string> MESSAGE_MAP = new Dictionary<MessageTypeEnum, string>(){
        {MessageTypeEnum.COLOR_SCHEMA, "Inventory.UIColorSchema"},
        {MessageTypeEnum.ISLOT, "Inventory.Slots.ISlot"},
        {MessageTypeEnum.IITEM, "Inventory.IItem"}
    };
    }
}