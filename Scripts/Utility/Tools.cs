using UnityEngine;

namespace Utility
{
    internal static class Tools
    {
        // $"錯誤訊息：\n遊戲物件：{name}\n錯誤組件：{GetType().Name}\n"
        public static bool CheckNull(bool condition, string message)
        {
            if (!condition) return false;
#if UNITY_EDITOR
            Debug.LogWarning(message);
#endif
            return true;
        }
    }
}