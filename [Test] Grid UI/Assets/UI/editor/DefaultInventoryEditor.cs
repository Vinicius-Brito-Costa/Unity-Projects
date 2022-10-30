using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(Inventory))]
public class DefaultInventoryEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Inventory inventory = (Inventory)target;

        DrawDefaultInspector();
    }
}