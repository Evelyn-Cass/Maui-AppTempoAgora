using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void ProcurarClicked(object sender, EventArgs e)
        {
            try
            {
                if (txt_cidade.Text != "")
                {
                    Tempo? t = await DataService.GetTempo(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = $"Latitude: {t.lat}\nLongitude: {t.lon}\nTemperatura: {t.temp}º\nTemperatura Máxima: {t.temp_max}º\nTemperatura Mínima: {t.temp_min}º\nSensação Térmica: {t.feels_like}º";
                        lbl_previsao.Text = dados_previsao;

                        string mapa = $"https://embed.windy.com/embed.html?type=map&location=coordinates&metricRain=default&metricTemp=default&metricWind=default&zoom=9&overlay=wind&product=ecmwf&level=surface&lat=-{t.lat.ToString().Replace(",", ".")}&lon={t.lon.ToString().Replace(",", ".")}";

                        mv_mapa.Source = mapa;
                    }
                }
                else
                {
                    throw new Exception("Cidade não pode ser nula.");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Erro", ex.Message, "Ok");
            }
        }
    }
}
