namespace Mallos.Input.Blazor
{
    using Mallos.Input.Blazor.Components;

    public class BlazorDeviceSet
        : DeviceSet<
            BlazorKeyboard,
            BlazorMouse,
            Dummy.DummyGamePad,
            BlazorTouchDevice
        >
    {
        public BlazorDeviceSet(MInputWrapperComponent component)
            : base("Blazor",
                  new BlazorKeyboard(component),
                  new BlazorMouse(component),
                  null,
                  new BlazorTouchDevice(component))
        {
        }
    }
}
