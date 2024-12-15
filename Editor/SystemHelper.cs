using System.IO;
using System.Diagnostics;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace CryII.EasyHelper
{
    public static class SystemHelper
    {
        public static void OpenFileByVsCode(string assetPath)
        {
            if (string.IsNullOrEmpty(assetPath) || !Directory.Exists(assetPath) && !File.Exists(assetPath))
            {
                throw new FileNotFoundException(assetPath);
            }

            Process.Start("code", assetPath);
        }

        public static bool TryGetPrefabPath(GameObject go, out string assetPath)
        {
            assetPath = string.Empty;

            if (!EditorSceneManager.IsPreviewSceneObject(go))
            {
                return false;
            }

            var prefabStage = PrefabStageUtility.GetPrefabStage(go);
            if (prefabStage != null)
            {
                assetPath = prefabStage.assetPath;
            }

            return true;
        }

        public static void LoadSceneByPath(string path)
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene(path);
            }
        }
    }
}