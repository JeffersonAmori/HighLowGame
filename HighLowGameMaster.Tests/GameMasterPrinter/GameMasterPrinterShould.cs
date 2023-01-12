using FluentAssertions;
using HighLowGameMaster.Engines;
using Moq;
using RandomnessService;
using RandomnessService.Providers;

namespace HighLowGameMaster.Tests
{
    [TestFixture]
    public sealed class GameMasterPrinterShould
    {

        [Test]
        public void NotAcceptNumberBelowMinimum()
        {
            // Arrange
            IGameMaster gameMaster = new GameMaster(new DefaultEngine(new EngineOptions(10, 30, Mock.Of<IRandomnessService>())));
            List<string> users = new() { "Jon Doe", "Jane Doe", "Mark" };
            foreach (var user in users)
            {
                for (int i = 0; i < Random.Shared.Next(3, 5); i++)
                {
                    gameMaster.ValidateGuess(user, Random.Shared.Next(gameMaster.MinimumValue, gameMaster.MaximumValue + 1));
                }
            }

            // Act
            string printableStatistics = gameMaster.PrintStatistics();
            var playerStatisticsMap = gameMaster.CurrentRoundState.GetPlayerStatisticsMap();

            // Assert
            printableStatistics.Should().ContainAll(playerStatisticsMap.Keys);
            printableStatistics.Should()
                .ContainAll(playerStatisticsMap.Values.SelectMany(x =>
                    x.GetGuessHistory()
                        .Select(y => y.ToString())));
        }
    }
}
