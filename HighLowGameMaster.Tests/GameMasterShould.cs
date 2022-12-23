using FluentAssertions;
using FluentResults;

namespace HighLowGameMaster.Tests
{
    [TestFixture]
    public class GameMasterShould
    {
        GameMaster _gameMaster;

        [SetUp]
        public void Setup()
        {
            _gameMaster = new GameMaster(10, 50);
        }

        [Test]
        public void NotAcceptNumberBelowMinimum()
        {
            // Act
            Result<ResponseToGuess> result = _gameMaster.ValidateGuess(_gameMaster.MinimumValue - 1);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Test]
        public void NotAcceptNumberAboveMaximum()
        {
            // Act
            Result<ResponseToGuess> result = _gameMaster.ValidateGuess(_gameMaster.MaximumValue + 1);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Test]
        public void ShouldPickNumberBetweenMinimumAndMaximum()
        {
            // Assert
            _gameMaster.MisteryNumber.Should().BeGreaterThanOrEqualTo(_gameMaster.MinimumValue);
            _gameMaster.MisteryNumber.Should().BeLessThanOrEqualTo(_gameMaster.MaximumValue);
        }

        [Test]
        public void ShouldSayHigh_When_GuessIsBelowMysteryNumber()
        {
            // Act
            Result<ResponseToGuess> result = _gameMaster.ValidateGuess(_gameMaster.MisteryNumber - 1);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(ResponseToGuess.High);
        }

        [Test]
        public void ShouldSayLow_When_GuessIsAboveMysteryNumber()
        {
            // Act
            Result<ResponseToGuess> result = _gameMaster.ValidateGuess(_gameMaster.MisteryNumber + 1);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(ResponseToGuess.Low);
        }

        [Test]
        public void ShouldSayCorrect_When_GuessEqualsMysteryNumber()
        {
            // Act
            Result<ResponseToGuess> result = _gameMaster.ValidateGuess(_gameMaster.MisteryNumber);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(ResponseToGuess.Correct);
        }

        [Test]
        public void ShouldPickNewMysteryNumber_When_StartingNewRound()
        {
            // Set up
            int oldMisteryNumber = _gameMaster.MisteryNumber;

            // Act
            _gameMaster.StartNewRound();

            // Assert
            _gameMaster.MisteryNumber.Should().NotBe(oldMisteryNumber);
        }
    }
}