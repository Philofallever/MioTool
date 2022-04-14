using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

#if ODIN_INSPECTOR_3

namespace MioTool.Editor
{
    public sealed class MioToolWidgetWindow : OdinEditorWindow
    {
        [MenuItem("MioTool/Widget Window")]
        static void OpenWindow()
        {
            GetWindow<MioToolWidgetWindow>(true, "MioTool Widgt").Show();
        }

        [AssetList(AutoPopulate = false),InlineEditor(InlineEditorModes.FullEditor)]
        [PreviewField(50, ObjectFieldAlignment.Center)]
        public Texture2D a;

        [AssetList(AutoPopulate = false), InlineEditor(InlineEditorModes.LargePreview)]
        [PreviewField(50, ObjectFieldAlignment.Center)]
        public GameObject b;

        [ColorPalette]
        public Color c;

        [EnumPaging,OnValueChanged(nameof(SetCurrentTool))]
        public UnityEditor.Tool sceneTool;

        void SetCurrentTool()
        {
            UnityEditor.Tools.current = sceneTool;
        }
    }
}

#endif