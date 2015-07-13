namespace OpenInput.RawInput
{
    using System.Windows.Forms;

    class PreMessageFilter : IMessageFilter
    {
        /// <summary>
        /// true  to filter the message and stop it from being dispatched 
        /// false to allow the message to continue to the next filter or control.
        /// </summary>
        public bool PreFilterMessage(ref Message m)
        {
            return m.Msg == Win32.WM_KEYDOWN;
        }
    }
}
