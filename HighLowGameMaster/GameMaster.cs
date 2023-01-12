using FluentResults;
using HighLowGameMaster.Engines;
using RandomnessService;

namespace HighLowGameMaster
{
    /// <summary>
    /// The Game Master is responsible for managing the Hi-Lo Game.
    /// </summary>
    public sealed class GameMaster : IGameMaster
    {
        private int _numberOfRounds = 0;

        /// <summary>
        /// The state of the current round.
        /// </summary>
        public GameState CurrentRoundState { get; }

        /// <summary>
        /// Indicates if it is the first round.
        /// </summary>
        public bool GameInProgress => NumberOfRounds > 0;

        /// <summary>
        /// How many rounds played (includes the current round).
        /// </summary>
        public int NumberOfRounds => _numberOfRounds;

        /// <summary>
        /// The Game Engine.
        /// </summary>
        public IEngine GameEngine { get; private set; }

        /// <summary>
        /// Minimum possible value.
        /// </summary>
        public int MinimumValue => GameEngine.EngineState.MinimumValue;

        /// <summary>
        /// Maximum possible value.
        /// </summary>
        public int MaximumValue => GameEngine.EngineState.MaximumValue;

        /// <summary>
        /// The Mystery Number picked.
        /// </summary>
        public int MysteryNumber => GameEngine.EngineState.MysteryNumber;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="minimumValue">Minimum possible value</param>
        /// <param name="maximumValue">Maximum possible value</param>
        /// <param name="randomnessService">The randomness provider</param>
        /// <param name="shouldExcludeBoundaries">Defines if Mystery Number can be equals the Minimum or Maximum value. </param>
        public GameMaster(int minimumValue, int maximumValue, IRandomnessService randomnessService, bool shouldExcludeBoundaries = false)
        {
            CurrentRoundState = new GameState();
            GameEngine = new DefaultEngine(new EngineOptions(minimumValue, maximumValue, randomnessService, shouldExcludeBoundaries));
            GameEngine.StartNewRound();
        }

        /// <summary>
        /// Constructor with a new IEngine.
        /// </summary>
        /// <param name="engine">The Engine.</param>
        public GameMaster(IEngine engine)
        {
            CurrentRoundState = new GameState();
            GameEngine = engine;
        }

        /// <summary>
        /// Validate the player's guess.
        /// </summary>
        /// <param name="guess">Player's guess</param>
        /// <returns>The response to the guess.</returns>
        public Result<ResponseToGuess> ValidateGuess(string user, int guess)
        {
            CurrentRoundState.RecordGuess(user, guess);
            return GameEngine.ValidateGuess(guess);
        }

        /// <summary>
        /// Start a new round.
        /// </summary>
        public void StartNewRound()
        {
            GameEngine.StartNewRound();
            CurrentRoundState.ResetPlayerStatistics();
            IncrementNumberOfRoundsBy(1);
        }

        private void IncrementNumberOfRoundsBy(int amount)
        {
            Interlocked.Add(ref _numberOfRounds, amount);
        }

        /// <summary>
        /// Set a new Engine.
        /// </summary>
        /// <param name="engine">The new Engine.</param>
        public void SetEngine(IEngine engine)
        {
            GameEngine = engine;
        }
    }
}
