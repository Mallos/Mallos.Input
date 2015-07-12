namespace OpenInput.Windows
{
    using Nine.Injection;
    using System.Linq;
    using System.Collections.Generic;
    using SharpDX.DirectInput;
    using DI_Mouse = SharpDX.DirectInput.Mouse;
    using DI_Keyboard = SharpDX.DirectInput.Keyboard;

    public class DeviceService : IDeviceService
    {
        private IContainer container;
        private DirectInput directInput;

        public DeviceService()
        {
            this.directInput = new DirectInput();
            this.container = new Container()
                .Map<IMouse>(new Mouse(new DI_Mouse(directInput)))
                .Map<IKeyboard>(new Keyboard(new DI_Keyboard(directInput)));
        }

        public IKeyboard GetKeyboard()
        {
            return this.container.Get<IKeyboard>();
        }

        public IMouse GetMouse()
        {
            return this.container.Get<IMouse>();
        }

        public IList<IDevice> GetDevices()
        {
            return this.container.GetAll<IDevice>().ToList();
        }
    }
}
