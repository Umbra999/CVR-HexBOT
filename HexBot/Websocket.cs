using HexBot.Wrappers;
using WebSocketSharpManaged;

namespace HexBot
{
    internal class Websocket
    {
        public static WebSocket WSClient;

        public static void Initialize()
        {
            WSClient = new WebSocket("ws://localhost:6666/Bot");
            WSClient.Log.Level = LogLevel.Fatal;
            WSClient.OnMessage += OnServerMessage;
            WSClient.OnError += OnError;
            WSClient.OnClose += OnClose;
            WSClient.Connect();
        }

        public static void SendMessage(string Message)
        {
            if (WSClient != null) WSClient.Send(Message);
        }

        public static void OnClose(object Sender, CloseEventArgs Close)
        {
            Wrappers.Logger.LogDebug("Photonbot disconnected from Websocket");
            WSClient.Connect();
        }

        public static void OnError(object Sender, ErrorEventArgs Error)
        {
            Wrappers.Logger.LogDebug($"PhotonBot had errors: {Error.Exception} with {Error.Message}");
        }

        public static void OnServerMessage(object Sender, MessageEventArgs Message)
        {
            CommandsHandler(Message.Data.Split('/'));
        }

        private static void CommandsHandler(string[] FullCommand)
        {
            string Command = "";
            string Data = "";
            string SecondData = "";
            string ThirdData = "";
            if (FullCommand.Length > 0) Command = FullCommand[0];
            if (FullCommand.Length > 1) Data = FullCommand[1];
            if (FullCommand.Length > 2) SecondData = FullCommand[2];
            if (FullCommand.Length > 3) ThirdData = FullCommand[3];

            switch (Command)
            {
                case "":
                    break;
            }
        }
    }
}
