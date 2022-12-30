using FluentResults;

namespace HighLowGameMaster.Engines
{
    /// <summary>
    /// The <see cref="GameMaster"/>'s Engine.
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// The game state.
        /// </summary>
        EngineState EngineState { get; }

        /// <summary>
        /// Start a new round.
        /// </summary>
        void StartNewRound();

        /// <summary>
        /// Validate the player's guess.
        /// </summary>
        /// <param name="guess">Player's guess</param>
        /// <returns>The response to the guess.</returns>
        Result<ResponseToGuess> ValidateGuess(int guess);
    }
}