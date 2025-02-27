using Item;
using Player;
using Storage.Slot;
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
        public PlayerStorageSystem playerStorageSystem;
        
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
            
            playerStorageSystem = EditorGUILayout.ObjectField(new GUIContent("玩家儲物系統"), playerStorageSystem, typeof(PlayerStorageSystem), true) as PlayerStorageSystem;
            EditorGUILayout.Space(3);
            itemData = EditorGUILayout.ObjectField(new GUIContent("物品資料"), itemData, typeof(Object), true);
            EditorGUILayout.Space(3);
            itemQuantity = EditorGUILayout.IntField(new GUIContent("物品數量"), itemQuantity);
            itemQuantity = Mathf.Clamp(itemQuantity, 1, 99);
            EditorGUILayout.Space(3);
            
            // 如果遊戲還沒開始。
            if (!Application.isPlaying)
            {
                EditorGUILayout.HelpBox(" 遊戲開始後方可使用該工具。", MessageType.Info);
                return;
            }
            
            // 當添加物品的按鈕被按下後所發生的事情。
            if (GUILayout.Button("添加物品"))
            {
                if (playerStorageSystem is null)
                {
                    EditorUtility.DisplayDialog("配置錯誤", "玩家儲物系統不能是空的 !", "OK");
                    return;
                }

                if (itemData is null)
                {
                    EditorUtility.DisplayDialog("配置錯誤", "被添加的物品資料不能是空的。", "OK");
                    return;
                }

                if (itemData is not ItemData item)
                {
                    EditorUtility.DisplayDialog("設置錯誤", "被添加的物品資料不是系統認定的物品。", "OK");
                    return;
                }

                var slotData = new StorageSlotData
                {
                    locked = false,
                    itemData = item,
                    itemQuantity = itemQuantity,
                };
                playerStorageSystem.AddItem(slotData);
            }
        }
    }
}
