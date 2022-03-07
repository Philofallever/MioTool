using UnityEngine;

namespace MioTool
{
    public class MioToolTrigger : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            print($"This:{name} 进入触发器 OTHER:{other.gameObject.name}");
        }

        void OnTriggerExit(Collider other)
        {
            print($"This:{name} 离开触发器 OTHER:{other.gameObject.name}");
        }
    }
}