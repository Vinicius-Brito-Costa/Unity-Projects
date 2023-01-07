using System;
using UnityEngine;

namespace Inventory
{
    [Serializable]
    public abstract class SO_BaseUIController : ScriptableObject, IUIController
    {
        public abstract UIControlEnum GetButtonPressed();
    }
}