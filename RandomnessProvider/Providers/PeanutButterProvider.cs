using PeanutButter.RandomGenerators;

namespace RandomnessService.Providers
{
    public sealed class PeanutButterProvider : IRandomnessProvider
    {
        public int Next(int minimumValue, int maximumValue)
        {
            return RandomValueGen.GetRandomInt(minimumValue, maximumValue);
        }
    }
}
