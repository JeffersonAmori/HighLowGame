using FluentResults;
using HighLowGame.Extensions;
using HighLowGameMaster;
using LoggerAdapter;
using Microsoft.AspNetCore.SignalR;
using RandomnessService;

namespace HighLowGame.Hubs
{
    public sealed class GameHub : Hub
    {
        private static IGameMaster? _gameMaster;
        private static readonly string GameMasterUser;
        private readonly GameMasterFactory _gameMasterFactory;
        private readonly IRandomnessService _randomnessService;
        private readonly ILoggerAdapter<GameHub> _logger;

        static GameHub()
        {
            GameMasterUser = "Hi-Lo-GameMaster-User-62554875";
        }

        public GameHub(GameMasterFactory gameMasterFactory, IRandomnessService randomnessService, ILoggerAdapter<GameHub> logger)
        {
            _gameMasterFactory = gameMasterFactory;
            _randomnessService = randomnessService;
            _logger = logger;

            if (_gameMaster is null)
            {
                _gameMaster = _gameMasterFactory.CreateGameMaster(GameMasterEngines.Default, randomnessService);
                StartNewRound();
            }
        }

        public async Task NewPlayerConnected(string user)
        {
            _logger.LogInformation("New player connected: {user}", user);
            await WriteToPageAsync(GameMasterUser, $"{user} just connected.");
        }

        public async Task PlayerDisconnected(string user)
        {
            _logger.LogInformation("Player disconnected: {user}", user);
            await WriteToPageAsync(GameMasterUser, $"{user} disconnected.");
        }

        public async Task Guess(string user, string guess)
        {
            _logger.LogInformation("{user} guessed {guess}", user, guess);
            if (!int.TryParse(guess, out int guessedNumber))
            {
                var errorMessage = $"Your guess {guess} should be a number (ex: 10).";
                _logger.LogError(errorMessage);
                await WriteToPageAsync(user, errorMessage);
                return;
            }

            await WriteToPageAsync(user, $"{user} guesses {guess}.");
            var responseToGuess = _gameMaster.ValidateGuess(guessedNumber);
            await WriteToPageAsync(GameMasterUser, MessageFrom(responseToGuess));

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
            await WriteToPageAsync(GameMasterUser, $"Starting new round! (Min: {_gameMaster.MinimumValue} - Max: {_gameMaster.MaximumValue})");
            StartNewRound();
            await WriteToPageAsync(GameMasterUser, "New Mystery number picked!");
        }

        private void StartNewRound()
        {
            _gameMaster.StartNewRound();
            _logger.LogInformation("New round started. Mystery number: {MysteryNumber}.", _gameMaster.MysteryNumber);
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
            _logger.LogInformation("Writing message {message} from {user}", message, user);
            await Clients.All.SendAsync("WriteToPage", user, message);
        }

        private async Task CelebrateAsync(string user)
        {
            _logger.LogInformation("Celebrating user {user}", user);
            await Clients.All.SendAsync("Celebrate", user);
        }

        private async Task UpdateEngineAsync(string newEngine)
        {
            _logger.LogInformation("Updating engine to {newEngine}", newEngine);
            await Clients.All.SendAsync("UpdateEngine", newEngine);
        }
    }
}
