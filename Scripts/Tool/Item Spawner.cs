#if UNITY_EDITOR

using Item;
using Storage;
using UnityEditor;
using UnityEngine;

namespace Tool
{
    public class ItemSpawner : EditorWindow
    {
        [field: Header("玩家背包"), Tooltip("玩家背包的資料"), SerializeField]
        private StorageDataSO playerBagData;
        
        [MenuItem("Tools/Minyinpop/物品生成器")]
        public static void ShowWindow()
        {
            GetWindow(typeof(ItemSpawner), false, "物品生成器");
        }

        private void OnGUI()
        {
            var titleStyle = new GUIStyle
            {
                fontSize = 16,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = Color.white }
            };

            var subtitleStyle = new GUIStyle
            {
                fontSize = 12,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleLeft,
                normal = { textColor = Color.white }
            };
            
            EditorGUILayout.Space(9);
            EditorGUILayout.LabelField("物品生成器", titleStyle);
            EditorGUILayout.Space(9);
            
            EditorGUILayout.LabelField("玩家背包", subtitleStyle);
            playerBagData = EditorGUILayout.ObjectField("玩家背包資訊", playerBagData, typeof(StorageDataSO), true) as StorageDataSO;

            if (Application.isPlaying)
            {
                EditorGUILayout.LabelField("紅蘑菇", subtitleStyle);
                EditorGUILayout.BeginHorizontal();
                ButtonInit("Lv.1", "Red Mushroom/Lv.1 Red Mushroom");
                ButtonInit("Lv.2", "Red Mushroom/Lv.2 Red Mushroom");
                ButtonInit("Lv.3", "Red Mushroom/Lv.3 Red Mushroom");
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.LabelField("白蘑菇", subtitleStyle);
                EditorGUILayout.BeginHorizontal();
                ButtonInit("Lv.1", "White Mushroom/Lv.1 White Mushroom");
                ButtonInit("Lv.2", "White Mushroom/Lv.2 White Mushroom");
                ButtonInit("Lv.3", "White Mushroom/Lv.3 White Mushroom");
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.LabelField("扇蘑菇", subtitleStyle);
                EditorGUILayout.BeginHorizontal();
                ButtonInit("Lv.1", "Panellus Mushroom/Lv.1 Panellus Mushroom");
                ButtonInit("Lv.2", "Panellus Mushroom/Lv.2 Panellus Mushroom");
                ButtonInit("Lv.3", "Panellus Mushroom/Lv.3 Panellus Mushroom");
                EditorGUILayout.EndHorizontal();
            }
            else
                EditorGUILayout.HelpBox(" 開始遊戲後才可以使用 !", MessageType.Info);
        }

        /// <summary>
        /// 生成按鈕並定義功能
        /// </summary>
        /// <param name="buttonName"></param>
        /// <param name="filePath"></param>
        private void ButtonInit(string buttonName, string filePath)
        {
            if (GUILayout.Button(buttonName))
                playerBagData.AddItem(Resources.Load<ItemCore>(filePath));
        }
    }
}

#endif