using Item.Interface;
using Player;
using Storage.Root.Backend.Struct;
using UnityEditor;
using UnityEngine;

namespace Tool
{
    public class ItemSpawner : EditorWindow
    {
        // 要被添加的儲物介面。
        private PlayerStorageSystem _storage;
        // 要被添加的物品。
        private Object _item;
        // 物品要添加的數量。
        private int _quantity = 1;
        
        [MenuItem("Tools/Minyinpop/物品生成器")]
        private static void ShowWindow() => GetWindow(typeof(ItemSpawner), false, "📦物品生成");
        
        private void OnGUI()
        {
            EditorGUILayout.Space(9);
            
            if (Application.isPlaying)
            {
                _storage = EditorGUILayout.ObjectField("儲物介面", _storage, typeof(PlayerStorageSystem), true) as PlayerStorageSystem;
                EditorGUILayout.Space(1);
                
                _item = EditorGUILayout.ObjectField("物品", _item, typeof(Object), true);
                EditorGUILayout.Space(1);
                
                _quantity = EditorGUILayout.IntField("數量", _quantity);
                _quantity = Mathf.Clamp(_quantity, 1, 99);
                EditorGUILayout.Space(1);
                
                if (GUILayout.Button("添加物品 !"))
                {
                    // 檢查儲物空間
                    if (_storage is null)
                    {
                        EditorUtility.DisplayDialog("缺失組件", "不知道要添加到哪一個儲物介面。", "OK");
                        return;
                    }

                    // 檢查物品資料
                    switch (_item)
                    {
                        case null:
                            EditorUtility.DisplayDialog("缺失組件", "不知道要添加甚麼物品到儲物介面。", "OK");
                            break;
                        
                        case ScriptableObject and ITem item:
                            _storage.AddItem(new StorageSlotData
                            {
                                Locked = false,
                                Item = item,
                                Quantity = _quantity
                            });
                            break;
                        
                        default:
                            EditorUtility.DisplayDialog("組件錯誤", "該組件並不是系統認定的物品。", "OK");
                            break;
                    }
                }
            }
            else
            {
                EditorGUILayout.HelpBox(" 請先開始遊戲 !", MessageType.Info);
            }
        }
    }
}