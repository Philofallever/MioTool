using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace MioTool
{
    public partial class MioTool
    {
        const string _showEditorAppGroup = nameof(_showEditorApp);
        const string _editorAppGroup = _showEditorAppGroup + "/EditorApplication";
        const string _editorAppBtnGroup = _editorAppGroup + "/Button";

        [SerializeField]
        private bool _showEditorApp;

        [ShowIfGroup(_showEditorAppGroup)]
        [SerializeField, BoxGroup(_editorAppGroup)]
        private string _;

        [PropertySpace(10)]
        [ButtonGroup(_editorAppBtnGroup)]
        public void SwitchPause()
        {
            EditorApplication.isPaused = !EditorApplication.isPaused;
        }

        [ButtonGroup(_editorAppBtnGroup)]
        public void SwitchPlay()
        {
            EditorApplication.isPlaying = !EditorApplication.isPlaying;
        }
    }
}