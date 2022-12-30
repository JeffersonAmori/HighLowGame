using FluentResults;
using RandomnessService;

namespace HighLowGameMaster.Engines
{
    /// <summary>
    /// The <see cref="GameMaster"/>'s Engine.
    /// </summary>
    public abstract class Engine : IEngine
    {
        protected readonly int MinValue;
        protected readonly int MaxValue;
        protected readonly IRandomnessService RandomnessService;

        /// <summary>
        /// The game state.
        /// </summary>
        public EngineState EngineState { get; protected set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="minimumValue">Minimum value</param>
        /// <param name="maximumValue">Maximum value</param>
        /// <param name="randomnessService">Randomness provider</param>
        public Engine(int minimumValue, int maximumValue, IRandomnessService randomnessService)
        {
            MinValue = minimumValue;
            MaxValue = maximumValue;
            RandomnessService = randomnessService;

            StartNewRound();
        }

        /// <summary>
        /// Start a new round.
        /// </summary>
        public void StartNewRound()
        {
            var mysteryNumber = PickRandomNumberBetween(MinValue, MaxValue);
            EngineState = new EngineState(MinValue, MaxValue, mysteryNumber);
        }
        
        /// <summary>
        /// Validate the player's guess.
        /// </summary>
        /// <param name="guess">Player's guess</param>
        /// <returns>The response to the guess.</returns>
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
