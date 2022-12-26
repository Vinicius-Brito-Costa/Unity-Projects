using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : ISlot
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private UIColorSchema _colorSchema;
    [SerializeField]
    private Item _item;
    [SerializeField]
    private bool _isActive;
    [SerializeField]
    private bool _isSelected;
    [SerializeField]
    private bool _submenuIsOpen;
    [SerializeField]
    private Submenu _activeSubmenu;
    [SerializeField]
    private Image _background;
    [SerializeField]
    private Image _icon;

    private void Start()
    {
        if (_item != null)
        {
            IItem item = (IItem)_item;
            if (item.GetItemCount() < 1 && item.GetItemCount() < item.GetMaxCount())
            {
                item.AddItem(1);
            }
            _item = null;
            AddItem(item);
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
            _item = (Item)item;
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
        if (_item != null)
        {
            _name = "";
            _item = null;
            _activeSubmenu = null;
            removeIcon();
        }
    }
    public override IItem GetItem()
    {
        return _item;
    }
    public override bool IsEmpty()
    {
        return _item == null;
    }
    public override void Activate()
    {
        _isActive = true;
    }
    public override void Deactivate()
    {
        _isActive = false;
    }
    public override bool IsActive()
    {
        return _isActive;
    }

    private void setIcon(Texture2D icon)
    {
        if (_icon != null)
        {
            _icon.color = _colorSchema.GetUsedSlotColor();
            _icon.sprite = Sprite.Create(icon, new Rect(0.0f, 0.0f, icon.width, icon.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
    }
    private void removeIcon()
    {
        if (_icon != null)
        {
            _icon.sprite = null;
            _icon.color = _colorSchema.GetEmptySlotColor();
        }
    }
    public override void MarkAsSelected()
    {
        _isSelected = true;
        if (_background && _colorSchema)
        {
            _background.color = _colorSchema.GetBGSelectedSlotColor();
        }
    }
    public override bool IsSelected()
    {
        return _isSelected;
    }
    public override void Deselect()
    {
        _isSelected = false;
        if (_background)
        {
            _background.color = _colorSchema.GetBGUnselectedSlotColor();
        }
    }
    public override bool OpenSubmenu(ISubmenu submenu)
    {
        if (_item == null || submenu == null)
        {
            Debug.Log("Não foi possivel abrir o Submenu. Item ou Submenu são nulos.");
            _submenuIsOpen = false;
            _activeSubmenu?.Close();
            _activeSubmenu = null;
        }
        else
        {
            Debug.Log("Abrindo Submenu...");
            _submenuIsOpen = true;
            List<UIAction.Action> activeOptions = _item.GetSubmenuActiveOptions();
            _activeSubmenu = (Submenu) submenu;
            _activeSubmenu.SetActiveActions(activeOptions);
            _activeSubmenu.gameObject.SetActive(true);
        }

        return _activeSubmenu == null ? false : true;
    }
    public override bool IsSubmenuOpen()
    {
        return _submenuIsOpen;
    }
    public override void CloseSubmenu()
    {
        if (_activeSubmenu != null)
        {
            _activeSubmenu.Close();
            _submenuIsOpen = false;
        }
    }

    public override void UpdateColor(UIColorSchema colorSchema)
    {
        _colorSchema = colorSchema;
    }
}
