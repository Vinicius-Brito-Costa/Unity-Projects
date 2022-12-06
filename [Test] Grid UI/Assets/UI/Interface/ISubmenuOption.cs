using TMPro;
public interface ISubmenuOption{
    public abstract UIAction GetOptionKey();
    public abstract void Action();
    public TextMeshProUGUI GetTextMesh();
}