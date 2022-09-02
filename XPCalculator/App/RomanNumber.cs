namespace XPCalculator.App
{
    public class RomanNumber
    {
        public static int Parse(string str)
        {
            char[] digits = { 'I', 'V', 'X', 'L', 'C', 'D', 'M' };
            int[] digitsValue = { 1, 5, 10, 50, 100, 500, 1000 };
            List<int> usedValues = new List<int>();
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
                    int a = usedValues.Last();

                    if (a > value)
                    {
                        result -= value;
                    }
                    else
                    {
                        result += value;
                    }
                }

                usedValues.Add(value);
            }

            return result;
        }
    }
}
