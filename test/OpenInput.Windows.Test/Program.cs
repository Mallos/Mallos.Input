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

            //var mouseGroup = new GroupBox();
            //form.Controls.Add(mouseGroup);

            // Not sure how I can get Nine.Hosting to work
            hostWindow?.Attach(form.Handle);
            Application.Run(form);
        }
    }
}
