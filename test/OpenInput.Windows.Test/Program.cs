#pragma warning disable CS4014

namespace OpenInput
{
    using System;
    using System.Timers;
    using Form = System.Windows.Forms.Form;
    
    public class Program
    {
        public void Main(string[] args)
        {
            var inputManager = new InputManager();
            inputManager.DeviceConnected += e => Console.WriteLine($"Device Connected '{e.Name}'");
            inputManager.DeviceDisconnected += e => Console.WriteLine($"Device Disconnected '{e.Name}'");

            var textInput = new TextInput();

            inputManager.KeyPress += (s, e) =>
            {
                textInput.Process(e);
                Console.WriteLine($"TextInput {textInput.Result}");
            };

            var timer = new Timer(1.0f / 30);
            timer.Elapsed += (s, e) =>
            {
                inputManager.Update();

                var mouseState = Mouse.GetState();
                if (mouseState.RightButton)
                {
                    Console.WriteLine($"Mouse right button pressed at {mouseState.X}, {mouseState.Y}");
                }

                //var keyState = Keyboard.GetState();
                //if (keyState.IsKeyDown(Keys.A) && !previusKeyboardState.IsKeyDown(Keys.A))
                //{
                //    Console.WriteLine($"Key 'A' is down!");
                //}
            };

            var form = new Form();
            form.Width = 500;
            form.Height = 500;
            
            inputManager.Mouse.SetHandle(form.Handle);

            timer.Start();
            form.ShowDialog();
        }
    }
}
