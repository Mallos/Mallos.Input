namespace OpenInput
{
    /// <summary>
    /// Provides support for text input.
    /// </summary>
    public abstract class TextInput
    {
        public TextInput()
        {
            this.Capture = false;
            this.Result = string.Empty;
            this.AllowNewLine = false;
        }

        /// <summary>
        /// Gets or sets, if capturing input.
        /// </summary>
        public bool Capture { get; set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// Gets or sets, if the capture should allow new lines.
        /// </summary>
        public bool AllowNewLine { get; set; }
    }
}
