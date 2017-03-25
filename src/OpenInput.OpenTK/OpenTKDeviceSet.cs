namespace OpenInput
{
    public class OpenTKDeviceSet : DeviceSet
    {
        public OpenTKDeviceSet(OpenTK.NativeWindow window)
            : base("OpenTK",
                  new OpenTKKeyboard(window.InputDriver.Keyboard.Count > 0 ? window.InputDriver.Keyboard[0] : null), // TODO: Cannot be null
                  new OpenTKMouse(window, window.InputDriver.Mouse.Count > 0 ? window.InputDriver.Mouse[0] : null),
                  (int index) => { return new OpenTKGamePad(index); })
        {

        }
    }
}
