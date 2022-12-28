using FluentResults;
using RandomnessService;

namespace HighLowGameMaster.Engines
{
    public abstract class Engine : IEngine
    {
        protected readonly int MinValue;
        protected readonly int MaxValue;
        protected readonly IRandomnessService RandomnessService;

        public GameState GameState { get; protected set; }

        public Engine(int minimumValue, int maximumValue, IRandomnessService randomnessService)
        {
            MinValue = minimumValue;
            MaxValue = maximumValue;
            RandomnessService = randomnessService;

            StartNewRound();
        }

        public void StartNewRound()
        {
            var mysteryNumber = PickRandomNumberBetween(MinValue, MaxValue);
            GameState = new GameState(MinValue, MaxValue, mysteryNumber);
        }

        public abstract Result<ResponseToGuess> ValidateGuess(int guess);

        /// <summary>
        /// Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The inclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
        /// <returns>A 32-bit signed integer greater than or equal to minValue and less than or equal maxValue.</returns>
        protected abstract int PickRandomNumberBetween(int minValue, int maxValue);
    }
}
