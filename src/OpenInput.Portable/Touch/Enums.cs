namespace OpenInput.Touch
{
    using System;

    /// <summary>
    /// Defines the possible states of a touch location.
    /// </summary>
    public enum TouchLocationState
    {
        /// <summary>
        /// This touch location position is invalid. Typically, you will encounter this state when a new touch location attempts to get the previous state of itself.
        /// </summary>
        Invalid,

        /// <summary>
        /// This touch location position was updated or pressed at the same position.
        /// </summary>
        Moved,

        /// <summary>
        /// This touch location position is new.
        /// </summary>
        Pressed,

        /// <summary>
        /// This touch location position was released.
        /// </summary>
        Released,
    }

    /// <summary>
    /// Contains values that represent different multitouch gestures.
    /// </summary>
    [Flags]
    public enum GestureType
    {
        /// <summary>
        /// Represents no gestures.
        /// </summary>
        None,

        /// <summary>
        /// The user briefly touched a single point on the screen.
        /// </summary>
        Tap,

        /// <summary>
        /// The user tapped the screen twice in quick succession. This always is preceded 
        /// by a Tap gesture. If the time between taps is too great to be considered a 
        /// DoubleTap, two Tap gestures will be generated instead.
        /// </summary>
        DoubleTap,


        /// <summary>
        /// The user touched a single point on the screen for approximately one second. 
        /// This is a single event, and not continuously generated while the user is 
        /// holding the touchpoint.
        /// </summary>
        Hold,

        /// <summary>
        /// The user touched the screen, and then performed a horizontal 
        /// (left to right or right to left) gesture.
        /// </summary>
        HorizontalDrag,

        /// <summary>
        /// The user touched the screen, and then performed a vertical 
        /// (top to bottom or bottom to top) gesture.
        /// </summary>
        VerticalDrag,

        /// <summary>
        /// The user touched the screen, and then performed a free-form drag gesture.
        /// </summary>
        FreeDrag,

        /// <summary>
        /// The user touched two points on the screen, and then converged or diverged them. 
        /// Pinch behaves like a two-finger drag. When this gesture is enabled, it takes 
        /// precedence over drag gestures while two fingers are down.
        /// </summary>
        Pinch,

        /// <summary>
        /// The user performed a touch combined with a quick swipe of the screen. Flicks are 
        /// positionless. The velocity of the flick can be retrieved by reading the Delta 
        /// member of GestureSample.
        /// </summary>
        Flick,

        /// <summary>
        /// A drag gesture (VerticalDrag, HorizontalDrag, or FreeDrag) was completed. 
        /// This signals only completion. No position or delta data is valid for this sample.
        /// </summary>
        DragComplete,

        /// <summary>
        /// A pinch operation was completed. This signals only completion. No position or 
        /// delta data is valid for this sample.
        /// </summary>
        PinchComplete,
    }
}
