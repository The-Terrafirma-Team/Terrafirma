using Microsoft.Xna.Framework;
using System;
using TerrafirmaRedux.Buffs.Debuffs;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Projectiles.Ranged
{
    internal class CarbonFiberArrowProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.damage = 19;
            Projectile.width = 4;
            Projectile.height = 4;
            DrawOffsetX = -Projectile.width / 2 - 5;
            Projectile.penetrate = 1;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.timeLeft = 600;
            Projectile.friendly = true;
            Projectile.arrow = true;
            Projectile.DamageType = DamageClass.Ranged;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.target = Projectile.owner;

            for (int i = 0; i < 8; i++)
            {
                Dust.NewDust(Projectile.Center, 2, 2, DustID.Obsidian, Projectile.velocity.X * Main.rand.NextFloat(0.2f, 0.3f), Projectile.velocity.Y * Main.rand.NextFloat(0.2f, 0.3f), 0, default, Main.rand.NextFloat(0.8f, 1f));
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {

            Projectile.Kill();

            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.Center, 2, 2, DustID.Obsidian, Projectile.velocity.X * Main.rand.NextFloat(0.2f, 0.3f), Projectile.velocity.Y * Main.rand.NextFloat(0.2f, 0.3f), 0, default, Main.rand.NextFloat(0.8f, 1f));
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
