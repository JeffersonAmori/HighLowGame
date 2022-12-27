using HighLowGameMaster.Engines;
using Microsoft.Extensions.Options;

namespace HighLowGameMaster
{
    public class GameMasterFactory
    {
        private readonly int minimumValue;
        private readonly int maximumValue;

        public GameMasterFactory(IOptions<GameMasterSettings> gameMasterSettings)
        {
            minimumValue = gameMasterSettings.Value.MinimumValue;
            maximumValue = gameMasterSettings.Value.MaximumValue;
        }

        public GameMaster CreateGameMaster(GameMasterEngines gameMasterEngine)
        {
            return gameMasterEngine switch
            {
                GameMasterEngines.Default => new GameMaster(new DefaultEngine(minimumValue, maximumValue)),
                GameMasterEngines.Wrong => new GameMaster(new WrongEngine(minimumValue, maximumValue)),
                GameMasterEngines.Random => new GameMaster(new RandomEngine(minimumValue, maximumValue)),
                _ => throw new ArgumentException($"A Game Master cannot be created with the engine {gameMasterEngine}.", nameof(gameMasterEngine))
            };
        }
    }
}
