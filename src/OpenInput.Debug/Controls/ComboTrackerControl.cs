namespace OpenInput.Debug.Controls
{
    using OpenInput.Mechanics;
    using OpenInput.Mechanics.Combo;
    using System.Collections.Generic;

    /// <summary>
    /// Debug Control for getting a insight into the combo tracker.
    /// </summary>
    /// <remark>
    /// TODO: A way to change general settings in the window
    /// TODO: View a possible combos
    /// </remark>
    public partial class ComboTrackerControl : Control
    {
        private SequenceCombo[] didYouMean;

        public ComboTrackerControl(ComboTracker comboTracker)
        {
            this.ComboTracker = comboTracker;
            this.ComboTracker.OnComboCalled += this.ComboTracker_OnComboCalled;
            this.ComboTracker.OnComboReset += this.ComboTracker_OnComboReset;
        }

        public ComboTracker ComboTracker { get; set; }
        public List<SequenceCombo> ComboHistory { get; } = new List<SequenceCombo>();
        public int ComboHistoryMax { get; set; } = 10;
        public bool ShowDidYouMean { get; set; } = true;
        public int DidYouMeanFuzzyness { get; set; } = 1;
        public bool ShowHistory { get; set; } = true;

        public override void Dispose()
        {
            base.Dispose();

            this.ComboTracker.OnComboCalled -= this.ComboTracker_OnComboCalled;
            this.ComboTracker.OnComboReset -= this.ComboTracker_OnComboReset;
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
            SequenceCombo[] result = this.ComboTracker.SequenceCombos
                .FuzzySearch(this.DidYouMeanFuzzyness, obj);

            this.didYouMean = result;
        }
    }
}