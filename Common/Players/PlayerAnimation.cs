using System;
using Terraria.Graphics.CameraModifiers;
using Terraria;
using Microsoft.Xna.Framework;

namespace Terrafirma.Common.Players
{
    public static class PlayerAnimation
    {
        public const int PointDown = 0;
        public const int PointUp = 1;
        public const int PointUpRight = 2;
        public const int PointRight = 3;
        public const int PointDownRight = 4;
        public const int Jump = 5;

        /// <summary>
        /// Only works on the swinging and jumping frames for now
        /// </summary>
        public static Vector2 getFrontArmPosition(this Player player)
        {
            Vector2 vector = new Vector2();
            switch (player.bodyFrame.Y / 56)
            {
                case PointDown:
                    vector = new Vector2(-6, 8);
                    break;
                case PointDownRight:
                    vector = new Vector2(4, 8);
                    break;
                case PointRight:
                    vector = new Vector2(6, 4);
                    break;
                case PointUpRight:
                    vector = new Vector2(4, -10);
                    break;
                case Jump:
                case PointUp:
                    vector = new Vector2(-10, -10);
                    break;
            }
            vector.X += player.direction == -1 ? 2 : 0;
            return new Vector2(vector.X * player.direction, vector.Y * player.gravDir);
        }
        public static bool LegFrameIsOneThatRaisesTheBody(this Player player)
        {
            return player.bodyFrame.Y >= 392 && player.bodyFrame.Y < 560 || player.bodyFrame.Y >= 784 && player.bodyFrame.Y < 952;
        }
        public static void gunStyle(Player player, float rotation = 0.1f, float backwardsMovement = 3f, float screenshakeIntensity = 0f)
        {
            if (player.ItemAnimationJustStarted && screenshakeIntensity != 0 && player.whoAmI == Main.myPlayer)
            {
                PunchCameraModifier modifier = new PunchCameraModifier(player.MountedCenter, new Vector2(Main.rand.NextFloat(-1.5f, -0.7f), 0).RotatedBy(player.MountedCenter.DirectionTo(Main.MouseWorld).ToRotation() + Main.rand.NextFloat(-0.1f, 0.1f)), screenshakeIntensity, 6f, 8, 200f, player.name);
                Main.instance.CameraModifiers.Add(modifier);
            }
            if (player.itemAnimationMax - player.itemAnimation < 15)
            {
                player.itemLocation -= new Vector2((float)Math.Sin((player.itemAnimationMax - player.itemAnimation) * 0.4f) * backwardsMovement * player.direction, 0).RotatedBy(player.itemRotation);

                //player.itemLocation -= new Vector2(((float)(player.itemAnimationMax - player.itemAnimation) / (float)player.itemAnimationMax) * backwardsMovement * player.direction, 0).RotatedBy(player.itemRotation);
                player.itemRotation -= (float)Math.Sin((player.itemAnimationMax - player.itemAnimation) * 0.41f) * rotation * player.direction * player.gravDir;
            }
        }
        public static void ArmPointToDirection(float rotation, Player player)
        {
            int bodyHeight = 56;
            int frame = 0;
            float trueRotation = rotation - MathHelper.PiOver2 * Math.Sign(rotation);
            if (Math.Abs(trueRotation) <= 0.4)
                frame = rotation < 0 ? PointUp : PointDown;
            else if (Math.Abs(trueRotation) <= 1.2)
                frame = rotation < 0 ? PointUpRight : PointDownRight;
            else if (Math.Abs(trueRotation) <= 2.2)
                frame = PointRight;
            if (trueRotation == 0)
                frame = PointRight;
            player.bodyFrame.Y = frame * bodyHeight;

            //Main.NewText("body: " + $"{player.bodyFrame.Height} " + "rotation: " + $"{rotation} " + "trueRotation: " + $"{trueRotation}", Main.DiscoColor);
        }
        public static void ArmPointToDirectionWithoutUpOrDown(float rotation, Player player)
        {
            int bodyHeight = 56;
            int frame = 0;
            float trueRotation = rotation - MathHelper.PiOver2 * Math.Sign(rotation);
            if (Math.Abs(trueRotation) <= 1.2)
                frame = rotation < 0 ? PointUpRight : PointDownRight;
            else
                frame = PointRight;
            player.bodyFrame.Y = frame * bodyHeight;
        }
    }
}
