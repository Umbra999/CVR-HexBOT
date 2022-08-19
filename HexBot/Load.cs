using HexBot.Modules;
using HexBot.Rift;
using HexBot.Wrappers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HexBot
{
    internal class Load
    {
        public static List<RiftClient> RiftClients = new List<RiftClient>();
        public static int BotAmount = int.MaxValue;
        private static readonly List<string> Accounts = new List<string>();

        static void Main(string[] args)
        {
            Console.Title = $"HexBOT | Starting...";
            Task.Run(() => CreateClients());
            Thread.Sleep(-1);
        }

        private static async Task CreateClients()
        {
            await Misc.Login();
            RunGUI();
        }

        private static void RunGUI()
        {
            for (; ; )
            {
                Logger.LogWarning("J [WORLDID] | Join into a World");
                Logger.LogWarning("L | Leave the World");
                Logger.LogWarning("A [AVATARID] | Change the Avatar");
                Logger.LogWarning("C [UserID] | Copy the Users Movement");
                Logger.LogWarning("S | Search Users in a Loop");
                Logger.LogWarning("X | Avatar Lagger Exploit");

                string Input = Console.ReadLine();
                string InputStart = Input.Split(' ')[0];
                switch (InputStart.ToLower())
                {
                    case "j":
                        Misc.JoinRoomAll(Input.Substring(2));
                        break;

                    case "l":
                        Misc.LeaveRoomAll();
                        break;

                    case "a":
                        foreach (RiftClient Client in RiftClients)
                        {
                            Client.ChangeAvatar(Input.Substring(2));
                        }
                        break;

                    case "c":
                        Movement.MovementEvent.CopyTarget = Input.Substring(2);
                        break;

                    case "s":
                        Task.Run(() => Searching.DoSearchLoop(false, true));
                        break;

                    case "x":
                        foreach (RiftClient Client in RiftClients)
                        {
                            Task.Run(() => Exploits.AvatarLagger(Client));
                        }               
                        break;
                }
            }
        }
    }
}
