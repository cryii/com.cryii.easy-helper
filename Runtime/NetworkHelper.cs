using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;

namespace CryII.EasyHelper
{
    public static class NetworkHelper
    {
        /// <summary>
        /// 检查网络是否可用
        /// </summary>
        /// <returns></returns>
        public static bool IsNetworkAvailable()
        {
            return Application.internetReachability != NetworkReachability.NotReachable;
        }

        /// <summary>
        /// 是否在使用wifi
        /// </summary>
        /// <returns></returns>
        public static bool IsUsingWiFi()
        {
            return Application.internetReachability != NetworkReachability.ReachableViaLocalAreaNetwork;
        }

        /// <summary>
        /// 解析网络文件字节数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static ulong TryParseNetFileBytes(UnityWebRequest request)
        {
            var bytes = ulong.Parse(request.GetResponseHeader("Content-Length"));
            return bytes;
        }

        /// <summary>
        /// 设置下载文件范围
        /// </summary>
        /// <param name="request"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public static void SetDownloadRange(UnityWebRequest request, ulong start, ulong end = default)
        {
            var range = new StringBuilder("bytes=");
            range.Append(start);
            range.Append("-");
            if (end != default && end > start)
            {
                range.Append(end);
            }

            request.SetRequestHeader("Range", range.ToString());
        }
        
        public static async UniTask<bool> WWWCopy(string sourceFile, string targetFile)
        {
            var request = UnityWebRequest.Get(sourceFile);
            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                if (File.Exists(targetFile))
                {
                    File.Delete(targetFile);
                }

                var data = request.downloadHandler.data;
                var fs = File.Create(targetFile);
                await fs.WriteAsync(data, 0, data.Length);
                fs.Flush();
                fs.Close();

                return true;
            }

            return false;
        }
    }
}