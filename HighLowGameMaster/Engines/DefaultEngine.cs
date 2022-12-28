using FluentResults;

namespace HighLowGameMaster.Engines
{
    public class DefaultEngine : IEngine
    {
        private int _minValue;
        private int _maxValue;

        public GameState GameState { get; private set; }

        public DefaultEngine(int minimumValue, int maximumValue)
        {
            if (minimumValue > maximumValue)
                throw new ArgumentOutOfRangeException($"The {nameof(maximumValue)} must be less than or equal to the {nameof(minimumValue)}.");

            _minValue = minimumValue;
            _maxValue = maximumValue;

            StartNewRound();
        }

        public void StartNewRound()
        {
            var MysteryNumber = PickRandomNumberBetween(_minValue, _maxValue);
            GameState = new GameState(_minValue, _maxValue, MysteryNumber);
        }

        public Result<ResponseToGuess> ValidateGuess(int guess)
        {
            if (guess < GameState.MinimumValue) return Result.Fail($"Guess {guess} is below minimum: {GameState.MinimumValue}");
            if (guess > GameState.MaximumValue) return Result.Fail($"Guess {guess} is above maximum: {GameState.MaximumValue}");

            if (guess < GameState.MysteryNumber)
                return ResponseToGuess.High;

            if (guess > GameState.MysteryNumber)
                return ResponseToGuess.Low;

            if (guess == GameState.MysteryNumber)
                return ResponseToGuess.Correct;

            return ResponseToGuess.Unknown;
        }

        /// <summary>
        /// Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The inclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
        /// <returns>A 32-bit signed integer greater than or equal to minValue and less than or equal maxValue.</returns>
        private static int PickRandomNumberBetween(int minValue, int maxValue)
        {
            return System.Random.Shared.Next(minValue, maxValue + 1);
        }
    }
}
