namespace XPCalculator.App
{
    public record RomanNumber
    {
        public static Dictionary<char, int> mapToParse = new Dictionary<char, int>()
        {
            { 'N', 0 },
            { 'I', 1 },
            { 'V', 5 },
            { 'X', 10 },
            { 'L', 50 },
            { 'C', 100 },
            { 'D', 500 },
            { 'M', 1000 },
        };
        public static Dictionary<string, int> mapToString = new Dictionary<string, int>()
        {
            { "M", 1000 },
            { "CM", 900 },
            { "D", 500 },
            { "CD", 400 },
            { "C", 100 },
            { "XC", 90 },
            { "L", 50 },
            { "XL", 40 },
            { "X", 10 },
            { "IX", 9 },
            { "V", 5 },
            { "IV", 4 },
            { "I", 1 },
        };
        public int Number { get; set; }

        public RomanNumber() { }

        public RomanNumber(int number)
        {
            Number = number;
        }

        public static int Parse(string number)
        {
            if (number == null)
            {
                throw new ArgumentNullException(nameof(number));
            }
            else if (number.Length == 0)
            {
                throw new ArgumentException("Empty string not allowed");
            }
            else if (number.Contains('-') && (number.Where(c => c == '-').Count() > 1 || number[0] != '-'))
            {
                throw new ArgumentException("Minus coul be only one and at start");
            }

            foreach (char digitInStr in number)
            {
                if (number.Length == 1 && !mapToParse.ContainsKey(digitInStr))
                {
                    throw new ArgumentException("Only roman numbers");
                }
            }

            List<int> usedValues = new();
            int result = 0;

            for (int pos = number.Length - 1; pos >= 0; pos--)
            {
                char digit = number[pos];

                if (digit == '-') break;

                int ind = Array.IndexOf(mapToParse.Keys.ToArray(), digit);

                if (ind == -1)
                {
                    throw new ArgumentException($"Invalid char {digit}");
                }

                int value = mapToParse.Values.ToArray()[ind];

                if (pos == number.Length - 1)
                {
                    result += value;
                }
                else
                {
                    int lastUsedValue = usedValues.Last();

                    result += lastUsedValue > value ? -value : value;
                }

                usedValues.Add(value);
            }

            return !number.Contains('-') ? result : -result;
        }

        public override string ToString()
        {
            if (Number == 0)
            {
                return "N";
            }

            int numberWithDigit = Number < 0 ? Number * -1 : Number;
            string res = "";

            int counter = 0;
            while (numberWithDigit > 0)
            {
                while (numberWithDigit < mapToString.Values.ToArray()[counter])
                {
                    counter++;
                }

                numberWithDigit -= mapToString.Values.ToArray()[counter];
                res += mapToString.Keys.ToArray()[counter];
            }

            return Number < 0 ? res.Insert(0, "-") : res;
        }

        private RomanNumber Sum(RomanNumber number1, RomanNumber number2)
        {
            return new RomanNumber(number1.Number + number2.Number); ;
        }

        public RomanNumber Add(RomanNumber number)
        {
            if (number == null)
            {
                throw new ArgumentNullException(nameof(number));
            }

            return new RomanNumber(Sum(this, number));
        }

        public RomanNumber Add(int number)
        {
            return new RomanNumber(Sum(this, new RomanNumber(number)));
        }

        public RomanNumber Add(string number)
        {
            if (number == null)
            {
                throw new ArgumentNullException(nameof(number));
            }
            else if (number.Length == 0)
            {
                throw new ArgumentException("Empty string");
            }

            try
            {
                return new RomanNumber(Sum(this, new RomanNumber(Parse(number))));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static RomanNumber Add(int number1, int number2)
        {
            return new RomanNumber().Sum(new RomanNumber(number1), new RomanNumber(number2));
        }

        public static RomanNumber Add(RomanNumber number1, int number2)
        {
            if (number1 == null)
            {
                throw new ArgumentNullException(nameof(number1));
            }

            return new RomanNumber().Sum(new RomanNumber(number1.Number), new RomanNumber(number2));
        }

        public static RomanNumber Add(string number1, string number2)
        {
            if (number1 == null || number2 == null)
            {
                throw new ArgumentNullException(number1 is null ? nameof(number1) : nameof(number2));
            }
            else if (number1.Length == 0 || number2.Length == 0)
            {
                throw new ArgumentException("Empty string");
            }

            return new RomanNumber().Sum(new RomanNumber(Parse(number1)), new RomanNumber(Parse(number2)));
        }

        public static RomanNumber Add(RomanNumber number1, string number2)
        {
            if (number1 == null || number2 == null)
            {
                throw new ArgumentNullException(number1 is null ? nameof(number1) : nameof(number2));
            }
            else if (number2.Length == 0)
            {
                throw new ArgumentException("Empty string");
            }

            return new RomanNumber().Sum(number1, new RomanNumber(Parse(number2)));
        }

        public static RomanNumber Add(RomanNumber number1, RomanNumber number2)
        {
            if (number1 == null || number2 == null)
            {
                throw new ArgumentNullException(number1 is null ? nameof(number1) : nameof(number2));
            }

            return new RomanNumber().Sum(number1, number2);
        }

        public static RomanNumber Add(object rn1, object rn2)
        {
            /*  Рефакторинг - разделение условий (условия внутри условия)
            if (rn1 is null || rn2 is null)
            {
                throw new ArgumentNullException(
                    rn1 is null ? nameof(rn1) : nameof(rn2)) ;
            }*/
            if (rn1 is null) throw new ArgumentNullException(nameof(rn1));
            if (rn2 is null) throw new ArgumentNullException(nameof(rn2));



            /* Рефакторинг - соединение (перераспределение) условий
             if (rn1 is int && rn2 is int) return new RomanNumber((int)rn1).Add((int)rn2);
             else if (rn1 is String && rn2 is String) return new RomanNumber(RomanNumber.Parse((String)rn1)).Add((String)rn2);
             else if (rn1 is int && rn2 is String) return new RomanNumber((int)rn1).Add((String)rn2);
             else if (rn1 is String && rn2 is int) return new RomanNumber((int)rn2).Add((String)rn1);

              (rn1 is int && rn2 is int) + (rn1 is int && rn2 is String) -->
             (rn1 is int)(  rn2 is int  + rn2 is String )
              */
            if (rn1 is int v1)
            {
                /* Рефакторинг - если код присутствует во всех блоках, его нужно вынести
                if(rn2 is int v2) return new RomanNumber(v1).Add(v2);
                if(rn2 is String s2) return new RomanNumber(v1).Add(s2);
                */
                var rn = new RomanNumber(v1);
                if (rn2 is int v2) return rn.Add(v2);
                if (rn2 is String s2) return rn.Add(s2);
            }
            else if (rn1 is string v2)
            {
                var rn = new RomanNumber(Parse(v2));
                if (rn2 is int v3) return rn.Add(v3);
                if (rn2 is string s3) return rn.Add(s3);
            }

            return new RomanNumber();
        }
    }
}
