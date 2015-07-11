namespace OpenInput
{
    using System;

    public partial class Mouse : Device
    {
        public Mouse(string name)
            : base(name)
        {

        }
        
        public void SetHandle(IntPtr handle)
        {
#if !PCL
            PlatformSetHandle(handle);
#endif
        }

        public MouseState GetCurrentState()
        {
#if !PCL
            return PlatformGetCurrentState();
#else
            throw new NotSupportedException("Calling on Portable Library!");
#endif
        }

        public static MouseState GetState()
        {
#if !PCL
            return InputManager.Current.Mouse.GetCurrentState();
#else
            throw new NotSupportedException("Calling on Portable Library!");
#endif
        }
    }
}
