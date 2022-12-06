using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultSubmenuOption", menuName = "Inventory/Submenu/DefaultSubmenuOption", order = 50)]
public class DefaultSubmenuOption : SO_SubmenuOption
{
    
    public override void Action()
    {
        switch(_action){
            case UIAction.Use:
                Debug.Log("Using item...");
                break;
            default:
                Debug.Log("Doing nothing...");
                break;
        }
    }
}