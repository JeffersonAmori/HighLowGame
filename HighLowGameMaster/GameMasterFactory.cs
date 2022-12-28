using HighLowGameMaster.Engines;
using Microsoft.Extensions.Options;
using RandomnessService;

namespace HighLowGameMaster
{
    public sealed class GameMasterFactory
    {
        private readonly int _minimumValue;
        private readonly int _maximumValue;

        public GameMasterFactory(IOptions<GameMasterSettings> gameMasterSettingsOptions) : this(gameMasterSettingsOptions.Value)
        { }

        public GameMasterFactory(GameMasterSettings gameMasterSettings)
        {
            if (gameMasterSettings == null) throw new ArgumentNullException(nameof(gameMasterSettings));

            if (gameMasterSettings.MinimumValue <= 0)
                throw new ArgumentOutOfRangeException(nameof(gameMasterSettings.MinimumValue),
                    gameMasterSettings.MinimumValue, "The minimum value must be a non-negative.");

            if (gameMasterSettings.MinimumValue > gameMasterSettings.MaximumValue)
                throw new ArgumentOutOfRangeException(
                    $"The {nameof(gameMasterSettings.MaximumValue)} must be less than or equal to the {nameof(gameMasterSettings.MinimumValue)}.");

            _minimumValue = gameMasterSettings.MinimumValue;
            _maximumValue = gameMasterSettings.MaximumValue;
        }

        public GameMaster CreateGameMaster(GameMasterEngines gameMasterEngine, IRandomnessService randomnessService)
        {
            return gameMasterEngine switch
            {
                GameMasterEngines.Default => new GameMaster(new DefaultEngine(_minimumValue, _maximumValue, randomnessService)),
                GameMasterEngines.Wrong => new GameMaster(new WrongEngine(_minimumValue, _maximumValue, randomnessService)),
                GameMasterEngines.Random => new GameMaster(new RandomEngine(_minimumValue, _maximumValue, randomnessService)),
                _ => throw new ArgumentException($"A Game Master cannot be created with the engine {gameMasterEngine}.", nameof(gameMasterEngine))
            };
        }
    }
}
