using System;

namespace OpenInput.Empty
{
    /// <summary>
    /// Empty Mouse, does nothing.
    /// </summary>
    public sealed class Mouse : IMouse
    {
        /// <inheritdoc />
        public string Name => "Empty Mouse";

        /// <inheritdoc />
        public event EventHandler<MouseButtonEventArgs> MouseDown;
        
        /// <inheritdoc />
        public event EventHandler<MouseButtonEventArgs> MouseUp;
        
        /// <inheritdoc />
        public event EventHandler<MouseWheelEventArgs> MouseWheel;

        /// <inheritdoc />
        public event EventHandler<MouseEventArgs> Move;

        /// <inheritdoc />
        public MouseState GetCurrentState()
        {
            return new MouseState();
        }

        /// <inheritdoc />
        public void GetPosition(out int x, out int y)
        {
            x = 0;
            y = 0;
        }

        /// <inheritdoc />
        public void SetPosition(int x, int y)
        {

        }
    }
}
