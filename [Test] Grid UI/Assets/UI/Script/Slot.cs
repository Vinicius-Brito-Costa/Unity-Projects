using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inventory.Items;

namespace Inventory.Slots
{
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
            SetIcon(_item.GetIcon());
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
                RemoveIcon();
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

        private void SetIcon(Texture2D icon)
        {
            if (_icon != null)
            {
                _icon.sprite = Sprite.Create(icon, new Rect(0.0f, 0.0f, icon.width, icon.height), new Vector2(0.5f, 0.5f), 100.0f);
                UpdateColor(_colorSchema);
            }
        }
        private void RemoveIcon()
        {
            if (_icon != null)
            {
                _icon.sprite = null;
                UpdateColor(_colorSchema);
            }
        }
        public override void MarkAsSelected()
        {
            _isSelected = true;
            UpdateColor(_colorSchema);
        }
        public override bool IsSelected()
        {
            return _isSelected;
        }
        public override void Deselect()
        {
            _isSelected = false;
            UpdateColor(_colorSchema);
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
                _activeSubmenu = (Submenu)submenu;
                _activeSubmenu.SetActiveActions(activeOptions);
                _activeSubmenu.UpdateColor(_colorSchema);
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
            if (colorSchema == null) return;

            _colorSchema = colorSchema;

            if (_background && _icon)
            {

                if (_item == null)
                {
                    _icon.sprite = null;
                    _name = "";
                }
                else if (_icon.sprite == null)
                {
                    SetIcon(_item.GetIcon());
                }

                if (IsActive())
                {
                    // Default state is: Slot is active, unselected, empty with no item. 
                    _background.color = _colorSchema.GetBGUnselectedSlotColor();
                    _icon.color = _colorSchema.GetIconEmptyColor();

                    if (IsSelected())
                    {
                        _background.color = _colorSchema.GetBGSelectedSlotColor();

                        if (IsEmpty())
                        {
                            _icon.color = _colorSchema.GetIconEmptyAndSelectedColor();
                        }
                    }

                    if (!IsEmpty())
                    {
                        _icon.color = _colorSchema.GetIconColor();
                    }
                }
                else
                {
                    _background.color = _colorSchema.GetSlotDisabledColor();
                    _icon.color = _colorSchema.GetIconEmptyColor();
                    if (!IsEmpty())
                    {
                        _icon.color = _colorSchema.GetIconColor();
                    }
                }
            }
            _activeSubmenu?.UpdateColor(colorSchema);
        }
    }
}