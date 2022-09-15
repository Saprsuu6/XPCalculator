namespace XPCalculator.App
{
    public class Process
    {
        private static Resources resources = new Resources();

        public static void MainProcess()
        {
            while (true)
            {
                Console.Write((resources.Culture == "en-EN"
                    ? en_EN.ENTER_NUMBER
                    : ru_RU.ENTER_NUMBER) + ": ");

                object? number1 = Console.ReadLine();

                Console.Write((resources.Culture == "en-EN"
                    ? en_EN.ENTER_NUMBER
                    : ru_RU.ENTER_NUMBER) + ": ");

                object? number2 = Console.ReadLine();

                Console.WriteLine((resources.Culture == "en-EN"
                    ? en_EN.ENTER_OPERATION
                    : ru_RU.ENTER_OPERATION) + "(+, -): ");

                try
                {
                    var romanNumber = MathLogic(number1, number2);
                    Console.WriteLine($"{number1} + {number2} = {romanNumber}");
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("System exception. Program is terminated");
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Thread.Sleep(2000);
                Console.Clear();
            }
        }

        private static RomanNumber MathLogic(object? number1, object? number2)
        {
            if (number2 is null)
            {
                throw new ApplicationException(resources.Culture == "en-EN"
                    ? en_EN.UNSUPPORTED_TYPE
                    : ru_RU.UNSUPPORTED_TYPE);
            }

            ConsoleKeyInfo key;
            key = Console.ReadKey();

            if (key.Key == ConsoleKey.OemMinus)
            {
                if (number2 is int number2int)
                {
                    number2int *= -1;
                    return RomanNumber.Add(number1, number2int);
                }
                else if (number2 is string number2string)
                {
                    int parsedString = RomanNumber.Parse(number2string);
                    parsedString *= -1;

                    return RomanNumber.Add(number1, parsedString);
                }
            }
            else if (key.Key == ConsoleKey.OemPlus)
            {
                return RomanNumber.Add(number1, number2);
            }
            else
            {
                throw new ApplicationException(resources.Culture == "en-EN"
                    ? en_EN.OPERATION_EXCAPTION
                    : ru_RU.OPERATION_EXCAPTION);
            }

            return null!;
        }

        public static void ChooseLanguage()
        {
            while (true)
            {
                Console.WriteLine(en_EN.CHOOSE_LANGUAGE + $"({ru_RU.CHOOSE_LANGUAGE}):");
                Console.WriteLine($"1. {en_EN.LANGUAGE_ENG}({ru_RU.LANGUAGE_ENG})" +
                    $"\n2. {en_EN.LANGUAGE_RUS}({ru_RU.LANGUAGE_RUS})");

                try
                {
                    SetLanguage();
                    Console.Clear();
                    break;
                }
                catch (ApplicationException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Thread.Sleep(1000);
                Console.Clear();
            }
        }

        private static void SetLanguage()
        {
            ConsoleKeyInfo key;
            key = Console.ReadKey();

            while (true)
            {
                if (key.Key == ConsoleKey.D2)
                {
                    resources.Culture = "ru-RU";
                    break;
                }
                else if (key.Key == ConsoleKey.D1)
                {
                    break;
                }
                else
                {
                    throw new ApplicationException(en_EN.LIST_EXCEPTION + $"({ru_RU.LIST_EXCEPTION})");
                }
            }
        }
    }
}
