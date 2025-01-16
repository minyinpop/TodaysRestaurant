using Item.Core;
using Storage.UI.Core;
using UnityEditor;
using UnityEngine;

namespace Tool.Editor
{
    public class ItemSpawner : EditorWindow
    {
        [field: Tooltip("將物品添加到哪一個儲存位置 ?")]
        public StorageUICore storageUI;

        [field: Tooltip("甚麼物品要被添加 ?")]
        public ItemCore item;
        
        [MenuItem("Tools/Minyinpop/Item Spawner")]
        public static void ShowWindow()
        {
            GetWindow(typeof(ItemSpawner), false, "Item Spawner");
        }

        private void OnGUI()
        {
            var titleStyle = new GUIStyle
            {
                fontSize = 18,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = Color.white }
            };
            
            EditorGUILayout.Space(9);
            EditorGUILayout.LabelField("物品添加工具", titleStyle);

            if (Application.isPlaying)
            {
                EditorGUILayout.Space(9);
                storageUI = EditorGUILayout.ObjectField("儲存空間", storageUI, typeof(StorageUICore), true) as StorageUICore;

                EditorGUILayout.Space(9);
                item = EditorGUILayout.ObjectField("物品", item, typeof(ItemCore), true) as ItemCore;

                EditorGUILayout.Space(9);
                if (GUILayout.Button("添加"))
                    storageUI?.AddItem(item);
            }
            else
            {
                EditorGUILayout.Space(9);
                EditorGUILayout.HelpBox(" 請先開始遊戲 !", MessageType.Info);
            }
        }
    }
}
