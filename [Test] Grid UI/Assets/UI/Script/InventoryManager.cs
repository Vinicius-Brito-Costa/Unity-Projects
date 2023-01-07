using UnityEngine;
using UnityEngine.Events;
using Inventory.State;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour, IInventoryManager
    {
        [SerializeField]
        private Inventory _inventory;
        [SerializeField]
        private ISubmenu _submenu;
        [SerializeField]
        private SO_BaseInventoryState _initialState;
        [SerializeField]
        private SO_BaseUIController _uiController;
        [SerializeField]
        private UIColorSchema _colorSchema;

        [SerializeField]
        private UnityEvent colorSchemaEventSystem;

        void Start()
        {

            _inventory.SetState((IInventoryState)_initialState);
            _inventory.SetSubmenu(_submenu);


            _initialState.SetSlotManager(_inventory);
            _initialState.SetUIController(_uiController);

            if (_submenu.isActiveAndEnabled)
            {
                _submenu.gameObject.SetActive(false);
            }
            _inventory.UpdateColor(_colorSchema);
        }
        void Update()
        {
            Action();
        }
        public void Action()
        {
            if (_inventory != null)
            {
                _inventory.Action();
            }
        }
        public void SetUIController(IUIController controller)
        {
            if (controller != null)
            {
                _uiController = (SO_BaseUIController)controller;
                _inventory.GetState().SetUIController(controller);
            }
        }
        public void SetInventoryState(IInventoryState state)
        {
            if (state != null)
            {
                _initialState = (SO_BaseInventoryState)state;
                if (_initialState.GetUIController() == null)
                {
                    _initialState.SetUIController(_uiController);
                }
                else
                {
                    _uiController = (SO_BaseUIController)_initialState.GetUIController();
                }

                _inventory.SetState(_initialState);
            }
        }
    }
}