using System;
using System.Collections.Generic;
using UnityEditor;

namespace CryII.EasyHelper
{
    public static class DialogHelper
    {
        public static bool Tip(string tips)
        {
            var lines = new List<string>();
            var ptr = 0;
            while (ptr < tips.Length)
            {
                var length = Math.Min(100, tips.Length - ptr);
                lines.Add(tips.Substring(ptr, length));
                ptr += length;
            }

            var msg = string.Join("\n", lines);
            return EditorUtility.DisplayDialog("Tips", msg, "确认");
        }

        public static bool Warning(string tips)
        {
            return EditorUtility.DisplayDialog("Warning", tips, "确认", "取消");
        }

        public static void Error(string tips)
        {
            EditorUtility.DisplayDialog("Error", tips, "确认");
        }
    }
}