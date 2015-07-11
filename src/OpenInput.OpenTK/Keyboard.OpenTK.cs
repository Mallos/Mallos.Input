namespace OpenInput
{
    partial class Keyboard : Device
    {
        public KeyboardState PlatformGetCurrentState()
        {
            return new KeyboardState(keys);
        }
    }
}
