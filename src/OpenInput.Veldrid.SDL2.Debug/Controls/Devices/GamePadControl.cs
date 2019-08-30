namespace OpenInput.Debug.Controls.Devices
{
    using ImGuiNET;
    using OpenInput.Mechanics;
    using OpenInput.Mechanics.Input;

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
            ImGui.Text($"GamePad (Name: \"{this.GamePad.Name}\", Index: {this.GamePad.Index})");

            var gamepadState = inputContext.GamePads[i].GetCurrentState();

            ImGui.Text($"Name: { inputContext.GamePads[i].Name }");
            ImGui.Text($"IsConnected: { gamepadState.IsConnected }");

            ImGui.BeginChild($"GamePad.{i}.Child1");
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
    }
}