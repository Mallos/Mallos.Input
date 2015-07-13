namespace OpenInput.RawInput
{
    using System;

    class RawInputEventArg : EventArgs
    {
        public KeyPressEvent KeyPressEvent { get; private set; }

        public RawInputEventArg(KeyPressEvent arg)
        {
            this.KeyPressEvent = arg;
        }
    }
}
