namespace Mallos.Input.Mechanics.Combo
{
    using System;
    using Mallos.Input.Trackers;

    /// <summary>
    /// Combo Tracker allows you to track button combos.
    /// </summary>
    public class ComboTracker : ITracker, IDisposable
    {
        private float comboTimeout = 0.5f;
        private int maxCombo = 4;
        private float timeSincelast = 0.0f;
        private InputKey[] history = null;
        private int historyIndex = 0;

        /// <summary>
        /// Initialize a new <see cref="ComboTracker"/> class.
        /// </summary>
        /// <param name="trackers">The trackers that we will use.</param>
        public ComboTracker(params ITracker[] trackers)
        {
            this.history = new InputKey[this.MaxCombo + 1];
            this.Tracker = new InputTracker(trackers);
            this.Tracker.OnKeyDown += this.OnKeyDown;
        }

        /// <summary>
        /// Gets the tracker that track all devices.
        /// </summary>
        public InputTracker Tracker { get; }

        /// <summary>
        /// Gets and register combos.
        /// </summary>
        public SequenceCollection SequenceCombos { get; } = new SequenceCollection();

        /// <summary>
        /// Gets the current button history. This array is bigger then the
        /// history use <see cref="HistoryCount"/> to see how many buttons there are.
        /// </summary>
        public Span<InputKey> History => new Span<InputKey>(this.history, 0, this.HistoryCount);

        /// <summary>
        /// Gets the current button history count.
        /// </summary>
        public int HistoryCount => this.historyIndex;

        /// <summary>
        /// Gets or sets the button history timeout.
        /// </summary>
        public float ComboTimeout
        {
            get => this.comboTimeout;
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
            get => this.maxCombo;
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
        public event EventHandler<SequenceCombo> OnComboCalled;

        /// <summary>
        /// Occures when a the current combo is reset and not called.
        /// </summary>
        public event EventHandler<InputKeys> OnComboReset;

        /// <summary>
        /// Gets the button history as a string.
        /// </summary>
        /// <returns>A string containing all the history keys.</returns>
        public string HistoryAsString(string seperator = ",")
        {
            string result = string.Empty;

            for (int i = 0; i < this.historyIndex; i++)
            {
                result += this.history[i];

                if ((i + 1) < this.historyIndex)
                {
                    result += seperator;
                }
            }

            return result;
        }

        /// <inheritdoc />
        public void Update(float elapsedTime)
        {
            this.timeSincelast += elapsedTime;
            if (this.timeSincelast > this.ComboTimeout & this.historyIndex > 0)
            {
                this.OnComboReset?.Invoke(this, new InputKeys(this.History.ToArray()));
                this.historyIndex = 0;
            }

            if (this.SequenceCombos.Match(out var hit, this.History.ToArray()))
            {
                this.OnComboCalled?.Invoke(this, hit);
                this.historyIndex = 0;
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.Tracker.OnKeyDown -= OnKeyDown;
        }

        private void OnKeyDown(object sender, InputKey key)
        {
            if (this.historyIndex >= this.MaxCombo)
            {
                this.historyIndex = 0;
            }
            else
            {
                this.history[this.historyIndex++] = key;
                this.timeSincelast = 0;
            }
        }
    }
}
