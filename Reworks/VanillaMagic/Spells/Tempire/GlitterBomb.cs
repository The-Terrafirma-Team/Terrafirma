using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terrafirma.Items.Weapons.Magic.Tempire;
using Terrafirma.Particles;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.Tempire
{
    internal class GlitterBomb : Spell
    {
        public override int UseAnimation => 80;
        public override int UseTime => 80;
        public override int ManaCost => 20;
        public override string TexurePath => "Terrafirma/Systems/MageClass/SpellIcons/Tempire/GlitterBomb";
        public override int[] SpellItem => new int[] { ModContent.ItemType<Majesty>() };


        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<GlitterBombProj>();

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
    public class GlitterBombProj : ModProjectile
    {
        public override string Texture => "Terrafirma/Reworks/VanillaMagic/Spells/Tempire/GlitterBomb";
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(20);
            Projectile.friendly = true;
            Projectile.timeLeft = 60 * 20;
        }
        public override void AI()
        {
            Projectile.ai[0]++;

            if (Projectile.ai[0] > 20) Projectile.velocity *= 0.92f;

            if (Projectile.ai[0] % 40 == 0)
            {
                BigSparkle bigsparkle = new BigSparkle();
                bigsparkle.fadeInTime = Main.rand.Next(8, 12);
                bigsparkle.Rotation = Main.rand.NextFloat(-0.1f, 0.1f);
                bigsparkle.Scale = 1f;
                ParticleSystem.AddParticle(bigsparkle, Projectile.Center + Main.rand.NextVector2Circular(12, 12), Vector2.Zero, new Color(1f, 0.3f, 0.2f, 0));
                //LegacyParticleSystem.AddParticle(new BigSparkle(), Projectile.Center + Main.rand.NextVector2Circular(12, 12), Vector2.Zero, new Color(1f, 0.3f, 0.2f, 0), 0, Main.rand.Next(8, 12), 1, 1f, Main.rand.NextFloat(-0.1f, 0.1f));
            }

        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = ModContent.Request<Texture2D>("Terrafirma/Reworks/VanillaMagic/Spells/Tempire/GlitterBomb").Value;
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, tex.Bounds, Color.White, 0, tex.Size() / 2, 1f, SpriteEffects.None);
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, tex.Bounds, new Color(1f, 1f, 1f, 0f) * (0.8f - (float)Math.Sin((Main.timeForVisualEffects % 80f) / 40f)), 0, tex.Size() / 2, 0.8f + (float)Math.Sin((Main.timeForVisualEffects % 80f) / 20f), SpriteEffects.None);

            return false;
        }

        public override void Kill(int timeLeft)
        {
            int maxproj = 8;
            for (int i = 0; i < maxproj; i++)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(5f, 0f).RotatedBy(((Math.PI * 2) / maxproj) * i), ModContent.ProjectileType<GlitterBolt>(), Projectile.damage / (int)(maxproj * 0.75f), Projectile.knockBack, Projectile.owner, 0, 0, 0);
            }
        }
    }
}
