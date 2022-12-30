namespace HighLowGameMaster
{
    public static class GameMasterPrinter
    {
        public static string PrintStatistics(this IGameMaster gameMaster)
        {
            var playerStatsAsString = gameMaster
                .CurrentRoundState
                .GetPlayerStatisticsMap()
                .Select(pair =>
                    $"{pair.Key} made {pair.Value.NumberOfGuesses} guesses ({string.Join(',', pair.Value.GetGuessHistory())}). ");

            var printableText =
@$"On this round: 
{string.Concat(playerStatsAsString)}
";
            return printableText;
        }
    }
}
