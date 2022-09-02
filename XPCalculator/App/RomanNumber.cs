namespace XPCalculator.App
{
    public class RomanNumber
    {
        public static int Parse(string str)
        {
            char[] digits = { 'I', 'V', 'X', 'L', 'C', 'D', 'M' };
            int[] digitsValue = { 1, 5, 10, 50, 100, 500, 1000 };
            int result = 0;

            for (int pos = str.Length - 1; pos >= 0; pos--)
            {
                char digit = str[pos];
                int ind = Array.IndexOf(digits, digit);

                if (ind == -1)
                {
                    throw new ArgumentException($"Invalid cahr {digit}");
                }

                int value = digitsValue[ind];

                if (pos == str.Length - 1)
                {
                    result += value;
                }
                else
                {
                    if (digitsValue[pos + 1] > value)
                    {
                        result -= value;
                    }
                    else
                    {
                        result += value;
                    }
                }
            }

            return result;
        }
    }
}
