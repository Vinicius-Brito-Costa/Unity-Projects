using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour, ISlot
{
    [SerializeField]
    private string _name;
    private IItem _item;
    [SerializeField]
    private bool _isActive;
    [SerializeField]
    private bool _isSelected;
    private Image _background;
    private Image _icon;

    private void Start() {
        GameObject backgroundChild = GameObjectUtil.Instance().getChildGameObject(gameObject, UIConstants.SLOT_BACKGROUND_GAME_OBJECT_NAME);
        if(backgroundChild != null){
            _background = backgroundChild.GetComponent<Image>();
        }

        GameObject iconChild = GameObjectUtil.Instance().getChildGameObject(gameObject, UIConstants.SLOT_ICON_GAME_OBJECT_NAME);
        if(iconChild != null){
            _icon = iconChild.GetComponent<Image>();
        }
    }

    public void AddItem(IItem item)
    {
        if (_item != null)
        {
            _item.AddItem(item.GetItemCount());
        }
        else
        {
            _item = item;
        }
        _name = _item.GetName();
        setIcon(_item.GetIcon());
    }
    public void MoveItem(ISlot slot)
    {
        slot.AddItem(_item);
        RemoveItem();
    }
    public void RemoveItem()
    {
        _item.DropItem(_item.GetItemCount());
        if(_item.GetItemCount() <= 0){
            _name = "";
            _item = null;
        }
        removeIcon();
        Debug.Log("Item removed? " + (_item == null));
    }
    public IItem GetItem()
    {
        return _item;
    }
    public bool IsEmpty(){
        return _item == null;
    }
    public void Activate(){
        _isActive = true;
    }
    public void Deactivate(){
        _isActive = false;
    }
    public bool IsActive(){
        return _isActive;
    }

    private void setIcon(Texture2D icon){
        if(_icon != null){
            _icon.color = UIConstants.SLOT_USED_COLOR;
            _icon.sprite = Sprite.Create(icon,new Rect(0.0f, 0.0f, icon.width, icon.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
    }
    private void removeIcon(){
        if(_icon != null){
            _icon.sprite = null;
            _icon.color = UIConstants.SLOT_EMPTY_COLOR;
        }
    }
    public void MarkAsSelected(){
        _isSelected = true;
        _background.color = UIConstants.SLOT_BG_SELECTED_COLOR;
    }
    public bool IsSelected(){
        return _isSelected;
    }
    public void Deselect(){
        _isSelected = false;
        _background.color = UIConstants.SLOT_BG_UNSELECTED_COLOR;
    }
}
