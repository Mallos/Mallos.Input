namespace OpenInput.Mechanics
{
    using System;
    using OpenInput.Trackers;
    using System.Collections.Generic;

    public struct SequenceCombo
    {
        public readonly string Name;
        public readonly InputKey[] Keys;

        public SequenceCombo(string name, params InputKey[] keys)
        {
            this.Name = name;
            this.Keys = keys;
        }
    }

    public class ComboTracker : ITracker, IDisposable
    {
        public readonly IKeyboardTracker KeyboardTracker;

        public readonly List<SequenceCombo> SequenceCombos = new List<SequenceCombo>();

        public float ComboTimeout;

        public event Action<SequenceCombo> OnComboCalled;

        private float timeSincelast = 0.0f;
        private List<InputKey> history = new List<InputKey>();

        public ComboTracker(IKeyboardTracker keyboardTracker, float comboTimeout = 0.5f)
        {
            this.KeyboardTracker = keyboardTracker ?? throw new ArgumentNullException(nameof(keyboardTracker));
            this.KeyboardTracker.KeyDown += KeyboardTracker_KeyDown;

            this.ComboTimeout = comboTimeout;
        }

        private void KeyboardTracker_KeyDown(object sender, KeyEventArgs e)
        {
            history.Add(new InputKey(e.Key));
            timeSincelast = 0;
        }

        public void Update(float elapsedTime)
        {
            timeSincelast += elapsedTime;
            if (timeSincelast > ComboTimeout & history.Count > 0)
            {   
                history.Clear();
            }

            foreach (var item in SequenceCombos)
            {
                var match = MatchHistory(item);
                if (match == item.Keys.Length)
                {
                    // TODO: Matching combos

                    OnComboCalled?.Invoke(item);
                    history.Clear();
                }
            }
        }

        private int MatchHistory(SequenceCombo combo)
        {
            if (combo.Keys.Length == 0)
                return 0;

            if (history.Count < combo.Keys.Length)
                return -1; // TODO: 

            for (int i = 0; i < history.Count; i++)
            {
                if (combo.Keys.Length <= i)
                    return i;

                if (combo.Keys[i] != history[i])
                    return i;
            }

            // Everything matched
            return combo.Keys.Length;
        }

        public void Dispose()
        {
            this.KeyboardTracker.KeyDown -= KeyboardTracker_KeyDown;
        }
    }
}
