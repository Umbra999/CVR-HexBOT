using DarkRift;
using System.Numerics;

namespace HexBot.Rift
{
    internal static class Events
    {
        public static void SendAuthentication(this RiftClient Client, string JWT)
        {
            if (Client.ConnectionState != ConnectionState.Connected) return;

            using DarkRiftWriter darkRiftWriter = DarkRiftWriter.Create();
            darkRiftWriter.Write(JWT);
            using Message messagee = Message.Create(1, darkRiftWriter);
            Client.RaiseMessage(messagee, SendMode.Reliable);
        }

        public static void ChangeAvatar(this RiftClient Client, string AvatarID)
        {
            if (Client.ConnectionState != ConnectionState.Connected) return;

            using DarkRiftWriter darkRiftWriter = DarkRiftWriter.Create();
            darkRiftWriter.Write(AvatarID);
            using Message message = Message.Create(14, darkRiftWriter);
            Client.RaiseMessage(message, SendMode.Reliable);
        }

        public static void SpawnPortal(this RiftClient Client, string WorldID, Vector3 Position)
        {
            if (Client.ConnectionState != ConnectionState.Connected) return;

            using DarkRiftWriter darkRiftWriter = DarkRiftWriter.Create();
            darkRiftWriter.Write(WorldID);
            darkRiftWriter.Write(Position.X);
            darkRiftWriter.Write(Position.Y);
            darkRiftWriter.Write(Position.Z);
            using Message message = Message.Create(10000, darkRiftWriter);
            Client.RaiseMessage(message, SendMode.Reliable);
        }

        public static void MoveObject(this RiftClient Client, string ObjectId, float PosX, float PosY, float PosZ, float RotX, float RotY, float RotZ, bool isActive, int syncType, string additionalData = "")
        {
            if (Client.ConnectionState != ConnectionState.Connected) return;

            using DarkRiftWriter darkRiftWriter = DarkRiftWriter.Create();
            darkRiftWriter.Write(ObjectId);
            darkRiftWriter.Write(PosX);
            darkRiftWriter.Write(PosY);
            darkRiftWriter.Write(PosZ);
            darkRiftWriter.Write(RotX);
            darkRiftWriter.Write(RotY);
            darkRiftWriter.Write(RotZ);
            darkRiftWriter.Write(isActive);
            darkRiftWriter.Write(syncType);
            darkRiftWriter.Write(additionalData);
            using Message message = Message.Create(10020, darkRiftWriter);
            Client.RaiseMessage(message, SendMode.Reliable);
        }
    }
}
