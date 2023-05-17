using CM.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CPaaS_Software_Test.Services;

using System;
using System.Threading.Tasks;
using System.Net.Http;

public class MessageService
{
    // SmsSender method uses TextClient from CM.Text nuget package to send a message
    public async Task<TextClientResult> SmsSender(string apikey)
    {
        var client = new TextClient(new Guid(apikey));
        Weather weather = GetWeatherData();
        var result = await client.SendMessageAsync(weather.ToString(), "Daily weather data", new List<string> { "0031614678711" }, "Test_Reference").ConfigureAwait(false);
        return result;
    }
    // Fetch current temperature in London from visualcrossing API and returning it as a weather object
    public Weather GetWeatherData()
    {
        using var httpClient = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get,
            "https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/London?unitGroup=metric&include=current&key=KYNKQGRHVAACY25GVNQWFW8KR&contentType=json");
        var response = httpClient.Send(request);
        using var reader = new StreamReader(response.Content.ReadAsStream());
        var responseBody = reader.ReadToEnd();
        return JsonParser(responseBody);
    }

    // Parse Json data and set resolvedaddress and temperature in weather object
    public Weather JsonParser(string responseBody)
    {
        dynamic data = JObject.Parse(responseBody);
        string resolvedAddress = data.resolvedAddress;
        string temperature = data.currentConditions.temp;
        Weather weather = new Weather(resolvedAddress, temperature);
        return weather;
    }
}