using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Eshop
{
    public partial class Cart : ContentPage
    {
        public Cart()
        {
            InitializeComponent();


            Task<HttpResponseMessage> secndJson = GetTheGoodStuff("?action=cart&table=obednavka&jmeno="+App.person.id);
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
                    DisplayAlert("Alert", "Momentalně žadné zboží v katalogu.", "OK");
                }
                else
                {
                    ItemsList.ItemsSource = JsonConvert.DeserializeObject<List<Item>>(json);
                }
            }
        }

        public void buy(object sender, EventArgs args)
        {
            Task<HttpResponseMessage> secndJson = GetTheGoodStuff("?action=buy&table=obednavka&jmeno=" + App.person.id);
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
                    DisplayAlert("Alert", "Momentalně žadné zboží v košiku.", "OK");
                }
                else
                {
                    DisplayAlert("Alert", "Koupeno.", "OK");
                }
            }

        }

        /// <summary>
        /// Otevření stránky s detailem po kliknutí
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        public async void SelectedItemMethod(object sender, ItemTappedEventArgs e)
        {
            //vytvoření var s info o uživately
            Item derp = e.Item as Item;
            //otevře novou stranku
            await Navigation.PushModalAsync(new Detail(derp));
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
