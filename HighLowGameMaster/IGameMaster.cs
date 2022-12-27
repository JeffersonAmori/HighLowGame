using FluentResults;
using HighLowGameMaster.Engines;

namespace HighLowGameMaster
{
    public interface IGameMaster
    {
        int MaximumValue { get; }
        int MinimumValue { get; }
        int MisteryNumber { get; }

        void SetEngine(IEngine engine);
        void StartNewRound();
        Result<ResponseToGuess> ValidateGuess(int guess);
    }
}