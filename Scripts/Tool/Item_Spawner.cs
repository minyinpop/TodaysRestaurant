using Item.Interface;
using UnityEditor;
using UnityEngine;

namespace Tool
{
    public class ItemSpawner : EditorWindow
    {
        // 要被添加的儲物介面。
        private Storage.Root.Frontend.Storage _storage;
        // 要被添加的物品。
        private Object _item;
        
        [MenuItem("Tools/Minyinpop/物品生成器")]
        private static void ShowWindow() => GetWindow(typeof(ItemSpawner), false, "📦物品生成");
        
        private void OnGUI()
        {
            EditorGUILayout.Space(9);
            
            if (Application.isPlaying)
            {
                _storage = EditorGUILayout.ObjectField("儲物介面", _storage, typeof(Storage.Root.Frontend.Storage), true) as Storage.Root.Frontend.Storage;
                EditorGUILayout.Space(1);
                _item = EditorGUILayout.ObjectField("物品", _item, typeof(Object), true);
                EditorGUILayout.Space(1);
                
                if (GUILayout.Button("添加物品 !"))
                {
                    if (_storage is null)
                    {
                        EditorUtility.DisplayDialog("缺失組件", "不知道要添加到哪一個儲物介面。", "OK");
                        return;
                    }

                    switch (_item)
                    {
                        case null:
                        {
                            EditorUtility.DisplayDialog("缺失組件", "不知道要添加甚麼物品到儲物介面。", "OK");
                            break;
                        }
                        case ScriptableObject and ITem item:
                        {
                            _storage.AddItem(item);
                            Debug.Log($"{_item.name} 添加成功 !");
                            break;
                        }
                        default:
                        {
                            EditorUtility.DisplayDialog("添加錯誤", "該組件並不是系統認定的物品", "OK");
                            break;
                        }
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