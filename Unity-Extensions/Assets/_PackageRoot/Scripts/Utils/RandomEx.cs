using System;

namespace Extensions.Utils
{
    public static class RandomEx
    {
        static Random random = new Random();

        public static Random Instance => random == null ? random = new Random() : random;

        public static int Range(int from, int to) => random.Next(from, to);
        public static long Range(long from, long to) => from + (long)(random.NextDouble() * (to - from));
        public static float Range(float from, float to) => from + (float)(random.NextDouble() * (to - from));
        public static double Range(double from, double to) => from + (double)(random.NextDouble() * (to - from));
        public static char Range(char from, char to) => (char)random.Next(from, to);
    }
}