using MioTool.Attributes;
using UnityEditor;
using UnityEngine;

namespace MioTool.Editor.AttributeDrawer
{
    [CustomPropertyDrawer(typeof(LabelTextAttribute))]
    public class LabelTextAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var labelText = attribute as LabelTextAttribute;
            label.text = labelText.Text;
            EditorGUI.PropertyField(position, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }
    }
}