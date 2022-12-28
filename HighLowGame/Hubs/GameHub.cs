using FluentResults;
using HighLowGame.Extensions;
using HighLowGameMaster;
using Microsoft.AspNetCore.SignalR;
using RandomnessService;

namespace HighLowGame.Hubs
{
    public sealed class GameHub : Hub
    {
        private static IGameMaster? _gameMaster;
        private static readonly string _noUser;
        private readonly GameMasterFactory _gameMasterFactory;
        private readonly IRandomnessService _randomnessService;

        static GameHub()
        {
            _noUser = Guid.NewGuid().ToString();
        }

        public GameHub(GameMasterFactory gameMasterFactory, IRandomnessService randomnessService)
        {
            _gameMasterFactory = gameMasterFactory;
            _randomnessService = randomnessService;
            _gameMaster ??= _gameMasterFactory.CreateGameMaster(GameMasterEngines.Default, randomnessService);
        }

        public async Task Guess(string user, string guess)
        {
            if (!int.TryParse(guess, out int guessedNumber))
            {
                var errorMessage = $"Your guess {guess} should be a number (ex: 10).";
                await WriteToPageAsync(user, errorMessage);
                return;
            }

            await WriteToPageAsync(user, $"{user} guesses {guess}.");
            var responseToGuess = _gameMaster.ValidateGuess(guessedNumber);
            await WriteToPageAsync(_noUser, MessageFrom(responseToGuess));

            if (responseToGuess.Value == ResponseToGuess.Correct)
            {
                await CelebrateAsync(user);
                await StartNewRoundAsync();
            }
        }

        public async Task EngineChanged(string user, string newEngine)
        {
            if (string.IsNullOrEmpty(user))
                user = "an anonymous person";

            var newSelectedEngine = newEngine.ToEnum<GameMasterEngines>();
            _gameMaster = _gameMasterFactory.CreateGameMaster(newSelectedEngine, _randomnessService);

            await WriteToPageAsync(user, $"The new engine {newEngine} has been selected by {user}.");
            await UpdateEngineAsync(newEngine);
            await StartNewRoundAsync();
        }

        private async Task StartNewRoundAsync()
        {
            await WriteToPageAsync(_noUser, $"Starting new round! (Min: {_gameMaster.MinimumValue} - Max: {_gameMaster.MaximumValue}");
            _gameMaster.StartNewRound();
            await WriteToPageAsync(_noUser, "New Mystery number picked!");
        }

        private static string MessageFrom(Result<ResponseToGuess> responseToGuess)
        {
            if (responseToGuess.IsFailed)
            {
                return string.Join(", ", responseToGuess.Errors.Select(x => x.Message));
            }

            return responseToGuess.Value switch
            {
                ResponseToGuess.High => "HI: the mystery number is bigger than your guess!",
                ResponseToGuess.Low => "LO: the mystery number is smaller than your guess!",
                ResponseToGuess.Correct => "CORRECT: your guess is absolutely right!",
                _ => "Jeffrey Epstein didn't kill himself.",
            };
        }

        private async Task WriteToPageAsync(string user, string message)
        {
            await Clients.All.SendAsync("WriteToPageAsync", user, message);
        }

        private async Task CelebrateAsync(string user)
        {
            await Clients.All.SendAsync("CelebrateAsync", user);
        }

        private async Task UpdateEngineAsync(string newEngine)
        {
            await Clients.All.SendAsync("UpdateEngineAsync", newEngine);
        }
    }
}
