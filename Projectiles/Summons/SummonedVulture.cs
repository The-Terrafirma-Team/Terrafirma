using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using static Terraria.GameContent.Animations.IL_Actions.NPCs;

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
            Projectile.damage = 14;

            Projectile.height = 30;
            Projectile.width = 30;
            DrawOffsetX = -Projectile.width;
            DrawOriginOffsetY = -15;

            Projectile.tileCollide = true;
            Projectile.timeLeft = 600;
            Projectile.penetrate = 12;
            Projectile.CritChance = 15;
            Projectile.frameCounter = 0;

        }

        public override void OnSpawn(IEntitySource source)
        {
            for (int i = 0; i < 24; i++)
            {
                Dust.NewDust(Projectile.Center, 10, 10, DustID.Smoke, Main.rand.NextFloat(-0.5f, 0.5f) * 4, Main.rand.NextFloat(-0.5f, 0.5f) * 4, 0, default, Main.rand.NextFloat(1.1f, 1.5f));
            }
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

            //Movement

            Projectile.ai[1]++;

            NPC ClosestNPC = TFUtils.FindClosestNPC(600f, Projectile.Center);
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
            NPC closestnpc = TFUtils.FindClosestNPC(600f, Projectile.Center);
            fallThrough = true;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCHit28 , Projectile.position);
            
            for (int i = 0; i < 24; i++)
            {
                Dust.NewDust(Projectile.Center, 10, 10, DustID.Smoke, Main.rand.NextFloat(-0.5f, 0.5f) * 4, Main.rand.NextFloat(-0.5f, 0.5f) * 4, 0, default, Main.rand.NextFloat(1.1f, 1.5f));
            }
        }

    }
}
