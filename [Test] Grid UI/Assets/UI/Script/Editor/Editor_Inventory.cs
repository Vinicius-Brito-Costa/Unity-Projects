using UnityEditor;

namespace Inventory.EditorConfig
{
    [CustomEditor(typeof(Inventory))]
    public class Editor_Inventory : Editor
    {
        public override void OnInspectorGUI()
        {
            Inventory inventory = (Inventory)target;

            DrawDefaultInspector();
        }
    }
}