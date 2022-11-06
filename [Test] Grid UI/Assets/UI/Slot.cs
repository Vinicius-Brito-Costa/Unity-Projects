using UnityEngine;
using UnityEngine.UI;

public class Slot : ISlot
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

    public override void AddItem(IItem item)
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
    public override void MoveItem(ISlot slot)
    {
        slot.AddItem(_item);
        RemoveItem();
    }
    public override void RemoveItem()
    {
        if(_item != null){
            _name = "";
            _item = null;
            removeIcon();           
        }
    }
    public override IItem GetItem()
    {
        return _item;
    }
    public override bool IsEmpty(){
        return _item == null;
    }
    public override void Activate(){
        _isActive = true;
    }
    public override void Deactivate(){
        _isActive = false;
    }
    public override bool IsActive(){
        return _isActive;
    }

    private void setIcon(Texture2D icon){
        if(_icon != null){
            _icon.color = UIConstants.SLOT_USED_COLOR;
            _icon.sprite = Sprite.Create(icon, new Rect(0.0f, 0.0f, icon.width, icon.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
    }
    private void removeIcon(){
        if(_icon != null){
            _icon.sprite = null;
            _icon.color = UIConstants.SLOT_EMPTY_COLOR;
        }
    }
    public override void MarkAsSelected(){
        _isSelected = true;
        if(_background){
            _background.color = UIConstants.SLOT_BG_SELECTED_COLOR;
        }
    }
    public override bool IsSelected(){
        return _isSelected;
    }
    public override void Deselect(){
        _isSelected = false;
        if(_background){
            _background.color = UIConstants.SLOT_BG_UNSELECTED_COLOR;
        }
    }
}
