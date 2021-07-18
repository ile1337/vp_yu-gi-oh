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
        public static async Task<List<Models.CardDto>> GetAllCardDtosShortAsync()
        {
            PageResponse<Models.CardDto> page;
            using (HttpClient http = new HttpClient())
            {
                var data = new HttpRequestMessage(HttpMethod.Post, $"http://{Properties.DB_HOST}:8080/api/CardDtos/page");
                data.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                var json = new JObject().ToString();

                data.Content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await http.SendAsync(data).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode) throw new Exception(await response.Content.ReadAsStringAsync());
                try
                {
                    page = JsonConvert.DeserializeObject<PageResponse<Models.CardDto>>(await response.Content.ReadAsStringAsync());
                }
                catch (JsonSerializationException e)
                {
                    page = null;
                    Debug.WriteLine(e.Message);
                }
            }

            return page.content;
        }


    }
}
/* using MiddlewareRevisited.Models;
using MiddlewareRevisited.Models.Meta;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareRevisited.Controllers
{
    public class CardDto
    {
        // TODO: Rework so it works like AdminPanel middleware 
        // TASK<Models.CardDto> -> Task zoso ne go koristis returnatiot CardDto
        // i plus vekje go imas updatenato lokalno CardDto-ot
        public static async Task<Models.CardDto> UpdateCardDto(Models.CardDto CardDto, Models.User currentUser)
        {
            CardDto.schoolClass = currentUser.schoolClass;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var data = new HttpRequestMessage(HttpMethod.Put, $"http://{Properties.Settings.Default.DB_HOST}:8080/api/CardDtos");
                var json = JsonConvert.SerializeObject(CardDto);
                data.Headers.Add("token", currentUser.token);
                data.Content = new StringContent(json, Encoding.UTF8, "application/json");
                var ret = await httpClient.SendAsync(data);
                if (!ret.IsSuccessStatusCode) throw new Exception(await ret.Content.ReadAsStringAsync());
                return JsonConvert.DeserializeObject<Models.CardDto>(await ret.Content.ReadAsStringAsync());
            }
        }

        public static async Task<Models.CardDto> GetCardDtoByIdAsync(string CardDtoId, User user)
        {
            Models.CardDto s;
            using (var http = new HttpClient())
            {
                var data = new HttpRequestMessage(HttpMethod.Post, $"http://{Properties.Settings.Default.DB_HOST}:8080/api/CardDtos/getOne");
                data.Headers.Add("token", user.token);
                data.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                //var json = new JObject(
                //        new JProperty("schoolClass", JToken.FromObject(user.schoolClass)
                //    )).ToString();

                data.Content = new StringContent(CardDtoId, Encoding.UTF8, MediaTypeNames.Text.Plain);
                var response = await http.SendAsync(data).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode) throw new Exception(await response.Content.ReadAsStringAsync());
                s = JsonConvert.DeserializeObject<Models.CardDto>(await response.Content.ReadAsStringAsync());
            }

            return s;
        }

        public static async Task<List<Models.CardDto>> GetAllCardDtosShortAsync(Models.User user)
        {
            PageResponse<Models.CardDto> page;
            using (HttpClient http = new HttpClient())
            {
                var data = new HttpRequestMessage(HttpMethod.Post, $"http://{Properties.Settings.Default.DB_HOST}:8080/api/CardDtos/page");
                data.Headers.Add("token", user.token);
                data.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                var json = new JObject(
                        new JProperty("schoolClass", JToken.FromObject(user.schoolClass)
                    )).ToString();

                data.Content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await http.SendAsync(data).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode) throw new Exception(await response.Content.ReadAsStringAsync());
                try
                {
                    page = JsonConvert.DeserializeObject<PageResponse<Models.CardDto>>(await response.Content.ReadAsStringAsync());
                } catch(JsonSerializationException e)
                {
                    page = null;
                    Debug.WriteLine(e.Message);
                }
            }

            return page.content;
        }


        public static async Task<List<Models.CardDto>> GetAllCardDtosFullAsync(Models.User user)
        {
            List<Models.CardDto> items;
            using (HttpClient http = new HttpClient())
            {
                var data = new HttpRequestMessage(HttpMethod.Get, $"http://{Properties.Settings.Default.DB_HOST}:8080/api/CardDtos");
                data.Headers.Add("token", user.token);
                data.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                var response = await http.SendAsync(data).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode) throw new Exception(await response.Content.ReadAsStringAsync());
                try
                {
                    items = JsonConvert.DeserializeObject<List<Models.CardDto>>(await response.Content.ReadAsStringAsync());
                }
                catch (JsonSerializationException e)
                {
                    items = null;
                    Debug.WriteLine(e.Message);
                }
            }

            return items;
        }
    }
}

 */
