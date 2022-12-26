using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class SubmenuOption : MonoBehaviour, IColorized {
    private Submenu _submenu;
    [SerializeField]
    private UIAction.Action _type;
    [SerializeField]
    private Image _image;
    [SerializeField]
    private Button _button;
    [SerializeField]
    private bool _active;
    public void Start(){
        Setup();
    }
    
    void Update()
    {
        if (!Application.isPlaying)
        {
            Setup();
        }
    }
    public UIAction.Action GetOptionType(){
        return _type;
    }
    private void Setup(){
        if(_image == null){
            _image = GetComponent<Image>();
        }
        if(_button == null){
            _button = GetComponent<Button>();
        }
        if(_submenu == null){
            _submenu = GetComponentInParent<Submenu>();
            _submenu?.AddOption(this);
        }
    }
    public void UpdateColor(UIColorSchema colorSchema)
    {
        if(_button != null){
            ColorBlock colorBlock = new ColorBlock();
            colorBlock.normalColor = colorSchema.GetBGUnselectedSlotColor();
            colorBlock.selectedColor = colorSchema.GetBGSelectedSlotColor();
            colorBlock.highlightedColor = colorSchema.GetBGSelectedSlotColor();
            colorBlock.pressedColor = colorSchema.GetBGSelectedSlotColor();
            colorBlock.disabledColor = colorSchema.GetDisabledColor();
            colorBlock.colorMultiplier = colorSchema.GetColorMultiplier();
            _button.colors = colorBlock;
        }
    }
    public void Activate(){
        _button.interactable = true;
    }
    public void Deactivate(){
        _button.interactable = false;
    }
}