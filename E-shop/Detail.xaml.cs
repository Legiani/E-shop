using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eshop
{
    public partial class Detail : ContentPage
    {
        private Item derp;

        public Detail(Item derp)
        {
            InitializeComponent();
            this.derp = derp;
            id.Text = derp.id.ToString();
            nazev.Text = derp.nazev.ToString();
            cena.Text = derp.cena.ToString();

        }

        public void ad(object sender, EventArgs args)
        {

            Task<HttpResponseMessage> secndJson = GetTheGoodStuff("?action=add&id ="+id.Text+"&jmeno="+App.person.id);
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
                    DisplayAlert("Alert", "Přidáno ", "OK");
                    Navigation.PopModalAsync();
                }
            }
        }

        public void dl(object sender, EventArgs args)
        {


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
