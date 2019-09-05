namespace Mallos.Input.Debug.Controls.Devices
{
    using ImGuiNET;

    /// <summary>
    /// Debug Control for getting a insight into a gamepad state.
    /// </summary>
    public class GamePadControl : Control
    {
        public GamePadControl(IGamePad gamepad)
        {
            this.GamePad = gamepad;
        }

        public IGamePad GamePad { get; set; }

        public override void DrawControl()
        {
            var gamepadState = this.GamePad.GetCurrentState();

            ImGui.Text($"Name: {this.GamePad.Name}");
            ImGui.Text($"IsConnected: {gamepadState.IsConnected}");

            ImGui.BeginChild($"GamePad.Child1");
            if (ImGui.CollapsingHeader("Buttons", ImGuiTreeNodeFlags.CollapsingHeader))
            {
                var buttonValues = System.Enum.GetValues(typeof(Buttons));
                for (int i2 = 0; i2 < buttonValues.Length; i2++)
                {
                    Buttons button = (Buttons)buttonValues.GetValue(i2);
                    var buttonDown = gamepadState.Buttons.IsButtonDown(button);
                    ImGui.Text($"{ button } = { buttonDown }");
                }
            }

            if (ImGui.CollapsingHeader("ThumbSticks", ImGuiTreeNodeFlags.CollapsingHeader))
            {
                ImGui.Text($"Left: { gamepadState.ThumbSticks.LeftThumbstick.X }, { gamepadState.ThumbSticks.LeftThumbstick.Y }");
                ImGui.Text($"Right: { gamepadState.ThumbSticks.RightThumbstick.X }, { gamepadState.ThumbSticks.RightThumbstick.Y }");
            }

            if (ImGui.CollapsingHeader("Triggers", ImGuiTreeNodeFlags.CollapsingHeader))
            {
                ImGui.Text($"Left: { gamepadState.Triggers.Left }");
                ImGui.Text($"Right: { gamepadState.Triggers.Right }");
            }
            ImGui.EndChild();
        }

        public override string ToString() => $"GamePad (Name: \"{this.GamePad.Name}\", Index: {this.GamePad.Index})";
    }
}
