using FluentResults;

namespace HighLowGameMaster
{
    public interface IGameMaster
    {
        int MaximumValue { get; }
        int MinimumValue { get; }
        int MisteryNumber { get; }

        void StartNewRound();
        Result<ResponseToGuess> ValidateGuess(int guess);
    }
}