using System;
using System.Text.RegularExpressions;

namespace CryII.EasyHelper
{
    public static class TextHelper
    {
        /// <summary>
        /// 驼峰命名
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string PascalCase(string name)
        {
            var pascalName = name.Substring(0, 1).ToUpper() + name.Substring(1);
            return pascalName;
        }

        /// <summary>
        /// 制表符换行
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static string TabLine(string line)
        {
            return $"\t{line}";
        }

        /// <summary>
        /// 缩进
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="withEndl"></param>
        /// <returns></returns>
        public static string TabScope(string scope, bool withEndl = true)
        {
            var tabScope = scope.Replace("\n", $"\n\t");
            if (withEndl)
            {
                tabScope = $"{tabScope}\n";
            }

            return $"\n\t{tabScope}";
        }

        /// <summary>
        /// 是否包含中文
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool HasChinese(string str)
        {
            return Regex.IsMatch(str, "[\u4e00-\u9fa5]");
        }

        /// <summary>
        /// 是否只包含字母
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool HasAlpha(string str)
        {
            return Regex.IsMatch(str, "[a-zA-Z]");
        }

        /// <summary>
        /// 是否包含数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool HasDigit(string str)
        {
            return Regex.IsMatch(str, "[0-9]");
        }

        /// <summary>
        /// 是否全中文
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsChinese(string str)
        {
            foreach (char c in str)
            {
                if (!Regex.IsMatch(c.ToString(), "[\u4e00-\u9fa5]"))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 是否全字母
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsAlpha(string str)
        {
            foreach (char c in str)
            {
                if (!Char.IsLetter(c))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 是否全数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDigit(string str)
        {
            foreach (char c in str)
            {
                if (!Char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 剔除中文字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RidChinese(string str)
        {
            return Regex.Replace(str, "[\u4e00-\u9fa5]", "", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 保留字母、数字、下划线
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToValidName(string str)
        {
            var res = Regex.Replace(str, @"[\W+]", "", RegexOptions.IgnoreCase);
            // 剔除中文
            return RidChinese(res);
        }
    }
}