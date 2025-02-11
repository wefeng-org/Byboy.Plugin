using System.Text.RegularExpressions;

namespace Byboy.SignPlugin
{
    internal static class Util
    {
        /// <summary>
        /// 与ListToString相反，从字符串中提取多个数字，包装成List类型，便于查询、存储
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<int> StrToIntList(string str)
        {
            List<int> result = new List<int>();

            try {
                if (!string.IsNullOrWhiteSpace(str)) {
                    Match m = Regex.Match(str,@"\d+");
                    while (m.Success) {
                        result.Add(int.Parse(m.Value));
                        m = m.NextMatch();
                    }
                }
            } catch {
            }
            return result;
        }



    }
}
