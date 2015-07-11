namespace OpenInput
{
    using System;

    public partial class Keyboard : Device
    {
        public Keyboard(string name)
            : base(name)
        {

        }
        
        public KeyboardState GetCurrentState()
        {
#if !PCL
            return PlatformGetCurrentState();
#else
            throw new NotSupportedException("Calling on Portable Library!");
#endif
        }

        public static KeyboardState GetState()
        {
#if !PCL
            return InputManager.Current.Keyboard.GetCurrentState();
#else
            throw new NotSupportedException("Calling on Portable Library!");
#endif
        }
    }
}
