namespace Mallos.Input.Window
{
    using System;
    using Veldrid;
    using Veldrid.Sdl2;

    public interface IWindow
    {
        /// <summary>
        /// Gets or sets the window X position.
        /// </summary>
        int X { get; set; }

        /// <summary>
        /// Gets or sets the window y position.
        /// </summary>
        int Y { get; set; }

        /// <summary>
        /// Gets or sets the window width.
        /// </summary>
        int Width { get; set; }

        /// <summary>
        /// Gets or sets the window height.
        /// </summary>
        int Height { get; set; }

        /// <summary>
        /// Gets or sets the window title.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets the window state.
        /// </summary>
        WindowState WindowState { get; set; }

        /// <summary>
        /// Gets or sets wether the cursor should be visible.
        /// </summary>
        bool CursorVisible { get; set; }

        /// <summary>
        /// Gets wether the window is visible.
        /// </summary>
        bool Visible { get; }

        /// <summary>
        /// Gets wether the window has focus.
        /// </summary>
        bool Focused { get; }
        
        event Action Closed;
        event Action FocusLost;
        event Action FocusGained;
        event Action Shown;
        event Action Hidden;
        event Action MouseEntered;
        event Action MouseLeft;
        event Action Exposed;
        event Action<DragDropEvent> DragDrop;

        Point ClientToScreen(Point p);

        Point ScreenToClient(Point p);

        bool PumpEvents();

        void Close();
    }
}
