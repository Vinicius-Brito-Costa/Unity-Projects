using UnityEngine;
using TMPro;
public abstract class SO_SubmenuOption : ScriptableObject, ISubmenuOption{
    [SerializeField]
    protected UIAction _action;
    [SerializeField]
    protected TextMeshProUGUI _textMeshPro;

    public UIAction GetOptionKey(){
        return _action;
    }
    public abstract void Action();
    public TextMeshProUGUI GetTextMesh(){
        return _textMeshPro;
    }
}