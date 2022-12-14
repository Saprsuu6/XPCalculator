using System;
using System.Reflection;
using System.Text;
using XPCalculator.Resources;

namespace XPCalculator.App
{
    public class Process
    {
        private static Dictionary<string, IEnumerable<PropertyInfo>> keyValuePairs = null!;
        private static Resources resources = new Resources();

        public static void SetInfosAboutResources()
        {
            Type[] types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => String.Equals(t.Namespace, "XPCalculator.Resources", StringComparison.Ordinal))
                .ToArray();

            Dictionary<string, IEnumerable<PropertyInfo>> keyValuePairs
                = new Dictionary<string, IEnumerable<PropertyInfo>>();

            foreach (Type type in types)
            {
                TypeInfo typeInfo = type.GetTypeInfo();
                IEnumerable<PropertyInfo> propertyInfos
                    = typeInfo.DeclaredProperties;

                keyValuePairs.Add(typeInfo.Name, propertyInfos);
            }

            Process.keyValuePairs = keyValuePairs;
        }

        private static string? GetStrignFromPair(int index, string propertyName)
        {
            object? obj = null!;
            return keyValuePairs.ElementAt(index).Value
                .First(property => property.Name == propertyName)
                .GetValue(obj) as string;
        }

        private static string? GetStrignFromPairByCulture(string propertyName)
        {
            KeyValuePair<string, IEnumerable<PropertyInfo>> pair =
                keyValuePairs.First(pair => pair.Key == resources.Culture.Replace('-', '_'));

            object? obj = null!;
            return pair.Value.First(property => property.Name == propertyName).GetValue(obj) as string;
        }

        public static void MainProcess()
        {
            while (true)
            {
                Console.Write(GetStrignFromPairByCulture("ENTER_NUMBER") + ": ");
                object? number1 = Console.ReadLine();

                Console.Write(GetStrignFromPairByCulture("ENTER_NUMBER") + ": ");
                object? number2 = Console.ReadLine();

                Console.Write(GetStrignFromPairByCulture("ENTER_OPERATION") + @"(+, -, *, \): ");

                try
                {
                    MathLogic(number1, number2);
                }
                catch (ArgumentNullException)
                {
                    Console.Write(GetStrignFromPairByCulture("SYSTEM_EXCETION"));
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
                throw new ApplicationException(GetStrignFromPairByCulture("UNSUPPORTED_TYPE"));
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
                throw new ApplicationException(GetStrignFromPairByCulture("OPERATION_EXCAPTION"));
            }
        }

        public static void ChooseLanguage()
        {
            StringBuilder msg = new StringBuilder();
            string? str;

            while (true)
            {
                for (int i = 0; i < keyValuePairs.Count; i++)
                {
                    str = GetStrignFromPair(i, "CHOOSE_LANGUAGE");
                    msg.Append(i != keyValuePairs.Count - 1 ? str + ", " : str);
                }

                Console.WriteLine(msg);
                msg.Clear();

                for (int i = 0; i < keyValuePairs.Count; i++)
                {
                    str = GetStrignFromPair(i, "LANGUAGE");
                    msg.Append($"{i + 1}. " + (i != keyValuePairs.Count - 1 ? str : str) + "\n");
                }

                Console.WriteLine(msg);
                msg.Clear();

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
            StringBuilder msg = new StringBuilder();
            string? str;

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
                    for (int i = 0; i < keyValuePairs.Count; i++)
                    {
                        str = GetStrignFromPair(i, "LIST_EXCEPTION");
                        msg.Append(i != keyValuePairs.Count - 1 ? str + ", " : str);
                    }

                    throw new ApplicationException(msg.ToString());
                }
            }
        }
    }
}
