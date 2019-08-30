namespace OpenInput.Debug.Controls
{
    using ImGuiNET;
    using System;
    using OpenInput.Mechanics;
    using OpenInput.Mechanics.Input;
    using OpenInput.Mechanics.Combo;
    using System.Collections.Generic;

    /// <summary>
    /// Debug Control for getting a insight into the combo tracker.
    /// </summary>
    /// <remark>
    /// TODO: A way to change general settings in the window
    /// TODO: View a possible combos
    /// </remark>
    public class ComboTrackerControl : Control
    {
        private SequenceCombo[] didYouMean;

        public ComboTrackerControl(ComboTracker comboTracker)
        {
            this.ComboTracker = comboTracker;
            this.ComboTracker.OnComboCalled += ComboTracker_OnComboCalled;
            this.ComboTracker.OnComboReset += ComboTracker_OnComboReset;
        }

        public ComboTracker ComboTracker { get; set; }
        public List<SequenceCombo> ComboHistory { get; } = new List<SequenceCombo>();
        public int ComboHistoryMax { get; set; } = 10;
        public bool ShowDidYouMean { get; set; } = true;
        public int DidYouMeanFuzzyness { get; set; } = 1;
        public bool ShowHistory { get; set; } = true;

        public override void DrawControl()
        {
            ImGui.Text($"Current: {this.ComboTracker.HistoryAsString()}");

            if (this.ShowDidYouMean && this.didYouMean != null && this.didYouMean.Length > 0)
            {
                ImGui.Separator();
                ImGui.Text("Did you mean:");
                foreach (var combo in this.didYouMean)
                    ImGui.Text($"{combo} ?");
            }

            if (this.ShowHistory)
            {
                ImGui.Separator();
                ImGui.BeginChild("ComboTrackerChild1");
                for (var i = this.ComboHistory.Count - 1; i >= 0; i--)
                {
                    ImGui.Text(this.ComboHistory[i].Name);
                }
                ImGui.EndChild();
            }
        }

        private void ComboTracker_OnComboCalled(object sender, SequenceCombo obj)
        {
            this.ComboHistory.Add(obj);
            if (this.ComboHistory.Count > this.ComboHistoryMax)
            {
                this.ComboHistory.RemoveRange(0, this.ComboHistory.Count - this.ComboHistoryMax);
            }
            this.didYouMean = null;
        }

        private void ComboTracker_OnComboReset(object sender, InputKeys obj)
        {
            var result = this.ComboTracker.SequenceCombos.FuzzySearch(this.DidYouMeanFuzzyness, obj);
            this.didYouMean = result;
        }
    }
}