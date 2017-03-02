namespace OpenInput.Test
{
    using System;

    class Program
    { 
        [STAThread]
        private static void Main()
        {
            var game = new Game();
            game.Run();
        }
    }
}
