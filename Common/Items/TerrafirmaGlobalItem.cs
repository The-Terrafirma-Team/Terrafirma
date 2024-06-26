﻿using Microsoft.Xna.Framework;
using System;
using Terrafirma.Common.Players;
using Terrafirma.Particles;
using Terrafirma.Rarities;
using Terrafirma.Tiles;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common.Items
{
    public class TerrafirmaGlobalItem : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if(item.useStyle == ItemUseStyleID.Swing && item.DamageType == DamageClass.Melee)
            {
                item.useTurn = false;
                item.useTurnOnAnimationStart = false;
            }
            switch (item.type)
            {
                case ItemID.Zenith:
                    item.rare = ModContent.RarityType<FinalQuestRarity>();
                    break;
                case ItemID.MusketBall:
                case ItemID.SilverBullet:
                case ItemID.TungstenBullet:
                    item.shootSpeed = 5.25f;
                    break;
                case ItemID.RainbowGun:
                    item.shoot = ProjectileID.WoodenArrowFriendly;
                    break;
                case ItemID.IceRod:
                    Item.staff[item.type] = true;
                    break;
            }
        }

        //public override bool? CanMeleeAttackCollideWithNPC(Item item, Rectangle meleeAttackHitbox, Player player, NPC target)
        //{
        //    Main.NewText(player.itemRotation);
        //    //player.itemRotation = MathF.Round((int)(player.itemRotation * 4) / 4f,1);
        //    return target.Hitbox.IntersectsConeFastInaccurate(player.RotatedRelativePoint(player.Center), new Vector2(Math.Max(TextureAssets.Item[item.type].Width(), TextureAssets.Item[item.type].Height())).Length() * item.scale, player.itemRotation,0.1f);
        //}
        public override void UpdateEquip(Item item, Player player)
        {
            PlayerStats stats = player.PlayerStats();
            switch (item.type)
            {
                case ItemID.FeralClaws:
                    if(stats.FeralChargeMax < 3)
                        stats.FeralChargeMax = 3;
                    break;
                case ItemID.PowerGlove:
                    if (stats.FeralChargeMax < 4)
                        stats.FeralChargeMax = 4;
                    break;
                case ItemID.BerserkerGlove:
                    if (stats.FeralChargeMax < 5)
                        stats.FeralChargeMax = 5;

                    if (stats.FeralChargeSpeed < PlayerStats.defaultFeralChargeSpeed * 2f)
                        stats.FeralChargeSpeed = PlayerStats.defaultFeralChargeSpeed * 2f;
                    break;
                case ItemID.FireGauntlet:
                case ItemID.MechanicalGlove:
                    if (stats.FeralChargeSpeed < PlayerStats.defaultFeralChargeSpeed * 1.3f)
                        stats.FeralChargeSpeed = PlayerStats.defaultFeralChargeSpeed * 1.3f;
                    if (stats.FeralChargeMax < 7)
                        stats.FeralChargeMax = 7;
                    break;
            }
        }
        public override bool? UseItem(Item item, Player player)
        {
            if (player.ItemAnimationJustStarted && item.useStyle == ItemUseStyleID.Swing)
            {
                player.direction = -Math.Sign(player.Center.X - Main.MouseWorld.X);
            }
            player.PlayerStats().TimesHeldWeaponHasBeenSwung++;

            return base.UseItem(item, player);
        }
        public override void PostDrawTooltipLine(Item item, DrawableTooltipLine line)
        {
            if (item.rare == ModContent.RarityType<FinalQuestRarity>())
            {

                if (line.Name == "ItemName")
                {
                    if (Main.timeForVisualEffects % 15 == 0)
                    {
                        BigSparkle p = new BigSparkle();
                        p.Rotation = Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2);
                        p.fadeInTime = 20;
                        p.Scale = Main.rand.NextFloat(0.3f, 1.2f);
                        ParticleSystem.AddParticle(new BigSparkle(), new Vector2(Main.rand.NextFloat(line.Text.Length * 9.5f), Main.rand.NextFloat(20f)),null,line.Color,ParticleLayer.UI);
                    }
                }
                ParticleSystem.DrawUIParticle(new Vector2(line.X, line.Y));
            }
        }
    }
}
