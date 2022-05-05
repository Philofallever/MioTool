using UnityEngine;
using UnityEngine.UI;

namespace MioTool
{
    [RequireComponent(typeof(Text))]
    public class BreakingSpace : MonoBehaviour
    {
        public static readonly string no_breaking_space = "\u00A0";

        protected Text mytext;

        private void Awake()
        {
            mytext = GetComponent<Text>();
            mytext.RegisterDirtyVerticesCallback(SetMyText);
        }

        public void SetMyText()
        {
            if (mytext.text.Contains(" "))
            {
                mytext.text = mytext.text.Replace(" ", no_breaking_space);
            }
        }
    }
}