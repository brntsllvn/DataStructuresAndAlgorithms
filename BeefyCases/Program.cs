using System.Linq;

namespace BeefyCases
{
    public class SpectacularClass
    {
        public static void Main(string[] args) {}

        public int SimpleAdd(int[] arrayOfIntegers)
        {
            return arrayOfIntegers.Sum();
        }

        public decimal DecimalAdd(decimal[] arrayOfDecimals)
        {
            return arrayOfDecimals.Sum(x => x);
        }

        public int ComplicatedAdd(WeatherBalloon[] arrayOfWeatherBalloons)
        {
            return arrayOfWeatherBalloons.Sum(x => x.Payload);
        }
    }

    public class WeatherBalloon
    {
        public int Payload { get; set; }

        public WeatherBalloon(int payload)
        {
            Payload = payload;
        }
    }
}
