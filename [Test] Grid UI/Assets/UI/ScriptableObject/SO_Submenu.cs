using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "DefaultSubmenu", menuName = "Inventory/Submenu/DefaultSubmenu", order = 50)]
public class SO_Submenu : ScriptableObject
{
    [SerializeField]
    protected List<SO_SubmenuOption> _options;
    [SerializeField]
    private Sprite _image;
    [SerializeField]
    private float _submenuWitdh;
    [SerializeField]
    private float _optionHeight;
    [SerializeField]
    private int _textSize;
    [SerializeField]
    private int _spaceBetweenOptions;

    public Submenu CreateSubmenu(GameObject gameObject)
    {
        GameObject submenu = new GameObject();
        VerticalLayoutGroup vlg = submenu.AddComponent<VerticalLayoutGroup>();
        vlg.childControlWidth = true;
        vlg.childControlHeight = false;
        vlg.childForceExpandWidth = true;
        vlg.childForceExpandHeight = false;
        submenu.name = "Submenu";
        submenu.transform.SetParent(gameObject.transform);
        RectTransform rect = submenu.GetComponent<RectTransform>();
        if(rect != null){
            rect.localPosition = Vector3.zero;
        }
        int iteration = 0;
        _options.ForEach(option =>
        {
            Vector3 pos = gameObject.transform.position;
            CreateButton(option.GetOptionKey().ToString(), submenu.transform, new Vector3(pos.x, pos.y - ((iteration * _textSize) + _spaceBetweenOptions), 0), new Vector2(_submenuWitdh, _textSize + 2), null);
            iteration++;
        });
        submenu.SetActive(false);
        return submenu.AddComponent<Submenu>();;
    }
    public void CreateButton(string text, Transform parent, Vector3 position, Vector2 size, UnityEngine.Events.UnityAction method)
    {
        GameObject button = new GameObject();
        button.name = text;
        button.transform.SetParent(parent);
        button.transform.position = Vector3.zero;
        RectTransform rectTransform = button.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(size.x, size.y);
        Button btnComp = button.AddComponent<Button>();
        Image image = button.AddComponent<Image>();
        image.sprite = _image;
        btnComp.transition = Selectable.Transition.ColorTint;
        btnComp.targetGraphic = image;
        //btnComp.onClick.AddListener(method);

        GameObject opt = new GameObject();
        TextMeshProUGUI textMesh = opt.AddComponent<TextMeshProUGUI>();
        textMesh.text = "Text";
        textMesh.fontSize = _textSize;
        textMesh.alignment = TextAlignmentOptions.Midline;
        textMesh.color = new Color(0, 0, 0, 255);
        textMesh.transform.SetParent(button.transform);
        textMesh.gameObject.name = textMesh.text;
    }
}