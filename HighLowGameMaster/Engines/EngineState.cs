namespace HighLowGameMaster.Engines
{
    /// <summary>
    /// The game state.
    /// </summary>
    /// <param name="MinimumValue">The minimum value</param>
    /// <param name="MaximumValue">The maximum value</param>
    /// <param name="MysteryNumber">The Mystery Number picked.</param>
    public record EngineState(int MinimumValue, int MaximumValue, int MysteryNumber);
}
