using DarkRift;
using System.Numerics;

namespace HexBot.Movement
{
    internal class MovementEvent
    {
        public static string CopyTarget = "";
        public static float PosXOffset = 0;
        public static float PosYOffset = 0;
        public static float PosZOffset = 0;

        public static float RotXOffset = 0;
        public static float RotYOffset = 0;
        public static float RotZOffset = 0;

        public Positions Positions = new Positions();

        public static MovementEvent Deserialize(Message Data)
        {
            MovementEvent Event = new MovementEvent();

            using DarkRiftReader darkRiftReader = Data.GetReader();
            string uid = darkRiftReader.ReadString();

            Event.Positions.DeviceType = darkRiftReader.ReadInt16();

            Event.Positions.RootPosition = new Vector3(darkRiftReader.ReadSingle(), darkRiftReader.ReadSingle(), darkRiftReader.ReadSingle());
            Event.Positions.RootRotation = new Vector3(darkRiftReader.ReadSingle(), darkRiftReader.ReadSingle(), darkRiftReader.ReadSingle());
            Event.Positions.BodyPosition = new Vector3(darkRiftReader.ReadSingle(), darkRiftReader.ReadSingle(), darkRiftReader.ReadSingle());
            Event.Positions.BodyRotation = new Vector3(darkRiftReader.ReadSingle(), darkRiftReader.ReadSingle(), darkRiftReader.ReadSingle());

            Event.Positions.AnimatorMovement = new Vector2(darkRiftReader.ReadSingle(), darkRiftReader.ReadSingle());
            Event.Positions.Animations.AnimatorGrounded = darkRiftReader.ReadBoolean();
            Event.Positions.Animations.AnimatorEmote = darkRiftReader.ReadSingle();
            Event.Positions.Animations.AnimatorCancelEmote = darkRiftReader.ReadBoolean();
            Event.Positions.Animations.AnimatorGestureLeft = darkRiftReader.ReadSingle();
            Event.Positions.Animations.AnimatorGestureRight = darkRiftReader.ReadSingle();
            Event.Positions.Animations.AnimatorToggle = darkRiftReader.ReadSingle();
            Event.Positions.Animations.AnimatorSitting = darkRiftReader.ReadBoolean();
            Event.Positions.Animations.AnimatorCrouching = darkRiftReader.ReadBoolean();
            Event.Positions.Animations.AnimatorFlying = darkRiftReader.ReadBoolean();
            Event.Positions.Animations.AnimatorProne = darkRiftReader.ReadBoolean();
            Event.Positions.Animations.SpineFrontBack = darkRiftReader.ReadSingle();
            Event.Positions.Animations.SpineLeftRight = darkRiftReader.ReadSingle();
            Event.Positions.Animations.SpineTwistLeftRight = darkRiftReader.ReadSingle();
            Event.Positions.Animations.ChestFrontBack = darkRiftReader.ReadSingle();
            Event.Positions.Animations.ChestLeftRight = darkRiftReader.ReadSingle();
            Event.Positions.Animations.ChestTwistLeftRight = darkRiftReader.ReadSingle();
            Event.Positions.Animations.UpperChestFrontBack = darkRiftReader.ReadSingle();
            Event.Positions.Animations.UpperChestLeftRight = darkRiftReader.ReadSingle();
            Event.Positions.Animations.UpperChestTwistLeftRight = darkRiftReader.ReadSingle();
            Event.Positions.Animations.NeckNodDownUp = darkRiftReader.ReadSingle();
            Event.Positions.Animations.NeckTiltLeftRight = darkRiftReader.ReadSingle();
            Event.Positions.Animations.NeckTurnLeftRight = darkRiftReader.ReadSingle();
            Event.Positions.Animations.HeadNodDownUp = darkRiftReader.ReadSingle();
            Event.Positions.Animations.HeadTiltLeftRight = darkRiftReader.ReadSingle();
            Event.Positions.Animations.HeadTurnLeftRight = darkRiftReader.ReadSingle();
            Event.Positions.Animations.LeftUpperLegFrontBack = darkRiftReader.ReadSingle();
            Event.Positions.Animations.LeftUpperLegInOut = darkRiftReader.ReadSingle();
            Event.Positions.Animations.LeftUpperLegTwistInOut = darkRiftReader.ReadSingle();
            Event.Positions.Animations.LeftLowerLegStretch = darkRiftReader.ReadSingle();
            Event.Positions.Animations.LeftLowerLegTwistInOut = darkRiftReader.ReadSingle();
            Event.Positions.Animations.LeftFootUpDown = darkRiftReader.ReadSingle();
            Event.Positions.Animations.LeftFootTwistInOut = darkRiftReader.ReadSingle();
            Event.Positions.Animations.LeftToesUpDown = darkRiftReader.ReadSingle();
            Event.Positions.Animations.RightUpperLegFrontBack = darkRiftReader.ReadSingle();
            Event.Positions.Animations.RightUpperLegInOut = darkRiftReader.ReadSingle();
            Event.Positions.Animations.RightUpperLegTwistInOut = darkRiftReader.ReadSingle();
            Event.Positions.Animations.RightLowerLegStretch = darkRiftReader.ReadSingle();
            Event.Positions.Animations.RightLowerLegTwistInOut = darkRiftReader.ReadSingle();
            Event.Positions.Animations.RightFootUpDown = darkRiftReader.ReadSingle();
            Event.Positions.Animations.RightFootTwistInOut = darkRiftReader.ReadSingle();
            Event.Positions.Animations.RightToesUpDown = darkRiftReader.ReadSingle();
            Event.Positions.Animations.LeftShoulderDownUp = darkRiftReader.ReadSingle();
            Event.Positions.Animations.LeftShoulderFrontBack = darkRiftReader.ReadSingle();
            Event.Positions.Animations.LeftArmDownUp = darkRiftReader.ReadSingle();
            Event.Positions.Animations.LeftArmFrontBack = darkRiftReader.ReadSingle();
            Event.Positions.Animations.LeftArmTwistInOut = darkRiftReader.ReadSingle();
            Event.Positions.Animations.LeftForearmStretch = darkRiftReader.ReadSingle();
            Event.Positions.Animations.LeftForearmTwistInOut = darkRiftReader.ReadSingle();
            Event.Positions.Animations.LeftHandDownUp = darkRiftReader.ReadSingle();
            Event.Positions.Animations.LeftHandInOut = darkRiftReader.ReadSingle();
            Event.Positions.Animations.RightShoulderDownUp = darkRiftReader.ReadSingle();
            Event.Positions.Animations.RightShoulderFrontBack = darkRiftReader.ReadSingle();
            Event.Positions.Animations.RightArmDownUp = darkRiftReader.ReadSingle();
            Event.Positions.Animations.RightArmFrontBack = darkRiftReader.ReadSingle();
            Event.Positions.Animations.RightArmTwistInOut = darkRiftReader.ReadSingle();
            Event.Positions.Animations.RightForearmStretch = darkRiftReader.ReadSingle();
            Event.Positions.Animations.RightForearmTwistInOut = darkRiftReader.ReadSingle();
            Event.Positions.Animations.RightHandDownUp = darkRiftReader.ReadSingle();
            Event.Positions.Animations.RightHandInOut = darkRiftReader.ReadSingle();
            Event.Positions.Animations.IndexUseIndividualFingers = darkRiftReader.ReadBoolean();
            if (Event.Positions.Animations.IndexUseIndividualFingers)
            {
                Event.Positions.Animations.IndividualFingers.LeftThumbCurl = darkRiftReader.ReadSingle();
                Event.Positions.Animations.IndividualFingers.LeftThumbSpread = darkRiftReader.ReadSingle();
                Event.Positions.Animations.IndividualFingers.LeftIndexCurl = darkRiftReader.ReadSingle();
                Event.Positions.Animations.IndividualFingers.LeftIndexSpread = darkRiftReader.ReadSingle();
                Event.Positions.Animations.IndividualFingers.LeftMiddleCurl = darkRiftReader.ReadSingle();
                Event.Positions.Animations.IndividualFingers.LeftMiddleSpread = darkRiftReader.ReadSingle();
                Event.Positions.Animations.IndividualFingers.LeftRingCurl = darkRiftReader.ReadSingle();
                Event.Positions.Animations.IndividualFingers.LeftRingSpread = darkRiftReader.ReadSingle();
                Event.Positions.Animations.IndividualFingers.LeftPinkyCurl = darkRiftReader.ReadSingle();
                Event.Positions.Animations.IndividualFingers.LeftPinkySpread = darkRiftReader.ReadSingle();
                Event.Positions.Animations.IndividualFingers.RightThumbCurl = darkRiftReader.ReadSingle();
                Event.Positions.Animations.IndividualFingers.RightThumbSpread = darkRiftReader.ReadSingle();
                Event.Positions.Animations.IndividualFingers.RightIndexCurl = darkRiftReader.ReadSingle();
                Event.Positions.Animations.IndividualFingers.RightIndexSpread = darkRiftReader.ReadSingle();
                Event.Positions.Animations.IndividualFingers.RightMiddleCurl = darkRiftReader.ReadSingle();
                Event.Positions.Animations.IndividualFingers.RightMiddleSpread = darkRiftReader.ReadSingle();
                Event.Positions.Animations.IndividualFingers.RightRingCurl = darkRiftReader.ReadSingle();
                Event.Positions.Animations.IndividualFingers.RightRingSpread = darkRiftReader.ReadSingle();
                Event.Positions.Animations.IndividualFingers.RightPinkyCurl = darkRiftReader.ReadSingle();
                Event.Positions.Animations.IndividualFingers.RightPinkySpread = darkRiftReader.ReadSingle();
            }
            Event.Positions.Animations.CameraEnabled = darkRiftReader.ReadBoolean();
            if (Event.Positions.Animations.CameraEnabled)
            {
                Event.Positions.Animations.Camera.CameraPosition = new Vector3(darkRiftReader.ReadSingle(), darkRiftReader.ReadSingle(), darkRiftReader.ReadSingle());
                Event.Positions.Animations.Camera.CameraRotation = new Vector3(darkRiftReader.ReadSingle(), darkRiftReader.ReadSingle(), darkRiftReader.ReadSingle());
            }
            Event.Positions.Animations.EyeTrackingOverride = darkRiftReader.ReadBoolean();
            if (Event.Positions.Animations.EyeTrackingOverride)
            {
                Event.Positions.Animations.EyeTracking.EyeTrackingPosition = new Vector3(darkRiftReader.ReadSingle(), darkRiftReader.ReadSingle(), darkRiftReader.ReadSingle());
            }
            Event.Positions.Animations.EyeBlinkingOverride = darkRiftReader.ReadBoolean();
            if (Event.Positions.Animations.EyeBlinkingOverride)
            {
                Event.Positions.Animations.EyeBlinking.EyeBlinkingPositions = new Vector2(darkRiftReader.ReadSingle(), darkRiftReader.ReadSingle());
            }
            Event.Positions.Animations.FaceTrackingEnabled = darkRiftReader.ReadBoolean();
            if (Event.Positions.Animations.FaceTrackingEnabled)
            {
                for (int i = 0; i < 37; i++)
                {
                    Event.Positions.Animations.FaceTracking.FaceTrackingData[i] = darkRiftReader.ReadSingle();
                }
            }

            return Event;
        }

