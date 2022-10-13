using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    string GetName();
    void AddItem(int count);
    void UseItem(GameObject user);
    void DropItem(int count);
    int GetMaxCount();
    int GetItemCount();
    Texture2D GetIcon();
}
