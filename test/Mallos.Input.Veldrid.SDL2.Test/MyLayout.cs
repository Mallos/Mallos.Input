namespace Mallos.Input.Test
{
    using Mallos.Input.Mechanics;
    using Mallos.Input.Mechanics.Layout;

    public class MyLayout : Layout
    {
        public MyLayout(string layoutId)
            : base(layoutId)
        {
        }

        [LayoutItem("Move Forwards", group: "Movement")]
        [AxisTrigger("MoveForward", 1)]
        public InputKeys MoveForwards { get; set; }

        [LayoutItem("Move Backwards", group: "Movement")]
        [AxisTrigger("MoveForward", -1)]
        public InputKeys MoveBackwards { get; set; }

        [LayoutItem("Move Left", group: "Movement")]
        [AxisTrigger("MoveRight", -1)]
        public InputKeys MoveLeft { get; set; }

        [LayoutItem("Move Right", group: "Movement")]
        [AxisTrigger("MoveRight", 1)]
        public InputKeys MoveRight { get; set; }

        [LayoutItem("Jump", group: "Movement")]
        [ActionTrigger("Jump")]
        public InputKeys Jump { get; set; }

        [LayoutItem("Fire")]
        [ActionTrigger("Fire")]
        public InputKeys Fire { get; set; }

        [ActionTrigger("Pause", isReadOnly: true)]
        public InputKeys Pause { get; set; }

        public static readonly MyLayout DefaultLayout = new MyLayout("KeyboardLayout")
        {
            LayoutName      = "Default Keyboard Layout",
            LayoutDescription = "The default keyboard layout.",
            MoveForwards    = new InputKeys(Keys.W, Keys.Up),
            MoveBackwards   = new InputKeys(Keys.S, Keys.Down),
            MoveLeft        = new InputKeys(Keys.A, Keys.Left),
            MoveRight       = new InputKeys(Keys.D, Keys.Right),
            Jump            = new InputKeys(Keys.Space),
            Fire            = new InputKeys(MouseButtons.Left, Keys.F),
            Pause           = new InputKeys(Keys.Escape),
        };

        public static readonly MyLayout DefaultGamePadLayout = new MyLayout("GamePadLayout")
        {
            LayoutName      = "Default GamePad Layout",
            LayoutDescription = "The default GamePad layout.",
            MoveForwards    = new InputKeys(Buttons.DPadUp),
            MoveBackwards   = new InputKeys(Buttons.DPadDown),
            MoveLeft        = new InputKeys(Buttons.DPadLeft),
            MoveRight       = new InputKeys(Buttons.DPadRight),
            Jump            = new InputKeys(Buttons.A),
            Fire            = new InputKeys(Buttons.RightTrigger),
            Pause           = new InputKeys(Buttons.Start),
        };
    }
}
