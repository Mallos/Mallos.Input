namespace OpenInput
{
    using Nine.Injection;
    using System;
    using System.Timers;
    using Form = System.Windows.Forms.Form;
    
    public class Program : Form
    {
        private IContainer container;
        private Timer timer;

        public Program()
        {
            container = new Container();
            container
                .Map<IMouse>(new Mouse())
                .Map<IKeyboard>(new Keyboard());

            timer = new Timer(1.0f / 4);
            timer.Elapsed += TimerElapsed;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            container.Get<IKeyboard>().SetHandle(this.Handle);
            container.Get<IMouse>().SetHandle(this.Handle);
            timer.Start();
        }
        
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            var keyboard = container.Get<IKeyboard>();
            if (keyboard != null)
            {
                var keyboardState = keyboard.GetCurrentState();
                Console.WriteLine(keyboardState.ToString());
            }

            var mouse = container.Get<IMouse>();
            if (mouse != null)
            {
                var mouseState = mouse.GetCurrentState();
                Console.WriteLine(mouseState.ToString());
            }
        }

        public void Main(string[] args)
        {
            var form = new Program();
            form.Width = 500;
            form.Height = 500;
            form.ShowDialog();
        }
    }
}
