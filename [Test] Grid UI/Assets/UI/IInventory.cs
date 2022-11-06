using System.Collections.Generic;
using UnityEngine;
public interface IInventory {
    void AddSlot();
    void AddItem(IItem item);
    void RemoveItem();
    void SelectSlot(UIMovementEnum move);
    void MoveItem(ISlot target);
    IItem GetItem();
    void OpenSlotMenu();
}