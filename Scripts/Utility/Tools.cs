using UnityEngine;

namespace Utility
{
    internal static class Tools
    {
        internal static bool CheckNull(bool condition, string message)
        {
            if (!condition) return false;
#if UNITY_EDITOR
            Debug.LogWarning(message);
#endif
            return true;
        }
    }
}