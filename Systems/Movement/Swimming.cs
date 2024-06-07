using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Systems.Movement
{
    public class Swimming : ModPlayer
    {
        public int swimRotation;
        public int OutOfWaterRotationTimerThing;
        public override void ResetEffects()
        {
            if(OutOfWaterRotationTimerThing > 0)
                OutOfWaterRotationTimerThing--;
            if (Player.wet && Player.wetCount == 0)
                OutOfWaterRotationTimerThing = 20;

            if(OutOfWaterRotationTimerThing == 20)
            {
                if (Player.ItemAnimationActive)
                {
                    Player.fullRotation = Utils.AngleLerp(Player.fullRotation, 0, 0.2f);
                    return;
                }

                Player.fullRotation = Utils.AngleLerp(Player.fullRotation,Player.velocity.Y != 0? Player.velocity.ToRotation() + MathHelper.PiOver2 : 0, 0.1f);
                Player.fullRotationOrigin = Player.DefaultSize / 2;
            }
            else
            {
                Player.fullRotation = Utils.AngleLerp(Player.fullRotation, 0, 0.2f);
            }
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            //if (Player.PlayerStats().newSwim)
            //{
            //    if(OutOfWaterRotationTimerThing == 20)
            //    {
            //        Player.jumpSpeedBoost = 30;
            //        Player.ignoreWater = true;
            //        Player.canFloatInWater = true;
            //        Player.gravity = 0;
            //        Player.velocity *= 0.99f;
            //        Player.maxFallSpeed = 9999;
            //        Player.UpdateJumpHeight();
            //        float maxSwimSpeed = 8;
            //        float acceleration = 1.2f;

            //        if (triggersSet.Down && Player.velocity.Y < maxSwimSpeed)
            //            Player.velocity.Y += acceleration;
            //        if (triggersSet.Up && Player.velocity.Y > -maxSwimSpeed)
            //            Player.velocity.Y -= acceleration;
            //        if (triggersSet.Right && Player.velocity.X < maxSwimSpeed)
            //            Player.velocity.X += acceleration;
            //        if (triggersSet.Left && Player.velocity.X > -maxSwimSpeed)
            //            Player.velocity.X -= acceleration;

            //        if (PlayerInput.Triggers.JustPressed.Jump)
            //        {
            //            SoundEngine.PlaySound(SoundID.DoubleJump,Player.position);
            //            Player.velocity *= 2;
            //        }
            //        Player.velocity = Player.velocity.LengthClamp(32);
            //    }
            //}
        }
    }
}
