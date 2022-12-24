using FluentResults;
using HighLowGameMaster;
using Microsoft.AspNetCore.SignalR;

namespace HighLowGame.Hubs
{
    public class GameHub : Hub
    {
        private readonly IGameMaster _gameMaster;

        public GameHub(IGameMaster gameMaster)
        {
            _gameMaster = gameMaster;
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
                await WriteToPage("Starting new round!");
                _gameMaster.StartNewRound();
                await WriteToPage("New mistery number picked!");
            }
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
    }
}
