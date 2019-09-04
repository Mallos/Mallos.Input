namespace OpenInput.Debug.Controls.Devices
{
    using ImGuiNET;

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
            var keyboardState = this.Keyboard.GetCurrentState();
            foreach (var item in keyboardState.Keys)
            {
                ImGui.Text(item.ToString());
            }
        }

        public override string ToString() => $"Keyboard (Name: \"{this.Keyboard.Name}\")";
    }
}
