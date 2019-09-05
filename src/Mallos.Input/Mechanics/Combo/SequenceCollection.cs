namespace Mallos.Input.Mechanics.Combo
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

    public class SequenceCollection : ObservableCollection<SequenceCombo>
    {
        /// <summary>
        /// Gets all the combos names that we have.
        /// </summary>
        public List<string> Keys { get; private set; } = new List<string>();

        /// <summary>
        /// Helpers methods for making it easier to add a new sequence.
        /// </summary>
        public void Add(string name, params InputKey[] keys)
        {
            base.Add(new SequenceCombo(name, keys));
        }

        /// <summary>
        /// Returns wether or not there is a full match.
        /// </summary>
        public bool Match(out SequenceCombo hit, IEnumerable<InputKey> keys)
        {
            foreach (var item in this.Items)
            {
                if (item.KeysMatch(keys))
                {
                    hit = item;
                    return true;
                }
            }

            hit = default(SequenceCombo);
            return false;
        }

        /// <summary>
        /// Search all comboes and return the ones that start with the same sequence.
        /// </summary>
        /// <return>The found combos; otherwise empty array if nothing was found.</return>
        public SequenceCombo[] Search(InputKeys keys) => this.Search(keys.Keys);

        /// <summary>
        /// Search all comboes and return the ones that start with the same sequence.
        /// </summary>
        /// <return>The found combos; otherwise empty array if nothing was found.</return>
        public SequenceCombo[] Search(params InputKey[] keys)
        {
            var result = new List<SequenceCombo>();
            var query = new InputKeyIndex(keys);

            foreach (var item in this.Items)
            {
                if (item.Index.Match(query, true))
                {
                    result.Add(item);
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// Fuzzy Search all comboes and return the ones that start with the same sequence.
        /// </summary>
        /// <return>The found combos; otherwise null if nothing was found</return>
        public SequenceCombo[] FuzzySearch(int fuzzy, InputKeys keys)
            => this.FuzzySearch(fuzzy, keys.Keys);

        /// <summary>
        /// Fuzzy Search all comboes and return the ones that start with the same sequence.
        /// </summary>
        /// <return>The found combos; otherwise null if nothing was found</return>
        public SequenceCombo[] FuzzySearch(int fuzzy, params InputKey[] keys)
        {
            var result = new List<SequenceCombo>();
            var query = new InputKeyIndex(keys);

            foreach (var item in this.Items)
            {
                var queryResult = item.Index.FuzzyMatch(query, fuzzy);
                if (queryResult == InputKeyMatch.FullMatch ||
                    queryResult == InputKeyMatch.PartialMatch)
                {
                    result.Add(item);
                }
            }

            return result.ToArray();
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            this.Keys = new List<string>(this.Items.Select(e => e.Name));
            base.OnCollectionChanged(args);
        }
    }
}
