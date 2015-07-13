namespace OpenInput.RawInput
{
    using System;
    using System.Windows.Forms;

    class KeyMapper
    {
        static Lazy<KeysConverter> keysConverter = new Lazy<KeysConverter>();
        
        public static string GetKeyName(int virtualKey)
        {
            return keysConverter.Value.ConvertToString(virtualKey);
        }
    }
}
