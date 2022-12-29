namespace HighLowGameMaster
{
    /// <summary>
    /// The settings for the <see cref="GameMaster"/>.
    /// </summary>
    public sealed class GameMasterSettings
    {
        /// <summary>
        /// The section name in the appsettings.json.
        /// </summary>
        public const string GameMaster = nameof(GameMaster);
        /// <summary>
        /// The minimum value.
        /// </summary>
        public int MinimumValue { get; set; }
        /// <summary>
        /// The maximum value.
        /// </summary>
        public int MaximumValue { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GameMasterSettings() { }

        /// <summary>
        /// Constructor with initial values.
        /// </summary>
        /// <param name="minimumValue">The minimum value</param>
        /// <param name="maximumValue">The maximum value</param>
        public GameMasterSettings(int minimumValue, int maximumValue)
        {
            MinimumValue = minimumValue;
            MaximumValue = maximumValue;
        }
    }
}
