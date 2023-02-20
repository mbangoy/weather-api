using System;
using System.Xml;
using System.Net.Http;
using System.Text.Json;
using System.Xml.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

class Program
{
    static async Task Main(string[] args)
    {
        try {
            string _city = ConfigManager.GetCity();
            string _appAPIKey = ConfigManager.GetKey();
            try {
                // display weather data from JSON format.
                OpenWeatherData _weatherData = await WeatherService.GetDataAsyncJSON(_city, _appAPIKey);
                Console.WriteLine($"The temperature in {_city} is {_weatherData.Main.Temp} degrees Celsius.");
                Console.WriteLine($"The wind speed in {_city} is {_weatherData.Wind.Speed} m/s.");
                Console.WriteLine($"The humidity in {_city} is {_weatherData.Main.Humidity}");
            } catch (Exception e) {
                Console.WriteLine("Error getting data as JSON format: {e}");
            }

            try {
                // Display weather info from XML format.
                WeatherInfo _weatherDataXML = await WeatherService.GetDataAsXML(_city, _appAPIKey);  
                Console.WriteLine($"The temperature in {_weatherDataXML.City.Name} is {_weatherDataXML.Temperature.Value} degrees Celsius.");  
            } catch (Exception e) {
                Console.WriteLine("Error getting data as XML format: {e}");
            }
        }
        catch (Exception e) {
            Console.WriteLine($"Failed to retrieve weather data. {e}");
        }
    }

}


/// <summary>
/// Represents the main weather data returned by the OpenWeather API in JSON format.
/// </summary>
public class OpenWeatherData
{
    public MainData Main { get; set; }
    public WindData Wind { get; set; }   
}

public class MainData
{
    public float? Temp { get; set; }
    public float? Humidity { get; set; }
}

public class WindData {
    public float? Speed { get; set; }
}

/// <summary>
/// Represents the weather info returned by the OpenWeather API in XML format.
/// </summary>
[XmlRoot("current")]
public class WeatherInfo
{
    [XmlElement("city")]
    public City City { get; set; }

    [XmlElement("temperature")]
    public Temperature Temperature { get; set; }

    [XmlElement("feels_like")]
    public Temperature FeelsLike { get; set; }

    [XmlElement("humidity")]
    public Humidity Humidity { get; set; }

    [XmlElement("pressure")]
    public Pressure Pressure { get; set; }

    [XmlElement("wind")]
    public Wind Wind { get; set; }

    
}

public class City
{
    [XmlAttribute("id")]
    public int Id { get; set; }

    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlElement("coord")]
    public Coord Coord { get; set; }

    [XmlElement("country")]
    public string Country { get; set; }

    [XmlElement("timezone")]
    public int Timezone { get; set; }

    [XmlElement("sun")]
    public Sun Sun { get; set; }
}

public class Coord
{
    [XmlAttribute("lon")]
    public float Lon { get; set; }

    [XmlAttribute("lat")]
    public float Lat { get; set; }
}

public class Sun
{
    [XmlAttribute("rise")]
    public DateTime Rise { get; set; }

    [XmlAttribute("set")]
    public DateTime Set { get; set; }
}

public class Temperature
{
    [XmlAttribute("value")]
    public float Value { get; set; }

    [XmlAttribute("min")]
    public float Min { get; set; }

    [XmlAttribute("max")]
    public float Max { get; set; }

    [XmlAttribute("unit")]
    public string Unit { get; set; }
}

public class Humidity
{
    [XmlAttribute("value")]
    public int Value { get; set; }

    [XmlAttribute("unit")]
    public string Unit { get; set; }
}

public class Pressure
{
    [XmlAttribute("value")]
    public int Value { get; set; }

    [XmlAttribute("unit")]
    public string Unit { get; set; }
}

public class Wind
{
    [XmlElement("speed")]
    public WindSpeed Speed { get; set; }

    [XmlElement("gusts")]
    public string Gusts { get; set; }

    [XmlElement("direction")]
    public WindDirection Direction { get; set; }
}

public class WindSpeed
{
    [XmlAttribute("value")]
    public float Value { get; set; }

    [XmlAttribute("unit")]
    public string Unit { get; set; }

    [XmlAttribute("name")]
    public string Name { get; set; }
}

public class WindDirection
{
    [XmlAttribute("value")]
    public int Value { get; set; }

    [XmlAttribute("code")]
    public string Code { get; set; }
}

/// Some properties have been omitted




