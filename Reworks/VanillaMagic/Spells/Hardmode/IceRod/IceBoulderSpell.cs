﻿using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using ReLogic.Content;
using System;
using Terrafirma.Buffs.Debuffs;
using Terrafirma.Dusts;
using Terrafirma.Particles;
using Terrafirma.Projectiles.Summon.Sentry.Hardmode;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.Hardmode.IceRod
{
    internal class IceBoulderSpell : Spell
    {
        Projectile HoldProj = null;
        public override int UseAnimation => 4;
        public override int UseTime => 4;
        public override int ManaCost => 2;
        public override int ReuseDelay => 0;
        public override int[] SpellItem => new int[] { ItemID.IceRod };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return false;
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (Main.mouseLeft && LeftMouseSwitch && HoldProj == null)
            {
                if (player.statMana >= ManaCost) LeftMouseSwitch = false;
                return false;
            }
            else return true;
        }

        public override void OnLeftMousePressed(Item item, Player player)
        {
            if (player.HeldItem != item) return;

            if (player.statMana >= ManaCost)
            {
                HoldProj = Projectile.NewProjectileDirect(player.GetSource_FromThis(), Main.MouseWorld + new Vector2(18, 18), Vector2.Zero, ModContent.ProjectileType<IceBoulder>(), item.damage * 3, item.knockBack, player.whoAmI, 0, 0, 0);
                HoldProj.ai[1] = Main.rand.Next(0, 120);
            }
        }

        public override void UpdateLeftMouse(Item item, Player player)
        {
            if (player.HeldItem != item) return;

            if (HoldProj != null)
            {
                HoldProj.ai[0] = 0;
                HoldProj.timeLeft = 300;
                HoldProj.scale *= 1.05f;
                HoldProj.velocity = Vector2.Lerp(HoldProj.velocity, HoldProj.Center.DirectionTo(Main.MouseWorld) * 5f, 0.02f);
                HoldProj.Size = new Vector2(HoldProj.scale * 30);
                HoldProj.damage = (int)(item.damage * 2f * HoldProj.scale);
                if (HoldProj.scale >= 2f)
                {
                    HoldProj = null;
                    LeftMouseSwitch = false;
                }
            }
        }

        public override void OnLeftMouseReleased(Item item, Player player)
        {
            HoldProj = null;
        }
        public override void Update(Item item, Player player)
        {
            item.useStyle = ItemUseStyleID.Shoot;
            item.channel = false;
            base.Update(item, player);
        }
    }

    public class IceBoulder : ModProjectile
    {
        int baseDamage = 0;
        public override void SetDefaults()
        {
            Projectile.penetrate = 1;
            Projectile.tileCollide = true;
            Projectile.friendly = true;
            Projectile.timeLeft = 300;
            Projectile.scale = 0.2f;
            Projectile.Size = new Vector2(200);
            Projectile.extraUpdates = 1;
        }

        public override void OnKill(int timeLeft)
        {
            SoundStyle BoulderBreakSound = new SoundStyle("Terrafirma/Sounds/IceBoulderBreak");
            SoundEngine.PlaySound(BoulderBreakSound with { Volume = 1.3f, Pitch = 0.5f + (2f - Projectile.scale) * 0.33f }, Projectile.Center);
            if (Projectile.scale > 0.4f)
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.Center, 20, 20, ModContent.DustType<IceBoulderDust>(), Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 0.5f), 0, new Color(0.9f, 1f, 0.9f, 1f), Main.rand.NextFloat(1f, 1.5f) * Projectile.scale);
                    dust = Dust.NewDustDirect(Projectile.Center, 20, 20, DustID.Ice, Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 0.5f), 0, new Color(0.9f, 1f, 0.9f, 0f), Main.rand.NextFloat(1f, 1.5f) * Math.Clamp(Projectile.scale, 0.6f, 1f));
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.Center, 20, 20, DustID.Ice, Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 0.5f), 0, new Color(0.9f, 1f, 0.9f, 0f), Main.rand.NextFloat(1f, 1.5f) * Math.Clamp(Projectile.scale, 0.6f, 2f));
                }
            }

            Projectile.Explode((int)(Projectile.scale * 32));
            base.OnKill(timeLeft);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.ai[0] > 2 || Projectile.ai[1] % 8 == 0)
            {
                SoundStyle BoulderBreakSound = new SoundStyle("Terrafirma/Sounds/IceBoulderBreak");
                SoundEngine.PlaySound(BoulderBreakSound with { Volume = 1.3f, Pitch = 0.5f + (2f - Projectile.scale) * 0.33f }, Projectile.Center);
            }
            if (Projectile.velocity.Y > 2f) target.AddBuff(ModContent.BuffType<Stunned>(), target.boss ? 60 : 60 * 4);
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void AI()
        {
            if (Projectile.ai[0] <= 1)
            {
                //Projectile.scale += 0.8f / 60f;

                Vector2 DustSpawnPos = Projectile.Center + Main.rand.NextVector2Circular(40f * Projectile.scale, 40f * Projectile.scale);
                if (Main.rand.NextBool(4))
                {
                    Dust dust = Dust.NewDustPerfect(DustSpawnPos, DustID.Ice, DustSpawnPos.DirectionTo(Projectile.Center) * 1.5f, 0, new Color(0.9f, 1f, 0.9f, 1f), Main.rand.NextFloat(1f, 1.2f));
                    dust.noGravity = true;
                }
                Projectile.rotation = Projectile.velocity.ToRotation() / 8f + (float)Math.Sin(Projectile.ai[1] / 30f) / 2f;

                if (Math.Abs(Projectile.velocity.X) > 1f && Main.rand.NextBool(6))
                {
                    Dust dust2 = Dust.NewDustDirect(Projectile.Center - Projectile.Size / 2, 20, 20, DustID.Ice, 0, 2f, 0, new Color(0.9f, 1f, 0.9f, 1f), Main.rand.NextFloat(1f, 1.2f));
                }

                baseDamage = Projectile.damage;
            }
            else
            {
                Projectile.damage = (int)(baseDamage * Math.Clamp(Projectile.velocity.Y * 0.15f, 1f, 2.5f));
                Projectile.rotation = MathHelper.Lerp(Projectile.rotation, 0f, 0.05f);
                Projectile.velocity.X *= 0.98f;
                Projectile.velocity.Y += 0.1f;
                if (Projectile.velocity.Y > 0) Projectile.velocity.Y *= 1.02f;

                Projectile.velocity.Y = Math.Clamp(Projectile.velocity.Y, -32f, 32f);
                if (Projectile.ai[0] % 4 == 0)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.Center - Projectile.Size / 2, (int)Projectile.Size.X, (int)Projectile.Size.Y, DustID.Ice, 0, 0, 0, new Color(0.9f, 1f, 0.9f, 0f), Main.rand.NextFloat(1f, 1.5f));
                    dust.noGravity = true;
                }

            }
            Projectile.ai[0]++;
            Projectile.ai[1]++;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return base.OnTileCollide(oldVelocity);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> Tex = ModContent.Request<Texture2D>(Texture);
            if (Projectile.scale <= 1.3f)

                Main.EntitySpriteDraw(Tex.Value,
                    Projectile.Center - Main.screenPosition,
                    new Rectangle(0, 66, 46, 38),
                    lightColor,
                    Projectile.rotation,
                    new Vector2(24, 20),
                    Projectile.scale,
                    SpriteEffects.None);
            else

                Main.EntitySpriteDraw(Tex.Value,
                    Projectile.Center - Main.screenPosition,
                    new Rectangle(0, 106, 92, 72),
                    lightColor,
                    Projectile.rotation,
                    new Vector2(46, 36),
                    Projectile.scale * 0.5f,
                    SpriteEffects.None);
            Main.EntitySpriteDraw(Tex.Value,
                Projectile.Center - Main.screenPosition,
                new Rectangle(0, 0, 42, 64),
                lightColor * 0.1f,
                Projectile.rotation,
                new Vector2(22, 58),
                new Vector2(Projectile.scale, Projectile.velocity.Y / 8f * Projectile.scale),
                SpriteEffects.None);
            Main.EntitySpriteDraw(Tex.Value,
                Projectile.Center - Main.screenPosition,
                new Rectangle(0, 0, 42, 64),
                lightColor * 0.25f,
                Projectile.rotation,
                new Vector2(22, 58),
                new Vector2(Projectile.scale, Projectile.velocity.Y / 13f * Projectile.scale),
                SpriteEffects.None);
            Main.EntitySpriteDraw(Tex.Value,
                Projectile.Center - Main.screenPosition,
                new Rectangle(0, 0, 42, 64),
                lightColor * 0.4f,
                Projectile.rotation,
                new Vector2(22, 58),
                new Vector2(Projectile.scale, Projectile.velocity.Y / 15f * Projectile.scale),
                SpriteEffects.None);
            return false;
        }
    }
}