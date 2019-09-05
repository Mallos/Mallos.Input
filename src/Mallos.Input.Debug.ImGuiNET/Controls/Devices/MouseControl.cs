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
            var mouseState = this.Mouse.GetCurrentState();

            sb.Clear();
            sb.AppendLine($"Position: { mouseState.X }, { mouseState.Y }");
            sb.AppendLine($"MouseWheel: { mouseState.ScrollWheelValue }");
            sb.AppendLine();
            sb.AppendLine($"Left Button: { mouseState.LeftButton }");
            sb.AppendLine($"Middle Button: { mouseState.MiddleButton }");
            sb.AppendLine($"Right Button: { mouseState.RightButton }");
            sb.AppendLine($"XButton1: { mouseState.XButton1 }");
            sb.AppendLine($"XButton2: { mouseState.XButton2 }");

            ImGui.Text(sb.ToString());
        }

        public override string ToString() => $"Mouse (Name: \"{this.Mouse.Name}\")";
    }
}
