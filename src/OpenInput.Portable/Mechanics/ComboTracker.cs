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

    /// <summary>
    /// Combo Tracker allows you to track button combos.
    /// </summary>
    public class ComboTracker : ITracker, IDisposable
    {
        public readonly IKeyboardTracker KeyboardTracker;

        /// <summary>
        /// Gets and register combos.
        /// </summary>
        public readonly List<SequenceCombo> SequenceCombos = new List<SequenceCombo>();

        /// <summary>
        /// Gets the current button history. This array is bigger then the 
        /// history use <see cref="HistoryCount"/> to see how many buttons there are.
        /// </summary>
        public InputKey[] History => history;

        /// <summary>
        /// Gets the current button history count.
        /// </summary>
        public int HistoryCount => historyIndex;

        /// <summary>
        /// Gets or sets the button history timeout.
        /// </summary>
        public float ComboTimeout
        {
            get { return comboTimeout; }
            set
            {
                if (comboTimeout != value)
                {
                    if (value < 0.1f) throw new ArgumentException("How is that fair? Atleast give them some time!");
                    comboTimeout = value;
                }
            }
        }
        private float comboTimeout = 0.5f;

        /// <summary>
        /// Gets or sets the max button combo history.
        /// </summary>
        public int MaxCombo
        {
            get { return maxCombo; }
            set
            {
                if (maxCombo != value)
                {
                    if (value <= 1) throw new ArgumentException("Atleast give them some combos.");
                    maxCombo = value;

                    history = new InputKey[MaxCombo + 1];
                    historyIndex = 0;
                }
            }
        }
        private int maxCombo;

        /// <summary>
        /// Occures when a new combo is called.
        /// </summary>
        public event Action<SequenceCombo> OnComboCalled;

        private float timeSincelast = 0.0f;
        private InputKey[] history = null;
        private int historyIndex = 0;

        /// <summary>
        /// Initialize a new <see cref="ComboTracker"/> class.
        /// </summary>
        /// <param name="keyboardTracker"></param>
        /// <param name="comboTimeout"></param>
        /// <param name="maxCombo"></param>
        public ComboTracker(IKeyboardTracker keyboardTracker, float comboTimeout = 0.5f, int maxCombo = 4) // TODO: IKeyboardTracker
        {
            this.KeyboardTracker = keyboardTracker ?? throw new ArgumentNullException(nameof(keyboardTracker));
            this.KeyboardTracker.KeyDown += KeyboardTracker_KeyDown;

            this.ComboTimeout = comboTimeout;
            this.MaxCombo = maxCombo;
        }

        public void Update(float elapsedTime)
        {
            timeSincelast += elapsedTime;
            if (timeSincelast > ComboTimeout & historyIndex > 0)
            {
                historyIndex = 0;
            }

            foreach (var item in SequenceCombos)
            {
                var match = MatchHistory(item);
                if (match == item.Keys.Length)
                {
                    // TODO: Matching combos

                    OnComboCalled?.Invoke(item);
                    historyIndex = 0;
                }
            }
        }

        /// <summary>
        /// Gets the button history as a string.
        /// </summary>
        /// <returns></returns>
        public string GetHistoryString()
        {
            string result = string.Empty;
            for (int i = 0; i < historyIndex; i++)
            {
                result += history[i] + ",";
            }
            return result;
        }

        private void KeyboardTracker_KeyDown(object sender, KeyEventArgs e)
        {
            if (historyIndex >= MaxCombo)
            {
                historyIndex = 0;
            }
            else
            {
                history[historyIndex++] = new InputKey(e.Key);
                timeSincelast = 0;
            }
        }

        private int MatchHistory(SequenceCombo combo)
        {
            if (combo.Keys.Length == 0)
                return 0;

            if (historyIndex < combo.Keys.Length)
                return -1; // TODO: Return if we have half of the combo done.
                           //       This could be useful for heads up display.

            for (int i = 0; i < historyIndex; i++)
            {
                if (combo.Keys.Length <= i)
                    return i;

                if (combo.Keys[i] != history[i])
                    return i;
            }

            return combo.Keys.Length;
        }

        public void Dispose()
        {
            this.KeyboardTracker.KeyDown -= KeyboardTracker_KeyDown;
        }
    }
}
