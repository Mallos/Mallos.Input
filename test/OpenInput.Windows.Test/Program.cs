namespace OpenInput
{
    using Nine.Hosting;
    using System.Windows.Forms;

    public class Program
    {
        private readonly IHostWindow hostWindow;

        public Program(IHostWindow hostWindow)
        {
            this.hostWindow = hostWindow;
        }

        public void Main(string[] args)
        {
            var form = new OutputForm();
            hostWindow?.Attach(form.Handle);
            Application.Run(form);
        }
    }
}
