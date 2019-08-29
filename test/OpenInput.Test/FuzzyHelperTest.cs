namespace OpenInput.Test
{
    using Xunit;

    public class FuzzyHelperTest
    {
        [Theory]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 1, 2 }, 0)]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 1, 2, 3, 4 }, 1)]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 1, 2, 2 }, 1)]
        public void StartWith(int[] actual, int[] startWith, int fuzzyCount)
        {
            var result = FuzzyHelper.StartWith(startWith, actual);
            Assert.Equal(fuzzyCount, result);
        }
    }
}
