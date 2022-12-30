using FluentResults;
using RandomnessService;

namespace HighLowGameMaster.Engines
{
    /// <summary>
    /// Random Engine - Randomly answer High or Low to the user's guess.
    /// </summary>
    public sealed class RandomEngine : Engine
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="minimumValue">Minimum value</param>
        /// <param name="maximumValue">Maximum value</param>
        /// <param name="randomnessService">Randomness provider</param>
        public RandomEngine(int minimumValue, int maximumValue, IRandomnessService randomnessService)
            : base(minimumValue, maximumValue, randomnessService)
        { }
        
        /// <summary>
        /// Validate the player's guess.
        /// </summary>
        /// <param name="guess">Player's guess</param>
        /// <returns>The response to the guess.</returns>
        public override Result<ResponseToGuess> ValidateGuess(int guess)
        {
            if (guess < EngineState.MinimumValue) return Result.Fail($"Guess {guess} is below minimum: {EngineState.MinimumValue}");
            if (guess > EngineState.MaximumValue) return Result.Fail($"Guess {guess} is above maximum: {EngineState.MaximumValue}");

            if (guess < EngineState.MysteryNumber || guess > EngineState.MysteryNumber)
                return PickOneRandomlyFrom(ResponseToGuess.Low, ResponseToGuess.High);

            if (guess == EngineState.MysteryNumber)
                return ResponseToGuess.Correct;

            return ResponseToGuess.Unknown;
        }

        /// <summary>
        /// Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The inclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
        /// <returns>A 32-bit signed integer greater than or equal to minValue and less than or equal maxValue.</returns>
        protected override int PickRandomNumberBetween(int minValue, int maxValue) => RandomnessService.Next(minValue, maxValue);

        private T PickOneRandomlyFrom<T>(params T[] args) => args[RandomnessService.Next(0, args.Length)];
    }
}
