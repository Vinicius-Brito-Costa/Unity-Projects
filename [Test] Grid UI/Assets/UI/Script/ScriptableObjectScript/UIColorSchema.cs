using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "UIColorSchema", menuName = "Inventory/UIColorSchema", order = 50)]
public class UIColorSchema : ScriptableObject
{
    [ColorUsage(true, true)][SerializeField]
    private Color SLOT_EMPTY_COLOR = new Color(0, 0, 0, .796f);
    [ColorUsage(true, true)][SerializeField]
    private Color SLOT_USED_COLOR = new Color(255, 255, 255, 1);
    [ColorUsage(true, true)][SerializeField]
    private Color SLOT_BG_SELECTED_COLOR = new Color(255, 0, 0, 1);
    [ColorUsage(true, true)][SerializeField]
    private Color SLOT_BG_UNSELECTED_COLOR = new Color(255, 255, 255, 255);
    [ColorUsage(true, true)][SerializeField]
    private Color DISABLED_COLOR = new Color(0, 0, 0, 255);
    [SerializeField]
    private float COLOR_MULTIPLIER = 1;

    public Color GetEmptySlotColor(){
        return SLOT_EMPTY_COLOR;
    }
    public Color GetUsedSlotColor(){
        return SLOT_USED_COLOR;
    }
    public Color GetBGSelectedSlotColor(){
        return SLOT_BG_SELECTED_COLOR;
    }
    public Color GetBGUnselectedSlotColor(){
        return SLOT_BG_UNSELECTED_COLOR;
    }
    public Color GetDisabledColor(){
        return DISABLED_COLOR;
    }
    public float GetColorMultiplier(){
        return COLOR_MULTIPLIER;
    }
}