namespace OpenInput
{
    public class OpenTKDeviceSet : DeviceSet
    {
        public OpenTKDeviceSet(OpenTK.GameWindow gameWindow)
            : base("OpenTK",
                  new OpenTKKeyboard(gameWindow.Keyboard),
                  new OpenTKMouse(gameWindow.Mouse),
                  (int index) => { return new OpenTKGamePad(index); })
        {

        }
    }
}
