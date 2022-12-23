using FluentResults;
using HighLowGameMaster.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HighLowGameMaster
{
    public class GameMaster : IGameMaster
    {
        private DefaultEngine gameStrategy;

        public int MinimumValue => gameStrategy.MinimumValue;
        public int MaximumValue => gameStrategy.MaximumValue;
        public int MisteryNumber => gameStrategy.MisteryNumber;

        public GameMaster(int minimumValue, int maximumValue)
        {
            gameStrategy = new DefaultEngine(minimumValue, maximumValue);
        }

        public Result<ResponseToGuess> ValidateGuess(int guess)
        {
            if (guess < MinimumValue) return Result.Fail($"Guess {guess} is below minimum: {MinimumValue}");
            if (guess > MaximumValue) return Result.Fail($"Guess {guess} is above maximum: {MaximumValue}");

            if (guess < MisteryNumber)
                return ResponseToGuess.High;

            if (guess > MisteryNumber)
                return ResponseToGuess.Low;

            if (guess == MisteryNumber)
                return ResponseToGuess.Correct;

            return ResponseToGuess.Unknown;
        }
    }
}
