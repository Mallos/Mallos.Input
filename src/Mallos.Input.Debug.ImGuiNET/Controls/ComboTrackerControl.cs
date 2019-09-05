namespace Mallos.Input.Debug.Controls
{
    using System.Collections.Generic;
    using ImGuiNET;
    using Mallos.Input.Mechanics.Combo;

    public partial class ComboTrackerControl
    {
        public override void DrawControl()
        {
            ImGui.Text($"Current: {this.ComboTracker.HistoryAsString()}");

            if (this.ShowDidYouMean && this.didYouMean != null && this.didYouMean.Length > 0)
            {
                this.DrawDidYouMean(this.didYouMean);
            }

            if (this.ShowHistory)
            {
                this.DrawHistory(this.ComboHistory);
            }
        }

        private void DrawDidYouMean(SequenceCombo[] sequences)
        {
            ImGui.Separator();
            ImGui.Text("Did you mean:");

            foreach (SequenceCombo combo in sequences)
            {
                ImGui.Text($"{combo} ?");
            }
        }

        private void DrawHistory(IList<SequenceCombo> collection)
        {
            ImGui.Separator();
            ImGui.BeginChild("ComboTrackerChild1");

            for (int i = collection.Count - 1; i >= 0; i--)
            {
                ImGui.Text(collection[i].Name);
            }

            ImGui.EndChild();
        }
    }
}
