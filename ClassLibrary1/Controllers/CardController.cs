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
    public class CardController
    {
        public static async Task<PageResponse<Models.CardDto>> GetAllCardDtosShortAsync(Models.CardDto example, int pageNumber, params string[] sortFields)
        {
            PageRequestByExample<Models.CardDto> prbe = new PageRequestByExample<Models.CardDto>(example, pageNumber, sortFields);
            
            using (HttpClient http = new HttpClient())
            {
                var data = new HttpRequestMessage(HttpMethod.Post, $"http://{Properties.DB_HOST}:8080/api/cards/find");
                data.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                var json = JsonConvert.SerializeObject(prbe);

                data.Content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await http.SendAsync(data).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode) throw new Exception(await response.Content.ReadAsStringAsync());
                try
                {
                    return JsonConvert.DeserializeObject<PageResponse<Models.CardDto>>(await response.Content.ReadAsStringAsync());
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

