public interface IInventoryManager{

    void SetUIController(IUIController controller);
    void SetInventoryState(IInventoryState state);
    void Action();

}