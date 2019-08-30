namespace OpenInput.Debug.Controls
{
    using System;
    using ImGuiNET;

    public abstract class Control : IDisposable
    {
        public void DrawWindow()
        {
            ImGui.Begin(this.ToString());
            this.DrawControl();
            ImGui.End();
        }

        public abstract void DrawControl();

        public virtual void Dispose()
        {
        }
    }
}