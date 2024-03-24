using Microsoft.Xna.Framework;
using System;
using Terrafirma.Buffs.Debuffs;
using Terrafirma.Particles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Melee
{
    internal class FireaxeSlash : ModProjectile
    {
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255,255,255,0) * Projectile.Opacity * Projectile.scale;
        }
        public override void SetDefaults()
        {
            Projectile.damage = 16;
            Projectile.Size = new Vector2(16);
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.alpha = 250;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 50;
            Projectile.penetrate = 3;
            DrawOffsetX = -17;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if (Projectile.alpha > 0)
                Projectile.alpha -= 25;

            Projectile.scale = 0.9f + MathF.Sin(Projectile.timeLeft * 0.3f) * 0.1f;
            if(Projectile.timeLeft == 50)
            {
                SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
            }
            if(Projectile.timeLeft < 20)
            {
                Projectile.velocity *= (1 - 1/15f);
                Projectile.alpha += 35;
            }
            Dust d = Dust.NewDustPerfect(Projectile.Center + new Vector2(Main.rand.NextFloat(-25,25), 0).RotatedBy(Projectile.rotation), DustID.Torch, Projectile.velocity);
            d.customData = 1;
            d.noGravity = true;
        }
        public override void OnKill(int timeLeft)
        {
            for(int i = 0; i < 10; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center,DustID.Torch,Main.rand.NextVector2Circular(2,2) + Projectile.velocity);
                d.customData = 1;
                d.noGravity = false;
                d.scale = 2;
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            target.AddBuff(BuffID.OnFire3, Main.rand.NextBool(3) ? 60 * 6 : 60);
        }
    }
}
