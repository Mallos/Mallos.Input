namespace Mallos.Input.Window
{
    using System;
    using System.Threading;
    using Veldrid.Sdl2;
    using static Veldrid.Sdl2.Sdl2Native;

    internal class WindowParams
    {
        public int X
        {
            get; set;
        }
        public int Y
        {
            get; set;
        }
        public int Width
        {
            get; set;
        }
        public int Height
        {
            get; set;
        }
        public string Title
        {
            get; set;
        }
        public SDL_WindowFlags WindowFlags
        {
            get; set;
        }

        public IntPtr WindowHandle
        {
            get; set;
        }

        public ManualResetEvent ResetEvent
        {
            get; set;
        }

        public SDL_Window Create()
        {
            if (this.WindowHandle != IntPtr.Zero)
            {
                return SDL_CreateWindowFrom(this.WindowHandle);
            }
            else
            {
                return SDL_CreateWindow(this.Title, this.X, this.Y, this.Width, this.Height, this.WindowFlags);
            }
        }
    }
}
