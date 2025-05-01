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
    internal class KebabArrowProjectile : ModProjectile
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
      

        public override void AI()
        {
            foreach(Player player in Main.player)
            {
                if (player.MountedCenter.Distance(Projectile.Center) < 30)
                {
                    if (player == Main.player[Projectile.owner] && Projectile.ai[1] > 10)
                    {
                        player.AddBuff(BuffID.WellFed, 60 * 150);
                        SoundEngine.PlaySound(SoundID.Item2, Projectile.Center);
                        
                        for (int i = 0; i < 3; i++)
                        {
                            Dust.NewDust(Projectile.Center, 2, 2, DustID.Pumpkin, Main.rand.NextFloat(-3f,3f), Main.rand.NextFloat(-3f, 3f), Scale:Main.rand.NextFloat(1f,1.4f));
                        }
                        Projectile.Kill();
                    }
                    else if (player != Main.player[Projectile.owner])
                    {
                        player.AddBuff(BuffID.WellFed, 60 * 150);
                        SoundEngine.PlaySound(SoundID.Item2, Projectile.Center);
                        for (int i = 0; i < 3; i++)
                        {
                            Dust.NewDust(Projectile.Center, 2, 2, DustID.Pumpkin, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), Scale: Main.rand.NextFloat(1f, 1.4f));
                        }
                        Projectile.Kill();
                    }
                }
            }
            Projectile.ai[1]++;
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 3; i++)
            {
                Dust.NewDust(Projectile.Center, 2, 2, DustID.Pumpkin, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), Scale: Main.rand.NextFloat(1f, 1.4f));
            }
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            return base.OnTileCollide(oldVelocity);
        }
    }
}
