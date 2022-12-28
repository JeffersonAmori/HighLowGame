namespace RandomnessService.Providers
{
    public sealed class DotNetProvider : IRandomnessProvider
    {
        private readonly Random _random;

        public DotNetProvider()
        {
            _random = new Random();
        }

        public int Next(int minimumValue, int maximumValue)
        {
            return _random.Next(minimumValue, maximumValue);
        }
    }
}
