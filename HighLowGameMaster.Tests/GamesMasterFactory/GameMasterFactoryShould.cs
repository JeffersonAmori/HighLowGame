using FluentAssertions;

namespace HighLowGameMaster.Tests.GamesMasterFactory
{
    [TestFixture]
    public sealed class GameMasterFactoryShould
    {
        [Test]
        public void ThrowException_When_MinimumValueIsLessThanOne()
        {
            // Arrange
            var action = () => { new GameMasterFactory(new GameMasterSettings(minimumValue: -1, maximumValue: 1, shouldExcludeBoundaries: true)); };

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void ThrowException_When_MaximumValue_IsBiggerThan_MinimumValue()
        {
            // Arrange
            var action = () => { new GameMasterFactory(new GameMasterSettings(minimumValue: 10, maximumValue: 5, shouldExcludeBoundaries: true)); };

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}
