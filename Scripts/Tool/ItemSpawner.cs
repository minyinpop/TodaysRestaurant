using UnityEditor;
using UnityEngine;

namespace Tool
{
    public class ItemSpawner : EditorWindow
    {
        // 物品要添加到哪一個儲物系統？
        
        [MenuItem("Tool/Item Spawner")]
        private static void ShowWindow()
        {
            GetWindow(typeof(ItemSpawner), false, "物品生成工具");
        }

        private void OnGUI()
        {
            EditorGUILayout.Space(9);
            
            // TODO: 儲物系統
            EditorGUILayout.Space(6);
            
            // TODO: 生成物品

            if (GUILayout.Button("開始生成"))
            {
                // TODO: 生成按鈕被點擊後的觸發事件
            }
        }
    }
}
