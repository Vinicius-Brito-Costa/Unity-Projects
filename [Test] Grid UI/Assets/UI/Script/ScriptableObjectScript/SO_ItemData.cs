using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/Item/ItemData", order = 50)]
    public class SO_ItemData : ScriptableObject
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private string _description;
        [SerializeField]
        private bool _isStackable;
        [SerializeField]
        private bool _isKey;
        [SerializeField]
        private bool _usedKeyItem;
        [SerializeField]
        private Texture2D _icon;
        [SerializeField]
        private List<UIAction.Action> _availableActions;

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
        }

        public bool IsStackable
        {
            get
            {
                return _isStackable;
            }
        }

        public bool IsKey
        {
            get
            {
                return _isKey;
            }
        }

        public Texture2D Icon
        {
            get
            {
                return _icon;
            }
        }
        public List<UIAction.Action> GetActions()
        {
            return _availableActions;
        }
    }
}