using UnityEditor;
using UnityEngine;

namespace Tools
{
    public class ItemSpawner : EditorWindow
    {
        private readonly GUIStyle _titleStyle = new GUIStyle()
        {
            fontSize = 16,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter,
            normal = { textColor = Color.white }
        };

        private readonly GUIStyle _subtitleStyle = new GUIStyle()
        {
            fontSize = 12,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleLeft,
            normal = { textColor = Color.white }
        };
        
        [MenuItem("Window/Item Spawner")]
        public static void OnWindow()
        {
            GetWindow(typeof(ItemSpawner));
        }

        private void OnGUI()
        {
            EditorGUILayout.Space(width: 6);
            EditorGUILayout.LabelField("Item Spawner", _titleStyle);
            EditorGUILayout.Space(width: 6);
            EditorGUILayout.LabelField("Red Mushroom", _subtitleStyle);
        }
    }
}