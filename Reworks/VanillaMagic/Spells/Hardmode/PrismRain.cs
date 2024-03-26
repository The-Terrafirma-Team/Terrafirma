using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terrafirma.Particles.LegacyParticles;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.Hardmode
{
    internal class PrismRain : Spell
    {
        public override int UseAnimation => 6;
        public override int UseTime => 6;
        public override int ManaCost => 4;
        public override string TexurePath => "Terrafirma/Systems/MageClass/SpellIcons/Hardmode/PrismRain";
        public override int[] SpellItem => new int[] { ItemID.RainbowGun };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<ColoredPrism>();
            damage = (int)(damage * 1.1f);
            velocity = velocity.RotatedByRandom(0.1f);

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
    public class ColoredPrism : ModProjectile
    {
        Color ShotColor = new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0);
        public override string Texture => "Terrafirma/Reworks/VanillaMagic/Spells/Hardmode/RainbowShot";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
        }
        public override void SetDefaults()
        {
            Projectile.penetrate = 3;
            Projectile.tileCollide = true;
            Projectile.friendly = true;

            Projectile.timeLeft = 300;
            Projectile.Opacity = 0f;

            Projectile.Size = new Vector2(10);
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D RainbowShot = ModContent.Request<Texture2D>("Terrafirma/Reworks/VanillaMagic/Spells/Hardmode/RainbowShot").Value;

            for (int i = 0; i < 5; i++)
            {
                Main.EntitySpriteDraw(RainbowShot, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, new Rectangle(0, 0, RainbowShot.Width, RainbowShot.Height), ShotColor * (Projectile.Opacity - (i * 0.2f) - 0.3f), Projectile.oldRot[i], RainbowShot.Size() / 2, 1, SpriteEffects.None, 0);
            }
            Main.EntitySpriteDraw(RainbowShot, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, RainbowShot.Width, RainbowShot.Height), ShotColor * Projectile.Opacity, Projectile.rotation, RainbowShot.Size() / 2, 1, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(RainbowShot, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, RainbowShot.Width, RainbowShot.Height), new Color(1f, 1f, 1f, 0f) * 0.4f * Projectile.Opacity, Projectile.rotation, RainbowShot.Size() / 2, 1, SpriteEffects.None, 0);
            return false;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.Opacity += 0.075f;

            if (Projectile.timeLeft == 300)
            {
                LegacyParticleSystem.AddParticle(new BigSparkle(), Projectile.Center + Vector2.Normalize(Projectile.velocity) * 46f, Vector2.Zero, ShotColor, 0, 10, 1, 1f, Main.rand.NextFloat(-0.1f, 0.1f));
            }

            if (Main.rand.NextBool(10))
            {
                LegacyParticleSystem.AddParticle(new BigSparkle(), Projectile.Center, Vector2.Zero, ShotColor * 0.3f, 0, 8, Main.rand.NextFloat(0.3f, 0.8f), 1f, Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2));
            }
        }

        public override void OnKill(int timeLeft)
        {
            LegacyParticleSystem.AddParticle(new BigSparkle(), Projectile.Center, Vector2.Zero, ShotColor, 0, 10, 1, 1f, Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2));
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            LegacyParticleSystem.AddParticle(new BigSparkle(), Projectile.Center, Vector2.Zero, ShotColor, 0, 10, 1, 1f, Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2));
        }
    }

}
