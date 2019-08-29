namespace OpenInput.Test
{
    using Xunit;
    using OpenInput.Mechanics;
    using System.Collections.Generic;

    public class InputKeyIndexTest
    {
        public static IEnumerable<object[]> MatchData =>
            new List<object[]>
            {
                new object[] { new InputKey[] { Keys.A }, new InputKey[] { Keys.A }, true },
                new object[] { new InputKey[] { Keys.A }, new InputKey[] { Keys.A, Keys.B }, false },
                new object[] { new InputKey[] { Keys.A, Keys.B }, new InputKey[] { Keys.A }, false },
            };

        [Theory]
        [MemberData(nameof(MatchData))]
        public void Match(InputKey[] actualKeys, InputKey[] expectedKeys, bool expected)
        {
            var actualSet = new InputKeyIndex(actualKeys);
            var expectedSet = new InputKeyIndex(expectedKeys);

            var result = actualSet.Match(expectedSet);

            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> FuzzyMatchData =>
            new List<object[]>
            {
                new object[] { new InputKey[] { Keys.A }, new InputKey[] { Keys.A }, InputKeyMatch.FullMatch },
                new object[] { new InputKey[] { Keys.A }, new InputKey[] { Keys.A, Keys.B }, InputKeyMatch.PartialMatch },
                new object[] { new InputKey[] { Keys.A, Keys.B }, new InputKey[] { Keys.A }, InputKeyMatch.NoMatch },
            };

        [Theory]
        [MemberData(nameof(FuzzyMatchData))]
        public void FuzzyMatch(InputKey[] actualKeys, InputKey[] expectedKeys, InputKeyMatch expected)
        {
            var actualSet = new InputKeyIndex(actualKeys);
            var expectedSet = new InputKeyIndex(expectedKeys);

            var result = actualSet.FuzzyMatch(expectedSet);

            Assert.Equal(expected, result);
        }
    }
}
