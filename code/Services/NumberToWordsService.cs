using System;
using System.Globalization;
using System.Text;

namespace NumberToWords.Web.Controllers
{
    public class NumberToWordsService : INumberToWordsService
    {
        private readonly string[] ones = { "", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE" };
        private readonly string[] teens = { "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
        private readonly string[] tens = { "", "", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };
        private readonly string[] thousands = { "", "THOUSAND", "MILLION", "BILLION" };

        public string ConvertToWords(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value cannot be null or empty");

            // Parse the decimal value
            if (!decimal.TryParse(value.Trim(), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, 
                CultureInfo.InvariantCulture, out decimal number))
            {
                throw new ArgumentException("Invalid number format");
            }

            // Check bounds
            if (number < -999999999.99m || number > 999999999.99m)
                throw new ArgumentException("Number must be between -999,999,999.99 and 999,999,999.99");

            // Handle zero
            if (number == 0)
                return "ZERO DOLLARS";

            // Handle negative numbers
            bool isNegative = number < 0;
            number = Math.Abs(number);

            // Split into dollars and cents
            long dollars = (long)Math.Floor(number);
            int cents = (int)Math.Round((number - dollars) * 100);

            StringBuilder result = new StringBuilder();

            // Add negative prefix if needed
            if (isNegative)
                result.Append("NEGATIVE ");

            // Convert dollars to words
            if (dollars == 0)
            {
                result.Append("ZERO");
            }
            else
            {
                result.Append(ConvertWholeNumber(dollars));
            }

            // Add "DOLLAR" or "DOLLARS"
            result.Append(dollars == 1 ? " DOLLAR" : " DOLLARS");

            // Add cents if present
            if (cents > 0)
            {
                result.Append(" AND ");
                if (cents < 10)
                {
                    result.Append(ones[cents]);
                }
                else if (cents < 20)
                {
                    result.Append(teens[cents - 10]);
                }
                else
                {
                    result.Append(tens[cents / 10]);
                    if (cents % 10 > 0)
                    {
                        result.Append("-").Append(ones[cents % 10]);
                    }
                }
                result.Append(cents == 1 ? " CENT" : " CENTS");
            }

            return result.ToString();
        }

        private string ConvertWholeNumber(long number)
        {
            if (number == 0)
                return "";

            int groupIndex = 0;
            string words = "";

            while (number > 0)
            {
                if (number % 1000 != 0)
                {
                    string groupWords = ConvertHundreds((int)(number % 1000));
                    if (groupIndex > 0)
                    {
                        groupWords += " " + thousands[groupIndex];
                    }
                    
                    if (words != "")
                    {
                        words = groupWords + " " + words;
                    }
                    else
                    {
                        words = groupWords;
                    }
                }
                
                number /= 1000;
                groupIndex++;
            }

            return words.Trim();
        }

        private string ConvertHundreds(int number)
        {
            string result = "";

            // Handle hundreds
            if (number >= 100)
            {
                result = ones[number / 100] + " HUNDRED";
                number %= 100;
                if (number > 0)
                {
                    result += " AND ";
                }
            }

            // Handle tens and ones
            if (number >= 20)
            {
                result += tens[number / 10];
                if (number % 10 > 0)
                {
                    result += "-" + ones[number % 10];
                }
            }
            else if (number >= 10)
            {
                result += teens[number - 10];
            }
            else if (number > 0)
            {
                result += ones[number];
            }

            return result;
        }
    }
}