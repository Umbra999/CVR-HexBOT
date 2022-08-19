using HexBot.Rift;
using HexBot.Wrappers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static HexBot.Wrappers.Misc;

namespace HexBot.Modules
{
    internal class Searching
    {
        private static readonly List<string> UsersToSearch = new List<string>();
        private static readonly List<string> ModeratorToSearch = new List<string>();
        private static readonly List<string> StreamerToSearch = new List<string>();
        private static readonly List<string> CreatorToSearch = new List<string>();
        private static readonly List<string> UsersToKOS = new List<string>();

        public static async Task DoSearchLoop(bool JoinCheck, bool APICheck)
        {
            Logger.LogWarning("Starting User Search");
            await DownloadSearchlist();
            int CurrentBotIndex = 0;

            for (; ; )
            {
                if (CurrentBotIndex >= Load.RiftClients.Count) CurrentBotIndex = 0;
                LimitedAPIWorld[] LimitedWorlds = await Load.RiftClients[CurrentBotIndex].APIClient.GetActiveWorlds();

                int CurrentWorldIndex = 1;
                foreach (LimitedAPIWorld LimitedWorld in LimitedWorlds)
                {
                    try
                    {
                        if (CurrentBotIndex >= Load.RiftClients.Count) CurrentBotIndex = 0;
                        RiftClient Bot = Load.RiftClients[CurrentBotIndex++];

                        Logger.LogDebug($"Searching World {CurrentWorldIndex++}/{LimitedWorlds.Length}");

                        APIWorld FullWorld = await Bot.APIClient.GetWorld(LimitedWorld.id);

                        if (FullWorld == null || FullWorld.instances.Length == 0)
                        {
                            Logger.LogDebug($"{LimitedWorld.name} has no Instances");
                            continue;
                        }

                        foreach (LimitedAPIInstance LimitedInstance in FullWorld.instances)
                        {
                            if (JoinCheck) await CheckInstance(Bot, LimitedInstance.id);

                            if (APICheck)
                            {
                                APIInstance FullInstance = await Bot.APIClient.GetInstance(LimitedInstance.id);
                                if (FullInstance == null || FullInstance.currentPlayerCount == 0 || FullInstance.members.Length == 0)
                                {
                                    Logger.LogDebug($"{LimitedInstance.name} has no Players");
                                    continue;
                                }

                                foreach (LimitedAPIUser LimitedUser in FullInstance.members)
                                {
                                    APIUser FullUser = await Bot.APIClient.GetUser(LimitedUser.id);
                                    LogPlayer(FullUser, FullInstance);     
                                }
                            }
                        }

                    }
                    catch { }
                }
            }
        }

        public static async Task DownloadSearchlist()
        {
            HttpClient CurrentClient = new HttpClient();
            CurrentClient.DefaultRequestHeaders.Add("User-Agent", "Hexed");
            HttpRequestMessage SearchUserPayload = new HttpRequestMessage(HttpMethod.Post, FromBase64("aHR0cDovLzYyLjY4Ljc1LjUyOjk5OS9HZXRTZWFyY2hVc2Vy"))
            {
                Content = new StringContent(JsonConvert.SerializeObject(new { Auth = "SERVER" }), Encoding.UTF8, "application/json")
            };
            HttpResponseMessage Response = await CurrentClient.SendAsync(SearchUserPayload);

            if (Response.IsSuccessStatusCode)
            {
                string FullString = await Response.Content.ReadAsStringAsync();
                string[] Search = FullString.Trim('\n', '\r', ' ').Split(new[] { Environment.NewLine }, StringSplitOptions.None);

                for (int i = 0; i < Search.Length; i++)
                {
                    string[] CurrentUser = Search[i].Split(':');
                    switch (CurrentUser[1])
                    {
                        case "Mod":
                            ModeratorToSearch.Add(CurrentUser[0]);
                            break;

                        case "Creator":
                            CreatorToSearch.Add(CurrentUser[0]);
                            break;

                        case "KOS":
                            UsersToKOS.Add(CurrentUser[0]);
                            break;

                        case "Streamer":
                            StreamerToSearch.Add(CurrentUser[0]);
                            break;

                        case "User":
                            UsersToSearch.Add(CurrentUser[0]);
                            break;
                    }
                }
            }
        }

        public static async Task CheckInstance(RiftClient Bot, string Instance)
        {
            if (await Bot.JoinRoom(Instance, true))
            {
                int i = 0;
                while (Bot.ConnectionState != DarkRift.ConnectionState.Connected)
                {
                    if (i++ > 20) break;
                    await Task.Delay(500);
                }
            }
        }

        private static bool CheckSearchbotUser(string Id)
        {
            return UsersToSearch.Contains(Id);
        }

        private static bool CheckSearchbotCreator(string Id)
        {
            return CreatorToSearch.Contains(Id);
        }

        private static bool CheckSearchbotStreamer(string Id)
        {
            return StreamerToSearch.Contains(Id);
        }

        private static bool CheckSearchbotModerator(string Id)
        {
            return ModeratorToSearch.Contains(Id);
        }

        private static bool CheckKOSUser(string Id)
        {
            return UsersToKOS.Contains(Id);
        }

        public static void LogPlayer(APIUser Player, APIInstance Instance)
        {
            string UserID = Player.id;
            var PlayerInfos = new
            {
                name = "User",
                value = $"**{Player.name}** [**{UserID}**] \n**{Player.rank}**"
            };

            var AvatarInfos = new
            {
                name = "Avatar",
                value = $"**{Player.avatar.name}** [**{Player.avatar.id}**]"
            };

            var WorldInfos = new
            {
                name = "World",
                value = $"**{Instance.name}** [**{Instance.currentPlayerCount}**] \n**{Instance.id}**"
            };

            object[] Fields = new object[]
            {
                PlayerInfos,
                AvatarInfos,
                WorldInfos
            };

            var Embed = new
            {
                title = "Player Found",
                color = 11342935,
                fields = Fields
            };

            object[] Embeds = new object[]
            {
                Embed
            };

            if (CheckSearchbotUser(UserID)) SendEmbedWebHook("https://discord.com/api/webhooks/1006963648373203014/jJ3iINk_-N32U8b3FHZW5Ce-nAHvvwsCnA4cmKTQkRs8H29jCUvVrvl_eT7gY6s_Gui-", Embeds);
            else if (CheckSearchbotCreator(UserID)) SendEmbedWebHook("https://discord.com/api/webhooks/1006963982118166548/isQ4p1A1P-ZOzX_VdHZSaYk8urgCCIK7oXDwoD-lngo1NCEa-3O-lY6SUjoAp3d4sabg", Embeds);
            else if (CheckSearchbotStreamer(UserID)) SendEmbedWebHook("https://discord.com/api/webhooks/1006964079799316530/rVE7e4gHrXWx2pr7RgGXCD8r6pWpWewrxoD0jCEYim6J5qw1fTE9quL-wznGaOcngcZs", Embeds);
            else if (CheckKOSUser(UserID)) SendEmbedWebHook("https://discord.com/api/webhooks/1006964240223055963/Ohuctcud0sGAS0m_5EitZdRasG3i0adGYTEn1dOVsjVlrVScwBf2LbLoRls_XgeWwTMo", Embeds);
            else if (Player.rank == "Developer" || Player.rank == "Moderator" || CheckSearchbotModerator(UserID)) SendEmbedWebHook("https://discord.com/api/webhooks/1006963766954565772/8i8WzTuE6gjApWE0WvFELHR9xT13A9t9m4Bb5fbKXChSAxyT_hvy0vB2jgQfuKRO9oav", Embeds);
        }
    }
}
