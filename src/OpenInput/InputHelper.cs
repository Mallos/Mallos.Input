namespace OpenInput
{
    using System;

    public static class InputHelper
    {
        public static bool IsLetter(this Keys key)
        {
            var b = ((byte)key);
            return b >= 65 && b <= 90;
        }

        public static bool IsNumber(this Keys key)
        {
            // TODO: NumPad etc
            var b = ((byte)key);
            return b >= 48 && b <= 57;
        }

        public static string ToText(this Keys key)
        {
            return Enum.GetName(typeof(Keys), key);
        }
    }
}