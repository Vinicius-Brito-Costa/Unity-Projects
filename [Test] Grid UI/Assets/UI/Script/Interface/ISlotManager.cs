using System.Collections.Generic;
using UnityEngine;
public interface ISlotManager{
    ISlot AddSlot();
    ISlot GetSelectedSlot();
    void SetSelectedSlot(ISlot slot);
    GameObject GetGameObject();
    ISlot GetPrefab();
    List<ISlot> GetAllSlots();
    InventoryMap GetInventoryMap();
    void SetSubmenu(ISubmenu submenu);
    ISubmenu GetSubmenu();
}