using HighLowGameMaster.Engines;
using Microsoft.Extensions.Options;
using RandomnessService;

namespace HighLowGameMaster
{
    /// <summary>
    /// The factory for the <see cref="GameMaster"/> class.
    /// </summary>
    public sealed class GameMasterFactory
    {
        private readonly GameMasterSettings _gameMasterSettings;

        /// <summary>
        /// The constructor that takes <see cref="IOptions{TOptions}"/> from the DI container.
        /// </summary>
        /// <param name="gameMasterSettingsOptions">The Options from the DI container.</param>
        public GameMasterFactory(IOptions<GameMasterSettings> gameMasterSettingsOptions) : this(gameMasterSettingsOptions.Value)
        { }

        /// <summary>
        /// The constructor that takes <see cref="GameMasterSettings"/>.
        /// </summary>
        /// <param name="gameMasterSettings">The setting for the Game Master</param>
        /// <exception cref="ArgumentNullException">The <see cref="GameMasterSettings"/> cannot be null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The minimum should be greater than zero and the maximum should be greater than the minimum.</exception>
        public GameMasterFactory(GameMasterSettings gameMasterSettings)
        {
            if (gameMasterSettings == null) throw new ArgumentNullException(nameof(gameMasterSettings));

            if (gameMasterSettings.MinimumValue <= 0)
                throw new ArgumentOutOfRangeException(nameof(gameMasterSettings.MinimumValue),
                    gameMasterSettings.MinimumValue, "The minimum value must be a non-negative.");

            if (gameMasterSettings.MinimumValue > gameMasterSettings.MaximumValue)
                throw new ArgumentOutOfRangeException(
                    $"The {nameof(gameMasterSettings.MaximumValue)} must be less than or equal to the {nameof(gameMasterSettings.MinimumValue)}.");

            _gameMasterSettings = gameMasterSettings;
        }

        /// <summary>
        /// Create a new Game Master.
        /// </summary>
        /// <param name="gameMasterEngine">The Game Master's <see cref="IEngine"/>.</param>
        /// <param name="randomnessService">The Games Master's <see cref="IRandomnessService"/>.</param>
        /// <returns>A new <see cref="GameMaster"/></returns>
        /// <exception cref="ArgumentException">Throws when <see cref="GameMasterEngines"/> doesn't have a valid value.</exception>
        public GameMaster CreateGameMaster(GameMasterEngines gameMasterEngine, IRandomnessService randomnessService)
        {
            return gameMasterEngine switch
            {
                GameMasterEngines.Default => new GameMaster(new DefaultEngine(new EngineOptions(_gameMasterSettings.MinimumValue, _gameMasterSettings.MaximumValue, randomnessService, _gameMasterSettings.ShouldExcludeBoundaries))),
                GameMasterEngines.Wrong => new GameMaster(new WrongEngine(new EngineOptions(_gameMasterSettings.MinimumValue, _gameMasterSettings.MaximumValue, randomnessService, _gameMasterSettings.ShouldExcludeBoundaries))),
                GameMasterEngines.Random => new GameMaster(new RandomEngine(new EngineOptions(_gameMasterSettings.MinimumValue, _gameMasterSettings.MaximumValue, randomnessService, _gameMasterSettings.ShouldExcludeBoundaries))),
                _ => throw new ArgumentException($"A Game Master cannot be created with the engine {gameMasterEngine}.", nameof(gameMasterEngine))
            };
        }
    }
}
