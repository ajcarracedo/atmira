using System.Collections.Generic;

namespace Backend.Constant
{
    public static class NASA
    {
        /// <summary>
        /// Key needed to make requests to https://api.nasa.gov/
        /// Demo keys have restrictions on the number of requests per hour and day
        /// </summary>
        public const string API_KEY = "NTHIocgkS4vEsDMHf86rehVhXaKlC2YQ3vojvVjh";
    }

    public static class Parameters
    {
        /// <summary>
        /// Name of currently supported planets
        /// </summary>
        public static readonly List<string> VALID_PLANETS = new List<string>() { "Mercury", "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune" };
    }

    public static class ResponseMessages
    {
        /// <summary>
        /// Default message for invalid param 'planet'
        /// </summary>
        public const string INVALID_PARAM = "Imput parameter 'planet' is missing or incorrect.";
    }
}