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

        private float comboTimeout = 0.5f;
        private int maxCombo = 4;
        private float timeSincelast = 0.0f;
        private InputKey[] history = null;
        private int historyIndex = 0;

        /// <summary>
        /// Initialize a new <see cref="ComboTracker"/> class.
        /// </summary>
        /// <param name="keyboardTracker">The keyboard we are going to track.</param>
        public ComboTracker(IKeyboardTracker keyboardTracker)
        {
            this.KeyboardTracker = keyboardTracker ?? throw new ArgumentNullException(nameof(keyboardTracker));
            this.KeyboardTracker.KeyDown += KeyboardTracker_KeyDown;
            this.history = new InputKey[MaxCombo + 1];
        }

        /// <summary>
        /// Gets the current button history. This array is bigger then the
        /// history use <see cref="HistoryCount"/> to see how many buttons there are.
        /// </summary>
        public InputKey[] History => this.history;

        /// <summary>
        /// Gets the current button history count.
        /// </summary>
        public int HistoryCount => this.historyIndex;

        /// <summary>
        /// Gets or sets the button history timeout.
        /// </summary>
        public float ComboTimeout
        {
            get { return this.comboTimeout; }
            set
            {
                if (this.comboTimeout != value)
                {
                    if (value < 0.1f)
                    {
                        throw new ArgumentException("How is that fair? At least give them some time!");
                    }

                    this.comboTimeout = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the max button combo history.
        /// </summary>
        public int MaxCombo
        {
            get { return this.maxCombo; }
            set
            {
                if (this.maxCombo != value)
                {
                    if (value <= 1)
                    {
                        throw new ArgumentException($"{nameof(this.MaxCombo)} needs to be higher then 1.");
                    }

                    this.maxCombo = value;
                    this.history = new InputKey[this.MaxCombo + 1];
                    this.historyIndex = 0;
                }
            }
        }

        /// <summary>
        /// Occures when a new combo is called.
        /// </summary>
        public event Action<SequenceCombo> OnComboCalled;

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
            if (this.historyIndex >= this.MaxCombo)
            {
                this.historyIndex = 0;
            }
            else
            {
                this.history[this.historyIndex++] = new InputKey(e.Key);
                this.timeSincelast = 0;
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
