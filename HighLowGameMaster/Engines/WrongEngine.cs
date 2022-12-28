using FluentResults;
using RandomnessService;

namespace HighLowGameMaster.Engines
{
    public sealed class WrongEngine : Engine
    {
        public WrongEngine(int minimumValue, int maximumValue, IRandomnessService randomnessService)
            : base(minimumValue, maximumValue, randomnessService)
        { }

        public override Result<ResponseToGuess> ValidateGuess(int guess)
        {
            if (guess < GameState.MinimumValue) return Result.Fail($"Guess {guess} is below minimum: {GameState.MinimumValue}");
            if (guess > GameState.MaximumValue) return Result.Fail($"Guess {guess} is above maximum: {GameState.MaximumValue}");

            if (guess < GameState.MysteryNumber)
                return ResponseToGuess.Low;

            if (guess > GameState.MysteryNumber)
                return ResponseToGuess.High;

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
        protected override int PickRandomNumberBetween(int minValue, int maxValue)
        {
            return RandomnessService.Next(minValue, maxValue + 1);
        }
    }
}
