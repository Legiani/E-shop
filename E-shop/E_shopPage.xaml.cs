using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;


namespace Eshop
{
    public partial class E_shopPage : ContentPage
    {
        public E_shopPage()
        {
            InitializeComponent();
            fill();
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
            await Navigation.PushAsync(new Detail(derp));
        }

        public void kosik(object sender, EventArgs args)
        {

            Navigation.PushAsync(new Cart());

        }

        public void user(object sender, EventArgs args)
        {
            Navigation.PushAsync(new My());

        }

        /// <summary>
        /// Napln ListView zbozim
        /// </summary>
        public void fill()
        {
            Task<HttpResponseMessage> secndJson = GetTheGoodStuff("?action=select&table=zbozi");
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
                    ZboziList.ItemsSource = JsonConvert.DeserializeObject<List<Item>>(json);
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
