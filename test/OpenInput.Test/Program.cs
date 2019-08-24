namespace OpenInput.Test
{
    using System;

    class Program
    { 
        private static void Main()
        {
            var layout = KeyboardLayoutTest.DefaultLayout;
            Console.WriteLine($"{layout.TriggerCount()} settings");

            Console.WriteLine("Hello World");
        }
    }
}
