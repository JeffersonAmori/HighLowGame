using Microsoft.AspNetCore.SignalR;

namespace HighLowGame.Hubs
{
    public class GameHub : Hub
    {
        public async Task Guess(string user, string number)
        {
            string message = $"{user} guesses {number}";
            await Clients.All.SendAsync("WriteToPage", message);
        }
    }
}
