using System.Collections.Generic;
using UnityEngine;
using Inventory.Slots;

namespace Inventory
{
    public abstract class ISubmenu : MonoBehaviour, IPublisher, IColorized
    {
        public abstract void Action(ISlot slot);
        public abstract void Move(UIControlEnum move);
        public abstract void Close();
        public abstract void SetActiveActions(List<UIAction.Action> activeOptions);
        public abstract void UpdateColor(UIColorSchema colorSchema);
        public abstract void Subscribe(ISubscriber subscriber);
        public abstract void Publish();
        public abstract void Unsubscribe(ISubscriber subscriber);
    }
}