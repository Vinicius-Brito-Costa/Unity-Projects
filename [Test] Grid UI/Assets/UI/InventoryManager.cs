using UnityEngine;

public class InventoryManager : MonoBehaviour, IInventoryManager
{
    [SerializeField]
    private Inventory _inventory;
    [SerializeField]
    private SO_BaseInventoryState _initialState;
    [SerializeField]
    private SO_BaseUIController _uiController;

    void Start(){
        _initialState.SetSlotManager(_inventory);
        _initialState.SetUIController(_uiController);
        _inventory.SetState((IInventoryState) _initialState);
    }
    void Update()
    {
        Action();
    }
    public void Action(){
        if(_inventory != null){
            _inventory.Action();
        }
    }
    public void SetUIController(IUIController controller){
        if(controller != null){
            _uiController = (SO_BaseUIController) controller;
            _inventory.GetState().SetUIController(controller);
        }
    }
    public void SetInventoryState(IInventoryState state){
        if(state != null){
            _initialState = (SO_BaseInventoryState) state;
            if(_initialState.GetUIController() == null){
                _initialState.SetUIController(_uiController);
            }
            else {
                _uiController = (SO_BaseUIController) _initialState.GetUIController();
            }
            
            _inventory.SetState(_initialState);
        }
    }
}
