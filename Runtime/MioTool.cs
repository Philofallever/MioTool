using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MioTool
{
    public sealed partial class MioTool : MonoBehaviour
    {
        public static bool LogEnabled { get; set; } = true;

        public static void Log(object message, Object context = null)
        {
            if (!LogEnabled) return;
            Debug.Log(message, context);
        }

        public static void LogError(object message, Object context = null)
        {
            if (!LogEnabled) return;
            Debug.LogError(message, context);
        }

        public static void LogException(Exception exception, Object context = null)
        {
            if (!LogEnabled) return;
            Debug.LogException(exception, context);
        }

        bool ValidateComponent<T>(bool includeChildren = true) where T : Component
        {
            if (includeChildren)
                return GetComponentInChildren<T>();
            else
                return GetComponent<T>();
        }
    }
}