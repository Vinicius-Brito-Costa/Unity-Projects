using UnityEngine;
[CreateAssetMenu(fileName = "SubmenuOption", menuName = "Inventory/Submenu/SubmenuOption", order = 50)]
public class SO_SubmenuOption : ScriptableObject, ISubmenuOption{
    [SerializeField]
    protected UIAction _action;

    public UIAction GetOptionKey(){
        return _action;
    }
}