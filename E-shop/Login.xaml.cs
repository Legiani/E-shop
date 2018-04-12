using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Eshop
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            
        }

        public void logAsync(object sender, EventArgs args)
        {
            log();
        }

        public void log(){
            string jmeno;
            string heslo;


            jmeno = xjmeno.Text;
            heslo = xheslo.Text;


            Task<HttpResponseMessage> secndJson = GetTheGoodStuff("?action=login&jmeno=" + jmeno + "&heslo=" + heslo);
            var code = secndJson.Result.EnsureSuccessStatusCode().StatusCode;
            System.Diagnostics.Debug.WriteLine(code);
            if (code.ToString() != "OK")
            {
                DisplayAlert("Alert", "Error code: " + code, "OK");
            }

            using (HttpContent content = secndJson.Result.Content)
            {
                var json = content.ReadAsStringAsync().Result;

                System.Diagnostics.Debug.WriteLine(json);
                if (json == "[]" || json == "null")
                {
                    DisplayAlert("Alert", "Neco se pokazilo", "OK");
                }
                else
                {
                    App.person = JsonConvert.DeserializeObject<Person>(json);
                    Application.Current.Properties["jmeno"] = App.person.jmeno;
                    Application.Current.Properties["heslo"] = App.person.heslo;

                    Application.Current.MainPage = new NavigationPage(new E_shopPage());
                }

            }
        }

        public void reset(object sender, EventArgs args)
        {
            DisplayAlert("Alert", "Dostupne pouze ve webovem prohližeči", "OK");
        }

        public Task<HttpResponseMessage> GetTheGoodStuff(string data)
        {
            System.Diagnostics.Debug.WriteLine("https://student.sps-prosek.cz/~bednaja14/api.php" + data);
            var httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://student.sps-prosek.cz/~bednaja14/api.php" + data);
            var response = httpClient.SendAsync(request);
            return response;
        }
    }
}
