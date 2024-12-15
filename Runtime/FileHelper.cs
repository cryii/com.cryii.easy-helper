using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace CryII.EasyHelper
{
    public static class FileHelper
    {
        public static long FreeSpaceBytes(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException(path);
            }

            var drive = new DriveInfo(path);
            return drive.AvailableFreeSpace;
        }

        public static bool SpaceEnough(string path, long bytes)
        {
            var freeBytes = FreeSpaceBytes(path);
            return freeBytes >= bytes;
        }

        public static bool RecursiveGetDirectories(string path, out List<string> result, bool fullPath = true,
            List<string> excludeDirs = null)
        {
            result = new();

            void CollectDirectories(string dirPath, ref List<string> result)
            {
                if (excludeDirs != null && excludeDirs.Contains(path))
                {
                    return;
                }

                var dirSlug = fullPath ? dirPath : dirPath.Substring(path.Length);
                result.Add(dirSlug);

                var subDirs = Directory.GetDirectories(dirPath);
                foreach (var dir in subDirs)
                {
                    CollectDirectories(dir, ref result);
                }
            }

            if (!Directory.Exists(path))
            {
                Debug.LogError($"Directory not found: {path}");
                return false;
            }

            CollectDirectories(path, ref result);

            return true;
        }

        public static bool RecursiveGetLeafDirectories(string path, out List<string> result, bool fullPath = true,
            List<string> excludeDirs = null)
        {
            result = new();

            void CollectDirectories(string dirPath, ref List<string> result)
            {
                if (excludeDirs != null && excludeDirs.Contains(dirPath))
                {
                    return;
                }

                var subDirs = Directory.GetDirectories(dirPath);
                foreach (var dir in subDirs)
                {
                    CollectDirectories(dir, ref result);
                }

                if (subDirs.Length < 1)
                {
                    var dirSlug = fullPath ? dirPath : dirPath.Substring(path.Length);
                    result.Add(dirSlug);
                }
            }

            if (!Directory.Exists(path))
            {
                Debug.LogError($"Directory not found: {path}");
                return false;
            }

            CollectDirectories(path, ref result);

            return true;
        }

        public static bool RecursiveGetFiles(string path, out List<string> result, string[] ignoreExtension = null)
        {
            result = new();
            if (!Directory.Exists(path))
            {
                return false;
            }

            void CollectFiles(string dirPath, ref List<string> result)
            {
                var files = Directory.GetFiles(dirPath);
                foreach (var file in files)
                {
                    var extension = System.IO.Path.GetExtension(file);
                    if (ignoreExtension != null && ignoreExtension.Contains(extension))
                    {
                        continue;
                    }

                    result.Add(file);
                }

                var subDirs = Directory.GetDirectories(dirPath);
                foreach (var dir in subDirs)
                {
                    CollectFiles(dir, ref result);
                }
            }

            CollectFiles(path, ref result);

            return true;
        }

        public static (float, string) FormatByteSize(long bytes)
        {
            string[] suffix = {"B", "KB", "MB", "GB", "TB"};
            int i;
            float floatBytes = bytes;

            for (i = 0; i < suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                floatBytes = bytes / 1024.0f;
            }

            return (floatBytes, suffix[i]);
        }

        public static long GetFileSize(string file)
        {
            long fileLength = 0;
            if (File.Exists(file))
            {
                FileInfo fileInfo = new FileInfo(file);
                fileLength = fileInfo.Length;
            }

            return fileLength;
        }

        public static long GetDirectorySize(string dirPath)
        {
            void GetDirectorySizeDeep(string path, ref long dirSize)
            {
                try
                {
                    var dirInfo = new DirectoryInfo(path);
                    var dirs = dirInfo.GetDirectories();
                    var files = dirInfo.GetFiles();

                    foreach (var dir in dirs)
                    {
                        GetDirectorySizeDeep(dir.FullName, ref dirSize);
                    }

                    foreach (var file in files)
                    {
                        dirSize += file.Length;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            var totalSize = 0L;
            GetDirectorySizeDeep(dirPath, ref totalSize);
            return totalSize;
        }

        /// <summary>
        /// 获取文件的MD5值
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFileMD5Code(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            using var fs = new FileStream(filePath, FileMode.Open);
            var md5 = new MD5CryptoServiceProvider();
            var retVal = md5.ComputeHash(fs);

            var sb = new StringBuilder();
            foreach (var t in retVal)
            {
                sb.Append(t.ToString("x2"));
            }

            return sb.ToString();
        }

        public static void CopyDirectory(string pathFrom, string pathTo, bool overwrite, List<string> skipFiles = null)
        {
            if (!Directory.Exists(pathFrom))
            {
                Debug.LogError($"Source dir not found: {pathFrom}");
                return;
            }

            if (!Directory.Exists(pathTo))
            {
                Directory.CreateDirectory(pathTo);
            }

            var files = Directory.GetFiles(pathFrom);
            foreach (var file in files)
            {
                var fileName = System.IO.Path.GetFileName(file);
                var pathCopyTo = System.IO.Path.Combine(pathTo, fileName);
                if (!overwrite && File.Exists(pathCopyTo))
                {
                    skipFiles?.Add(file);
                    continue;
                }

                File.Copy(file, pathCopyTo);
            }

            var subDirectories = Directory.GetDirectories(pathFrom);
            foreach (var subDirectory in subDirectories)
            {
                var directoryName = System.IO.Path.GetFileName(subDirectory);
                var pathCopyTo = System.IO.Path.Combine(pathTo, directoryName);
                CopyDirectory(subDirectory, pathCopyTo, overwrite, skipFiles);
            }
        }

        public static bool TryGetChildFiles(string dirPath, out List<string> files, string[] ignoreExtension = null)
        {
            files = new();
            if (!Directory.Exists(dirPath))
            {
                return false;
            }

            var allFiles = Directory.GetFiles(dirPath);
            foreach (var file in allFiles)
            {
                if (ignoreExtension != null)
                {
                    var extension = System.IO.Path.GetExtension(file);
                    if (ignoreExtension.Contains(extension))
                    {
                        continue;
                    }
                }

                files.Add(file);
            }

            return true;
        }

        public static FileStream EnsureOpenFile(string path)
        {
            if (File.Exists(path))
            {
                return new FileStream(path, FileMode.Open);
            }
            
            var lackDirs = new Stack<string>();
            var dir = Path.GetDirectoryName(path);
            while (dir != null)
            {
                if (Directory.Exists(dir))
                {
                    break;
                }
                lackDirs.Push(dir);
                dir = Path.GetDirectoryName(dir);
            }

            while (lackDirs.TryPop(out dir))
            {
                Directory.CreateDirectory(dir);
            }

            return File.Create(path);
        }
    }
}