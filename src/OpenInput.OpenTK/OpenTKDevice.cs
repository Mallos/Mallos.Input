namespace OpenInput
{
    using OpenTK.Input;

    public abstract class OpenTKDevice<TInputDevice>
        where TInputDevice : IInputDevice
    {
        public readonly TInputDevice Device;

        public bool HasDevice => Device != null;

        protected OpenTKDevice(TInputDevice device)
        {
            this.Device = device;
        }
    }
}
