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
                await WriteToPage(user, errorMessage);
                return;
            }

            await WriteToPage(user, $"{user} guesses {guess}.");
            var responseToGuess = _gameMaster.ValidateGuess(guessedNumber);
            await WriteToPage(_noUser, MessageFrom(responseToGuess));

            if (responseToGuess.Value == ResponseToGuess.Correct)
            {
                await Celebrate(user);
                await StartNewRound();
            }
        }

        public async Task EngineChanged(string user, string newEngine)
        {
            if (string.IsNullOrEmpty(user))
                user = "an anonymous person";

            var newSelectedEngine = newEngine.ToEnum<GameMasterEngines>();
            _gameMaster = _gameMasterFactory.CreateGameMaster(newSelectedEngine, _randomnessService);

            await WriteToPage(user, $"The new engine {newEngine} has been selected by {user}.");
            await UpdateEngine(newEngine);
            await StartNewRound();
        }

        private async Task StartNewRound()
        {
            await WriteToPage(_noUser, "Starting new round!");
            _gameMaster.StartNewRound();
            await WriteToPage(_noUser, "New Mystery number picked!");
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

        private async Task WriteToPage(string user, string message)
        {
            await Clients.All.SendAsync("WriteToPage", user, message);
        }

        private async Task Celebrate(string user)
        {
            await Clients.All.SendAsync("Celebrate", user);
        }

        private async Task UpdateEngine(string newEngine)
        {
            await Clients.All.SendAsync("UpdateEngine", newEngine);
        }
    }
}
