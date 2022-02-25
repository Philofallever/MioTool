using Sirenix.OdinInspector;
using UnityEngine;

namespace MioTool
{
    public partial class MioTool
    {
        [PropertySpace(10, 10), BoxGroup("GameObject"), Button]
        void TestDestroy()
        {
            if (!Application.isPlaying) return;
            Destroy(gameObject);

            print("TestDestroy:\r\n"
                + $"GameObject: {gameObject}\r\n"                       // gameobject.name
                + $"GameObject == true: {gameObject == true}\r\n"       // true
                + $"GameObject == null: {gameObject == null}\r\n"       // false
                + $"gameObject.activeSelf: {gameObject.activeSelf}\r\n" // true
                + $"this enabled: {enabled}\r\n"                        // false
                + $"this == true: {this == true}\r\n"                   // true
                + $"this == null: {this == null}");                     // false
        }
    }
}