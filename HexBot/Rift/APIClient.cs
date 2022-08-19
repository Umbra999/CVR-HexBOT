using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static HexBot.Wrappers.Misc;

namespace HexBot.Rift
{
    internal class APIClient
    {
        private HttpClient Client;
        public AuthUser APIUser;

        public async Task<bool> Login(string Mail, string Pass, WebProxy proxy = null)
        {
            Client = new HttpClient(new HttpClientHandler { UseCookies = false, Proxy = proxy }, true);
            Client.DefaultRequestHeaders.Add("Host", "api.abinteractive.net");

            string Body = JsonConvert.SerializeObject(new { AuthType = 2, Password = Pass, Username = Mail });
            HttpRequestMessage payload = new HttpRequestMessage(HttpMethod.Post, $"https://api.abinteractive.net/1/users/auth")
            {
                Content = new StringContent(Body, Encoding.UTF8, "application/json")
            };
            payload.Headers.Add("Connection", "keep-alive");

            HttpResponseMessage Response = await Client.SendAsync(payload);
            if (Response.IsSuccessStatusCode)
            {
                string ResponseBody = await Response.Content.ReadAsStringAsync();
                APIUser = JsonConvert.DeserializeObject<APIResponse>(ResponseBody).data.ToObject<AuthUser>();
                Client.DefaultRequestHeaders.Add("Username", APIUser.username);
                Client.DefaultRequestHeaders.Add("AccessKey", APIUser.accessKey);
                Client.DefaultRequestHeaders.Add("User-Agent", "ChilloutVR API-Requests");
                return true;
            }
            return false;
        }


        public async Task<APIWorld> GetWorld(string WorldID)
        {
            HttpRequestMessage payload = new HttpRequestMessage(HttpMethod.Get, $"https://api.abinteractive.net/1/worlds/{WorldID}");
            HttpResponseMessage Response = await Client.SendAsync(payload);
            if (Response.IsSuccessStatusCode)
            {
                string ResponseBody = await Response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<APIResponse>(ResponseBody).data.ToObject<APIWorld>();
            }
            return null;
        }

        public async Task<APIInstance> GetInstance(string InstanceID)
        {
            HttpRequestMessage payload = new HttpRequestMessage(HttpMethod.Get, $"https://api.abinteractive.net/1/instances/{InstanceID}");
            HttpResponseMessage Response = await Client.SendAsync(payload);
            if (Response.IsSuccessStatusCode)
            {
                string ResponseBody = await Response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<APIResponse>(ResponseBody).data.ToObject<APIInstance>();
            }
            return null;
        }

        public async Task<InstanceToken> GetInstanceToken(string InstanceID)
        {
            HttpRequestMessage payload = new HttpRequestMessage(HttpMethod.Get, $"https://api.abinteractive.net/1/instances/{InstanceID}/join");
            HttpResponseMessage Response = await Client.SendAsync(payload);
            if (Response.IsSuccessStatusCode)
            {
                string ResponseBody = await Response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<APIResponse>(ResponseBody).data.ToObject<InstanceToken>();
            }
            return null;
        }

        public async Task<APIAvatar> GetAvatar(string AvatarUD)
        {
            HttpRequestMessage payload = new HttpRequestMessage(HttpMethod.Get, $"https://api.abinteractive.net/1/avatars/{AvatarUD}");
            HttpResponseMessage Response = await Client.SendAsync(payload);
            if (Response.IsSuccessStatusCode)
            {
                string ResponseBody = await Response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<APIResponse>(ResponseBody).data.ToObject<APIAvatar>();
            }
            return null;
        }

        public async Task<APIUser> GetUser(string UserID)
        {
            HttpRequestMessage payload = new HttpRequestMessage(HttpMethod.Get, $"https://api.abinteractive.net/1/users/{UserID}");
            HttpResponseMessage Response = await Client.SendAsync(payload);
            if (Response.IsSuccessStatusCode)
            {
                string ResponseBody = await Response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<APIResponse>(ResponseBody).data.ToObject<APIUser>();
            }
            return null;
        }

        public async Task<LimitedAPIWorld[]> GetActiveWorlds()
        {
            HttpRequestMessage payload = new HttpRequestMessage(HttpMethod.Get, $"https://api.abinteractive.net/1/worlds/list/wrldactive");
            HttpResponseMessage Response = await Client.SendAsync(payload);
            if (Response.IsSuccessStatusCode)
            {
                string ResponseBody = await Response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<APIResponse>(ResponseBody).data.ToObject<LimitedAPIWorld[]>();
            }
            return null;
        }
    }
}
