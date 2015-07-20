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
        static tkGameWindow gameWindow;

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

            switch (target)
            {
                case "directinput":
                    createContainer = (handle) =>
                    {
                        var container = new Container();
                        container
                            .Map<IMouse>(new DirectInput.Mouse())
                            .Map<IKeyboard>(new DirectInput.Keyboard());
                        return container;
                    };
                    break;

                default:
                case "rawinput":
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

                    throw new NotImplementedException();

                    gameWindowThread = new Thread(() =>
                        {
                            using (var game = gameWindow = new tkGameWindow())
                            {
                                game.Run(30.0);
                            }
                        });

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

            var form = new OutputForm(createContainer);
            Application.Run(form);
        }
    }
}
