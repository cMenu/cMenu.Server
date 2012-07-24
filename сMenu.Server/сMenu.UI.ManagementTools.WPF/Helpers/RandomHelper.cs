using System;
using System.Text;
using System.Windows.Media;

namespace iMenu.UI.ManagementTools.WPF.Helpers
{
    public static class RandomHelper
    {
        private static Random randomSeed = new Random();

        /// <summary>
        /// Generates a random string with the given length
        /// </summary>
        /// <param name="size">Size of the string</param>
        /// <param name="lowerCase">If true, generate lowercase string</param>
        /// <returns>Random string</returns>
        public static string RandomString(int size, bool lowerCase)
        {
            // StringBuilder is faster than using strings (+=)
            StringBuilder RandStr = new StringBuilder(size);

            // Ascii start position (65 = A / 97 = a)
            int Start = (lowerCase) ? 97 : 65;

            // Add random chars
            for (int i = 0; i < size; i++)
                RandStr.Append((char)(26 * randomSeed.NextDouble() + Start));

            return RandStr.ToString();
        }

        public static int RandomInt(int min, int max)
        {
            return randomSeed.Next(min, max);
        }

        public static double RandomDouble()
        {
            return randomSeed.NextDouble();
        }

        public static double RandomNumber(int min, int max, int digits)
        {
            return Math.Round(randomSeed.Next(min, max - 1) + randomSeed.NextDouble(), digits);
        }

        public static bool RandomBool()
        {
            return (randomSeed.NextDouble() > 0.5);
        }

        public static DateTime RandomDate()
        {
            return RandomDate(new DateTime(1900, 1, 1), DateTime.Now);
        }

        public static DateTime RandomDate(DateTime from, DateTime to)
        {
            TimeSpan range = new TimeSpan(to.Ticks - from.Ticks);
            return from + new TimeSpan((long)(range.Ticks * randomSeed.NextDouble()));
        }

        public static Color RandomColor()
        {
            return Color.FromRgb((byte)randomSeed.Next(255), (byte)randomSeed.Next(255), (byte)randomSeed.Next(255));
        }

    }
}
