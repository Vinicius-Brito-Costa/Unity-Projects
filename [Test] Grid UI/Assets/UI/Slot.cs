using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class Slot : ISlot
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private UIConfig _config;
    private IItem _item;
    [SerializeField]
    private bool _isActive;
    [SerializeField]
    private bool _isSelected;
    [SerializeField]
    private SO_Submenu _subMenu;
    [SerializeField]
    private bool _submenuIsOpen;
    private Image _background;
    private Image _icon;
    private Submenu _submenuObject;

    private void Start() {
        GameObject backgroundChild = GameObjectUtil.Instance().getChildGameObject(gameObject, UIConfig.SLOT_BACKGROUND_GAME_OBJECT_NAME);
        if(backgroundChild != null){
            _background = backgroundChild.GetComponent<Image>();
        }

        GameObject iconChild = GameObjectUtil.Instance().getChildGameObject(gameObject, UIConfig.SLOT_ICON_GAME_OBJECT_NAME);
        if(iconChild != null){
            _icon = iconChild.GetComponent<Image>();
        }
        
        _submenuObject = _subMenu?.CreateSubmenu(this.gameObject);
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
            _icon.color = _config.GetUsedSlotColor();
            _icon.sprite = Sprite.Create(icon, new Rect(0.0f, 0.0f, icon.width, icon.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
    }
    private void removeIcon(){
        if(_icon != null){
            _icon.sprite = null;
            _icon.color = _config.GetEmptySlotColor();
        }
    }
    public override void MarkAsSelected(){
        _isSelected = true;
        if(_background){
            _background.color = _config.GetBGSelectedSlotColor();
        }
    }
    public override bool IsSelected(){
        return _isSelected;
    }
    public override void Deselect(){
        _isSelected = false;
        if(_background){
            _background.color = _config.GetBGUnselectedSlotColor();
        }
    }
    public override bool OpenSubmenu()
    {
        bool opened = false;
        if(_item == null){
            if(_submenuIsOpen){
                opened = true;
            }
            else{
                Debug.Log("Abrir Submenu");
                _submenuObject.gameObject.SetActive(true);
                _submenuIsOpen = true;
                opened = true;
            }
        }
        
        return opened;
    }
    public override bool IsSubmenuOpen()
    {
        return _submenuIsOpen;
    }
    public override Submenu GetSubmenu()
    {
        return _submenuObject;
    }
    public override void CloseSubmenu()
    {
        _submenuObject.gameObject.SetActive(false);
        _submenuIsOpen = false;
    }

}
