namespace Mallos.Input.Blazor
{
    public class BlazorDeviceSet
        : DeviceSet<BlazorKeyboard, BlazorMouse, Dummy.DummyGamePad>
    {
        public BlazorDeviceSet()
            : base("Blazor",
                  new BlazorKeyboard(),
                  new BlazorMouse(),
                  null)
        {
        }
    }
}
