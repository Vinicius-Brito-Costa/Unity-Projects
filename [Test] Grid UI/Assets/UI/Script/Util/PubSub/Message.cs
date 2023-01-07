using UnityEngine;
using static Inventory.MessageType;

namespace Util
{
    public class Message<T> : MonoBehaviour, IMessage<T>
    {
        [SerializeField]
        private MessageTypeEnum _type;
        [SerializeField]
        private T _message;
        public T GetMessage()
        {
            return _message;
        }

        public MessageTypeEnum GetMessageType()
        {
            return _type;
        }

        public void SetMessage(T subject, MessageTypeEnum type)
        {
            this._type = type;
            this._message = subject;
        }
    }
}