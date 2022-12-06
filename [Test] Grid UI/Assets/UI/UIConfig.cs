using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "UIConfig", menuName = "Inventory/UIConfig", order = 50)]
public class UIConfig : ScriptableObject
{
    public static string SLOT_BACKGROUND_GAME_OBJECT_NAME { get; private set; } = "Background";
    public static string SLOT_ICON_GAME_OBJECT_NAME { get; private set; } = "Icon";
    [ColorUsage(true, true)][SerializeField]
    private Color SLOT_EMPTY_COLOR = new Color(0, 0, 0, .796f);
    [ColorUsage(true, true)][SerializeField]
    private Color SLOT_USED_COLOR = new Color(255, 255, 255, 1);
    [ColorUsage(true, true)][SerializeField]
    private Color SLOT_BG_SELECTED_COLOR = new Color(255, 0, 0, 1);
    [ColorUsage(true, true)][SerializeField]
    private Color SLOT_BG_UNSELECTED_COLOR = new Color(255, 255, 255, 255);

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
}