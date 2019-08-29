namespace OpenInput.Test
{
    using System.Linq;
    using Xunit;
    using OpenInput.Mechanics;
    using OpenInput.Mechanics.Layout;

    public class LayoutTest : Layout
    {
        public LayoutTest(string layoutId)
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

        [ActionTrigger("Pause", isReadOnly: true)]
        public InputKeys Pause { get; set; }

        public static readonly LayoutTest DefaultLayout = new LayoutTest("KeyboardLayout")
        {
            LayoutName      = "Default Keyboard Layout",
            LayoutDescription = "The default keyboard layout.",
            MoveForwards    = new InputKeys(Keys.W, Keys.Up),
            MoveBackwards   = new InputKeys(Keys.S, Keys.Down),
            MoveLeft        = new InputKeys(Keys.A, Keys.Left),
            MoveRight       = new InputKeys(Keys.D, Keys.Right),
            Jump            = new InputKeys(Keys.Space),
            Pause           = new InputKeys(Keys.Escape),
        };

        public static readonly LayoutTest DefaultGamePadLayout = new LayoutTest("GamePadLayout")
        {
            LayoutName      = "Default GamePad Layout",
            LayoutDescription = "The default GamePad layout.",
            MoveForwards    = new InputKeys(Buttons.DPadUp),
            MoveBackwards   = new InputKeys(Buttons.DPadDown),
            MoveLeft        = new InputKeys(Buttons.DPadLeft),
            MoveRight       = new InputKeys(Buttons.DPadRight),
            Jump            = new InputKeys(Buttons.A),
            Pause           = new InputKeys(Buttons.Start),
        };
    }

    public class InputLayoutTest
    {
        [Fact]
        public void CheckSettings()
        {
            var layout = LayoutTest.DefaultLayout;

            Assert.Equal(6, layout.SettingsCount);
        }

        [Fact]
        public void CheckSettingsOptions()
        {
            var layout = LayoutTest.DefaultLayout;
            var settings = layout.GetSettings();

            Assert.Equal(new string[] { "Movement" }, settings.Keys.ToArray());
            Assert.Equal(5, settings["Movement"].Count);
        }
    }
}
