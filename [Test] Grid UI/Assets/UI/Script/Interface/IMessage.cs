using static Inventory.MessageType;

namespace Util
{
    public interface IMessage<T>
    {
        MessageTypeEnum GetMessageType();
        void SetMessage(T subject, MessageTypeEnum type);
        T GetMessage();
    }
}