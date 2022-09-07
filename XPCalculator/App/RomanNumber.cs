namespace XPCalculator.App
{
    public class RomanNumber
    {
        public static int Parse(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException();
            }

            if (str.Length == 0)
            {
                throw new ArgumentException("Empty string not allowed");
            }

            char[] digits = { 'N', 'I', 'V', 'X', 'L', 'C', 'D', 'M' };
            int[] digitsValue = { 0, 1, 5, 10, 50, 100, 500, 1000 };
            List<int> usedValues = new();
            int result = 0;

            for (int pos = str.Length - 1; pos >= 0; pos--)
            {
                char digit = str[pos];
                int ind = Array.IndexOf(digits, digit);

                if (ind == -1)
                {
                    throw new ArgumentException($"Invalid char {digit}");
                }

                int value = digitsValue[ind];

                if (pos == str.Length - 1)
                {
                    result += value;
                }
                else
                {
                    int a = usedValues.Last();

                    result += a > value ? -value : value;
                }

                usedValues.Add(value);
            }

            return result;
        }
    }
}
