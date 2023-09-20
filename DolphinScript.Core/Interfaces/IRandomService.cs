namespace DolphinScript.Core.Interfaces
{
    public interface IRandomService
    {
        int GetRandomNumber(int min, int max);
        double GetRandomDouble(double minimum, double maximum);
    }
}