using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Controllers
{
    public class HttpClientBuilder
    {

        private static string token = "";

        public static HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }

        public static void SetToken(string token)
        {
            HttpClientBuilder.token = token;
        }

    }
}
