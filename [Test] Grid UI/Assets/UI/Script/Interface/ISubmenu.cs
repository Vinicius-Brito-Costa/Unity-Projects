using System.Collections.Generic;
using UnityEngine;

public abstract class ISubmenu : MonoBehaviour, IColorized{
    public abstract void Action(ISlot slot);
    public abstract void Move(UIControlEnum move);
    public abstract void Close();
    public abstract void SetActiveActions(List<UIAction.Action> activeOptions);
    public abstract void UpdateColor(UIColorSchema colorSchema);
}