using System.Collections.Concurrent;
using System.Collections.Immutable;

namespace HighLowGameMaster
{
    /// <summary>
    /// Represents the state of the game at any given moment.
    /// </summary>
    public class GameState
    {
        private readonly ConcurrentDictionary<string, PlayerStatistics> _playersStatistics = new();

        /// <summary>
        /// Keep the history of all players guesses.
        /// </summary>
        /// <param name="user">The user that made the guess.</param>
        /// <param name="guess">The guess made by the user.</param>
        public void RecordGuess(string user, int guess)
        {
            _playersStatistics.AddOrUpdate(user, new PlayerStatistics(user, firstGuess: guess), (key, playerStatistics) =>
            {
                playerStatistics.RecordGuess(guess);
                return playerStatistics;
            });
        }

        /// <summary>
        /// Clears the history for all players.
        /// </summary>
        public void ResetPlayerStatistics()
        {
            _playersStatistics.Clear();
        }

        /// <summary>
        /// Get the statistics from all the players.
        /// </summary>
        /// <returns>The map with all players statistics.</returns>
        public ImmutableDictionary<string, PlayerStatistics> GetPlayerStatisticsMap()
        {
            return _playersStatistics.ToImmutableDictionary();
        }
    }
}
