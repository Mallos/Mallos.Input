namespace OpenInput
{
    using SharpDX.DirectInput;
    using System.Linq;

    partial class InputManager
    {
        private DirectInput directInput;

        public InputManager()
        {
            SetSingleton();

            directInput = new DirectInput();

            PlatformUpdate();

            //var watcher = new ManagementEventWatcher();
            //var query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2");
            //watcher.EventArrived += (s, e) =>
            //{
            //    System.Diagnostics.Debug.WriteLine("Win_DeviceChanged");
            //    PlatformUpdate();
            //};
            //watcher.Query = query;
            //watcher.Start();
        }
        
        public void PlatformUpdate()
        {
            var mouses = directInput.GetDevices(DeviceType.Mouse, DeviceEnumerationFlags.AllDevices);
            if (mouses.Count > 0 && this.currentMouse == null)
            {
                var mouse = mouses.First();
                var newMouse = new Mouse(mouse.ProductName.Trim('\0'));
                newMouse.PlatformDevice = mouse;

                newMouse.PlatformMouse = new SharpDX.DirectInput.Mouse(directInput);
                newMouse.PlatformMouse.Acquire();

                if (this.DeviceConnected != null)
                    this.DeviceConnected(newMouse);

                this.currentMouse = newMouse;
            }

            var keyboards = directInput.GetDevices(DeviceType.Keyboard, DeviceEnumerationFlags.AllDevices);
            if (keyboards.Count > 0 && this.currentKeyboard == null)
            {
                var keyboard = keyboards.First();
                var newKeyboard = new Keyboard(keyboard.ProductName.Trim('\0'));
                newKeyboard.PlatformDevice = keyboard;

                newKeyboard.PlatformKeyboard = new SharpDX.DirectInput.Keyboard(directInput);
                newKeyboard.PlatformKeyboard.Acquire();

                if (this.DeviceConnected != null)
                    this.DeviceConnected(newKeyboard);

                this.currentKeyboard = newKeyboard;
            }
        }
    }
}
