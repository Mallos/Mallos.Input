namespace OpenInput
{
    using Microsoft.Framework.Runtime.Common.CommandLine;
    using Nine.Injection;
    using System;
    using System.Threading;
    using System.Windows.Forms;

    public class Program
    {
        static Thread gameWindowThread;

        public void Main(string[] args)
        {
            var app = new CommandLineApplication(throwOnUnexpectedArg: false);
            app.Name = app.FullName = "OpenInput";
            app.HelpOption("-?|--help");

            var platform = app.Option("-p|--platform <TYPE>", "Set the input platform (DirectInput, RawInput, XInput, OpenTK)", CommandOptionType.SingleValue);

            app.Execute(args);

            if (app.IsShowingInformation) return;

            string target = "rawinput";

            if (platform.HasValue())
                target = platform.Value().ToLower();
            
            Func<IntPtr, IContainer> createContainer;
            string title = string.Empty;

            switch (target)
            {
                //case "directinput":
                //    title = "DirectInput";
                //    createContainer = (handle) =>
                //    {
                //        var container = new Container();
                //        container
                //            .Map<IMouse>(new DirectInput.Mouse())
                //            .Map<IKeyboard>(new DirectInput.Keyboard());
                //        return container;
                //    };
                //    break;

                default:
                case "rawinput":
                    title = "RawInput";
                    createContainer = (handle) =>
                    {
                        var container = new Container();
                        container
                            .Map<IMouse>(new RawInput.Mouse(handle))
                            .Map<IKeyboard>(new RawInput.Keyboard(handle));
                        return container;
                    };
                    break;

                case "opentk":
                    title = "OpenTK";

                    // Not sure if I have to create a opentk window or how it works
                    gameWindowThread = new Thread(() => {
                        using (var game = new tkGameWindow())
                            game.Run(30.0);
                    });
                    gameWindowThread.Start();

                    createContainer = (handle) =>
                    {
                        var container = new Container();
                        container
                            .Map<IMouse>(new OpenInput.OpenTK.Mouse())
                            .Map<IKeyboard>(new OpenInput.OpenTK.Keyboard());
                        return container;
                    };
                    break;
            }

            var form = new OutputForm(title, createContainer);
            Application.Run(form);
        }
    }
}
