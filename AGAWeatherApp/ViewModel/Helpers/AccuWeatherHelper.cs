using AGAWeatherApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace AGAWeatherApp.ViewModel.Helpers
{
    /// <summary>
    /// Helper Class for connecting with AccuWeather API.
    /// </summary>
    public static class AccuWeatherHelper
    {
        /// <summary>
        /// The base url of the API.
        /// </summary>
        private const string BASE_URL = "http://dataservice.accuweather.com/";

        /// <summary>
        /// The Autocomplete search end point for the Locations API.
        /// </summary>
        private const string AUTOCOMPLETE_ENDPOINT = "locations/v1/cities/autocomplete?apikey={0}&q={1}";

        /// <summary>
        /// The Current Conditions end point for the Current Conditions API.
        /// </summary>
        private const string CURRENT_CONDITIONS_ENDPOINT = "currentconditions/v1/{0}?apikey={1}";

        /// <summary>
        /// The API Key of the app to be used.
        /// Create your app from https://developer.accuweather.com/
        /// Then copy the API Key and paste it below as a string.
        /// Limitations for free package: 50 calls/day, 1 key/developer.
        /// </summary>
        private const string API_KEY = "Your_App_API_Key";

        /// <summary>
        /// The method of searching for a city through the API.
        /// </summary>
        /// <param name="query">Text to use for Autocomplete search.</param>
        /// <returns>List of cities about locations matching the autocomplete search text.</returns>
        public static async Task<List<City>> GetCities(string query)
        {
            // The request url
            string url = String.Format($"{BASE_URL}{AUTOCOMPLETE_ENDPOINT}", API_KEY, query);

            // Initializing the list of cities
            List<City> cities = new List<City>();

            // Sending the request
            using (HttpClient client = new HttpClient())
            {
                // Getting the response
                var response = await client.GetAsync(url);

                // Getting the json string
                string json = await response.Content.ReadAsStringAsync();

                // Trying to deserialize the json to the list of cities
                try
                {
                    // Deserializing the json to the list of cities
                    cities = JsonConvert.DeserializeObject<List<City>>(json);
                }
                catch (Exception)
                {
                    // Initializing the json error
                    JsonError jsonError = new JsonError();

                    // Deserializing the json to the json error
                    jsonError = JsonConvert.DeserializeObject<JsonError>(json);

                    // Showing the json error message to the user
                    MessageBox.Show(jsonError.Message, jsonError.Code, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return cities;
        }

        /// <summary>
        /// The method of getting the weather status of the provided city.
        /// </summary>
        /// <param name="cityKey">The key of the desired city to get its weather status.</param>
        /// <returns>CurrentConditions object containing the weather status.</returns>
        public static async Task<CurrentConditions> GetCurrentConditions(string cityKey)
        {
            // The request url
            string url = String.Format($"{BASE_URL}{CURRENT_CONDITIONS_ENDPOINT}", cityKey, API_KEY);

            // Initializing the current conditions object
            CurrentConditions currentConditions = new CurrentConditions();

            // Sending the request
            using (HttpClient client = new HttpClient())
            {
                // Getting the response
                var response = await client.GetAsync(url);

                // Getting the json string
                string json = await response.Content.ReadAsStringAsync();

                // Deserializing the json to the current conditions object
                currentConditions = JsonConvert.DeserializeObject<List<CurrentConditions>>(json).FirstOrDefault();
            }

            return currentConditions;
        }
    }
}
