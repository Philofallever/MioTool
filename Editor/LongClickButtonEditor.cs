#if MIO_EXTERNAL
using UnityEditor;
using UnityEditor.UI;

namespace Assets.MioTool
{
    [CustomEditor(typeof(LongClickButton))]
    public class LongClickButtonEditor : ButtonEditor
    {
        private SerializedProperty _onLongClick;
        private SerializedProperty _everyFrame;

        protected override void OnEnable()
        {
            base.OnEnable();
            _onLongClick = serializedObject.FindProperty("_mOnLongClick");
            _everyFrame  = serializedObject.FindProperty("m_everyFrame");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            EditorGUILayout.PropertyField(_onLongClick, EditorGUIUtility.TrTextContent("On Long Click"));
            EditorGUILayout.PropertyField(_everyFrame,  EditorGUIUtility.TrTextContent("长按后每帧触发事件"));
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif