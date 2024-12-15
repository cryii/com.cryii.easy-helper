namespace CryII.EasyHelper
{
    public static class Extension
    {
        /// <summary>
        /// 路径格式化；/ 替换 \\ 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string PathFormat(this string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }
        
            var tempPath = path.Replace(@"//", "/")
                .Replace(@"\\", "/")
                .Replace(@"\", "/")
                .TrimStart('/').TrimEnd('/');
            return tempPath;
        }
    }
}