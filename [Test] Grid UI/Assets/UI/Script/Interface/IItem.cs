using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    /// <summary>Return the item name.</summary>
    string GetName();
    
    /// <summary>Add to item count using the provided value. If the provided value is negative, the function will turn it positive.</summary>
    void AddItem(int count);
    
    /// <summary>Uses the item using the provided user as context.</summary>
    void UseItem();
    
    /// <summary>Deletes the item by subtracting the provided value. If the item count goes to zero, the item will be deleted.</summary>
    void DropItem(int count);
    
    /// <summary>Returns the max item count.</summary>
    int GetMaxCount();
    
    /// <summary>Returns the current item count.</summary>
    int GetItemCount();
    
    /// <summary>Return the item icon.</summary>
    Texture2D GetIcon();
    /// <summary>Returns the item submenu</summary>
    List<UIAction.Action> GetSubmenuActiveOptions();
}
