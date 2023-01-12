using FluentResults;

namespace HighLowGameMaster.Engines
{
    /// <summary>
    /// The <see cref="GameMaster"/>'s Engine.
    /// </summary>
    public abstract class Engine : IEngine
    {
        protected readonly EngineOptions _engineOptions;

        /// <summary>
        /// The game state.
        /// </summary>
        public EngineState EngineState { get; protected set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="engineOptions">The Engine Options.</param>
        public Engine(EngineOptions engineOptions)
        {
            _engineOptions = engineOptions;
            StartNewRound();
        }

        /// <summary>
        /// Start a new round.
        /// </summary>
        public void StartNewRound()
        {
            int newMysteryNumber = PickRandomNumberBetween(_engineOptions.MinimumValue, _engineOptions.MaximumValue);
            EngineState = new EngineState(_engineOptions.MinimumValue, _engineOptions.MaximumValue, newMysteryNumber);
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
        protected virtual int PickRandomNumberBetween(int minValue, int maxValue)
        {
            int newMysteryNumber = default;

            do
                newMysteryNumber = PickRandomNumberBetweenInternal(minValue, maxValue);
            while (DoesNotMeetCriteria(newMysteryNumber));

            return newMysteryNumber;
        }

        /// <summary>
        /// Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The inclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
        /// <returns>A 32-bit signed integer greater than or equal to minValue and less than or equal maxValue.</returns>
        protected abstract int PickRandomNumberBetweenInternal(int minValue, int maxValue);

        protected virtual bool DoesNotMeetCriteria(int newMysteryNumber) =>
            _engineOptions.ShouldExcludeBoundaries &&
            (_engineOptions.MinimumValue == newMysteryNumber || _engineOptions.MaximumValue == newMysteryNumber);
    }
}
