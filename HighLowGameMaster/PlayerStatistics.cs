namespace HighLowGameMaster;

/// <summary>
/// Represents the statistics for any given player.
/// </summary>
public readonly struct PlayerStatistics
{
    private readonly List<int> _guessHistory = new();

    /// <summary>
    /// The statistics regards this player.
    /// </summary>
    public string PlayerName { get; }

    /// <summary>
    /// Number os guesses the user made so far.
    /// </summary>
    public int NumberOfGuesses  => _guessHistory.Count;

    public PlayerStatistics(string user, int firstGuess)
    {
        PlayerName = user;
        _guessHistory.Add(firstGuess);
    }

    /// <summary>
    /// Keep the history of all players guesses.
    /// </summary>
    /// <param name="guess">The player's guess.</param>
    public void RecordGuess(int guess)
    {
        _guessHistory.Add(guess);
    }

    /// <summary>
    /// Return the guess history for the current <see cref="PlayerName"/>.
    /// </summary>
    /// <returns>The guess history.</returns>
    public IReadOnlyCollection<int> GetGuessHistory() => _guessHistory.ToList();
}