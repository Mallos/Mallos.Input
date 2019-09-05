namespace Mallos.Input.Debug.Controls
{
    using ImGuiNET;

    public partial class Control
    {
        public void DrawWindow()
        {
            ImGui.Begin(this.ToString());
            this.DrawControl();
            ImGui.End();
        }

        public abstract void DrawControl();
    }
}
