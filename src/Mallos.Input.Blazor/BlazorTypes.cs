namespace Mallos.Input.Blazor
{
    /// <summary>
    /// This is taken from <seealso cref="Microsoft.AspNetCore.Components.Web.MouseEventArgs"/>.
    /// Doc: https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.web.mouseeventargs?view=aspnetcore-5.0
    /// </summary>
    public enum BlazorMouseButtons
    {
        LeftButton = 0,
        MiddleButton = 1,
        RightButton = 2,
        XButton1 = 4,
        XButton2 = 8
    }

    public struct BlazorTouchPoint
    {
        /// <summary>
        /// Gets a value uniquely identifying this point of contact with the touch surface.
        /// This value remains consistent for every event involving this finger's
        /// (or stylus's) movement on the surface until it is lifted off the surface.
        /// </summary>
        public int identifier;

        /// <summary>
        /// Gets the X coordinate of the touch point relative to the viewport, including any scroll offset.
        /// </summary>
        public int x;

        /// <summary>
        /// Gets the Y coordinate of the touch point relative to the viewport, including any scroll offset.
        /// </summary>
        public int y;

        /// <summary>
        /// Gets the amount of pressure the user is applying to the touch surface for a Touch point.
        /// https://developer.mozilla.org/en-US/docs/Web/API/Touch/force
        /// </summary>
        public float force;

        /// <summary>
        /// Gets the radius of the ellipse that most closely circumscribes
        /// the area of contact with the touch surface.
        /// https://developer.mozilla.org/en-US/docs/Web/API/Touch/radiusX
        /// https://developer.mozilla.org/en-US/docs/Web/API/Touch/radiusY
        /// </summary>
        public int radius;
    }
}
