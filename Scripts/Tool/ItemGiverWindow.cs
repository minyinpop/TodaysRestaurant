using Item;
using Player;
using UnityEditor;
using UnityEngine;

namespace Tool
{
    /// <summary>
    /// 用來給予物品的工具視窗。
    /// </summary>
    public class ItemGiverWindow : EditorWindow
    {
        // 要被添加物品的玩家儲物系統。
        public PlayerStorageSystem storageUI;
        
        // 要被添加的物品的資料。
        public Object itemData;
        
        // 要被添加的物品的數量。
        public int itemQuantity;
        
        [MenuItem("Tools/Item Giver")]
        private static void OpenWindow()
        {
            GetWindow<ItemGiverWindow>("Item Giver");
        }
        
        private void OnGUI()
        {
            var titleStyle = new GUIStyle
            {
                fontSize = 20,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = Color.white }
            };
            EditorGUILayout.Space(9);
            EditorGUILayout.LabelField("Item Giver", titleStyle);
            EditorGUILayout.Space(9);

            storageUI = EditorGUILayout.ObjectField(new GUIContent("玩家儲物系統"), storageUI, typeof(PlayerStorageSystem), true) as PlayerStorageSystem;
            EditorGUILayout.Space(3);
            itemData = EditorGUILayout.ObjectField(new GUIContent("物品資料"), itemData, typeof(Object), true);
            EditorGUILayout.Space(3);
            itemQuantity = EditorGUILayout.IntField(new GUIContent("物品數量"), itemQuantity);
            itemQuantity = Mathf.Max(1, 99);
            EditorGUILayout.Space(3);
            
            // 當添加物品的按鈕被按下後所發生的事情。
            if (GUILayout.Button("添加物品"))
            {
                if (itemData is not ItemData)
                {
                    EditorUtility.DisplayDialog("設置錯誤", "這個不是系統認定的品。", "OK");
                    return;
                }
                // TODO: 添加物品 ......
            }
        }
    }
}
