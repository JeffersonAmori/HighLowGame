using FluentResults;

namespace HighLowGameMaster
{
    public interface IGameMaster
    {
        int MaximumValue { get; }
        int MinimumValue { get; }
        int MisteryNumber { get; }

        Result<ResponseToGuess> ValidateGuess(int guess);
    }
}