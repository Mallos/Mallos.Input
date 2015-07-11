namespace OpenInput
{
    using System;

    partial class Mouse : Device
    {
        public SharpDX.DirectInput.Mouse PlatformMouse { get; internal set; }

        private void PlatformSetHandle(IntPtr handle)
        {
            this.PlatformMouse.Unacquire();
            this.PlatformMouse.SetCooperativeLevel(handle, (SharpDX.DirectInput.CooperativeLevel.NonExclusive | SharpDX.DirectInput.CooperativeLevel.Background));
            this.PlatformMouse.Acquire();
        }

        private MouseState PlatformGetCurrentState()
        {
            if (PlatformMouse.IsDisposed)
                return new MouseState();

            PlatformMouse.Poll();

            var state = PlatformMouse.GetCurrentState();
            return new MouseState(
                state.X, state.Y, state.Z,
                state.Buttons[0], state.Buttons[1], state.Buttons[2],
                state.Buttons[3], state.Buttons[4]);
        }
    }
}
