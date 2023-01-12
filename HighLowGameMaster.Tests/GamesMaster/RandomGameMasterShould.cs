using FluentAssertions;
using FluentResults;
using RandomnessService;
using RandomnessService.Providers;

namespace HighLowGameMaster.Tests.GamesMaster
{
    public sealed class RandomGameMasterShould
    {
        GameMaster? _gameMaster;
        private const string RandomUser = nameof(RandomUser);

        [SetUp]
        public void Setup()
        {
            var gameMasterFactory = new GameMasterFactory(new GameMasterSettings(minimumValue: 10, maximumValue: 50, shouldExcludeBoundaries: true));
            _gameMaster = gameMasterFactory.CreateGameMaster(GameMasterEngines.Random, new NeverRepeatRandomnessService(new PeanutButterProvider()));
        }

        [Test]
        public void NotAcceptNumberBelowMinimum()
        {
            // Act
            Result<ResponseToGuess> result = _gameMaster.ValidateGuess(RandomUser, _gameMaster.MinimumValue - 1);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Test]
        public void NotAcceptNumberAboveMaximum()
        {
            // Act
            Result<ResponseToGuess> result = _gameMaster.ValidateGuess(RandomUser, _gameMaster.MaximumValue + 1);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Test]
        public void ShouldPickNumberBetweenMinimumAndMaximum()
        {
            // Assert
            _gameMaster.MysteryNumber.Should().BeGreaterThanOrEqualTo(_gameMaster.MinimumValue);
            _gameMaster.MysteryNumber.Should().BeLessThanOrEqualTo(_gameMaster.MaximumValue);
        }

        [Test]
        public void ShouldSayHighOrLow_When_GuessIsWrong()
        {
            // Act
            var result = _gameMaster.ValidateGuess(RandomUser, _gameMaster.MysteryNumber + 1);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeOneOf(ResponseToGuess.High, ResponseToGuess.Low);
        }

        [Test]
        public void ShouldSayCorrect_When_GuessEqualsMysteryNumber()
        {
            // Act
            Result<ResponseToGuess> result = _gameMaster.ValidateGuess(RandomUser, _gameMaster.MysteryNumber);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(ResponseToGuess.Correct);
        }

        [Test]
        public void ShouldPickNewMysteryNumber_When_StartingNewRound()
        {
            // Set up
            int oldMysteryNumber = _gameMaster.MysteryNumber;

            // Act
            _gameMaster.StartNewRound();

            // Assert
            _gameMaster.MysteryNumber.Should().NotBe(oldMysteryNumber);
        }
    }
}
