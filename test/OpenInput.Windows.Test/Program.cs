namespace OpenInput
{
    using Nine.Injection;
    using System;
    using System.Timers;
    using Form = System.Windows.Forms.Form;
    
    public class Program
    {
        public void Main(string[] args)
        {
            var container = new Container();
            container
                .Map<IMouse>(new Mouse())
                .Map<IKeyboard>(new Keyboard());


            var timer = new Timer(1.0f / 4);
            timer.Elapsed += (s, e) =>
            {
                var keyboard = container.Get<IKeyboard>();
                if (keyboard != null)
                {
                    var keyboardState = keyboard.GetCurrentState();
                    Console.WriteLine(keyboardState.ToString());
                }
            };

            var form = new Form();
            form.Width = 500;
            form.Height = 500;
            
            timer.Start();
            form.ShowDialog();
        }
    }
}
