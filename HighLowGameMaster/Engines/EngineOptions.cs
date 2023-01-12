using RandomnessService;

namespace HighLowGameMaster.Engines;

public class EngineOptions
{
    /// <summary>
    /// The minimum value.
    /// </summary>
    public int MinimumValue { get; }
    /// <summary>
    /// The maximum value.
    /// </summary>
    public int MaximumValue { get; }
    /// <summary>
    /// The <see cref="IRandomnessService"/>.
    /// </summary>
    public IRandomnessService RandomnessService { get; }
    /// <summary>
    /// Should the Engine allow the Mystery Number to be equals the minimum or maximum values.
    /// </summary>
    public bool ShouldExcludeBoundaries { get; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="minimumValue">Minimum value</param>
    /// <param name="maximumValue">Maximum value</param>
    /// <param name="randomnessService">Randomness provider</param>
    /// <param name="shouldExcludeBoundaries">Defines if Mystery Number can be equals the Minimum or Maximum value. </param>
    public EngineOptions(int minimumValue, int maximumValue, IRandomnessService randomnessService, bool shouldExcludeBoundaries = false)
    {
        MinimumValue = minimumValue;
        MaximumValue = maximumValue;
        RandomnessService = randomnessService;
        ShouldExcludeBoundaries = shouldExcludeBoundaries;
    }
}