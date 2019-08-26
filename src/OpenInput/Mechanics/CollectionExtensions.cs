namespace OpenInput.Mechanics
{
    using System.Collections.Generic;

    public static class CollectionExtensions
    {
        public static void Add(
            this ICollection<SequenceCombo> collection,
            string name,
            params InputKey[] keys)
        {
            collection.Add(new SequenceCombo(name, keys));
        }
    }
}
