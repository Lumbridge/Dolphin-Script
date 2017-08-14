using System;

namespace RASL.Lib.Backend
{
    class RandomNumber
    {
        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();

        public static int GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            {
                return getrandom.Next(min, max + 1);
            }
        }

        public static double GetRandomDouble(double minimum, double maximum)
        {
            lock (syncLock)
            {
                return getrandom.NextDouble() * (maximum - minimum) + minimum;
            }
        }
    }
}
