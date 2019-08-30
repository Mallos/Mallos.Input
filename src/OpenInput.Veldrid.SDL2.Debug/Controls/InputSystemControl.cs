namespace OpenInput.Debug.Controls
{
    using ImGuiNET;
    using OpenInput.Mechanics;
    using OpenInput.Mechanics.Input;

    /// <summary>
    /// Debug Control for getting a insight into the input system.
    /// </summary>
    public class InputSystemControl : Control
    {
        public InputSystemControl(InputSystem inputSystem)
        {
            this.InputSystem = inputSystem;
        }

        public InputSystem InputSystem { get; set; }

        public override void DrawControl()
        {
            foreach (var item in this.InputSystem.Actions.GetValues())
            {
                ImGui.Text($"{item.Key} = {item.Value}");
            }

            ImGui.Separator();
            foreach (var item in this.InputSystem.Axis.GetValues())
            {
                ImGui.Text($"{item.Key} = {item.Value}");
            }
        }
    }
}