using MauiAppTempoAgora.Models;
using System.Text.Json.Nodes;

namespace MauiAppTempoAgora.Services
{
    public class DataService
    {
        public static async Task<Tempo?> GetTempo(string cidade)
        {
            Tempo? tempo = null;

            string apiKey = "705f4c0cf66485e035327a76fa7f697e";
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade}&units=metric&appid={apiKey}";

            using (HttpClient client = new())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    var rascunho = JsonObject.Parse(json);

                    DateTime time = new();
                    DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]);
                    DateTime sunset = time.AddSeconds((double)rascunho["sys"]["sunset"]);

                    tempo = new()
                    {
                        lat = (double)rascunho["coord"]["lat"],
                        lon = (double)rascunho["coord"]["lon"],
                        description = (string)rascunho["weather"][0]["main"],
                        temp_max = (double)rascunho["main"]["temp_max"],
                        temp_min = (double)rascunho["main"]["temp_min"],
                        temp = (double)rascunho["main"]["temp"],
                        feels_like = (double)rascunho["main"]["feels_like"],
                        visibility = (int)rascunho["visibility"],
                        sunrise = sunrise,
                        sunset = sunset,
                        timezone = (int)rascunho["timezone"],
                        icon = (string)rascunho["weather"][0]["icon"]
                    };

                }
            }
            return tempo;
        }
    }

}