        public DarkRiftWriter Serialize()
        {
            using DarkRiftWriter darkRiftWriter = DarkRiftWriter.Create();
            // Device Type
            darkRiftWriter.Write(Positions.DeviceType);

            // Root Positions
            darkRiftWriter.Write(Positions.RootPosition.X + PosXOffset);
            darkRiftWriter.Write(Positions.RootPosition.Y + PosYOffset);
            darkRiftWriter.Write(Positions.RootPosition.Z + PosZOffset);

            // Root Rotations
            darkRiftWriter.Write(Positions.RootRotation.X + RotXOffset);
            darkRiftWriter.Write(Positions.RootRotation.Y + RotYOffset);
            darkRiftWriter.Write(Positions.RootRotation.Z + RotZOffset);

            // Body Position
            darkRiftWriter.Write(Positions.BodyPosition.X + PosXOffset);
            darkRiftWriter.Write(Positions.BodyPosition.Y + PosYOffset);
            darkRiftWriter.Write(Positions.BodyPosition.Z + PosZOffset);

            // Body Rotation
            darkRiftWriter.Write(Positions.BodyRotation.X + RotXOffset);
            darkRiftWriter.Write(Positions.BodyRotation.Y + RotYOffset);
            darkRiftWriter.Write(Positions.BodyRotation.Z + RotZOffset);

            // Animator Movement
            darkRiftWriter.Write(Positions.AnimatorMovement.X);
            darkRiftWriter.Write(Positions.AnimatorMovement.Y);

            // Animations
            darkRiftWriter.Write(Positions.Animations.AnimatorGrounded);
            darkRiftWriter.Write(Positions.Animations.AnimatorEmote);
            darkRiftWriter.Write(Positions.Animations.AnimatorCancelEmote);
            darkRiftWriter.Write(Positions.Animations.AnimatorGestureLeft);
            darkRiftWriter.Write(Positions.Animations.AnimatorGestureRight);
            darkRiftWriter.Write(Positions.Animations.AnimatorToggle);
            darkRiftWriter.Write(Positions.Animations.AnimatorSitting);
            darkRiftWriter.Write(Positions.Animations.AnimatorCrouching);
            darkRiftWriter.Write(Positions.Animations.AnimatorFlying);
            darkRiftWriter.Write(Positions.Animations.AnimatorProne);

            // Spine Positions
            darkRiftWriter.Write(Positions.Animations.SpineFrontBack);
            darkRiftWriter.Write(Positions.Animations.SpineLeftRight);
            darkRiftWriter.Write(Positions.Animations.SpineTwistLeftRight);

            // Chest Positions
            darkRiftWriter.Write(Positions.Animations.ChestFrontBack);
            darkRiftWriter.Write(Positions.Animations.ChestLeftRight);
            darkRiftWriter.Write(Positions.Animations.ChestTwistLeftRight);

            // Upper Chest Positions
            darkRiftWriter.Write(Positions.Animations.UpperChestFrontBack);
            darkRiftWriter.Write(Positions.Animations.UpperChestLeftRight);
            darkRiftWriter.Write(Positions.Animations.UpperChestTwistLeftRight);

            // Neck Positions
            darkRiftWriter.Write(Positions.Animations.NeckNodDownUp);
            darkRiftWriter.Write(Positions.Animations.NeckTiltLeftRight);
            darkRiftWriter.Write(Positions.Animations.NeckTurnLeftRight);

            // Head Positions
            darkRiftWriter.Write(Positions.Animations.HeadNodDownUp);
            darkRiftWriter.Write(Positions.Animations.HeadTiltLeftRight);
            darkRiftWriter.Write(Positions.Animations.HeadTurnLeftRight);

            // Left Upper Leg Positions
            darkRiftWriter.Write(Positions.Animations.LeftUpperLegFrontBack);
            darkRiftWriter.Write(Positions.Animations.LeftUpperLegInOut);
            darkRiftWriter.Write(Positions.Animations.LeftUpperLegTwistInOut);

            // Left Lower Leg Positions
            darkRiftWriter.Write(Positions.Animations.LeftLowerLegStretch);
            darkRiftWriter.Write(Positions.Animations.LeftLowerLegTwistInOut);

            // Left Foot Positions
            darkRiftWriter.Write(Positions.Animations.LeftFootUpDown);
            darkRiftWriter.Write(Positions.Animations.LeftFootTwistInOut);

            // Left Toes Positions
            darkRiftWriter.Write(Positions.Animations.LeftToesUpDown);

            // Right Upper Leg Positions
            darkRiftWriter.Write(Positions.Animations.RightUpperLegFrontBack);
            darkRiftWriter.Write(Positions.Animations.RightUpperLegInOut);
            darkRiftWriter.Write(Positions.Animations.RightUpperLegTwistInOut);

            // Right Lower Leg Positions
            darkRiftWriter.Write(Positions.Animations.RightLowerLegStretch);
            darkRiftWriter.Write(Positions.Animations.RightLowerLegTwistInOut);

            // Right Foot Positions
            darkRiftWriter.Write(Positions.Animations.RightFootUpDown);
            darkRiftWriter.Write(Positions.Animations.RightFootTwistInOut);

            // Right Toes Positions
            darkRiftWriter.Write(Positions.Animations.RightToesUpDown);

            // Left Shoulder Positions
            darkRiftWriter.Write(Positions.Animations.LeftShoulderDownUp);
            darkRiftWriter.Write(Positions.Animations.LeftShoulderDownUp);
            darkRiftWriter.Write(Positions.Animations.LeftShoulderFrontBack);

            // Left Arm Positions
            darkRiftWriter.Write(Positions.Animations.LeftArmDownUp);
            darkRiftWriter.Write(Positions.Animations.LeftArmFrontBack);
            darkRiftWriter.Write(Positions.Animations.LeftArmTwistInOut);

            // Left Forearm Positions
            darkRiftWriter.Write(Positions.Animations.LeftForearmStretch);
            darkRiftWriter.Write(Positions.Animations.LeftForearmTwistInOut);

            // Left Hand Positions
            darkRiftWriter.Write(Positions.Animations.LeftHandDownUp);
            darkRiftWriter.Write(Positions.Animations.LeftHandInOut);

            // Right Shoulder Positions
            darkRiftWriter.Write(Positions.Animations.RightShoulderDownUp);
            darkRiftWriter.Write(Positions.Animations.RightShoulderFrontBack);

            // Right Arm Positions
            darkRiftWriter.Write(Positions.Animations.RightArmDownUp);
            darkRiftWriter.Write(Positions.Animations.RightArmFrontBack);
            darkRiftWriter.Write(Positions.Animations.RightArmTwistInOut);

            // Right Forearm Positions
            darkRiftWriter.Write(Positions.Animations.RightForearmStretch);
            darkRiftWriter.Write(Positions.Animations.RightForearmTwistInOut);

            // Right Hand Positions
            darkRiftWriter.Write(Positions.Animations.RightHandDownUp);
            darkRiftWriter.Write(Positions.Animations.RightHandInOut);

            // Toggle to use fingers
            darkRiftWriter.Write(Positions.Animations.IndexUseIndividualFingers);

            if (Positions.Animations.IndexUseIndividualFingers)
            {
                // Left
                darkRiftWriter.Write(Positions.Animations.IndividualFingers.LeftThumbCurl);
                darkRiftWriter.Write(Positions.Animations.IndividualFingers.LeftThumbSpread);
                darkRiftWriter.Write(Positions.Animations.IndividualFingers.LeftIndexCurl);
                darkRiftWriter.Write(Positions.Animations.IndividualFingers.LeftIndexSpread);
                darkRiftWriter.Write(Positions.Animations.IndividualFingers.LeftMiddleCurl);
                darkRiftWriter.Write(Positions.Animations.IndividualFingers.LeftMiddleSpread);
                darkRiftWriter.Write(Positions.Animations.IndividualFingers.LeftRingCurl);
                darkRiftWriter.Write(Positions.Animations.IndividualFingers.LeftRingSpread);
                darkRiftWriter.Write(Positions.Animations.IndividualFingers.LeftPinkyCurl);
                darkRiftWriter.Write(Positions.Animations.IndividualFingers.LeftPinkySpread);

                // Right
                darkRiftWriter.Write(Positions.Animations.IndividualFingers.RightThumbCurl);
                darkRiftWriter.Write(Positions.Animations.IndividualFingers.RightThumbSpread);
                darkRiftWriter.Write(Positions.Animations.IndividualFingers.RightIndexCurl);
                darkRiftWriter.Write(Positions.Animations.IndividualFingers.RightIndexSpread);
                darkRiftWriter.Write(Positions.Animations.IndividualFingers.RightMiddleCurl);
                darkRiftWriter.Write(Positions.Animations.IndividualFingers.RightMiddleSpread);
                darkRiftWriter.Write(Positions.Animations.IndividualFingers.RightRingCurl);
                darkRiftWriter.Write(Positions.Animations.IndividualFingers.RightRingSpread);
                darkRiftWriter.Write(Positions.Animations.IndividualFingers.RightPinkyCurl);
                darkRiftWriter.Write(Positions.Animations.IndividualFingers.RightPinkySpread);
            }

            // Camera Toggle
            darkRiftWriter.Write(Positions.Animations.CameraEnabled);

            if (Positions.Animations.CameraEnabled)
            {
                // Camera Position
                darkRiftWriter.Write(Positions.Animations.Camera.CameraPosition.X);
                darkRiftWriter.Write(Positions.Animations.Camera.CameraPosition.Y);
                darkRiftWriter.Write(Positions.Animations.Camera.CameraPosition.Z);

                // Camera Rotation
                darkRiftWriter.Write(Positions.Animations.Camera.CameraRotation.X);
                darkRiftWriter.Write(Positions.Animations.Camera.CameraRotation.Y);
                darkRiftWriter.Write(Positions.Animations.Camera.CameraRotation.Z);
            }

            // Eye Tracking Toggle
            darkRiftWriter.Write(Positions.Animations.EyeTrackingOverride);

            if (Positions.Animations.EyeTrackingOverride)
            {
                // Eye Positions
                darkRiftWriter.Write(Positions.Animations.EyeTracking.EyeTrackingPosition.X);
                darkRiftWriter.Write(Positions.Animations.EyeTracking.EyeTrackingPosition.Y);
                darkRiftWriter.Write(Positions.Animations.EyeTracking.EyeTrackingPosition.Z);
            }

            // Eye Blinking Toggle
            darkRiftWriter.Write(Positions.Animations.EyeBlinkingOverride);

            if (Positions.Animations.EyeBlinkingOverride)
            {
                // Eye Blinking Positions
                darkRiftWriter.Write(Positions.Animations.EyeBlinking.EyeBlinkingPositions.X);
                darkRiftWriter.Write(Positions.Animations.EyeBlinking.EyeBlinkingPositions.Y);
            }

            // Face Tracking Toggle
            darkRiftWriter.Write(Positions.Animations.FaceTrackingEnabled);

            if (Positions.Animations.FaceTrackingEnabled)
            {
                // Face Tracking Data
                for (int i = 0; i < 37; i++)
                {
                    darkRiftWriter.Write(Positions.Animations.FaceTracking.FaceTrackingData[i]);
                }
            }

            return darkRiftWriter;
        }
    }
}
