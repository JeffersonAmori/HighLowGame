using FluentResults;
using HighLowGameMaster;
using Microsoft.AspNetCore.SignalR;
using HighLowGame.Extensions;

namespace HighLowGame.Hubs
{
    public class GameHub : Hub
    {
        private static IGameMaster _gameMaster;
        private GameMasterFactory _gameMasterFactory;

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
                await WriteToPage(errorMessage);
                return;
            }

            await WriteToPage($"{user} guesses {guess}.");
            var responseToGuess = _gameMaster.ValidateGuess(guessedNumber);
            await WriteToPage(MessageFrom(responseToGuess));

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

            await WriteToPage($"The new engine {newEngine} has been selected by {user}.");
            await UpdateEngine(newEngine);
            await StartNewRound();
        }

        private async Task StartNewRound()
        {
            await WriteToPage("Starting new round!");
            _gameMaster.StartNewRound();
            await WriteToPage("New mistery number picked!");
        }

        private static string MessageFrom(Result<ResponseToGuess> responseToGuess)
        {
            return responseToGuess.Value switch
            {
                ResponseToGuess.High => "HI: the mystery number is bigger than your guess!",
                ResponseToGuess.Low => "LO: the mystery number is smaller than your guess!",
                ResponseToGuess.Correct => "CORRECT: your guess is absolutely right!",
                _ => "Jeffrey Epstein didn't kill himself.",
            };
        }

        private async Task WriteToPage(string errorMessage)
        {
            await Clients.All.SendAsync("WriteToPage", errorMessage);
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
