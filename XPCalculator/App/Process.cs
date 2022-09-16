using System.Reflection;
using XPCalculator.Resources;

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
                    : ru_RU.ENTER_OPERATION) + @"(+, -, *, \): ");

                try
                {
                    MathLogic(number1, number2);
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

        private static void MathLogic(object? number1, object? number2)
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
                Console.WriteLine($"{number1} + {number2} = " +
                    $"{RomanNumber.Add(number1, number2, Operation.MINUS)}");
            }
            else if (key.Key == ConsoleKey.OemPlus)
            {
                Console.WriteLine($"{number1} + {number2} = " +
                    $"{RomanNumber.Add(number1, number2, Operation.PLUS)}");
            }
            else if (key.Modifiers.HasFlag(ConsoleModifiers.Shift) && key.Key == ConsoleKey.D8)
            {
                Console.WriteLine($"{number1} + {number2} = " +
                    $"{RomanNumber.Add(number1, number2, Operation.MULTIPLY)}");
            }
            else if (key.Key == ConsoleKey.Oem5)
            {
                Console.WriteLine($"{number1} + {number2} = " +
                   $"{RomanNumber.Add(number1, number2, Operation.DIVISION)}");
            }
            else
            {
                throw new ApplicationException(resources.Culture == "en-EN"
                    ? en_EN.OPERATION_EXCAPTION
                    : ru_RU.OPERATION_EXCAPTION);
            }
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
