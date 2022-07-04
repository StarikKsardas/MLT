using System.Text.RegularExpressions;

namespace MLT.Desktop.AppUsers.Helpers
{
    public static class TextboxInput
    {
        private static Regex digitalRegex = new Regex("^[0-9+]+$");
        private static Regex PhoneRegex = new Regex("^[0-9+]+$");

        public static bool IsDigits(string text)
        {
            return digitalRegex.IsMatch(text);
        }
        public static bool IsNormalLenght(string text)
        {
            if (text.Length > 10)
            {
                return false;
            }
            return true;
        }
    }
}
