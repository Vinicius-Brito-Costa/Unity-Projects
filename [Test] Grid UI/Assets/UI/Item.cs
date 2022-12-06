using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IItem
{
    [SerializeField]
    private SO_ItemData _itemData;
    [SerializeField]
    private int _count = 1;
    [SerializeField]
    private int _maxCount = 1;
    public string GetName(){
        return _itemData.Name;
    }
    public void AddItem(int count){
        if(_count > 0){
            if(_itemData.IsStackable){
                _count += Mathf.Abs(count);
            }
        }
        else {
            _count++;
        }
    }
    public void UseItem(){
        if(_count > 0){
            // do stuff
            Debug.Log("Using Item" + _itemData.Name + "...");
            _count--;
        }
    }
    
    public void DropItem(int count){
        if(count > _count){
            _count = 0;
        }
        else {
            _count -= count;
        }
    }
    public int GetMaxCount(){
        return _maxCount;
    }
    public int GetItemCount(){
        return _count;
    }

    public Texture2D GetIcon(){
        return _itemData.Icon;
    }
}
