using System;
using System.Collections.Generic;
using System.Text;

namespace Converter
{
    public static class Strings
    {
        public static string Merger(string[] lines)
        {
            string result = string.Empty;

            foreach (var item in lines)
                result += item + ',';

            result = result.Remove(result.Length - 1);

            return result;
        }
    }
}
