using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlot
{
    void AddItem(IItem item);
    void MoveItem(ISlot slot);
    void RemoveItem();
    IItem GetItem();
    bool IsEmpty();
    void Activate();
    void Deactivate();
    bool IsActive();
    void MarkAsSelected();
    bool IsSelected();
    void Deselect();
}