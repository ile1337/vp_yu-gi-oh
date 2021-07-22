using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Controllers
{
    public class HttpClientBuilder
    {

        public static HttpClient GetHttpClient()
        {
            return new HttpClient();
        }

    }
}
