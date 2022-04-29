using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

namespace MioTool
{
    [CustomEditor(typeof(MioGridLayoutGroup))]
    public class MioGridLayoutGroupEditor : GridLayoutGroupEditor
    {
        private SerializedProperty _useItemSize;
        private SerializedProperty _childMidd;

        protected override void OnEnable()
        {
            base.OnEnable();
            _useItemSize = serializedObject.FindProperty("_useChildSize");
            _childMidd = serializedObject.FindProperty("_childMidd");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("除了扩展功能,和普通网格布局器毫无区别", MessageType.Info, true);
            base.OnInspectorGUI();
            serializedObject.Update();
            EditorGUILayout.PropertyField(_useItemSize, EditorGUIUtility.TrTextContent("使用Item尺寸", "将第0个item的size用做cellSize"));
            EditorGUILayout.PropertyField(_childMidd, EditorGUIUtility.TrTextContent("Item行居中", "勾选后行居中,反之使用unity默认方式"));
            serializedObject.ApplyModifiedProperties();
        }
    }
}