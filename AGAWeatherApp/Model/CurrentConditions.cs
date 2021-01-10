using System;

namespace AGAWeatherApp.Model
{
    /// <summary>
    /// The UnitSystem entity
    /// </summary>
    public class UnitSystem
    {
        public float Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }

    /// <summary>
    /// The Temperature entity.
    /// </summary>
    public class Temperature
    {
        public UnitSystem Metric { get; set; }
        public UnitSystem Imperial { get; set; }
    }

    /// <summary>
    /// The CurrentConditions entity.
    /// </summary>
    public class CurrentConditions
    {
        public DateTime LocalObservationDateTime { get; set; }
        public int EpochTime { get; set; }
        public string WeatherText { get; set; }
        public int WeatherIcon { get; set; }
        public bool HasPrecipitation { get; set; }
        public object PrecipitationType { get; set; }
        public bool IsDayTime { get; set; }
        public Temperature Temperature { get; set; }
        public string MobileLink { get; set; }
        public string Link { get; set; }
    }
}
