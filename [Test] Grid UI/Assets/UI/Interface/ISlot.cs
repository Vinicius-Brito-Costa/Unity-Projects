using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class ISlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    protected UnityEvent<ISlot> _mouseOverEvent;
    [SerializeField]
    protected UnityEvent<UIControlEnum> _clickEvent;
    public abstract void AddItem(IItem item);
    public abstract void MoveItem(ISlot slot);
    public abstract void RemoveItem();
    public abstract IItem GetItem();
    public abstract bool IsEmpty();
    public abstract void Activate();
    public abstract void Deactivate();
    public abstract bool IsActive();
    public abstract void MarkAsSelected();
    public abstract bool IsSelected();
    public abstract void Deselect();
    public abstract Submenu GetSubmenu();
    public abstract bool OpenSubmenu();
    public abstract bool IsSubmenuOpen();
    public abstract void CloseSubmenu();
    private void OnEnable() {
        GameObject parent = transform.parent.gameObject;
        if(parent != null){
            IInventory parentInventory = parent.GetComponent<IInventory>();
            if(parentInventory != null){
                _mouseOverEvent.AddListener(parentInventory.SetSelectedSlot);
                _clickEvent.AddListener(parentInventory.Action);
            }
        }
    }
    private void OnDisable() {
        _mouseOverEvent.RemoveAllListeners();
        _clickEvent.RemoveAllListeners();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _mouseOverEvent.Invoke(this);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //Deselect();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
        _clickEvent.Invoke(UIControlEnum.MOUSE_LEFT_CLICK);
    }
}