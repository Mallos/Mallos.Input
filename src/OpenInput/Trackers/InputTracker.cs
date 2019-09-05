namespace OpenInput.Trackers
{
    using System;
    using OpenInput.Mechanics;

    /// <summary>
    /// A tracker that takes any trackers and convert the events
    /// to a InputKey event, making all devices act the same.
    /// </summary>
    /// <note>
    /// In the future we might want to move this to the device tracker it-self.
    /// Or like a shared interface for trackers that support this.
    /// </note>
    public class InputTracker : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of <see cref="InputTracker"/>.
        /// </summary>
        public InputTracker(params ITracker[] trackers)
        {
            if (trackers == null || trackers.Length < 0)
            {
                throw new ArgumentNullException(nameof(trackers));
            }

            this.Trackers = trackers;

            this.BindTrackers();
        }

        /// <summary>
        /// Gets all the trackers used.
        /// </summary>
        public ITracker[] Trackers { get; }

        /// <summary>
        /// Occures when a new key or button is pressed.
        /// </summary>
        public event EventHandler<InputKey> OnKeyDown;


        /// <summary>
        /// Occures when a key or button is released.
        /// </summary>
        public event EventHandler<InputKey> OnKeyUp;

        public void Dispose()
        {
            this.UnbindTrackers();
        }

        private void BindTrackers()
        {
            foreach (ITracker tracker in this.Trackers)
            {
                if (tracker is IKeyboardTracker keyboardTracker)
                {
                    keyboardTracker.KeyDown += this.OnKeyboardKeyDown;
                    keyboardTracker.KeyUp += this.OnKeyboardKeyUp;
                }

                if (tracker is IMouseTracker mouseTracker)
                {
                    mouseTracker.MouseDown += this.OnMouseDown;
                    mouseTracker.MouseUp += this.OnMouseUp;
                }

                if (tracker is IGamePadTracker gamePadTracker)
                {
                    gamePadTracker.ButtonDown += this.OnGamePadButtonDown;
                    gamePadTracker.ButtonUp += this.OnGamePadButtonUp;
                }
            }
        }

        private void UnbindTrackers()
        {
            foreach (ITracker tracker in this.Trackers)
            {
                if (tracker is IKeyboardTracker keyboardTracker)
                {
                    keyboardTracker.KeyDown -= this.OnKeyboardKeyDown;
                    keyboardTracker.KeyUp -= this.OnKeyboardKeyUp;
                }

                if (tracker is IMouseTracker mouseTracker)
                {
                    mouseTracker.MouseDown -= this.OnMouseDown;
                    mouseTracker.MouseUp -= this.OnMouseUp;
                }

                if (tracker is IGamePadTracker gamePadTracker)
                {
                    gamePadTracker.ButtonDown -= this.OnGamePadButtonDown;
                    gamePadTracker.ButtonUp -= this.OnGamePadButtonUp;
                }
            }
        }

        private void OnKeyboardKeyDown(object sender, KeyEventArgs args)
        {
            this.OnKeyDown?.Invoke(sender, new InputKey(args.Key));
        }

        private void OnKeyboardKeyUp(object sender, KeyEventArgs args)
        {
            this.OnKeyUp?.Invoke(sender, new InputKey(args.Key));
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs args)
        {
            this.OnKeyDown?.Invoke(sender, new InputKey(args.Button));
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs args)
        {
            this.OnKeyUp?.Invoke(sender, new InputKey(args.Button));
        }

        private void OnGamePadButtonDown(object sender, GamePadEventArgs args)
        {
            // TODO: GamePad Support
            // this.OnKeyDown?.Invoke(sender, new InputKey(args.Button));
        }

        private void OnGamePadButtonUp(object sender, GamePadEventArgs args)
        {
            // TODO: GamePad Support
            // this.OnKeyUp?.Invoke(sender, new InputKey(args.Button));
        }
    }
}