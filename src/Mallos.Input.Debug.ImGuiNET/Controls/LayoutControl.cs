namespace Mallos.Input.Debug.Controls
{
    using ImGuiNET;
    using System.Linq;
    using Mallos.Input.Mechanics.Layout;

    /// <summary>
    /// Debug Control for getting a insight into the layout.
    /// </summary>
    public class LayoutControl : Control
    {
        public LayoutControl(Layout layout)
        {
            this.Layout = layout;
        }

        public Layout Layout { get; set; }

        public override void DrawControl()
        {
            var settings = this.Layout.GetSettings();
            var settingsKeys = settings.Keys.ToArray();
            for (var gi = 0; gi < settingsKeys.Length; gi++)
            {
                var groupKey = settingsKeys[gi];
                var group = settings[groupKey];

                ImGui.Text($"- {groupKey} -");

                ImGui.Separator();
                ImGui.Columns(3, null, true);
                for (var i = 0; i < group.Count; i++)
                {
                    var setting = group[i];
                    if (setting.IsReadOnly)
                    {
                        ImGui.Text($"[ReadOnly] {setting.Name}");
                    }
                    else
                    {
                        ImGui.Text($"{setting.Name}");
                    }

                    if (ImGui.IsItemHovered() && !string.IsNullOrWhiteSpace(setting.Description))
                    {
                        ImGui.SetTooltip(setting.Description);
                    }

                    ImGui.NextColumn();
                    if (setting.Keys.Keys.Length > 0)
                    {
                        ImGui.Text($"{setting.Keys.Keys[0]}");
                    }
                    ImGui.NextColumn();
                    if (setting.Keys.Keys.Length > 1)
                    {
                        ImGui.Text($"{setting.Keys.Keys[1]}");
                    }
                    ImGui.NextColumn();
                }
                ImGui.Columns(1);
                ImGui.Separator();
            }
        }
    }
}
