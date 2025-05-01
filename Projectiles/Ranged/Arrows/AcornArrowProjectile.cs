using Microsoft.Xna.Framework;
using System;
using Terrafirma.Buffs.Debuffs;
using Terrafirma.Data;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Ranged.Arrows
{
    internal class AcornArrowProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            DrawOffsetX = -Projectile.width / 2 - 5;
            Projectile.penetrate = 1;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.timeLeft = 600;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.arrow = true;
        }
        public override void SetStaticDefaults()
        {
            ProjectileSets.CanBeReflected[Type] = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);

            Point pos = Projectile.Center.ToTileCoordinates();
            for (int i = -2; i <= 2; i++)
            {
                if (Main.tile[pos.X, pos.Y + i].TileType == TileID.Grass && !Main.tile[pos.X, pos.Y + i - 1].HasTile)
                {
                    Terraria.WorldGen.PlaceTile(pos.X, pos.Y + i - 1, TileID.Saplings);
                }
            }

            return base.OnTileCollide(oldVelocity);
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust d = Dust.NewDustDirect(Projectile.Center, 2, 2, DustID.t_LivingWood, Main.rand.NextFloat(-0.2f, 0.2f), Main.rand.NextFloat(-0.2f, 0.2f), 0, default, Main.rand.NextFloat(1f, 1.5f));
                d.noGravity = true;
                d.fadeIn = 1.3f;
            }

            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
    }
}
