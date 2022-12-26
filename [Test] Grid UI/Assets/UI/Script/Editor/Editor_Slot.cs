using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(Slot))]
public class Editor_Slot : Editor
{
    public override void OnInspectorGUI()
    {
        Slot slot = (Slot)target;

        DrawDefaultInspector();
    }
}