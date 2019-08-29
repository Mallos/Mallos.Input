namespace OpenInput.Test
{
    using Xunit;
    using OpenInput.Mechanics;
    using OpenInput.Mechanics.Combo;

    public class SequenceCollectionTest
    {
        [Fact]
        public void Match()
        {
            var name = "test";
            var keys = new InputKey[] { Keys.A, Keys.B, Keys.C };

            var sequences = new SequenceCollection();
            sequences.Add(name, keys);

            var match = sequences.Match(out var hit, keys);

            Assert.True(match);
            Assert.Equal(name, hit.Name);
            Assert.Equal(keys, hit.Keys);
        }
    }
}
