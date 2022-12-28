using RandomnessService.Providers;

namespace RandomnessService
{
    public sealed class NeverRepeatRandomnessService : IRandomnessService
    {
        private readonly IRandomnessProvider _provider;
        private int _last = -1;

        public NeverRepeatRandomnessService(IRandomnessProvider provider)
        {
            _provider = provider;
        }

        public int Next(int minimumValue, int maximumValue)
        {
            var next = _provider.Next(minimumValue, maximumValue);

            if (_last != next)
            {
                _last = next;
                return next;
            }

            return this.Next(minimumValue, maximumValue);
        }
    }
}
