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
        /// <summary>
        /// The Game Engine.
        /// </summary>
        public IEngine GameEngine { get; private set; }

        /// <summary>
        /// Minimum possible value.
        /// </summary>
        public int MinimumValue => GameEngine.GameState.MinimumValue;

        /// <summary>
        /// Maximum possible value.
        /// </summary>
        public int MaximumValue => GameEngine.GameState.MaximumValue;

        /// <summary>
        /// The Mystery Number picked.
        /// </summary>
        public int MysteryNumber => GameEngine.GameState.MysteryNumber;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="minimumValue">Minimum possible value</param>
        /// <param name="maximumValue">Maximum possible value</param>
        /// <param name="randomnessService">The randomness provider</param>
        public GameMaster(int minimumValue, int maximumValue, IRandomnessService randomnessService)
        {
            GameEngine = new DefaultEngine(minimumValue, maximumValue, randomnessService);
            GameEngine.StartNewRound();
        }

        /// <summary>
        /// Constructor with a new IEngine.
        /// </summary>
        /// <param name="engine">The Engine.</param>
        public GameMaster(IEngine engine)
        {
            GameEngine = engine;
        }

        /// <summary>
        /// Validate the player's guess.
        /// </summary>
        /// <param name="guess">Player's guess</param>
        /// <returns>The response to the guess.</returns>
        public Result<ResponseToGuess> ValidateGuess(int guess)
        {
            return GameEngine.ValidateGuess(guess);
        }

        /// <summary>
        /// Start a new round.
        /// </summary>
        public void StartNewRound()
        {
            GameEngine.StartNewRound();
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
