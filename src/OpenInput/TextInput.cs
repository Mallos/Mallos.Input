namespace OpenInput
{
    /// <summary>
    /// Provides support for text input.
    /// </summary>
    public abstract class TextInput
    {
        /// <summary>
        /// Gets or sets, if capturing input.
        /// </summary>
        public bool Capture
        {
            get { return capture; }
            set { capture = value; }
        }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public string Result
        {
            get { return result; }
            set { result = value; }
        }

        /// <summary>
        /// Gets or sets, if the capture should allow new lines.
        /// </summary>
        public bool AllowNewLine
        {
            get { return allowNewLine; }
            set { allowNewLine = value; }
        }

        private bool capture;
        private string result;
        private bool allowNewLine;

        public TextInput()
        {
            this.Capture = false;
            this.Result = string.Empty;
            this.allowNewLine = false;
        }
    }
}
