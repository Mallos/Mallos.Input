namespace Mallos.Input
{
    /// <summary>
    /// The possible mouse buttons.
    /// </summary>
    public enum MouseButtons : byte
    {
        Empty = 0,

        /// <summary>
        /// The left mouse button.
        /// </summary>
        Left = 1,

        /// <summary>
        /// The middle mouse button.
        /// </summary>
        Middle = 2,

        /// <summary>
        /// The right mouse button.
        /// </summary>
        Right = 4,

        /// <summary>
        /// The extra mouse button 1.
        /// </summary>
        XButton1 = 8,

        /// <summary>
        /// The extra mouse button 2.
        /// </summary>
        XButton2 = 16,
    }
}
