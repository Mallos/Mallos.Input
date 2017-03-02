namespace OpenInput.RawInput
{
    using System.Windows.Forms;

    /// <remarks>
    /// Note: IMessageFilter doesn't work with WPF applications.
    /// </remarks>
    class PreMessageFilter : IMessageFilter
    {
        /// <summary>
        /// true  to filter the message and stop it from being dispatched 
        /// false to allow the message to continue to the next filter or control.
        /// </summary>
        public bool PreFilterMessage(ref Message m)
        {
            return m.Msg == WindowsInterop.WM_KEYDOWN;
        }
    }
}
