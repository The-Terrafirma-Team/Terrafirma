using Microsoft.Xna.Framework;
using System;
using TerrafirmaRedux.Buffs.Debuffs;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Projectiles.Ranged
{
    internal class ICBAProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.damage = 17;
            Projectile.width = 4;
            Projectile.height = 4;
            DrawOffsetX = -Projectile.width / 2 - 5;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.friendly = true;
            Projectile.extraUpdates = 1;
            Projectile.arrow = true;
            Projectile.DamageType = DamageClass.Ranged;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.FinalDamage *= Projectile.velocity.Length() / 10;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 2;
            height = 2;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.velocity *= 0.05f;
            
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.velocity *= 1.025f;
            Projectile.velocity = Projectile.velocity.LengthClamp(15);

            if (Projectile.velocity.Length() > 2)
            {
                if (Projectile.ai[1] % 2 == 0)
                {
                    Dust TorchDust = Dust.NewDustPerfect(Projectile.position + new Vector2(2 * Projectile.direction, 32).RotatedBy(Projectile.rotation), DustID.Torch, new Vector2(Main.rand.NextFloat(-0.3f, 0.3f), Main.rand.NextFloat(-0.3f, 0.3f)), 0, default, Main.rand.NextFloat(1.2f, 1.5f));
                    TorchDust.noGravity = true;
                }
                Dust SmokeDust = Dust.NewDustPerfect(Projectile.position + new Vector2(2 * Projectile.direction, 32).RotatedBy(Projectile.rotation), DustID.Smoke, new Vector2(Main.rand.NextFloat(-0.3f, 0.3f), Main.rand.NextFloat(-0.3f, 0.3f)), 128, default, Main.rand.NextFloat(0.9f, 1.2f));
                SmokeDust.noGravity = true;
            }
            

        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {

            Projectile.Kill();
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust TorchDust = Dust.NewDustPerfect(Projectile.position, DustID.Torch, new Vector2(Main.rand.NextFloat(-1.6f, 1.6f) * 3, Main.rand.NextFloat(-1.6f, 1.6f) * 3), 0, default, Main.rand.NextFloat(1.5f, 1.8f));
                TorchDust.noGravity = true;
            }
            for (int i = 0; i < 17; i++)
            {
                Dust SmokeDust = Dust.NewDustPerfect(Projectile.position, DustID.Smoke, new Vector2(Main.rand.NextFloat(-1.4f, 1.4f) * 3, Main.rand.NextFloat(-1.4f, 1.4f) * 3), 128, default, Main.rand.NextFloat(1.2f, 1.7f));
                SmokeDust.noGravity = true;
            }
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            Projectile.Explode(100);
        }
    }
}
