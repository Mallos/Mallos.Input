namespace OpenInput.Trackers
{
    using System;

    /// <summary>
    /// Interface for a basic tracker. 
    /// </summary>
    public interface ITracker
    {
        /// <summary>
        /// Update the tracker.
        /// </summary>
        void Update();
    }

    /// <summary>
    /// Interface for a keyboard tracker.
    /// </summary>
    public interface IKeyboardTracker : ITracker
    {
        /// <summary> 
        /// Occurs when a key is pressed. 
        /// </summary>
        event EventHandler<KeyEventArgs> KeyDown;

        /// <summary> 
        /// Occurs when a key is released. 
        /// </summary>
        event EventHandler<KeyEventArgs> KeyUp;
    }

    /// <summary>
    /// Interface for a mouse tracker.
    /// </summary>
    public interface IMouseTracker : ITracker
    {
        /// <summary> 
        /// Occurs when the mouse pointer moves. 
        /// </summary>
        event EventHandler<MouseEventArgs> Move;

        /// <summary> 
        /// Occurs when a mouse wheel moved. 
        /// </summary>
        event EventHandler<MouseWheelEventArgs> MouseWheel;

        /// <summary> 
        /// Occurs when a mouse button is pressed. 
        /// </summary>
        event EventHandler<MouseButtonEventArgs> MouseDown;

        /// <summary> 
        /// Occurs when a mouse button is released. 
        /// </summary>
        event EventHandler<MouseButtonEventArgs> MouseUp;
    }
}
