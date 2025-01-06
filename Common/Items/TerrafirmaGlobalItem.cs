using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terrafirma.Buffs.Debuffs;
using Terrafirma.Common.Players;
using Terrafirma.Data;
using Terrafirma.Particles;
using Terrafirma.Rarities;
using Terrafirma.Tiles;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Terrafirma.Common.Items
{
    public class TerrafirmaGlobalItem : GlobalItem
    {
        public override void SetStaticDefaults()
        {
            ItemSets.ThrowerWeapon[ItemID.PaperAirplaneA] = true;
            ItemSets.ThrowerWeapon[ItemID.PaperAirplaneB] = true;
            ItemSets.ThrowerWeapon[ItemID.Shuriken] = true;
            ItemSets.ThrowerWeapon[ItemID.ThrowingKnife] = true;
            ItemSets.ThrowerWeapon[ItemID.PoisonedKnife] = true;
            ItemSets.ThrowerWeapon[ItemID.Snowball] = true;
            ItemSets.ThrowerWeapon[ItemID.SpikyBall] = true;
            ItemSets.ThrowerWeapon[ItemID.Bone] = true;
            ItemSets.ThrowerWeapon[ItemID.RottenEgg] = true;
            ItemSets.ThrowerWeapon[ItemID.StarAnise] = true;
            ItemSets.ThrowerWeapon[ItemID.MolotovCocktail] = true;
            ItemSets.ThrowerWeapon[ItemID.FrostDaggerfish] = true;
            ItemSets.ThrowerWeapon[ItemID.Javelin] = true;
            ItemSets.ThrowerWeapon[ItemID.BoneJavelin] = true;
            ItemSets.ThrowerWeapon[3379] = true; // bone throwing knife
            ItemSets.ThrowerWeapon[ItemID.Grenade] = true;
            ItemSets.ThrowerWeapon[ItemID.StickyGrenade] = true;
            ItemSets.ThrowerWeapon[ItemID.BouncyGrenade] = true;
            ItemSets.ThrowerWeapon[ItemID.Beenade] = true;
            ItemSets.ThrowerWeapon[ItemID.PartyGirlGrenade] = true;
            ItemSets.ThrowerWeapon[ItemID.AleThrowingGlove] = true;
        }
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
                case ItemID.EngineeringHelmet:
                    item.vanity = false;
                    item.defense = 4;
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
                //case ItemID.FeralClaws:
                //    if (stats.FeralChargeMax < 3)
                //        stats.FeralChargeMax = 3;
                //    break;
                //case ItemID.PowerGlove:
                //    if (stats.FeralChargeMax < 4)
                //        stats.FeralChargeMax = 4;
                //    break;
                //case ItemID.BerserkerGlove:
                //    if (stats.FeralChargeMax < 5)
                //        stats.FeralChargeMax = 5;

                //    if (stats.FeralChargeSpeed < PlayerStats.defaultFeralChargeSpeed * 2f)
                //        stats.FeralChargeSpeed = PlayerStats.defaultFeralChargeSpeed * 2f;
                //    break;
                //case ItemID.FireGauntlet:
                //case ItemID.MechanicalGlove:
                //    if (stats.FeralChargeSpeed < PlayerStats.defaultFeralChargeSpeed * 1.3f)
                //        stats.FeralChargeSpeed = PlayerStats.defaultFeralChargeSpeed * 1.3f;
                //    if (stats.FeralChargeMax < 7)
                //        stats.FeralChargeMax = 7;
                //    break;
                case ItemID.EngineeringHelmet:
                    player.PlayerStats().SentrySpeedMultiplier += 0.1f;
                    player.PlayerStats().SentryDamageMultiplier += 0.05f;
                    player.maxTurrets += 1;
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

            if(item.healMana > 0)
            {
                player.AddBuff(ModContent.BuffType<ManaPotionSickness>(), 3600);
            }

            return base.UseItem(item, player);
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            for(int i = 0; i < tooltips.Count; i++)
            {
                if (tooltips[i].Mod.Equals("Terraria") && tooltips[i].Name.Equals("HealLife"))
                {
                    tooltips[i].Text = Language.GetTextValue("CommonItemTooltip.RestoresLife", (int)(item.healLife * Main.LocalPlayer.PlayerStats().HealingMultiplier * Main.LocalPlayer.PlayerStats().PotionHealingMultiplier));
                    break;
                }
            }
            switch (item.type)
            {
                case ItemID.LivingCursedFireBlock:
                case ItemID.LivingDemonFireBlock:
                case ItemID.LivingFireBlock:
                case ItemID.LivingFrostFireBlock:
                case ItemID.LivingIchorBlock:
                case ItemID.LivingUltrabrightFireBlock:
                    tooltips.Add(new TooltipLine(Mod, "Tooltip", Language.GetTextValue("Mods.Terrafirma.VanillaItemTooltips.LivingFireBlock")));
                    break;
                case ItemID.EngineeringHelmet:
                    tooltips.Add(new TooltipLine(Mod, "Tooltip", Language.GetTextValue("Mods.Terrafirma.VanillaItemTooltips.EngineeringHelmet")));
                    break;
            }
        }
        public override void GetHealLife(Item item, Player player, bool quickHeal, ref int healValue)
        {
            healValue = (int)(healValue * player.PlayerStats().HealingMultiplier * player.PlayerStats().PotionHealingMultiplier);
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
                        p.smallestSize = 0f;
                        ParticleSystem.AddParticle(new BigSparkle(), new Vector2(Main.rand.NextFloat(line.Text.Length * 9.5f), Main.rand.NextFloat(20f)), null, line.Color, ParticleLayer.UI);
                    }
                    ParticleSystem.DrawUIParticles(new Vector2(line.X, line.Y));
                }
            }
        }
    }
}
