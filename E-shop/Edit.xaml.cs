using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eshop
{
    public partial class Edit : ContentPage
    {
        public Edit()
        {
            InitializeComponent();

            jmeno.Text = App.person.jmeno;
            prijmeni.Text = App.person.prijmeni;
            dateOfBird.SetValue(DatePicker.MaximumDateProperty, App.person.narozeni);

        }

        void save(object sender, EventArgs args)
        {
            Task<HttpResponseMessage> secndJson = GetTheGoodStuff("?action=change_name&id=" + App.person.id + "&jmeno=" + jmeno.Text);
            var code = secndJson.Result.EnsureSuccessStatusCode().StatusCode;
            if (code.ToString() != "OK")
            {
                DisplayAlert("Alert", "Error code: " + code, "OK");
            }

            using (HttpContent content = secndJson.Result.Content)
            {
                var json = content.ReadAsStringAsync().Result;

                System.Diagnostics.Debug.WriteLine(json);
                if (json == "[]")
                {
                    DisplayAlert("Alert", "Chyba" + code, "OK");
                }
                else
                {
                    DisplayAlert("Alert", "Zmeneno ", "OK");
                    Navigation.PopModalAsync();
                }
            }

            //vrat se na domovskou obrazovku
            Navigation.PopModalAsync();
        }

        public Task<HttpResponseMessage> GetTheGoodStuff(string data)
        {
            var httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://student.sps-prosek.cz/~bednaja14/api.php" + data);
            var response = httpClient.SendAsync(request);
            return response;
        }

    }
}
