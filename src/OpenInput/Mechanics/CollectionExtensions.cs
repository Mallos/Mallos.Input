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

        public static void Add(
            this ICollection<InputAction> collection,
            string name,
            Keys keys)  // FIXME: InputKey
        {
            collection.Add(new InputAction(name, keys));
        }

        public static void Add(
            this ICollection<InputAxis> collection,
            string name,
            Keys keys, // FIXME: InputKey
            float value)
        {
            collection.Add(new InputAxis(name, keys, value));
        }
    }
}
