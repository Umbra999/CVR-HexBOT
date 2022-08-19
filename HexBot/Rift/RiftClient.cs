using DarkRift;
using DarkRift.Client;
using DarkRift.Dispatching;
using HexBot.Modules;
using HexBot.Wrappers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace HexBot.Rift
{
    internal class RiftClient : DarkRiftClient
    {
        public APIClient APIClient;
        private Dispatcher Dispatcher;
        private string JWT;
        private bool LogRoom;
        public readonly Dictionary<string, string> RoomPlayers = new Dictionary<string, string>();

        public RiftClient(APIClient WebClient)
        {
            APIClient = WebClient;

            MessageReceived += ReceivedEvent;
            Disconnected += OnDisconnected;

            Logger.Log($"{APIClient.APIUser.username} connected as Bot", Logger.LogsType.Info);

            new Thread(() =>
            {
                Dispatcher = new Dispatcher(true);
                for (; ; )
                {
                    Dispatcher.ExecuteDispatcherTasks();

                    if (ConnectionState == ConnectionState.Connected)
                    {
                        using DarkRiftWriter darkRiftWriter = DarkRiftWriter.Create();
                        using Message message = Message.Create(5019, darkRiftWriter);
                        message.MakePingMessage();
                        RaiseMessage(message, SendMode.Reliable);
                    }

                    Thread.Sleep(1);
                }
            })
            { IsBackground = true }.Start();
        }

        private void OnConnectionComplete(Exception exc)
        {
            if (exc == null) Logger.LogWarning($"{APIClient.APIUser.username} connected to Server");
            else Logger.LogError($"{APIClient.APIUser.username} failed to connect to Server | Exception: " + exc.Message);
        }

        public async Task<bool> JoinRoom(string InstanceID, bool LogInstance)
        {
            RoomPlayers.Clear();
            LogRoom = LogInstance;
            ConnectCompleteHandler Handler = new ConnectCompleteHandler((e) =>
            {
                OnConnectionComplete(e);
            });

            Misc.InstanceToken Token = await APIClient.GetInstanceToken(InstanceID);
            if (Token == null)
            {
                Logger.LogError($"{APIClient.APIUser.username} failed to recevie Token");
                return false;
            }

            JWT = Token.jwt;

            IPHostEntry hostEntry = Dns.GetHostEntry(Token.host.fqdn);
            if (hostEntry.AddressList.Length < 1)
            {
                Logger.LogError($"{APIClient.APIUser.username} failed to resolve IP");
                return false;
            }

            ConnectInBackground(hostEntry.AddressList[0], Token.host.port, IPVersion.IPv4, Handler);
            return true;
        }

        private void ReceivedEvent(object sender, MessageReceivedEventArgs e)
        {
            Message message = e.GetMessage();
            Misc.Tags EventCode = (Misc.Tags)message.Tag;

            //Logger.LogDebug("Received Game Message | " + EventCode + " | DataLength: " + message.DataLength);
            switch (EventCode)
            {
                case Misc.Tags.PleaseAuthenticate:
                    this.SendAuthentication(JWT);
                    Task.Run(() => OnJoinedRoom());
                    break;

                case Misc.Tags.NetworkedRootData:
                    {
                        using DarkRiftReader darkRiftReader = message.GetReader();
                        string uid = darkRiftReader.ReadString();
                        if (Movement.MovementEvent.CopyTarget == uid)
                        {
                            Movement.MovementEvent Event = Movement.MovementEvent.Deserialize(message);

                            using Message Data = Message.Create(20, Event.Serialize());
                            RaiseMessage(Data, SendMode.Unreliable);
                        }
                    }
                    break;

                case Misc.Tags.UserAccountingData:
                    {
                        using DarkRiftReader darkRiftReader = message.GetReader();
                        string uid = darkRiftReader.ReadString();
                        string username = darkRiftReader.ReadString();

                        if (!RoomPlayers.ContainsKey(uid))
                        {
                            RoomPlayers.Add(uid, username);
                            Logger.Log($"[ + ] {username} [{uid}]", Logger.LogsType.Clean);
                        }
                    }
                    break;

                case Misc.Tags.PlayerDisconnection:
                    {
                        using DarkRiftReader darkRiftReader = message.GetReader();
                        string uid = darkRiftReader.ReadString();

                        if (RoomPlayers.ContainsKey(uid))
                        {
                            Logger.Log($"[ - ] {RoomPlayers[uid]} [{uid}]", Logger.LogsType.Clean);
                            RoomPlayers.Remove(uid);
                        }
                    }
                    break;
            }
        }

        private async Task OnJoinedRoom()
        {
            Logger.LogWarning($"{APIClient.APIUser.username} authenticated to Server");
            if (LogRoom)
            {
                JwtSecurityToken Token = new JwtSecurityTokenHandler().ReadJwtToken(JWT);
                string InstanceID = Token.Claims.First(claim => claim.Type == "InstanceId").Value;

                Misc.APIInstance CurrentRoom = await APIClient.GetInstance(InstanceID);
                if (CurrentRoom != null)
                {
                    foreach (string UserID in RoomPlayers.Keys)
                    {
                        Misc.APIUser User = await APIClient.GetUser(UserID);
                        Searching.LogPlayer(User, CurrentRoom);
                    }
                }
            }
        }

        private void OnDisconnected(object sender, DisconnectedEventArgs e)
        {
            Logger.LogWarning($"{APIClient.APIUser.username} diconnected");
        }

        public bool RaiseMessage(Message message, SendMode sendMode)
        {
            return SendMessage(message, sendMode);
        }
    }
}
