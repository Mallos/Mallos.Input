namespace OpenInput.Debug.Controls
{
    using System.Collections.Generic;
    using ImGuiNET;

    public partial class InputSystemControl
    {
        public override void DrawControl()
        {
            this.DrawActions(this.InputSystem.Actions.GetValues());
            this.DrawAxis(this.InputSystem.Axis.GetValues());
        }

        private void DrawActions(IReadOnlyDictionary<string, bool> values)
        {
            foreach (KeyValuePair<string, bool> item in values)
            {
                ImGui.Text($"{item.Key} = {item.Value}");
            }
        }

        private void DrawAxis(IReadOnlyDictionary<string, float> values)
        {
            ImGui.Separator();
            foreach (KeyValuePair<string, float> item in values)
            {
                ImGui.Text($"{item.Key} = {item.Value}");
            }
        }
    }
}
