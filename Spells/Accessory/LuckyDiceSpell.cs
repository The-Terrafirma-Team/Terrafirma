using Terrafirma.Systems.MageClass;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terrafirma.Particles;

namespace Terrafirma.Spells.Accessory
{
    public class LuckyDiceSpell : Spell
    {
        public override int UseAnimation => 32;
        public override int UseTime => 32;
        public override int ManaCost => 40;
        public override int[] SpellItem => new int[] {ModContent.ItemType<Items.Equipment.Magic.LuckyDice>()};

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, player.Center.DirectionTo(Main.MouseWorld) * 4f, ModContent.ProjectileType<LuckyDiceProjectile>(), damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }

    public class LuckyDiceProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.Size = new Vector2(16);
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 60 * 10;
        }

        float rotationspeed = 0.03f;
        public override void AI()
        {
            Projectile.rotation += rotationspeed;
            if (Projectile.ai[0] <= 60) rotationspeed = 0.03f;
            if (Projectile.ai[0] > 60)
            {
                Projectile.velocity *= 0.98f;
                rotationspeed *= 0.99f;
            }
            if (Projectile.ai[0] == 480)
            {
                SoundStyle Bell = new SoundStyle("Terrafirma/Sounds/DoomBell");
                SoundEngine.PlaySound(Bell, Projectile.Center);
                SoundStyle Boom = new SoundStyle("Terrafirma/Sounds/LongBoom");
                SoundEngine.PlaySound(Boom, Projectile.Center);

                LuckyDiceNumber particle = new LuckyDiceNumber();
                particle.displayNumber = Main.rand.Next(0, 10);
                ParticleSystem.AddParticle(particle, Projectile.Center);
            }
            if (Projectile.ai[0] > 480)
            {
                Projectile.rotation = Utils.AngleLerp(Projectile.rotation, 0f, 0.1f);
            }
            if (Projectile.ai[0] > 700)
            {
                Projectile.Kill();
            }
            Projectile.ai[0]++;
            base.AI();
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.GemTopaz, Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f), 1, Scale: 1.5f);
                d.noGravity = true;
            }
            base.OnKill(timeLeft);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> BaseTex = TextureAssets.Projectile[Type];
            Asset<Texture2D> GlowTex = ModContent.Request<Texture2D>("Terrafirma/Spells/Accessory/LuckyDiceProjectileGlow");

            Main.EntitySpriteDraw(GlowTex.Value,
                Projectile.Center - Main.screenPosition,
                GlowTex.Frame(),
                new Color(1f, 0.85f, 0.6f, 0f) * 0.8f,
                Projectile.rotation,
                GlowTex.Size() / 2,
                Projectile.scale + (float)((Math.Sin(Main.timeForVisualEffects / 40f) + 1f) / 8f),
                SpriteEffects.None);
            Main.EntitySpriteDraw(BaseTex.Value,
                Projectile.Center - Main.screenPosition,
                BaseTex.Frame(),
                Color.Lerp(lightColor, Color.White, 0.4f),
                Projectile.rotation,
                BaseTex.Size() / 2,
                Projectile.scale,
                SpriteEffects.None);
            Main.EntitySpriteDraw(GlowTex.Value,
                Projectile.Center - Main.screenPosition,
                GlowTex.Frame(),
                new Color(1f, 0.7f, 0.4f, 0f) * 0.3f,
                Projectile.rotation,
                GlowTex.Size() / 2,
                Projectile.scale + (float)((Math.Sin(Main.timeForVisualEffects / 40f) + 1f) / 8f),
                SpriteEffects.None);
            return false;
        }
    }
}
