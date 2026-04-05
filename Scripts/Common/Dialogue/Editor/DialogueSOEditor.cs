using System.Collections.Generic;
using Common.Dialogue.Data;
using Common.Dialogue.Main;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Common.Dialogue.Editor
{
    [CustomEditor(typeof(DialogueSO))]
    public sealed class DialogueSOEditor : UnityEditor.Editor
    {
        private SerializedProperty _startIndex;
        
        private SerializedProperty _dialogueDataEntries;
        private ReorderableList _dialogueDataEntriesList;

        private Dictionary<string, ReorderableList> _innerLists = new();

        private void OnEnable()
        {
            _startIndex = serializedObject.FindProperty("startIndex");
            
            _dialogueDataEntries = serializedObject.FindProperty("dialogueDataEntries");

            _dialogueDataEntriesList = new ReorderableList(serializedObject, _dialogueDataEntries)
            {
                drawHeaderCallback = rect =>
                {
                    var style = new GUIStyle(EditorStyles.boldLabel)
                    {
                        alignment = TextAnchor.MiddleCenter
                    };

                    EditorGUI.LabelField(rect, "段落列表", style);
                },

                drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    var element = _dialogueDataEntries.GetArrayElementAtIndex(index);
                    var dialogueData = element.FindPropertyRelative("dialogueData");

                    rect.x += 12;

                    var foldoutRect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

                    element.isExpanded = EditorGUI.Foldout(
                        foldoutRect,
                        element.isExpanded,
                        $"段落 {index + 1:D2}",
                        true
                    );

                    if (!element.isExpanded)
                        return;

                    var contentRect = new Rect(
                        rect.x,
                        rect.y + EditorGUIUtility.singleLineHeight + 4,
                        rect.width - 10,
                        GetDialogueDataHeight(dialogueData)
                    );

                    DrawDialogueData(contentRect, dialogueData);
                },

                elementHeightCallback = index =>
                {
                    var element = _dialogueDataEntries.GetArrayElementAtIndex(index);

                    float height = EditorGUIUtility.singleLineHeight;

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
            
                EditorGUILayout.BeginVertical("box");

                var titleStyle = new GUIStyle(EditorStyles.boldLabel)
                {
                    alignment = TextAnchor.MiddleCenter,
                    fontSize = 12
                };

                EditorGUILayout.LabelField("起始段落", titleStyle);

                EditorGUILayout.Space(4);

                    EditorGUILayout.BeginHorizontal();
                    
                    EditorGUILayout.LabelField("段落", GUILayout.Width(64));
                    _startIndex.intValue = EditorGUILayout.IntField(_startIndex.intValue);

                    if (_dialogueDataEntries.arraySize > 0)
                    {
                        if (_startIndex.intValue > _dialogueDataEntries.arraySize - 1)
                        {
                            _startIndex.intValue = _dialogueDataEntries.arraySize;
                        }
                    }

                    if (_startIndex.intValue < 0)
                    {
                        _startIndex.intValue = EditorGUILayout.IntField(0);
                    }

                    EditorGUILayout.EndHorizontal();

                EditorGUILayout.EndVertical();
            
            EditorGUILayout.Space(32);

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
                    
                    EditorGUI.LabelField(rect, "指令列表", style);
                },

                drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    var element = property.GetArrayElementAtIndex(index);

                    rect.y += 2;

                    string label;

                    if (element.objectReferenceValue is not DialogueData data)
                    {
                        label = $"⚪ 指令 {index + 1:D2}";
                    }
                    else
                    {
                        if (data.AutoPass)
                        {
                            label = $"🟢 指令 {index + 1:D2}";
                        }
                        else
                        {
                            label = $"🟡 指令 {index + 1:D2}";
                        }
                    }
                    
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

                if (GUI.Button(btnRect, "新增一筆指令"))
                {
                    list.arraySize++;
                }

                return;
            }

            var reorderableList = GetDialogueList(list);
            reorderableList.DoList(rect);
        }
    }
}