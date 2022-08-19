using System.Numerics;

namespace HexBot.Movement
{
    public class Positions
    {
		public void SetDeviceType(UsingDeviceType type)
        {
			DeviceType = (short)type;
        }

		public short DeviceType = 1;

		public Vector3 RootPosition = new Vector3(0, 0, 0);
		public Vector3 RootRotation = new Vector3(0, 0, 0);

		public Vector3 BodyPosition = new Vector3(0, 0, 0);
		public Vector3 BodyRotation = new Vector3(0, 0, 0);

		public Vector2 AnimatorMovement = new Vector2(0, 0);

		public Animations Animations = new Animations();

		public enum UsingDeviceType
		{
			PCStanalone,
			PCVR,
			OculusQuest
		}
	}
}
