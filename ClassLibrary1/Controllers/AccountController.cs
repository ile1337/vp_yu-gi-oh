using Middleware.Models.Meta;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Controllers
{
    public class AccountController
    {
        public static async Task<Models.OAuth> GetLoginToken(string username, string password)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("username",username);
            dict.Add("password", password);
            dict.Add("grant_type", "password");
  

            using (HttpClient http = new HttpClient())
            {
                var data = new HttpRequestMessage(HttpMethod.Post, $"http://{Properties.DB_HOST}:8080/oauth/token");
                data.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                data.Headers.Add("Authorization", "Basic yugiohjwtclientid XY7kmzoNzl100");
              
                data.Content = new FormUrlEncodedContent(dict);
                var response = await http.SendAsync(data).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode) throw new Exception(await response.Content.ReadAsStringAsync());
                try
                {
                    return JsonConvert.DeserializeObject<Models.OAuth>(await response.Content.ReadAsStringAsync());
                }
                catch (JsonSerializationException e)
                {
                    Debug.WriteLine(e.Message);
                }
            }

            return null;
        }
    }
}
