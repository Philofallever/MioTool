
using Sirenix.OdinInspector;
using UnityEditor;

namespace MioTool.Editor
{
    public partial class MioToolEditor
    {
        [PropertySpace(10)]
        [BoxGroup("EditorApplication"),Button]
        public void SwitchPause()
        {
            EditorApplication.isPaused = !EditorApplication.isPaused;
        }


    }
}