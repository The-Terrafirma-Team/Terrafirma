using Microsoft.Xna.Framework;
using TerrafirmaRedux.Buffs.Debuffs;
using TerrafirmaRedux.Particles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Projectiles.Melee
{
    internal class CrucibleBeam : ModProjectile
    {
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void SetDefaults()
        {
            Projectile.damage = 16;
            Projectile.Size = new Vector2(16);
            Projectile.aiStyle = -1;
            //Projectile.timeLeft = 40;
            Projectile.friendly = true;
            Projectile.hide = true;
            Projectile.extraUpdates = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.DamageType = DamageClass.Melee;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;

            //if(Projectile.timeLeft % 10 == 0)
            //{
            //    Projectile.velocity = Projectile.velocity.RotatedByRandom(0.2f);
            //    Projectile.netUpdate = true;
            //}
            Projectile.velocity.Y += 0.1f;

            if (Projectile.timeLeft % 2 == 0)
            {
                ParticleSystem.AddParticle(new HiResFlame(), Projectile.Center + Projectile.velocity, Projectile.velocity.RotatedByRandom(0.2f) * -0.1f, Color.Lerp(TFUtils.getAgnomalumFlameColor(), new Color(1f, Main.masterColor * 0.7f, 0f, 0f), Projectile.ai[0] / 60), 2);
                if (Main.rand.NextBool(5))
                    ParticleSystem.AddParticle(new ColorDot(), Projectile.Center + Projectile.velocity, Projectile.velocity.RotatedByRandom(0.2f) * Main.rand.NextFloat(-1, 1), TFUtils.getAgnomalumFlameColor(), 0.2f);
            }
            Projectile.ai[0]++;
            if (Projectile.ai[0] > 60)
                Projectile.Kill();
        }
        public override void OnKill(int timeLeft)
        {
            Projectile.Explode(100);
            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.position);

            for (int i = 0; i < 20; i++)
            {
                ParticleSystem.AddParticle(new HiResFlame(), Projectile.Center, Main.rand.NextVector2Circular(5, 5), TFUtils.getAgnomalumFlameColor(), 5);
                if (Main.rand.NextBool())
                {
                    ParticleSystem.AddParticle(new ColorDot(), Projectile.Center, Main.rand.NextVector2Circular(10, 10), TFUtils.getAgnomalumFlameColor(), 0.4f);
                }
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            target.AddBuff(ModContent.BuffType<AgnomalumBurns>(), Main.rand.NextBool(3) ? 60 * 6 : 60);
        }
        //public override bool PreDraw(ref Color lightColor)
        //{
        //    //Texture2D tex = TextureAssets.Projectile[Type].Value;

        //    //Main.EntitySpriteDraw(tex,Projectile.Center - Main.screenPosition,new Rectangle(0,0,tex.Width,tex.Height),Color.White,Projectile.rotation,new Vector2(tex.Width - 8,8),Projectile.scale,SpriteEffects.None);
        //    return false;
        //}
    }
}
