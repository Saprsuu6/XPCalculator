using XPCalculator.Resources;

namespace XPCalculator.App
{
    public class Resources
    {
        public string Culture { get; set; } = "en-EN";

        private string DefineCulture(string message_ru, string message_en, string? culture = null)
        {
            if (culture is null)
            {
                culture = Culture;
            }

            return culture switch
            {
                "ru-RU" => message_ru,
                "en-EN" => message_en,
                _ => throw new Exception("Unsupported culture")
            };
        }

        public string GetEmptyStringException(string? culture = null)
        {
            try
            {
                return DefineCulture(ru_RU.EMPTY_STRING, en_EN.EMPTY_STRING, culture);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GetInvalidCharException(char digit, string? culture = null)
        {
            try
            {
                return DefineCulture(ru_RU.INVALID_CHAR + $" {digit}", en_EN.INVALID_CHAR + $" {digit}", culture);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GetMinusException(string? culture = null)
        {
            try
            {
                return DefineCulture(ru_RU.MINUS_EXCEPTION, en_EN.MINUS_EXCEPTION, culture);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GetOnlyRomanException(string? culture = null)
        {
            try
            {
                return DefineCulture(ru_RU.ROMAN_NUMBERS, en_EN.ROMAN_NUMBERS, culture);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GetObjException(string? culture = null)
        {
            try
            {
                return DefineCulture(ru_RU.OBJECT_EXCEPTION, en_EN.OBJECT_EXCEPTION, culture);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GetUnsupportedTypeException(string type, string? culture = null)
        {
            try
            {
                return DefineCulture(ru_RU.OBJECT_EXCEPTION + $": " + ru_RU.UNSUPPORTED_TYPE,
                    en_EN.OBJECT_EXCEPTION + ": " + en_EN.UNSUPPORTED_TYPE, culture);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
