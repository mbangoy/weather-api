using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

public static class WeatherService
{
    private const string _OpenWeatherApiUrl = "http://api.openweathermap.org/data/2.5/weather";


    /// <summary>
    /// Retrieves weather information for a specified city as default JSON format from the OpenWeather API and returns it as a OpenWeatherData object.
    /// </summary>
    /// <param name="_city">The name of the city to retrieve weather information for.</param>
    /// <param name="_apiKey">The API key required to access the OpenWeather API.</param>
    /// <returns>A OpenWeatherData object containing the weather information for the specified city, or null if an error occurred.</returns>
    public static async Task<OpenWeatherData> GetDataAsyncJSON(string _city, string _apiKey)
    {
        string _openWeatherUrl = $"{_OpenWeatherApiUrl}?q={_city}&appid={_apiKey}&units=metric";
        using (HttpClient _httpClient = new HttpClient())
        {
            /// Create the HttpResponseMessage object and determine the status code.
            HttpResponseMessage _responseMessage = await _httpClient.GetAsync(_openWeatherUrl);
            if (_responseMessage.IsSuccessStatusCode){
                string _weatherDataResponseJSON = await _httpClient.GetStringAsync(_openWeatherUrl);
                /// Create the JSONSerializerOptions object.
                JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                /// Deserialize the response data in JSON format into OpenWeatherData object.
                OpenWeatherData _weatherDataJSON = JsonSerializer.Deserialize<OpenWeatherData>(_weatherDataResponseJSON, _jsonSerializerOptions);           
                /// Should be any logging framework. For now let's use the Console.Writeline.
                Console.WriteLine("Weather data is retrieved from the server.");
                return _weatherDataJSON;
            } else {
                 Console.WriteLine($"Failed to retrieve weather data. Status code: {_responseMessage.StatusCode}");
                return null;
            }
           
        }
    }

    
    /// <summary>
    /// Retrieves weather information for a specified city from the OpenWeather API and returns it as a WeatherInfo object.
    /// </summary>
    /// <param name="_city">The name of the city to retrieve weather information for.</param>
    /// <param name="_apiKey">The API key required to access the OpenWeather API.</param>
    /// <returns>A WeatherInfo object containing the weather information for the specified city, or null if an error occurred.</returns>
    public static async Task<WeatherInfo> GetDataAsXML(string _city, string _apiKey)
    {
    string _openWeatherUrlXML = $"{_OpenWeatherApiUrl}?q={_city}&appid={_apiKey}&mode=xml";
    using (HttpClient _httpClient = new HttpClient())
    {
        HttpResponseMessage _response = await _httpClient.GetAsync(_openWeatherUrlXML);
        if (_response.IsSuccessStatusCode)
        {
            string _weatherDataResponseXML = await _response.Content.ReadAsStringAsync();
            // Create the XML Document object and Load the response data.
            XmlDocument _weatherXMLDoc = new XmlDocument();
            _weatherXMLDoc.LoadXml(_weatherDataResponseXML);
            //create the XML serializer object.
            XmlSerializer _serializer = new XmlSerializer(typeof(WeatherInfo));          
            // Create a StringReader object to read the XML from the XmlDocument
            StringReader _stringXMLReader = new StringReader(_weatherXMLDoc.OuterXml);
            // Deserialize the XML and cast it to the appropriate type
            WeatherInfo _weatherDataXML = (WeatherInfo)_serializer.Deserialize(_stringXMLReader);
            // should be the logging framework. For now let's just used the Console.WriteLine.
            Console.WriteLine("Weather Info has been retrieved from the server.");
            return _weatherDataXML;
        }
        else
        {
            Console.WriteLine($"Failed to retrieve weather data. Status code: {_response.StatusCode}");
            return null;
        }
    }
}

}
