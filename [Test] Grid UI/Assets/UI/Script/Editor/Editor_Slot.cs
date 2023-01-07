using UnityEditor;
using Inventory.Slots;
namespace Inventory.EditorConfig
{
    [CustomEditor(typeof(Slot))]
    public class Editor_Slot : Editor
    {
        public override void OnInspectorGUI()
        {
            Slot slot = (Slot)target;

            DrawDefaultInspector();
        }
    }
}