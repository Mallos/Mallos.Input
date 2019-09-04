namespace OpenInput.Debug.Controls
{
    using System.Collections.Generic;
    using ImGuiNET;
    using OpenInput.Debug.Controls.Devices;

    /// <summary>
    /// Debug Control for getting a insight into a device set.
    /// </summary>
    public class DeviceSetControl : Control
    {
        public DeviceSetControl(IDeviceSet deviceSet)
        {
            this.DeviceSet = deviceSet;

            this.KeyboardControl = new KeyboardControl(deviceSet.Keyboard);
            this.MouseControl = new MouseControl(deviceSet.Mouse);
        }

        public IDeviceSet DeviceSet { get; }

        public KeyboardControl KeyboardControl { get; }

        public MouseControl MouseControl { get; }

        public List<GamePadControl> GamePadControls { get; } = new List<GamePadControl>();

        public override void DrawControl()
        {
            if (ImGui.CollapsingHeader(this.KeyboardControl.ToString()))
            {
                this.KeyboardControl.DrawControl();
            }

            if (ImGui.CollapsingHeader(this.MouseControl.ToString()))
            {
                this.MouseControl.DrawControl();
            }

            foreach (var gamepad in this.GamePadControls)
            {
                if (ImGui.CollapsingHeader(gamepad.ToString()))
                {
                    gamepad.DrawControl();
                }
            }
        }
        public override string ToString() => $"Device Set (Name: \"{this.DeviceSet.Name}\")";
    }
}
