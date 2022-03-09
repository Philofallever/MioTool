using Sirenix.OdinInspector;
using UnityEngine;

namespace MioTool
{
    public partial class MioTool
    {
        private const string _showGOGroup = "_showGameObject";
        private const string _goGroup = _showGOGroup + "/GameObject";
        private const string _goButtonGroup = _goGroup + "/Button";

        [SerializeField]
        private bool _showGameObject = false;

        [SerializeField, BoxGroup(_goGroup)]
        private GameObject obj;

        [ShowIfGroup(_showGOGroup)]
        [PropertySpace(10), ButtonGroup(_goButtonGroup)]
        void TestDestroy()
        {
            if (!Application.isPlaying) return;
            Destroy(gameObject);

            // ScriptReference/Object.Destroy.html
            // Actual object destruction is always delayed until
            // after the current Update loop, but will always be done before rendering.

            print("TestDestroy: This Show When Destroy Gameobject,It only turn enable to false \r\n"
                + $"GameObject: {gameObject}\r\n"                       // gameobject.name
                + $"GameObject == true: {gameObject == true}\r\n"       // true
                + $"GameObject == null: {gameObject == null}\r\n"       // false
                + $"gameObject.activeSelf: {gameObject.activeSelf}\r\n" // true
                + $"this enabled: {enabled}\r\n"                        // false
                + $"this == true: {this == true}\r\n"                   // true
                + $"this == null: {this == null}");                     // false

            AfterCall();
        }

        void OnDestroy()
        {
            AfterCall();
        }

        void AfterCall()
        {
            print("TestDestroy: This Show When Destroy Gameobject,It only turn enable to false \r\n"
                + $"GameObject: {gameObject}\r\n"                       // gameobject.name
                + $"GameObject == true: {gameObject == true}\r\n"       // true
                + $"GameObject == null: {gameObject == null}\r\n"       // false
                + $"gameObject.activeSelf: {gameObject.activeSelf}\r\n" // *false*
                + $"this enabled: {enabled}\r\n"                        // false
                + $"this == true: {this == true}\r\n"                   // true
                + $"this == null: {this == null}");                     // false
        }
    }
}