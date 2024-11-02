using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Linq;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.SpaceGun
{
    internal class PlasmaBall : Spell
    {
        public override int UseAnimation => 6;
        public override int UseTime => 6;
        public override int ManaCost => 2;
        public override int ReuseDelay => -1;
        public override int[] SpellItem => new int[] { ItemID.SpaceGun };

        bool channelSwitch = false;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (!channelSwitch)
            {
                channelSwitch = true;
                damage *= 2;
                Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<PlasmaBallProj>(), damage, knockback, player.whoAmI);
            }
            return false;
        }
        public override void Update(Item item, Player player)
        {
            if (!player.channel) channelSwitch = false;
            item.channel = true;
        }
    }

    internal class PlasmaBallProj : ModProjectile
    {
        bool channel = false;
        bool released = false;
        public override void SetDefaults()
        {
            Projectile.QuickDefaults();
            Projectile.timeLeft = 36000;
            Projectile.scale = 0.5f;
            Projectile.Opacity = 0f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            channel = player.channel;

            if ((!channel || !player.CheckMana(2)) && !released)
            {
                if (Projectile.scale < 0.7f) Projectile.Kill();
                released = true;
                Projectile.timeLeft = 150;
                Projectile.velocity = new Vector2(4f * player.direction, 0f).RotatedBy(Projectile.rotation);
                Projectile.damage = (int)(Projectile.originalDamage * Math.Pow(Projectile.scale, 3));
                Projectile.penetrate = (int)(4 * Projectile.scale) + 1;
            }
            else if (!released)
            {
                Projectile.Center = Vector2.Lerp(Projectile.Center, player.MountedCenter + new Vector2(40 * player.direction, 0).RotatedBy(player.itemRotation),0.6f);
                Projectile.rotation = player.itemRotation;
                Projectile.scale = Math.Clamp(Projectile.scale += 0.005f, 0f, 1.5f);
                Projectile.Opacity += 0.01f;
                Projectile.damage = (int)(Projectile.originalDamage * Math.Pow(Projectile.scale, 2));
                Projectile.penetrate = -1;

                if (Projectile.Opacity > 0.7f)
                {
                    Vector2 vel = Main.rand.NextVector2Circular(2f, 2f);
                    Dust d = Dust.NewDustPerfect(Projectile.Center + new Vector2(8, 0).RotatedBy(vel.ToRotation()), DustID.CursedTorch, vel, 0, Scale: 1.5f);
                    d.noGravity = true;
                }
                if (Projectile.ai[0] <= 1)
                {
                    Dust d2 = Dust.NewDustPerfect(player.MountedCenter + new Vector2(20 * player.direction, 0f).RotatedBy(Projectile.rotation), DustID.CursedTorch, new Vector2(2f * player.direction, 0f).RotatedBy(Projectile.rotation), Scale:1.2f);
                    d2.noGravity = true;
                }

            }
            else if (released)
            {
                Projectile.rotation = MathHelper.Lerp(Projectile.rotation, 0f, 0.05f);
                Vector2 vel = Main.rand.NextVector2Circular(2f, 2f);
                Dust d = Dust.NewDustPerfect(Projectile.Center + new Vector2(8, 0).RotatedBy(vel.ToRotation()), DustID.CursedTorch, vel, 0, Scale: 1.5f);
                d.noGravity = true;
            }

            Projectile.Size = new Vector2(30) * Projectile.scale;

            if (Projectile.ai[0] > 4)
            {
                Projectile.frameCounter = (Projectile.frameCounter + 1) % 4;
                Projectile.ai[0] = 0;
            }
            Projectile.ai[0]++;
        }

        public override void OnKill(int timeLeft)
        {
            int radius = 80;
            int amount = (int)(50 * (Projectile.scale * 2));

            if (Projectile.scale > 0.7f)
            {
                TFUtils.Explode(Projectile, (int)(radius * 2 * Projectile.scale));
            }
            else
            {
                radius = (int)(40 * Projectile.scale);
                amount = (int)(amount * 0.2f);
            }

            for (int i = 0; i < amount; i++)
            {
                Vector2 vel = new Vector2(((radius / 2) * Projectile.scale) * Main.rand.NextFloat(0.8f,1f), 0).RotatedBy((MathHelper.TwoPi / amount) * i);
                Dust d = Dust.NewDustPerfect(Projectile.Center + vel, DustID.CursedTorch, vel * 0.2f, Scale: 1.5f);
                d.noGravity = true;
            }
            base.OnKill(timeLeft);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> tex = ModContent.Request<Texture2D>(Texture);
            //Main orb
            Main.EntitySpriteDraw(tex.Value,
                Projectile.Center - Main.screenPosition,
                new Rectangle(tex.Value.Width / 2, Projectile.frameCounter * (tex.Value.Height / 4), tex.Value.Width / 2, tex.Value.Height / 4),
                Color.White * Projectile.Opacity,
                Projectile.rotation,
                new Vector2(24),
                Projectile.scale,
                SpriteEffects.None);
            //Swooshy Orb
            Main.EntitySpriteDraw(tex.Value,
                Projectile.Center - Main.screenPosition,
                new Rectangle(0, Projectile.frameCounter * (tex.Value.Height / 4), tex.Value.Width / 2, tex.Value.Height / 4),
                new Color(1f,1f,1f,1f) * 0.05f * Projectile.Opacity,
                Projectile.rotation,
                new Vector2(24),
                Projectile.scale * 1.5f,
                SpriteEffects.None);
            Main.EntitySpriteDraw(tex.Value,
                Projectile.Center - Main.screenPosition,
                new Rectangle(0, Projectile.frameCounter * (tex.Value.Height / 4), tex.Value.Width / 2, tex.Value.Height / 4),
                new Color(1f, 1f, 1f, 1f) * 0.3f * Projectile.Opacity,
                Projectile.rotation,
                new Vector2(24),
                Projectile.scale,
                SpriteEffects.None);
            return false;
        }

    }
}
