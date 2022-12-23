using FluentResults;

namespace HighLowGameMaster.Engines
{
    public interface IEngine
    {
        GameState GameState { get; }
        void StartNewRound();
        Result<ResponseToGuess> ValidateGuess(int guess);
    }
}