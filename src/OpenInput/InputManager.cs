namespace OpenInput
{
    using Nine.Injection;
    using System;

    public class InputManager
    {
        public static InputManager Current
        {
            get
            {
                if (current == null)
                    throw new ArgumentNullException("You have to create a instance of InputManager before.");
                return current;
            }
        }
        private static InputManager current;

        // This is just a prototype!
        public readonly IDeviceService Service;
        private IContainer container;

        public InputManager(IDeviceService service)
        {
            if (current == null)
            {
                current = this;
            }
            else
            {
                throw new ArgumentException("You have already initialized one InputManager.");
            }

            this.Service = service;
            this.container = new Container();
        }
    }
}
