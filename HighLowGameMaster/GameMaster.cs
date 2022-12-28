using FluentResults;
using HighLowGameMaster.Engines;
using RandomnessService;

namespace HighLowGameMaster
{
    public sealed class GameMaster : IGameMaster
    {
        public IEngine GameEngine { get; private set; }

        public int MinimumValue => GameEngine.GameState.MinimumValue;
        public int MaximumValue => GameEngine.GameState.MaximumValue;
        public int MysteryNumber => GameEngine.GameState.MysteryNumber;

        public GameMaster(int minimumValue, int maximumValue, IRandomnessService randomnessService)
        {
            GameEngine = new DefaultEngine(minimumValue, maximumValue, randomnessService);
            GameEngine.StartNewRound();
        }

        public GameMaster(IEngine engine)
        {
            GameEngine = engine;
        }

        public Result<ResponseToGuess> ValidateGuess(int guess)
        {
            return GameEngine.ValidateGuess(guess);
        }

        public void StartNewRound()
        {
            GameEngine.StartNewRound();
        }

        public void SetEngine(IEngine engine)
        {
            GameEngine = engine;
        }
    }
}
