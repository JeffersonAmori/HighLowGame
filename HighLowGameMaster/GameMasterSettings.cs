namespace HighLowGameMaster
{
    public sealed class GameMasterSettings
    {
        public const string GameMaster = nameof(GameMaster);
        public int MinimumValue { get; set; }
        public int MaximumValue { get; set; }

        public GameMasterSettings(int minimumValue, int maximumValue)
        {
            MinimumValue = minimumValue;
            MaximumValue = maximumValue;
        }
    }
}
