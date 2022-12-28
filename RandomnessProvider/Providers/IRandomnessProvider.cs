namespace RandomnessService.Providers;

public interface IRandomnessProvider
{
    int Next(int minimumValue, int maximumValue);
}