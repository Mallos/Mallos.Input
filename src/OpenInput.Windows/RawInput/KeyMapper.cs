namespace OpenInput.RawInput
{
    using System;
    using KeysConverter = System.Windows.Forms.KeysConverter;

    class KeyMapper
    {
        static Lazy<KeysConverter> keysConverter = new Lazy<KeysConverter>();
        
        public static string GetKeyName(int virtualKey)
        {
            return keysConverter.Value.ConvertToString(virtualKey);
        }

        public static Keys ToKey(int key)
        {
            // TODO: Fix some of the keys
            return (Keys)key;
        }
    }
}
