namespace OpenInput
{
    using Nine.Injection;
    using System.Windows.Forms;
    using System;
    using System.Text;
    using OpenInput.Touch;

    public partial class OutputForm : Form
    {
        private IContainer container;
        private Timer timer;

        public OutputForm()
        {
            this.Text = "OpenInput";
            this.InitializeComponent();

            this.container = new Container();
            this.container
                //.Map<IMouse>(new DirectInput.Mouse())
                //.Map<IKeyboard>(new DirectInput.Keyboard());
                .Map<IMouse>(new RawInput.Mouse(this.Handle))
                .Map<IKeyboard>(new RawInput.Keyboard(this.Handle));

            this.timer = new Timer();
            this.timer.Interval = (int)TimeSpan.FromSeconds(1.0f / 12).TotalMilliseconds;
            this.timer.Tick += TimerElapsed;
        }
        
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            timer.Start();
        }

        private void TimerElapsed(object sender, EventArgs e)
        {
            var keyboard = container.Get<IKeyboard>();
            if (keyboard != null)
            {
                this.keyboardNamesLabel.Text = $"Name/s: '{keyboard.Name}', Type: {keyboard.GetType().FullName}";

                var keyboardState = keyboard.GetCurrentState();
                if (keyboardState.Keys.Length > 0)
                {
                    AddKeyboardHistory(keyboard.TextInput.Capture ? keyboard.TextInput.Result : keyboardState.ToString());
                }
            }

            var mouse = container.Get<IMouse>();
            if (mouse != null)
            {
                var mouseState = mouse.GetCurrentState();

                this.mouseNamesLabel.Text = $"Name/s: '{mouse.Name}', Type: {mouse.GetType().FullName}";

                var sb = new StringBuilder();
                sb.AppendLine($"Position: {mouseState.X}, {mouseState.Y}, MouseWheel: {mouseState.ScrollWheelValue}");
                sb.AppendLine();
                sb.AppendLine($"Left Button: {mouseState.LeftButton}, Middle Button: {mouseState.MiddleButton}, Right Button: {mouseState.RightButton}");
                sb.AppendLine($"XButton1: {mouseState.XButton1}, XButton2: {mouseState.XButton2}");
                this.mouseStateLabel.Text = sb.ToString();

                //if (mouseState.LeftButton && keyboard != null)
                //    keyboard.TextInput.Capture = !keyboard.TextInput.Capture;
            }

            var touchDevice = container.Get<ITouchDevice>();
            if (touchDevice != null)
            {
                var touchCollection = touchDevice.GetCurrentState();

                // Get all the touch location
                foreach (TouchLocation tl in touchCollection)
                {
                    if (tl.State == TouchLocationState.Pressed
                        || tl.State == TouchLocationState.Moved)
                    {
                        Console.WriteLine($"TouchLocation; X: {tl.Position.X}, Y: {tl.Position.Y}");
                    }
                }

                // Get all the touch gestures
                while (touchDevice.IsGestureAvailable)
                {
                    var gs = touchDevice.ReadGesture();
                    Console.WriteLine($"TouchGesture: {Enum.GetName(typeof(GestureType), gs.GestureType)}");
                }
            }
        }
    }
}
