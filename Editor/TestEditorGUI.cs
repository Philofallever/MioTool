using UnityEngine;
using UnityEditor;


namespace MioTool.Editor
{
    public class TestEditorGUIWindow : EditorWindow
    {

        [MenuItem("TestWindow/TestEditorGUI", false, 20)]
        static void OpenWin()
        {
            var win = GetWindow<TestEditorGUIWindow>();
            win.Show();
        }

        void OnGUI()
        {
            EditorGUILayout.Vector2Field("Test V2",Vector2.zero);
            EditorGUILayout.Vector2Field("Test V2",Vector2.zero);
            EditorGUILayout.Vector2Field("Test V2",Vector2.zero);
            EditorGUILayout.Vector2Field("Test V2",Vector2.zero);
            EditorGUILayout.Vector2Field("Test V2",Vector2.zero);
        }
    }
}
