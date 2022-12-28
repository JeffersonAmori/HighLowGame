namespace RandomnessService;

public interface IRandomnessService
{
    int Next(int minimumValue, int maximumValue);
}