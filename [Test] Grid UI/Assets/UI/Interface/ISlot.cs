using UnityEngine;

public abstract class ISlot : MonoBehaviour
{
    public abstract void AddItem(IItem item);
    public abstract void MoveItem(ISlot slot);
    public abstract void RemoveItem();
    public abstract IItem GetItem();
    public abstract bool IsEmpty();
    public abstract void Activate();
    public abstract void Deactivate();
    public abstract bool IsActive();
    public abstract void MarkAsSelected();
    public abstract bool IsSelected();
    public abstract void Deselect();
    public abstract bool OpenSubmenu();
    public abstract bool IsSubmenuOpen();
    public abstract void CloseSubmenu();
}