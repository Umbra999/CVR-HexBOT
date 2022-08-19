using HexBot.Rift;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HexBot.Wrappers
{
    internal class Misc
    {
        public static string FromBase64(string Data)
        {
            var base64EncodedBytes = Convert.FromBase64String(Data);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string ToBase64(string Data)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(Data);
            return Convert.ToBase64String(plainTextBytes);
        }

        public class AuthUser
        {
            public string username { get; set; }
            public string accessKey { get; set; }
            public string userId { get; set; }
            public string currentAvatar { get; set; }
            public string currentHomeWorld { get; set; }
        }

        public class APIResponse
        {
            public string message { get; set; }
            public dynamic data { get; set; }
        }

        public class APIUser
        {
            public string id { get; set; }
            public string name { get; set; }
            public string rank { get; set; }
            public bool onlineState { get; set; }
            public APIInstance instance { get; set; }
            public LimitedAPIAvatar avatar { get; set; }
            public bool isBlocked { get; set; }
            public bool isFriend { get; set; }
        }

        public class LimitedAPIUser
        {
            public string id { get; set; }
            public string name { get; set; }
            public string imageUrl { get; set; }
        }

        public class LimitedAPIAvatar
        {
            public string id { get; set; }
            public string imageUrl { get; set; }
            public string name { get; set; }
        }

        public class APIAvatar
        {
            public string id { get; set; }
            public string imageUrl { get; set; }
            public string name { get; set; }
            public bool isPublished { get; set; }
            public bool switchPermitted { get; set; }
            public LimitedAPIUser user { get; set; }
            public string description { get; set; }
        }

        public class APIWorld
        {
            public LimitedAPIUser author { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public string[] tags { get; set; }
            public LimitedAPIInstance[] instances { get; set; }
            public string description { get; set; }
        }

        public class LimitedAPIWorld
        {
            public string id { get; set; }
            public string name { get; set; }
            public string imageUrl { get; set; }
        }

        public class LimitedAPIInstance
        {
            public string id { get; set; }
            public int maxPlayerCount { get; set; }
            public string name { get; set; }
            public int playerCount { get; set; }
            public string region { get; set; }
        }

        public class APIInstance
        {
            public LimitedAPIUser author { get; set; }
            public string id { get; set; }
            public int maxPlayer { get; set; }
            public LimitedAPIUser[] members { get; set; }
            public string instanceSettingPrivacy { get; set; }
            public int currentPlayerCount { get; set; }
            public string region { get; set; }
            public LimitedAPIWorld world { get; set; }
            public string name { get; set; }
            public string gameModeName { get; set; }
            public string gameModeId { get; set; }
        }

        public class InstanceToken
        {
            public Host host { get; set; }
            public string jwt { get; set; }
        }

        public class Host
        {
            public string fqdn { get; set; }
            public int port { get; set; }
        }

        public enum Tags
        {
            AuthenticateCreateConnection = 1,
            AuthenticationResponse,
            ClientConfiguration = 7,
            PlayerDisconnection,
            RichPresenceData = 11,
            SpawnAvatar = 14,
            SwitchIntoAvatar = 17,
            NetworkedRootData = 20,
            OwnUserAccountingData = 150,
            UserAccountingData,
            CoreUpdateData,
            LoadScriptedFiles = 168,
            UsingWrongGameVersion = 1200,
            GameMeta,
            PleaseAuthenticate = 1205,
            GuardianMessage = 9000,
            GuardianHandshake,
            GuardianRequestKick = 9040,
            DropPortal = 10000,
            DropPortalBroadcast,
            ObjectSync = 10020,
            ObjectSyncOnJoin = 10025,
            InteractableSync = 10030,
            InteractableTriggerApf = 10035,
            CreateSpawnableObject = 10050,
            UpdateSpawnableObject,
            DestroySpawnableObject,
            AdvancedAvatarSettings = 10100,
            VideoPlayerCreate = 10500,
            VideoPlayerPlay,
            VideoPlayerPause,
            VideoPlayerResync,
            VideoPlayerSetUrl,
            VideoPlayerSetTime,
            VideoPlayerMetaControl,
            VideoPlayerResyncRequest = 10510,
            TeleportPlayer = 11000,
            ConfigureGameSession = 14210,
            JoinTeam = 14220,
            LeaveTeam,
            Ready = 14227,
            StartGame = 14240,
            StartRound,
            EndGame,
            EndRound,
            ScoreUpdated = 14300,
            GameSessionUpdated,
            GameSessionLateJoinSync = 14399,
            PlayerHit,
            PlayerKilled,
            GunFire = 14410,
            NewtonEditorBuilderLoadLibrary = 19000,
            NewtonEditorBuilderUnloadLibrary,
            NewtonEditorBuilderLibraryState,
            NewtonEditorBuilderItemCreate = 19050,
            NewtonEditorBuilderItemDelete,
            NewtonEditorBuilderItemAlter
        }

        public static void JoinRoomAll(string InstanceID)
        {
            foreach (RiftClient client in Load.RiftClients)
            {
                Task.Run(() => client.JoinRoom(InstanceID, false));
            }
        }

        public static void LeaveRoomAll()
        {
            foreach (RiftClient client in Load.RiftClients)
            {
                Task.Run(() => client.Disconnect());
            }
        }

        public static async Task EventSpammer(int Count, int Amount, int Delay, Action action)
        {
            for (int j = 0; j < Count; j++)
            {
                for (int i = 0; i < Amount; i++)
                {
                    try
                    {
                        action();
                    }
                    catch (Exception e)
                    {
                        Logger.LogError($"Exception during EventSpamming: {e}");
                    }
                }
                await Task.Delay(Delay);
            }
        }

        public static void SendWebHook(string URL, string MSG)
        {
            Task.Run(async delegate
            {
                var req = new
                {
                    content = MSG
                };

                HttpClient CurrentClient = new HttpClient(new HttpClientHandler { UseCookies = false });
                HttpRequestMessage Payload = new HttpRequestMessage(HttpMethod.Post, URL);
                string joinWorldBody = JsonConvert.SerializeObject(req);
                Payload.Content = new StringContent(joinWorldBody, Encoding.UTF8, "application/json");
                Payload.Headers.Add("User-Agent", "LunaR");
                HttpResponseMessage Response = await CurrentClient.SendAsync(Payload);
            });
        }

        public static void SendEmbedWebHook(string URL, object[] MSG)
        {
            Task.Run(async delegate
            {
                var req = new
                {
                    embeds = MSG
                };

                HttpClient CurrentClient = new HttpClient(new HttpClientHandler { UseCookies = false });
                HttpRequestMessage Payload = new HttpRequestMessage(HttpMethod.Post, URL);
                string joinWorldBody = JsonConvert.SerializeObject(req);
                Payload.Content = new StringContent(joinWorldBody, Encoding.UTF8, "application/json");
                Payload.Headers.Add("User-Agent", "LunaR");
                HttpResponseMessage Response = await CurrentClient.SendAsync(Payload);
            });
        }

        public static readonly Random random = new Random(Environment.TickCount);
        public static string RandomString(int length)
        {
            char[] array = "abcdefghlijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToArray();
            string text = string.Empty;
            for (int i = 0; i < length; i++)
            {
                text += array[random.Next(array.Length)].ToString();
            }
            return text;
        }

        public static string RandomNumberString(int length)
        {
            char[] array = "0123456789".ToArray();
            string text = string.Empty;
            for (int i = 0; i < length; i++)
            {
                text += array[random.Next(array.Length)].ToString();
            }
            return text;
        }

        public static byte RandomByte()
        {
            return (byte)random.Next(byte.MinValue, byte.MaxValue);
        }

        public static async Task Login()
        {
            if (!File.Exists("Accounts.txt")) return;
            string[] Accounts = File.ReadAllLines("Accounts.txt");

            if (Load.BotAmount > Accounts.Length) Load.BotAmount = Accounts.Length;

            for (int i = 0; i < Load.BotAmount; i++)
            {
                string[] Credentials = Accounts[i].Split(':');
                int index = 0;

                string Mail = Credentials[index++];
                string Password = Credentials[index++];

                WebProxy WebProxy = null;

                if (Credentials[index] != "null")
                {
                    if (Credentials[index + 1].Contains("@"))
                    {
                        string Proxystring = Credentials[index++] + ":" + Credentials[index++] + ":" + Credentials[index++];
                        string[] Splitted = Proxystring.Split(':', '@');
                        string AuthUser = Splitted[0];
                        string AuthPassword = Splitted[1];
                        WebProxy = new WebProxy { Address = new Uri("http://" + Proxystring), Credentials = new NetworkCredential(AuthUser, AuthPassword) };
                    }
                    else
                    {
                        string Proxystring = Credentials[index++] + ":" + Credentials[index++];
                        WebProxy = new WebProxy { Address = new Uri("http://" + Proxystring) };
                    }
                }

                APIClient APIClient = new APIClient();
                if (await APIClient.Login(Mail, Password, WebProxy))
                {
                    Load.RiftClients.Add(new RiftClient(APIClient));
                    continue;
                }

                Logger.LogError($"Failed to Validate Account {Mail}:{Password}");
            }
            Console.Title = $"HexBOT - {Load.RiftClients.Count} Bots | {RandomString(20)}";
        }
    }
}
