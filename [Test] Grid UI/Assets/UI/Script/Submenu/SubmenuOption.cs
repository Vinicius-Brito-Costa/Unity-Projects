using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace Inventory {
[ExecuteInEditMode]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class SubmenuOption : MonoBehaviour, IColorized,
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler {
    private Submenu _submenu;
    [SerializeField]
    private UIAction.Action _type;
    [SerializeField]
    private Image _image;
    [SerializeField]
    private Button _button;
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private bool _active;
    private UIColorSchema _colorSchema;
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
        if(colorSchema){
            _colorSchema = colorSchema;
            if(_button){
                ColorBlock colorBlock = new ColorBlock();
                colorBlock.normalColor = colorSchema.GetSubmenuUnselectedColor();
                colorBlock.selectedColor = colorSchema.GetSubmenuSelectedColor();
                colorBlock.highlightedColor = colorSchema.GetSubmenuSelectedColor();
                colorBlock.pressedColor = colorSchema.GetSubmenuSelectedColor();
                colorBlock.disabledColor = colorSchema.GetSubmenuDisabledColor();
                colorBlock.colorMultiplier = colorSchema.GetSubmenuColorMultiplier();
                _button.colors = colorBlock;
            }
        }
    }
    public void Activate(){
        _button.interactable = true;
        _text.color = _colorSchema.GetSubmenuUnselectedTextColor();
        gameObject.SetActive(true);
    }
    public void Deactivate(){
        _button.interactable = false;
        _text.color = _colorSchema.GetSubmenuDisabledTextColor();
        gameObject.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_button.interactable){
            _text.color = _colorSchema.GetSubmenuSelectedTextColor();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(_button.interactable){
            _text.color = _colorSchema.GetSubmenuUnselectedTextColor();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(_button.interactable){
            _text.color = _colorSchema.GetSubmenuSelectedTextColor();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(_button.interactable){
            _text.color = _colorSchema.GetSubmenuSelectedTextColor();
        }
    }
}
}