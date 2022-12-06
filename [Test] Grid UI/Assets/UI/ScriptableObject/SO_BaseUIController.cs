using System;
using UnityEngine;

[Serializable]
public abstract class SO_BaseUIController : ScriptableObject, IUIController {
    public abstract UIControlEnum GetButtonPressed();
}