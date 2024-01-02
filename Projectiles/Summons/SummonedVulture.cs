using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace TerrafirmaRedux.Projectiles.Summons
{
    internal class SummonedVulture : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.damage = 8;
            Projectile.height = 46;
            Projectile.width = 30;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 600;
            Projectile.penetrate = 12;
            Projectile.CritChance = 15;
            Projectile.frameCounter = 0;

        }

        public override void AI()
        {
            //Animation

            Projectile.frame = Projectile.frameCounter + 1;

            if (Projectile.timeLeft % 5 == 0)
            {
                Projectile.frameCounter++;
            }

            if (Projectile.frameCounter == 4)
            {
                Projectile.frameCounter = 0;
            }

            Projectile.spriteDirection = -Projectile.direction;
            Projectile.rotation = Projectile.velocity.X / 10;

            //Attack

            Projectile.ai[1]++;

            NPC ClosestNPC = Utils.FindClosestNPC(600f, Projectile.Center);
            Player player = Main.player[Projectile.owner];

            if (ClosestNPC != null)
            {
                Projectile.velocity += -Vector2.Normalize(Projectile.Center - ClosestNPC.Center) * 0.4f;
                Projectile.velocity.X = MathHelper.Clamp(Projectile.velocity.X, -5f , 5f);

                if (Vector2.Distance(Projectile.Center, ClosestNPC.Center) < 100f)
                {
                    Projectile.velocity.Y = (MathHelper.Clamp(Projectile.velocity.Y, -2f , 2f) + (float)Math.Sin(Projectile.ai[1] / 10)  * 0.5f);
                }
                else 
                {
                    Projectile.velocity.Y = MathHelper.Clamp(Projectile.velocity.Y, -2f, 2f);
                }

            }
            else
            {
                if (Vector2.Distance(Projectile.Center, new Vector2(player.MountedCenter.X, player.MountedCenter.Y - 100f)) > 60f)
                {
                    Projectile.velocity += -Vector2.Normalize(Projectile.Center - new Vector2(player.MountedCenter.X, player.MountedCenter.Y - 100f)) * 0.4f;
                }

                Projectile.velocity.X = MathHelper.Clamp(Projectile.velocity.X, -5f, 5f) ;

                if (Vector2.Distance(Projectile.Center, new Vector2(player.MountedCenter.X, player.MountedCenter.Y - 100f)) < 60f)
                {
                    Projectile.velocity.Y = (MathHelper.Clamp(Projectile.velocity.Y, -2f, 2f) + (float)Math.Sin(Projectile.ai[1] / 20) * 0.2f);
                }
                else
                {
                    Projectile.velocity.Y = MathHelper.Clamp(Projectile.velocity.Y, -2f, 2f);
                }
            }
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            NPC closestnpc = Utils.FindClosestNPC(600f, Projectile.Center);
            fallThrough = true;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return true;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCHit28 , Projectile.position);
            
            for (int i = 0; i < 12; i++)
            {
                Dust.NewDust(Projectile.Center, 10, 10, DustID.Smoke, Main.rand.Next(-2, 2), Main.rand.Next(-2, 2), 0, default, Main.rand.NextFloat(1.8f, 2.5f));
            }
        }

    }
}
