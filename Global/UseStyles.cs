using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Graphics.CameraModifiers;
using Terraria;
using Microsoft.Xna.Framework;

namespace Terrafirma.Global
{
    public class UseStyles
    {
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
    }
}
