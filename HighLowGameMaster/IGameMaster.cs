using FluentResults;
using HighLowGameMaster.Engines;

namespace HighLowGameMaster
{
    /// <summary>
    /// The <see cref="GameMaster"/>'s interface.
    /// </summary>
    public interface IGameMaster
    {
        /// <summary>
        /// The state of the current round.
        /// </summary>
        public GameState CurrentRoundState { get; }

        /// <summary>
        /// The maximum value
        /// </summary>
        int MaximumValue { get; }

        /// <summary>
        /// The minimum value
        /// </summary>
        int MinimumValue { get; }

        /// <summary>
        /// The Mystery Number picked
        /// </summary>
        int MysteryNumber { get; }

        /// <summary>
        /// Sets a new <see cref="IEngine"/>.
        /// </summary>
        /// <param name="engine"></param>
        void SetEngine(IEngine engine);

        /// <summary>
        /// Start a new round.
        /// </summary>
        void StartNewRound();

        /// <summary>
        /// Validate the player's guess.
        /// </summary>
        /// <param name="user">Player's name</param>
        /// <param name="guess">Player's guess</param>
        /// <returns>The response to the guess.</returns>
        Result<ResponseToGuess> ValidateGuess(string user, int guess);
    }
}