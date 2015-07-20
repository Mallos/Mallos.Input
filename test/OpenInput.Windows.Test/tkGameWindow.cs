using Nine.Injection;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;

namespace OpenInput
{
    public class tkGameWindow :  GameWindow
    {
        private IContainer container;

        public tkGameWindow()
            : base(800, 600, GraphicsMode.Default, "GameWindow")
        {
            this.VSync = VSyncMode.On;

            this.container = new Container();
            this.container
                .Map<IMouse>(new OpenTK.Mouse())
                .Map<IKeyboard>(new OpenTK.Keyboard());
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(0.1f, 0.2f, 0.5f, 0.0f);
            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 64.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            var keyboard = container.Get<IKeyboard>();
            if (keyboard != null)
            {
                var keyboardState = keyboard.GetCurrentState();
                Console.WriteLine("Keyboard: " + keyboardState);
            }

            var mouse = container.Get<IMouse>();
            if (mouse != null)
            {
                var mouseState = mouse.GetCurrentState();
                Console.WriteLine("Mouse: " + mouseState);
            }

            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);

            GL.Begin(PrimitiveType.Triangles);

            GL.Color3(1.0f, 1.0f, 0.0f); GL.Vertex3(-1.0f, -1.0f, 4.0f);
            GL.Color3(1.0f, 0.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, 4.0f);
            GL.Color3(0.2f, 0.9f, 1.0f); GL.Vertex3(0.0f, 1.0f, 4.0f);

            GL.End();

            SwapBuffers();
        }
    }
}
