using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

namespace Assets.MioTool.Editor
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
            _childMidd   = serializedObject.FindProperty("_childMidd");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            EditorGUILayout.PropertyField(_useItemSize, EditorGUIUtility.TrTextContent("使用Item尺寸"));
            EditorGUILayout.PropertyField(_childMidd,   EditorGUIUtility.TrTextContent("Item居中"));
            serializedObject.ApplyModifiedProperties();
        }
    }
}