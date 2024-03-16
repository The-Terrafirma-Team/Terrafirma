using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Summon.Sentry
{
    public class BlueSlimeFriend : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.height = 24;
            Projectile.width = 32;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 600;
            Projectile.penetrate = 5;
            Projectile.alpha = 255;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            if (Projectile.ai[2] != -1 && Main.npc[(int)Projectile.ai[2]].Center.Y >= Projectile.Bottom.Y)
            {
                fallThrough = true;
            }
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            //Visuals
            if (Projectile.alpha > 64)
                Projectile.alpha-= 24;

            if(Projectile.timeLeft > 400 && Main.rand.NextBool(3))
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0, -2f, 600 - Projectile.timeLeft);
                d.velocity *= 0.2f;
                d.velocity += Projectile.velocity * 0.2f;
            }

            if (Projectile.frameCounter > 100) 
            {
                Projectile.frameCounter = 0;
                Projectile.frame = Projectile.frame == 0 ? 1 : 0;
            }

            NPC potentialTarget = TFUtils.FindClosestNPC(1600, Projectile.Center);
            if (owner.HasMinionAttackTargetNPC)
                Projectile.ai[2] = owner.MinionAttackTargetNPC;
            else if (potentialTarget != null)
            {
                Projectile.ai[2] = potentialTarget.whoAmI;
            }
            else
                Projectile.ai[2] = -1;

            // The Jump
            Projectile.velocity.Y += 0.5f;
            if (Projectile.ai[0] == 90)
            {
                Projectile.frame = 1;
                Projectile.ai[0] = 0;
                Projectile.ai[1]++;
                Projectile.velocity.Y -= (Projectile.ai[1] % 3 == 0)? 15 : 10;
                float difference = 0;
                if (Projectile.ai[2] != -1)
                {
                    difference = Projectile.Center.X - Main.npc[(int)Projectile.ai[2]].Center.X;
                    Projectile.direction = Math.Sign(difference);
                    Projectile.velocity.X += -Math.Clamp(difference * 0.02f,-5,5);
                }
                else
                    Projectile.velocity.X += Projectile.direction * 5;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (oldVelocity.Y != Projectile.velocity.Y)
            {
                Projectile.frameCounter += 8 + (int)(Projectile.ai[0] / 8);
                Projectile.ai[0]++;
                Projectile.velocity.X *= 0.7f;
            }
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath1, Projectile.position);
            for (int i = 0; i < 12; i++)
            {
                Dust d = Dust.NewDustDirect(Projectile.Center, 10, 10, DustID.t_Slime, Main.rand.Next(-2, 2), Main.rand.Next(-2, 2), 0, default, 1);
                d.color = new Color(4, 144, 243, 0) * 0.65f;
                d.alpha = 64;
            }
        }
    }
    public class OrangeSlimeFriend : BlueSlimeFriend
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.penetrate = 1;
        }
        public override void OnKill(int timeLeft)
        {
            Projectile.Explode(16 * 8);
            SoundEngine.PlaySound(SoundID.NPCDeath1, Projectile.position);
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int i = 0; i < 32; i++)
            {
                Dust d = Dust.NewDustDirect(Projectile.Center, 10, 10, Main.rand.NextBool(3) ? DustID.InfernoFork : DustID.Palladium, Main.rand.Next(-10, 10), Main.rand.Next(-10, 10), 0, default, 1);
                //d.color = new Color(240, 94, 24, 0) * 0.65f;
                d.alpha = 64;
                d.scale = Main.rand.NextFloat(1, 2);
                d.noGravity = true;
            }
        }
    }
}
