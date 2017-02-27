using ImGuiNET;
using ImGuiNET.OpenTK;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenInput.Test
{
    class Program : GameWindow
    {
        private readonly RenderContext renderContext;

        private readonly List<InputContext> inputContexts;

        private StringBuilder sb = new StringBuilder();

        public Program()
            : base(1280, 800, null, "OpenInput.Test")
        {
            Keyboard.KeyDown += Keyboard_KeyDown;

            this.inputContexts = new List<InputContext>(new[]
            {
                InputContext.CreateOpenTK(),
            });

            this.renderContext = new RenderContext(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(114f / 255f, 144f / 255f, 154f / 255f, 1.0f);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            renderContext.BeginFrame((float)e.Time);

            foreach (var item in inputContexts)
            {
                AddWindow(item);
            }

            renderContext.EndFrame();

            this.SwapBuffers();
        }

        private void AddWindow(InputContext inputContext)
        {
            string windowTitle = inputContext.Name + " Input";
            ImGui.BeginWindow(windowTitle);
            {
                if (inputContext.Keyboard != null)
                {
                    if (ImGui.CollapsingHeader($"Keyboard (Name: \"{ inputContext.Keyboard.Name }\")", TreeNodeFlags.CollapsingHeader))
                    {
                        ImGui.Text("Hello World!");
                    }
                }

                if (inputContext.Mouse != null)
                {
                    var mouseState = inputContext.Mouse.GetCurrentState();
                    if (ImGui.CollapsingHeader($"Mouse (Name: \"{ inputContext.Mouse.Name }\")", TreeNodeFlags.CollapsingHeader))
                    {
                        sb.AppendLine($"Position: { mouseState.X }, { mouseState.Y }");
                        sb.AppendLine($"MouseWheel: { mouseState.ScrollWheelValue }");
                        sb.AppendLine();
                        sb.AppendLine($"Left Button: { mouseState.LeftButton }");
                        sb.AppendLine($"Middle Button: { mouseState.MiddleButton }");
                        sb.AppendLine($"Right Button: { mouseState.RightButton }");
                        sb.AppendLine($"XButton1: { mouseState.XButton1 }");
                        sb.AppendLine($"XButton2: { mouseState.XButton2 }");

                        ImGui.Text(sb.ToString());

                        sb.Clear();
                    }
                }

                //if (inputContext.GamePad != null)
                //{
                //    if (ImGui.CollapsingHeader("GamePad", TreeNodeFlags.CollapsingHeader))
                //    {
                //        ImGui.Text("Hello World!");
                //    }
                //}
                //
                //if (ImGui.CollapsingHeader("Touch", TreeNodeFlags.CollapsingHeader))
                //{
                //    ImGui.Text("Hello World!");
                //}
            }
            ImGui.EndWindow();
        }

        private void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Exit();

            if (e.Key == Key.F11)
            {
                this.WindowState = (this.WindowState == WindowState.Fullscreen) ? WindowState.Normal : WindowState.Fullscreen;
            }
        }

        [STAThread]
        static void Main(string[] args)
        {
            using (var game = new Program())
                game.Run(30.0);
        }
    }
}