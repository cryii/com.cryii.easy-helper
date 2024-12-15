using System;
using UnityEditor;
using UnityEngine;

namespace CryII.EasyHelper
{
    public static class LayoutHelper
    {
        public static Rect Horizontal(Action onGUI, params GUILayoutOption[] options)
        {
            var rect = EditorGUILayout.BeginHorizontal(options);
            onGUI();
            EditorGUILayout.EndHorizontal();
            return rect;
        }

        public static Rect Vertical(Action onGUI, params GUILayoutOption[] options)
        {
            var rect = EditorGUILayout.BeginVertical(options);
            onGUI();
            EditorGUILayout.EndVertical();
            return rect;
        }
    }
}