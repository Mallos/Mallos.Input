namespace Mallos.Input
{
    using System;

    /// <summary>
    /// The possible mouse buttons.
    /// </summary>
    [Flags]
    public enum MouseButtons
    {
        Empty,

        /// <summary>
        /// The left mouse button.
        /// </summary>
        Left,

        /// <summary>
        /// The middle mouse button.
        /// </summary>
        Middle,

        /// <summary>
        /// The right mouse button.
        /// </summary>
        Right,

        /// <summary>
        /// The extra mouse button 1.
        /// </summary>
        XButton1,

        /// <summary>
        /// The extra mouse button 2.
        /// </summary>
        XButton2,
    }
}
