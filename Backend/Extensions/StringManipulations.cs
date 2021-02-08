using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Extensions
{
    public static class StringManipulations
    {
        public static double ConvertPercentageToDouble(this string stringToConvert)
        {
            double number;
            if (!double.TryParse(stringToConvert.Remove(stringToConvert.Length - 1), out number))
            {
                number = -1;
            }
            return number;
        }

        public static double ConvertNumberToDouble(this string stringToConvert)
        {
            double number;
            if (!double.TryParse(stringToConvert, out number))
            {
                number = -1;
            }
            return number;
        }
    }
}
