using FluentResults;
using HighLowGameMaster;
using Microsoft.AspNetCore.SignalR;
using HighLowGame.Extensions;

namespace HighLowGame.Hubs
{
    public class GameHub : Hub
    {
        private static IGameMaster _gameMaster;
        private static string _noUser;
        private GameMasterFactory _gameMasterFactory;

        static GameHub()
        {
            _noUser = Guid.NewGuid().ToString();
        }

        public GameHub(GameMasterFactory gameMasterFactory)
        {
            _gameMasterFactory = gameMasterFactory;
            if (_gameMaster is null)
                _gameMaster = _gameMasterFactory.CreateGameMaster(GameMasterEngines.Default);
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
            _gameMaster = _gameMasterFactory.CreateGameMaster(newSelectedEngine);

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
