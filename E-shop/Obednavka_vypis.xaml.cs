using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Eshop
{
    public partial class Obednavka_vypis : ContentPage
    {
        public Obednavka_vypis(int id)
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, true);

            fill(id);
        }

        /// <summary>
        /// Napln ListView zbozim
        /// </summary>
        public void fill(int id)
        {
            Task<HttpResponseMessage> secndJson = GetTheGoodStuff("?action=obednavka&id="+id);
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
                    obednavka.ItemsSource = JsonConvert.DeserializeObject<List<Obednavka>>(json);
                }
            }
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
