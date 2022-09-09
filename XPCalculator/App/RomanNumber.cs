﻿namespace XPCalculator.App
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

            if (str.Contains('-') && (str.Where(c => c == '-').Count() > 1 || str[0] != '-'))
            {
                throw new ArgumentException("Minus coul be only one and at start");
            }

            if (str.Length == 0)
            {
                throw new ArgumentException("Empty string not allowed");
            }

            char[] digits = { 'N', 'I', 'V', 'X', 'L', 'C', 'D', 'M' };

            foreach (char digitInStr in str)
            {
                if (str.Length == 1 && !digits.Contains(digitInStr))
                {
                    throw new ArgumentException("Only roman numbers");
                }
            }

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

            return Number < 0 ? res.Insert(0, "-") : res;
        }

        public RomanNumber Add(RomanNumber number)
        {
            if (number is null)
            {
                throw new ArgumentNullException();
            }

            int sum = Number + number.Number;
            return new RomanNumber(sum);
        }

        public RomanNumber Add(int number)
        {
            if (number == 0)
            {
                throw new ArgumentNullException();
            }

            int sum = Number + number;
            return new RomanNumber(sum);
        }

        public RomanNumber Add(string number)
        {
            if (number == null || number.Length == 0)
            {
                throw new ArgumentNullException();
            }

            int sum;

            try
            {
                sum = Number + Parse(number);
            }
            catch (Exception)
            {
                throw;
            }

            return new RomanNumber(sum);
        }

        public static RomanNumber Add(int a, int b)
        {
            if (a == 0 || b == 0)
            {
                throw new ArgumentNullException();
            }

            return new RomanNumber(a + b);
        }

        public static RomanNumber Add(RomanNumber a, int b)
        {
            if (a == null || a.Number == 0 || b == 0)
            {
                throw new ArgumentNullException();
            }

            return new RomanNumber(a.Number + b);
        }

        public static RomanNumber Add(string a, string b)
        {
            if (a == null || a.Length == 0 || b == null || b.Length == 0)
            {
                throw new ArgumentNullException();
            }

            return new RomanNumber(Parse(a) + Parse(b));
        }

        public static RomanNumber Add(RomanNumber a, string b)
        {
            if (b == null || b.Length == 0 || a == null || a.Number == 0)
            {
                throw new ArgumentNullException();
            }

            return new RomanNumber(Parse(b) + a.Number);
        }

        public static RomanNumber Add(RomanNumber a, RomanNumber b)
        {
            if (a == null || b == null || a.Number == 0 || b.Number == 0)
            {
                throw new ArgumentNullException();
            }

            return new RomanNumber(a.Number + b.Number);
        }
    }
}
