using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ChatGptBot.Business.Manager
{
    public  class CitiesManager
    {
        private const string BASE_URL = "https://wft-geo-db.p.rapidapi.com/v1/geo/cities";

        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public CitiesManager(string apiKey)
        {
            _apiKey = apiKey;

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", _apiKey);
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", "wft-geo-db.p.rapidapi.com");
        }

        public async Task<string[]> GetCityNames()
        {
            string response = await _httpClient.GetStringAsync(BASE_URL);
            
            JObject jsonObject = JObject.Parse(response);
            JArray data = (JArray)jsonObject["data"];

            string[] cityNames = new string[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                cityNames[i] = (string)data[i]["city"];
            }

            return cityNames;
        }
    }
}
