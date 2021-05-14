using System;
using System.Linq;

namespace Converter
{
    public static class Numbers
    {
        public static string EditToReadableNumber(double? number) => 
            number.HasValue ? EditToReadableNumber((double)number) : "0";        

        public static string EditToReadableNumber(double number)
        {
            if (number < 10)
                number = Math.Round(number, 4);
            else
                number = Math.Round(number, 2);

            string[] numberParts = number.ToString().Split(",");
            string result = AddingCommas(numberParts[0]);

            try
            {                
                result += '.' + numberParts[1];                
            }
            catch(IndexOutOfRangeException)
            {
                result += ".00";
            }

            return result;
        }

        public static string EditToReadableNumber(ulong? number) => 
            number.HasValue ? EditToReadableNumber((ulong)number) : "∞";

        public static string EditToReadableNumber(ulong number) => 
            AddingCommas(number.ToString());        

        public static string EditToReadablePercent(double percent) => 
            Math.Round(percent, 2).ToString().Replace(',', '.');        

        private static string AddingCommas(string number)
        {
            if (number.Length >= 3)
            {
                string result = string.Empty;

                for (int i = number.Length - 1, k = 1; i >= 0; i--, k++)
                {
                    if (k % 3 == 0)
                        result += number[i] + ",";
                    else
                        result += number[i];
                }

                if (result.EndsWith(','))
                    result = result.Remove(result.Length - 1, 1);

                return new string(result.ToCharArray().Reverse().ToArray());
            }
            else return number;
        }
    }
}
