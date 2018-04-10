using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Eshop
{
    public partial class My : ContentPage
    {
        public My()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, true);

            jmeno.Text = App.person.jmeno;
            prijmeni.Text = App.person.prijmeni;
            fill();
        }

        /// <summary>
        /// Napln ListView zbozim
        /// </summary>
        public void fill()
        {
            Task<HttpResponseMessage> secndJson = GetTheGoodStuff("?action=obednavky&jmeno="+App.person.id);
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
                    DisplayAlert("Alert", "Momentalně žadné zboží v katalogu. Použita offline DB", "OK");
                    Obednavky.ItemsSource = App.Database.GetItemsAsync().Result;
                }
                else
                {
                    App.Database.DeleteObednavka();
                    foreach (var item in JsonConvert.DeserializeObject<List<Obednavka>>(json))
                    {
                        App.Database.SaveItemAsync(item);
                    }
                    Obednavky.ItemsSource = JsonConvert.DeserializeObject<List<Obednavka>>(json);
                }
            }
        }

        void edit(object sender, EventArgs args)
        {
            Navigation.PushModalAsync(new Edit());
        }

        /// <summary>
        /// Otevření stránky s detailem po kliknutí
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        public async void SelectedItemMethod(object sender, ItemTappedEventArgs e){
            //vytvoření var s info o uživately
            Obednavka obednavaka = e.Item as Obednavka;

            await Navigation.PushModalAsync(new Obednavka_vypis(obednavaka.id));
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
