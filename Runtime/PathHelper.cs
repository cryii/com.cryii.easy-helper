using System.IO;
using UnityEngine;

namespace CryII.EasyHelper
{
    public static class PathHelper
        {
            /// <summary>
            /// 去除匹配前缀
            /// </summary>
            /// <param name="path"></param>
            /// <param name="sep"></param>
            /// <returns></returns>
            public static string DelBegin(string path, string sep)
            {
                if (string.IsNullOrEmpty(path))
                {
                    return string.Empty;
                }

                var subPath = path.Split(sep)[0];
                return subPath;
            }

            /// <summary>
            /// 去除匹配后缀
            /// </summary>
            /// <param name="path"></param>
            /// <param name="sep"></param>
            /// <returns></returns>
            public static string DelEnd(string path, string sep)
            {
                if (string.IsNullOrEmpty(path))
                {
                    return string.Empty;
                }

                var paths = path.Split(sep);
                return paths.Length > 1 ? paths[1] : path;
            }

            /// <summary>
            /// 合并路径
            /// </summary>
            /// <param name="path"></param>
            /// <param name="paths"></param>
            /// <returns></returns>
            public static string Combine(string path, params string[] paths)
            {
                if (string.IsNullOrEmpty(path)) return string.Empty;
                var tempPath = path;
                foreach (var item in paths)
                {
                    tempPath = Path.Combine(tempPath, item);
                }

                return Path.GetFullPath(tempPath).PathFormat();
            }

            /// <summary>
            /// 获取路径偏移
            /// </summary>
            /// <param name="path"></param>
            /// <param name="basePath"></param>
            /// <returns></returns>
            public static string OffsetPath(string path, string basePath)
            {
                path = path.PathFormat();
                basePath = basePath.PathFormat();
                var relativePath = path.Replace(basePath, string.Empty).PathFormat();
                return relativePath;
            }

            public static class Unity
            {
                /// <summary>
                /// 项目路径
                /// </summary>
                /// <returns></returns>
                public static string ProjectPath => Path.GetFullPath(Path.Combine(Application.dataPath, "../"));

                /// <summary>
                /// 获取相对项目路径的绝对路径
                /// </summary>
                /// <param name="path"></param>
                /// <returns></returns>
                public static string RelativeToProject(string path)
                {
                    return Path.GetFullPath(path, ProjectPath);
                }

                /// <summary>
                /// 获取相对Assets的绝对路径
                /// </summary>
                /// <param name="path"></param>
                /// <returns></returns>
                public static string RelativeToAssets(string path)
                {
                    return Path.GetRelativePath(Application.dataPath, path).PathFormat();
                }


                /// <summary>
                /// 获取基于Project的相对路径
                /// </summary>
                /// <param name="path"></param>
                /// <returns></returns>
                public static string RelativeOfProject(string relativePath)
                {
                    return Path.GetRelativePath(ProjectPath, relativePath);
                }

                /// <summary>
                /// 获取基于Assets的相对路径
                /// </summary>
                /// <param name="path"></param>
                /// <returns></returns>
                public static string RelativeOfAssets(string relativePath)
                {
                    return Path.GetRelativePath(Application.dataPath, relativePath);
                }
            }
        }
}