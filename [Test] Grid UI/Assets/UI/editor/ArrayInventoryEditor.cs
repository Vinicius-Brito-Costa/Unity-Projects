using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(ArrayInventory))]
public class ArrayInventoryEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ArrayInventory inventory = (ArrayInventory)target;

        DrawDefaultInspector();
    }
}