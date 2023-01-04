using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "UIColorSchema", menuName = "Inventory/UIColorSchema", order = 50)]
public class UIColorSchema : ScriptableObject
{
    [Header("Icon:")]
    [ColorUsage(true, true)][SerializeField]
    private Color _defaultIconColor = new Color(0, 0, 0, 0);
    [ColorUsage(true, true)][SerializeField]
    private Color _emptyIconColor = new Color(0, 0, 0, 0);
    [ColorUsage(true, true)][SerializeField]
    private Color _emptyAndSelectedIconColor = new Color(0, 0, 0, 0);
    [ColorUsage(true, true)][SerializeField]
    
    [Header("Slot:")]
    private Color _emptySlotTextColor = new Color(0, 0, 0, 0);
    [ColorUsage(true, true)][SerializeField]
    private Color _emptySlotColor = new Color(0, 0, 0, 0);
    [ColorUsage(true, true)][SerializeField]
    private Color _usedSlotTextColor = new Color(0, 0, 0, 0);
    [ColorUsage(true, true)][SerializeField]
    private Color _usedSlotColor = new Color(255, 255, 255, 1);
    [ColorUsage(true, true)][SerializeField]
    private Color _selectedSlotTextColor = new Color(0, 0, 0, 0);
    [ColorUsage(true, true)][SerializeField]
    private Color _selectedSlotColor = new Color(255, 0, 0, 1);
    [ColorUsage(true, true)][SerializeField]
    private Color _unselectedSlotTextColor = new Color(0, 0, 0, 0);
    [ColorUsage(true, true)][SerializeField]
    private Color _unselectedSlotColor = new Color(255, 255, 255, 255);
    [ColorUsage(true, true)][SerializeField]
    private Color _disabledSlotTextColor = new Color(0, 0, 0, 0);
    [ColorUsage(true, true)][SerializeField]
    private Color _disabledSlotColor = new Color(0, 0, 0, 255);
    [SerializeField]
    private float _slotColorMultiplier = 1;
    

    [Header("Submenu Color")]
    [ColorUsage(true)][SerializeField]
    private Color _unselectedSubmenuTextColor = new Color(0, 0, 0, 255);
    [ColorUsage(true)][SerializeField]
    private Color _unselectedSubmenuColor = new Color(0, 0, 0, 255);
    [ColorUsage(true)][SerializeField]
    private Color _disabledSubmenuTextColor = new Color(0, 0, 0, 255);
    [ColorUsage(true)][SerializeField]
    private Color _disabledSubmenuColor = new Color(0, 0, 0, 255);
    [ColorUsage(true)][SerializeField]
    private Color _selectedSubmenuTextColor = new Color(0, 0, 0, 255);
    [ColorUsage(true)][SerializeField]
    private Color _selectedSubmenuColor = new Color(0, 0, 0, 255);
    [SerializeField]
    private float _submenuColorMultiplier = 1;

    // SLOT
    public Color GetIconColor(){
        return _defaultIconColor;
    }
    public Color GetIconEmptyColor(){
        return _emptyIconColor;
    }
    public Color GetIconEmptyAndSelectedColor(){
        return _emptyAndSelectedIconColor;
    }
    public Color GetEmptySlotColor(){
        return _emptySlotColor;
    }
    public Color GetUsedSlotColor(){
        return _usedSlotColor;
    }
    public Color GetBGSelectedSlotColor(){
        return _selectedSlotColor;
    }
    public Color GetBGUnselectedSlotColor(){
        return _unselectedSlotColor;
    }
    public Color GetSlotDisabledColor(){
        return _disabledSlotColor;
    }
    public float GetSlotColorMultiplier(){
        return _slotColorMultiplier;
    }
    // SUBMENU
    public Color GetSubmenuUnselectedColor(){
        return _unselectedSubmenuColor;
    }
    public Color GetSubmenuUnselectedTextColor(){
        return _unselectedSubmenuTextColor;
    }
    public Color GetSubmenuSelectedColor(){
        return _selectedSubmenuColor;
    }
    public Color GetSubmenuSelectedTextColor(){
        return _selectedSubmenuTextColor;
    }
    public Color GetSubmenuDisabledTextColor(){
        return _disabledSubmenuTextColor;
    }
    public Color GetSubmenuDisabledColor(){
        return _disabledSubmenuColor;
    }
    public float GetSubmenuColorMultiplier(){
        return _submenuColorMultiplier;
    }
}