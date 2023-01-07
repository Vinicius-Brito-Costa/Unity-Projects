using UnityEngine;

namespace Inventory
{
    public interface ISubscriber
    {
        void Listen(GameObject message);
    }
}