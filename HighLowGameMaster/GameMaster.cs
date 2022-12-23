using FluentResults;
using HighLowGameMaster.Engines;

namespace HighLowGameMaster
{
    public class GameMaster : IGameMaster
    {
        public IEngine GameEngine { get; private set; }

        public int MinimumValue => GameEngine.GameState.MinimumValue;
        public int MaximumValue => GameEngine.GameState.MaximumValue;
        public int MisteryNumber => GameEngine.GameState.MisteryNumber;

        public GameMaster(int minimumValue, int maximumValue)
        {
            GameEngine = new DefaultEngine(minimumValue, maximumValue);
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
