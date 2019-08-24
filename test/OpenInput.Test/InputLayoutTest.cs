namespace OpenInput.Test
{
    using OpenInput.Mechanics;
    using OpenInput.Mechanics.Layout;

    public class KeyboardLayoutTest : Layout
    {
        public override string LayoutId => "KeyboardLayoutTest";

        public override string LayoutName => "Keyboard Layout Test";

        public override string LayoutDescription => "My Layout Description";

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

        public static readonly KeyboardLayoutTest DefaultLayout = new KeyboardLayoutTest()
        {
            MoveForwards = new InputKeys(Keys.W),
            MoveBackwards = new InputKeys(Keys.S),
            MoveLeft = new InputKeys(Keys.A),
            MoveRight = new InputKeys(Keys.D),
            Jump = new InputKeys(Keys.Space)
        };
    }

    public class InputLayoutTest
    {
        public void ConfigureInputSystem()
        {
            throw new System.NotImplementedException();
        }
    }
}
