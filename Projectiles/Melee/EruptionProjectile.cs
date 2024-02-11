using Terraria.ID;
using Terraria;
using Terrafirma.Global.Templates;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System;
using Terraria.Audio;

namespace Terrafirma.Projectiles.Melee
{
    public class EruptionProjectile: SpearTemplate
    {
        protected override float HoldoutRangeMax => 150;
        protected override float HoldoutRangeMin => 40;

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Spear);
            DrawOriginOffsetY = 100;
        }
        public override void PostAI()
        {
            Dust d = Dust.NewDustPerfect(Projectile.Center + new Vector2(-7,5).RotatedBy(Projectile.rotation), DustID.Torch, Main.rand.NextVector2Circular(1,1) + Projectile.velocity * 3);
            d.noGravity = true;
            d.fadeIn = Main.rand.NextFloat(0, 1.5f);
        }

        public override void OnTurnAround(Player player)
        {
            SoundEngine.PlaySound(SoundID.DD2_FlameburstTowerShot, Projectile.position);
            if (Main.player[Projectile.owner] == Main.LocalPlayer)
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center + new Vector2(80 * Projectile.direction, -90  ).RotatedBy(Projectile.rotation), Projectile.velocity * 10f, ModContent.ProjectileType<EruptionFloatProjectile>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 0, 0);
        }
    }

    public class EruptionFloatProjectile : ModProjectile

    {

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.timeLeft = 200;

            DrawOffsetX = -15;
            DrawOriginOffsetY = -15;

            Projectile.penetrate = 15;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.Lerp(new Color(255, 60, 0, 0), new Color(255, 128, 0, 0),(float)Math.Sin(Main.timeForVisualEffects * 0.01f) * 0.5f + 0.5f) * Projectile.Opacity;
        }

        public override void AI()
        {
            double rot = Projectile.spriteDirection == 1 ? MathHelper.PiOver4 : MathHelper.PiOver2 + MathHelper.PiOver4;
            
            Projectile.spriteDirection = Math.Sign(Projectile.velocity.X);
            Projectile.velocity *= 0.96f;

            Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(15, 15), DustID.Torch, Main.rand.NextVector2Circular(1, 1) + -Projectile.velocity * 2);
            d.noGravity = true;
            d.fadeIn = Main.rand.NextFloat(0, 1.5f);

            if (Projectile.timeLeft > 60) Projectile.rotation = Projectile.velocity.ToRotation() + (float)rot;
            else
            {
                Projectile.velocity.Y += 0.1f;
                Projectile.Opacity = Projectile.timeLeft / 60f;
                Projectile.rotation += 0.01f * Projectile.direction * (Math.Abs(Projectile.timeLeft - 60) / 10f);
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 30; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(15, 15), DustID.Torch, Main.rand.NextVector2Circular(6, 6) + -Projectile.velocity * 2, 0);
                d.noGravity = true;
                d.fadeIn = Main.rand.NextFloat(0, 1.5f);
            }
            SoundEngine.PlaySound(SoundID.Item73, Projectile.position);
        }

    }
}
