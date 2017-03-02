namespace OpenInput.Test
{
    using ImGuiNET;
    using OpenTK;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Input;
    using System;

    public class Program : GameWindow
    {
        private readonly ImGuiRenderContext renderContext;

        private readonly TestContext TestContext;

        public Program()
            : base(1280, 800, null, "OpenInput.Test")
        {
            Keyboard.KeyDown += Keyboard_KeyDown;

            this.TestContext = new TestContext(new OpenTKDeviceSet(this));

            // Initialize ImGui render context.
            this.renderContext = new ImGuiRenderContext(this);
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
            TestContext.Update((float)e.Time);

            // Begin the frame
            GL.Clear(ClearBufferMask.ColorBufferBit);
            renderContext.BeginFrame((float)e.Time);

            TestContext.AddImGuiStuff();

            // End the frame
            renderContext.EndFrame();
            this.SwapBuffers();
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
        public static void Main(string[] args)
        {
            using (var game = new Program())
                game.Run(30.0);
        }
    }
}