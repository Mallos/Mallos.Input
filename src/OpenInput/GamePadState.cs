namespace OpenInput
{
    using System.Numerics;

    /// <summary>
    /// Represents specific state of a controller, including the current state of buttons and sticks.
    /// </summary>
    public struct GamePadState
    {
        public static readonly GamePadState Empty
            = new GamePadState(
                false,
                new GamePadThumbSticks(),
                new GamePadTriggers(),
                new GamePadButtons(),
                new GamePadDPad());

        /// <summary>
        /// Initializes a new instance of the <see cref="GamePadState"/> struct.
        /// </summary>
        public GamePadState(
            bool isConnected,
            GamePadThumbSticks thumbSticks,
            GamePadTriggers triggers,
            GamePadButtons buttons,
            GamePadDPad dPad)
        {
            this.IsConnected = isConnected;
            this.ThumbSticks = thumbSticks;
            this.Triggers = triggers;
            this.Buttons = buttons;
            this.DPad = dPad;
        }

        /// <summary>
        /// Indicates whether the controller is connected.
        /// </summary>
        public bool IsConnected { get; internal set; }

        /// <summary>
        /// Returns a structure that indicates the position of the thumbsticks.
        /// </summary>
        public GamePadThumbSticks ThumbSticks { get; internal set; }

        /// <summary>
        /// Returns a structure that identifies the position of triggers are.
        /// </summary>
        public GamePadTriggers Triggers { get; internal set; }

        /// <summary>
        /// Returns a structure that identifies what buttons are pressed.
        /// </summary>
        public GamePadButtons Buttons { get; internal set; }

        /// <summary>
        /// Returns a structure that identifies what directions of the
        /// directional pad are pressed.
        /// </summary>
        public GamePadDPad DPad { get; internal set; }
    }

    /// <summary>
    /// Structure that represents the position of left and right sticks
    /// (thumbsticks) on an controller.
    /// </summary>
    public struct GamePadThumbSticks
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GamePadThumbSticks"/> struct.
        /// </summary>
        public GamePadThumbSticks(
            float leftThumbstickX, float leftThumbstickY,
            float rightThumbstickX, float rightThumbstickY)
        {
            this.LeftThumbstick = new Vector2(leftThumbstickX, leftThumbstickY);
            this.RightThumbstick = new Vector2(rightThumbstickX, rightThumbstickY);
        }

        /// <summary>
        /// Gets the position of the left controller stick.
        /// </summary>
        public Vector2 LeftThumbstick { get; internal set; }
        
        /// <summary>
        /// Gets the position of the right controller stick.
        /// </summary>
        public Vector2 RightThumbstick { get; internal set; }
    }

    /// <summary>
    /// Structure that defines the position of the left and right triggers on an controller.
    /// </summary>
    public struct GamePadTriggers
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GamePadTriggers"/> struct.
        /// </summary>
        public GamePadTriggers(float leftTrigger, float rightTrigger)
        {
            this.Left = leftTrigger;
            this.Right = rightTrigger;
        }

        /// <summary>
        /// Identifies the position of the left trigger on the controller.
        /// </summary>
        public float Left { get; internal set; }

        /// <summary>
        /// Identifies the position of the right trigger on the controller.
        /// </summary>
        public float Right { get; internal set; }
    }

    /// <summary>
    /// Structure that defines the what buttons of the controller are being pressed.
    /// </summary>
    public struct GamePadButtons
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GamePadButtons"/> struct.
        /// </summary>
        public GamePadButtons(Buttons buttons)
        {
            this.Buttons = buttons;
        }

        /// <summary>
        /// Gets all the pressed buttons as a flagged enum.
        /// </summary>
        public Buttons Buttons { get; }

        /// <summary>
        /// Returns whether a specified button is currently being pressed.
        /// </summary>
        public bool IsButtonDown(Buttons button) => (this.Buttons & button) == 0;
    }

    /// <summary>
    /// Structure that defines the directions on the directional pad of
    /// a controller are being pressed.
    /// </summary>
    public struct GamePadDPad
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GamePadDPad"/> struct.
        /// </summary>
        public GamePadDPad(
            bool upValue,
            bool downValue,
            bool leftValue,
            bool rightValue)
        {
            this.Up = upValue;
            this.Down = downValue;
            this.Left = leftValue;
            this.Right = rightValue;
        }

        /// <summary>
        /// Identifies whether the Down direction on the controller
        /// directional pad is pressed.
        /// </summary>
        public bool Down { get; internal set; }

        /// <summary>
        /// Identifies whether the Left direction on the controller
        /// directional pad is pressed.
        /// </summary>
        public bool Left { get; internal set; }

        /// <summary>
        /// Identifies whether the Right direction on the controller
        /// directional pad is pressed.
        /// </summary>
        public bool Right { get; internal set; }

        /// <summary>
        /// Identifies whether the Up direction on the controller
        /// directional pad is pressed.
        /// </summary>
        public bool Up { get; internal set; }
    }
}
