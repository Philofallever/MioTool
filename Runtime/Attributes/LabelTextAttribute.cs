using UnityEngine;

namespace MioTool.Attributes
{
    public sealed class LabelTextAttribute : PropertyAttribute
    {
        public string Text;
        public bool Nicety;

        public LabelTextAttribute(string text, bool nicety = true)
        {
            Text = text;
            Nicety = nicety;
        }
    }
}