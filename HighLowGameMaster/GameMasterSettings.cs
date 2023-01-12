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
        /// Defines if Mystery Number can be equals the Minimum or Maximum value.
        /// </summary>
        public bool ShouldExcludeBoundaries { get; set; } = false;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GameMasterSettings() { }

        /// <summary>
        /// Constructor with initial values.
        /// </summary>
        /// <param name="minimumValue">The minimum value</param>
        /// <param name="maximumValue">The maximum value</param>
        /// <param name="shouldExcludeBoundaries">Defines if Mystery Number can be equals the Minimum or Maximum value.</param>
        public GameMasterSettings(int minimumValue, int maximumValue, bool shouldExcludeBoundaries = false)
        {
            MinimumValue = minimumValue;
            MaximumValue = maximumValue;
            ShouldExcludeBoundaries = shouldExcludeBoundaries;
        }
    }
}
