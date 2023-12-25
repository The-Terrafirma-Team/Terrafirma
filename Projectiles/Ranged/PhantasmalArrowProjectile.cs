using Microsoft.Xna.Framework;
using System;
using TerrafirmaRedux.Buffs.Debuffs;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Projectiles.Ranged
{
    internal class PhantasmalArrowProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.damage = 16;
            Projectile.width = 4;
            Projectile.height = 4;
            DrawOffsetX = -Projectile.width / 2 - 5;
            Projectile.penetrate = 1;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.timeLeft = 600;
            Projectile.friendly = true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<PhantasmalBurn>(), 240);
        }

        public override void AI()
        {
            Projectile.ai[1] += 1;
            if (Projectile.ai[1] % 2 == 0)
            {
                Dust.NewDust(Projectile.Center - new Vector2(4, 4), 0, 0, DustID.BlueFlare, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 0, default, Main.rand.NextFloat(1.8f, 2.3f));
            }
            else if (Projectile.ai[1] % 3 == 0)
            {
                Dust.NewDust(Projectile.Center - new Vector2(4, 4), 0, 0, DustID.BlueFlare, Projectile.velocity.X * 0.9f, Projectile.velocity.Y * 0.7f, 0, default, Main.rand.NextFloat(0.8f, 1.2f));
            }

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {

            Projectile.Kill();

            for (int i = 0; i < 15; i++)
            {
                Dust.NewDust(Projectile.Center, 2, 2, DustID.BlueFlare, Projectile.velocity.X * Main.rand.NextFloat(0.8f, 1.5f), Projectile.velocity.Y * Main.rand.NextFloat(0.8f, 1.5f), 0, default, Main.rand.NextFloat(1.8f, 2.3f));
            }

            return false;
        }

        public override void OnKill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
    }
}
