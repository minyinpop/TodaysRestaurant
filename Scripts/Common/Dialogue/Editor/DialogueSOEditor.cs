using System.Collections.Generic;
using Common.Dialogue.Main;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Common.Dialogue.Editor
{
    [CustomEditor(typeof(DialogueSO))]
    public sealed class DialogueSOEditor : UnityEditor.Editor
    {
        private SerializedProperty _dialogueDataEntries;
        private ReorderableList _dialogueDataEntriesList;

        // 🔥 內層 List Cache（關鍵）
        private Dictionary<string, ReorderableList> _innerLists = new();

        private void OnEnable()
        {
            _dialogueDataEntries = serializedObject.FindProperty("dialogueDataEntries");

            _dialogueDataEntriesList = new ReorderableList(serializedObject, _dialogueDataEntries)
            {
                drawHeaderCallback = rect =>
                {
                    var style = new GUIStyle(EditorStyles.boldLabel)
                    {
                        alignment = TextAnchor.MiddleCenter
                    };

                    EditorGUI.LabelField(rect, "對話資料", style);
                },

                drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    var element = _dialogueDataEntries.GetArrayElementAtIndex(index);
                    var dialogueData = element.FindPropertyRelative("dialogueData");

                    rect.x += 12;

                    // Foldout
                    var foldoutRect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

                    element.isExpanded = EditorGUI.Foldout(
                        foldoutRect,
                        element.isExpanded,
                        $"對話 {index + 1:D2}",
                        true
                    );

                    if (!element.isExpanded)
                        return;

                    // 子 List
                    var contentRect = new Rect(
                        rect.x + 10,
                        rect.y + EditorGUIUtility.singleLineHeight + 4,
                        rect.width - 10,
                        GetDialogueDataHeight(dialogueData)
                    );

                    DrawDialogueData(contentRect, dialogueData);
                },

                elementHeightCallback = index =>
                {
                    var element = _dialogueDataEntries.GetArrayElementAtIndex(index);

                    float height = EditorGUIUtility.singleLineHeight + 6;

                    if (element.isExpanded)
                    {
                        var dialogueData = element.FindPropertyRelative("dialogueData");
                        height += GetDialogueDataHeight(dialogueData);
                    }

                    return height;
                }
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.Space(8);

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("展開全部"))
            {
                for (int i = 0; i < _dialogueDataEntries.arraySize; i++)
                {
                    _dialogueDataEntries.GetArrayElementAtIndex(i).isExpanded = true;
                }
            }

            if (GUILayout.Button("收起全部"))
            {
                for (int i = 0; i < _dialogueDataEntries.arraySize; i++)
                {
                    _dialogueDataEntries.GetArrayElementAtIndex(i).isExpanded = false;
                }
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(8);

            _dialogueDataEntriesList.DoLayoutList();

            EditorGUILayout.Space(8);

            serializedObject.ApplyModifiedProperties();
        }

        // ===== 🔥 內層 ReorderableList =====

        private ReorderableList GetDialogueList(SerializedProperty property)
        {
            string key = property.propertyPath;

            if (_innerLists.TryGetValue(key, out var list))
                return list;

            list = new ReorderableList(property.serializedObject, property)
            {
                drawHeaderCallback = rect =>
                {
                    var style = new GUIStyle(EditorStyles.boldLabel)
                    {
                        alignment = TextAnchor.MiddleCenter
                    };
                    
                    EditorGUI.LabelField(rect, "台詞列表", style);
                },

                drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    var element = property.GetArrayElementAtIndex(index);

                    rect.y += 2;

                    var label = GetDialoguePreview(index);

                    EditorGUI.PropertyField(rect, element, new GUIContent(label), true);
                },

                elementHeightCallback = index =>
                {
                    var element = property.GetArrayElementAtIndex(index);
                    return EditorGUI.GetPropertyHeight(element, true) + 4;
                }
            };

            _innerLists[key] = list;
            return list;
        }

        // ===== 巢狀繪製 =====

        private float GetDialogueDataHeight(SerializedProperty list)
        {
            if (list.arraySize == 0)
                return 50;

            var reorderableList = GetDialogueList(list);
            return reorderableList.GetHeight();
        }

        private void DrawDialogueData(Rect rect, SerializedProperty list)
        {
            if (list.arraySize == 0)
            {
                var helpRect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.HelpBox(helpRect, "沒有 DialogueData", MessageType.Info);

                var btnRect = new Rect(rect.x, rect.y + 22, rect.width, EditorGUIUtility.singleLineHeight);

                if (GUI.Button(btnRect, "新增一筆對話"))
                {
                    list.arraySize++;
                }

                return;
            }

            var reorderableList = GetDialogueList(list);
            reorderableList.DoList(rect);
        }

        // ===== Preview =====

        private string GetDialoguePreview(int index)
        {
            return $"劇情 {index + 1:D2}";
        }
    }
}