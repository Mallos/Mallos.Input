namespace Mallos.Input.Debug.Controls.Devices
{
    using System.Text;
    using ImGuiNET;

    /// <summary>
    /// Debug Control for getting a insight into a mouse state.
    /// </summary>
    public class MouseControl : Control
    {
        private readonly StringBuilder sb = new StringBuilder();

        public MouseControl(IMouse mouse)
        {
            this.Mouse = mouse;
        }

        public IMouse Mouse { get; set; }

        public override void DrawControl()
        {
            MouseState mouseState = this.Mouse.GetCurrentState();

            this.sb.Clear();
            this.sb.AppendLine($"Position: { mouseState.X }, { mouseState.Y }");
            this.sb.AppendLine($"MouseWheel: { mouseState.ScrollWheelValue }");
            this.sb.AppendLine();
            this.sb.AppendLine($"Left Button: { mouseState.LeftButton }");
            this.sb.AppendLine($"Middle Button: { mouseState.MiddleButton }");
            this.sb.AppendLine($"Right Button: { mouseState.RightButton }");
            this.sb.AppendLine($"XButton1: { mouseState.XButton1 }");
            this.sb.AppendLine($"XButton2: { mouseState.XButton2 }");

            ImGui.Text(this.sb.ToString());
        }

        public override string ToString() => $"Mouse (Name: \"{this.Mouse.Name}\")";
    }
}
