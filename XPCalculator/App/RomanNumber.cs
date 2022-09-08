namespace XPCalculator.App
{
    public record RomanNumber
    {
        public int Number { get; set; }

        public RomanNumber() { }

        public RomanNumber(int number)
        {
            Number = number;
        }

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

                if (digit == '-') break;

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

            return !str.Contains('-') ? result : -result;
        }

        public override string ToString()
        {
            if (Number == 0)
            {
                return "N";
            }

            int n = Number < 0 ? Number * -1 : Number;
            string res = "";
            String[] parts = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            int[] values = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };

            int counter = 0;
            while (n > 0)
            {
                while (n < values[counter])
                {
                    counter++;
                }

                n -= values[counter];
                res += parts[counter];
            }

            return Number < 0 ? res.Insert(0, "-"): res;
        }
    }
}
