namespace OpenInput
{
    using System;

    /// <summary>
    /// Helper class for <see cref="Keys"/>.
    /// </summary>
    public static class InputHelper
    {
        /// <summary>
        /// Returns if a specific key is a letter.
        /// </summary>
        public static bool IsLetter(this Keys key)
        {
            byte b = (byte)key;
            return b >= 65 && b <= 90;
        }

        /// <summary>
        /// Returns if a specific key is a number.
        /// </summary>
        public static bool IsNumber(this Keys key)
        {
            byte b = (byte)key;
            return b >= 48 && b <= 57;
        }

        /// <summary>
        /// Returns the name of the key, same as enum name.
        /// </summary>
        public static string ToText(this Keys key) => Enum.GetName(typeof(Keys), key);
    }
}
