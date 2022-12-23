using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighLowGameMaster.Engines
{
    public class DefaultEngine
    {
        public int MinimumValue { get; }
        public int MaximumValue { get; }
        public int MisteryNumber { get; }

        public DefaultEngine(int minimumValue, int maximumValue)
        {
            if (minimumValue > maximumValue)
                throw new ArgumentOutOfRangeException($"The {nameof(maximumValue)} must be less than or equal to the {nameof(minimumValue)}.");

            MinimumValue = minimumValue;
            MaximumValue = maximumValue;
            MisteryNumber = PickRandomNumberBetween(MinimumValue, MaximumValue);
        }

        /// <summary>
        /// Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The inclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
        /// <returns>A 32-bit signed integer greater than or equal to minValue and less than or equal maxValue.</returns>
        private static int PickRandomNumberBetween(int minValue, int maxValue)
        {
            return Random.Shared.Next(minValue, maxValue + 1);
        }
    }
}
