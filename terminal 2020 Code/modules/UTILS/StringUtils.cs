using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace sorec_gamma.modules.UTILS
{
    public static class StringUtils
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
        public static string GetLastNChars(string source, int numberOfChars)
        {
            if (numberOfChars >= source.Length)
                return source;
            return source.Substring(source.Length - numberOfChars);
        }
        
        public static string GetLastWord(string source, char separator)
        {
            int idx = source.LastIndexOf(separator);
            return source.Substring(idx + 1);
        }


        public static string AddSuffix(string input, char c, int length)
        {
            if (input.Length > length)
            {
                return input.Substring(0, length);
            }
            else if (input.Length < length)
            {
                return input + new string(c, length - input.Length);
            }
            else
            {
                return input;
            }
        }

        public static List<List<string>> SplitList(List<string> source, int nSize = 3)
        {
            var list = new List<List<string>>();

            for (int i = 0; i < source.Count; i += nSize)
            {
                list.Add(source.GetRange(i, Math.Min(nSize, source.Count - i)));
            }

            return list;
        }
        public static MatchCollection SplitToLines(string stringToSplit, int maximumLineLength)
        {
            return Regex.Matches(stringToSplit, @"(.{1," + maximumLineLength + @"})(?:\s|$)");
        }
    }
}
