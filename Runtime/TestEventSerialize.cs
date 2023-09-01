using Sirenix.OdinInspector.Editor;
using System;
using UnityEditor;
using UnityEngine;

namespace MioTool
{
    public class TestEventSerialize : MonoBehaviour
    {

        [VEvent]
        public string TestVent;
    }


    [AttributeUsage(AttributeTargets.Event | AttributeTargets.Field)]
    public class VEventAttribute : Attribute
    {
    }

    public class VEventAttributeDrawer : OdinAttributeDrawer<VEventAttribute>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            var rect = EditorGUILayout.GetControlRect();
            //SirenixEditorGUI.dr
        }
    }
}
