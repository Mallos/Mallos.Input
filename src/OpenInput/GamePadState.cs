namespace OpenInput
{
    public struct GamePadState
    {
        public GamePadThumbSticks ThumbSticks { get; internal set; }
        public GamePadTriggers Triggers { get; internal set; }
        public GamePadButtons Buttons { get; internal set; }
        public GamePadDPad DPad { get; internal set; }

        public GamePadState(
            GamePadThumbSticks thumbSticks,
            GamePadTriggers triggers,
            GamePadButtons buttons,
            GamePadDPad dPad)
        {
            this.ThumbSticks = thumbSticks;
            this.Triggers = triggers;
            this.Buttons = buttons;
            this.DPad = dPad;
        }
    }

    public struct GamePadThumbSticks
    {
        public float LeftThumbstickX { get; internal set; }
        public float LeftThumbstickY { get; internal set; }

        public float RightThumbstickX { get; internal set; }
        public float RightThumbstickY { get; internal set; }

        public GamePadThumbSticks(
            float leftThumbstickX, float leftThumbstickY,
            float rightThumbstickX, float rightThumbstickY)
        {
            this.LeftThumbstickX = leftThumbstickX;
            this.LeftThumbstickY = leftThumbstickY;

            this.RightThumbstickX = rightThumbstickX;
            this.RightThumbstickY = rightThumbstickY;
        }
    }

    public struct GamePadTriggers
    {

    }

    public struct GamePadButtons
    {
        public Buttons Buttons { get; internal set; }

        public GamePadButtons(Buttons buttons)
        {
            this.Buttons = buttons;
        }
    }

    public struct GamePadDPad
    {

    }
}
