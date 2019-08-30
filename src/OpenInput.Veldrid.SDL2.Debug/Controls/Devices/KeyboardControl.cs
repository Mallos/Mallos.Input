namespace OpenInput.Debug.Controls.Devices
{
    using ImGuiNET;
    using OpenInput.Mechanics;
    using OpenInput.Mechanics.Input;

    /// <summary>
    /// Debug Control for getting a insight into a keyboard state.
    /// </summary>
    public class KeyboardControl : Control
    {
        public KeyboardControl(IKeyboard keyboard)
        {
            this.Keyboard = keyboard;
        }

        public IKeyboard Keyboard { get; set; }

        public override void DrawControl()
        {
            ImGui.Text($"Keyboard (Name: \"{this.Keyboard.Name}\")");

            var keyboardState = this..Keyboard.GetCurrentState();
            foreach (var item in keyboardState.Keys)
            {
                ImGui.Text(item.ToString());
            }
        }
    }
}