using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Global;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terrafirma.Reworks.VanillaMagic.Projectiles;
using Terraria.DataStructures;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using Terrafirma.Projectiles.Summons;
using Terrafirma.Projectiles.Magic;
using Terrafirma.Systems.MageClass;

namespace Terrafirma.Reworks.VanillaMagic
{
    internal class ModifyMagicSpellStats : GlobalItemInstanced
    {
        //Applies to Entity
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.DamageType == DamageClass.Magic;
        }

        //Set Defaults
        public override void SetDefaults(Item entity)
        {

            if (entity.type == ItemID.InfernoFork) entity.UseSound = null;
            if (entity.type == ItemID.GoldenShower || entity.type == ItemID.CursedFlames) entity.UseSound = null;
            if (entity.type == ItemID.RainbowGun) entity.shoot = ModContent.ProjectileType<ColoredPrism>();
        }

        //Modify Mana Cost
        public override void ModifyManaCost(Item item, Player player, ref float reduce, ref float mult)
        {
            mult = ModContent.GetInstance<SpellIndex>().SpellCatalogue[item.GetGlobalItem<GlobalItemInstanced>().Spell].Item5 / item.mana;
            base.ModifyManaCost(item, player, ref reduce, ref mult);
        }

        //Use Speed Multiplier
        public override float UseSpeedMultiplier(Item item, Player player)
        {
            switch (item.GetGlobalItem<GlobalItemInstanced>().Spell)
            {
                case 0: return 1.2f;
                case 2: return 1.2f;
                case 3: return 1.1f;
                case 5: return 0.85f;
                case 7: return 0.6f;
                case 9: return 0.2f;
                case 11: return 0.75f;
            }
            return base.UseSpeedMultiplier(item, player);
        }

        //Use Time Multiplier
        public override float UseTimeMultiplier(Item item, Player player)
        {
            switch (item.GetGlobalItem<GlobalItemInstanced>().Spell)
            {
                case 21: return 9;
                case 30: return 0.4f;

                case 15: return 0.14f;
                case 16: return 0.3f;
                case 18: return 1.8f;
                case 23: return 0.3f;
                case 27: return 5f;

                case 25: return 0.15f;

                case 31: return (1f / (float)item.useTime) * 50f;

                case 33: return 20 / 4f;
            }
            return base.UseTimeMultiplier(item, player);
        }

        //Use Animation Multiplier
        public override float UseAnimationMultiplier(Item item, Player player)
        {
            switch (item.GetGlobalItem<GlobalItemInstanced>().Spell)
            {
                case 21: return 3;
                case 30: return 0.4f;

                case 16: return 1.2f;
                case 18: return 1.8f;
                case 23: return 0.3f;
                case 27: return 2f;

                case 25: return 0.15f;

                case 31: return (1f / (float)item.useAnimation) * 50f;

                case 33: return 5f;


            }
            return base.UseAnimationMultiplier(item, player);
        }

        //Can Use Item
        public override bool CanUseItem(Item item, Player player)
        {
            switch (item.GetGlobalItem<GlobalItemInstanced>().Spell)
            {
                case 9: return player.ownedProjectileCounts[ModContent.ProjectileType<DiamondTurret>()] < 1;
                case 13:
                    if (player.ownedProjectileCounts[ModContent.ProjectileType<AmberWall>()] < 4 && player.ownedProjectileCounts[ModContent.ProjectileType<AmberWallCrystal>()] < 1) { return true; }
                    return false;
                case 28: return player.ownedProjectileCounts[ModContent.ProjectileType<SkeletonHand>()] < 1 ? base.CanUseItem(item, player) : false;
            }
            return base.CanUseItem(item, player);
        }

        //Use Item
        public override bool? UseItem(Item item, Player player)
        {
            switch (item.GetGlobalItem<GlobalItemInstanced>().Spell)
            {
                case 31:
                    CombatText.NewText(player.Hitbox, CombatText.HealMana, 10, false);
                    player.statMana += 10; 
                    return true;
            }

            return base.UseItem(item, player);
        }

        //Shoot
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            switch (item.GetGlobalItem<GlobalItemInstanced>().Spell)
            {
                case 9: return player.ownedProjectileCounts[ModContent.ProjectileType<DiamondTurret>()] < 1;
                case 13: if (player.ownedProjectileCounts[ModContent.ProjectileType<AmberWall>()] < 4 && player.ownedProjectileCounts[ModContent.ProjectileType<AmberWallCrystal>()] < 1) { return true; } return false;

                case 20: if (player.ItemAnimationJustStarted) SoundEngine.PlaySound(SoundID.Item13, player.position); break;
                case 21: SoundEngine.PlaySound(SoundID.NPCDeath19, player.position); break;
                case 30: SoundEngine.PlaySound(SoundID.Item34, player.position); break;

                case 14: SoundEngine.PlaySound(SoundID.Item73, player.position); break;
                case 15: if (player.ItemAnimationJustStarted) SoundEngine.PlaySound(SoundID.Item34, player.position); break;

                case 31: return true;

            }
            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }

        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            switch (item.GetGlobalItem<GlobalItemInstanced>().Spell)
            {
                //Gem Staves
                case 1:
                    type = ModContent.ProjectileType<HomingAmethyst>();
                    damage = (int)(damage * 0.8f);
                    break;
                case 3:
                    type = ModContent.ProjectileType<SplittingTopaz>();
                    break;
                case 5:
                    type = ModContent.ProjectileType<PiercingEmerald>();
                    velocity *= 0.9f;
                    break;
                case 7:
                    type = ModContent.ProjectileType<ExplodingRuby>();
                    velocity *= 0.7f;
                    damage = (int)(damage * 1.2f);
                    break;
                case 9:
                    type = ModContent.ProjectileType<DiamondTurret>();
                    break;
                case 11:
                    velocity *= 2f;
                    damage = (int)(damage * 1.2f);
                    break;
                case 13:
                    type = ModContent.ProjectileType<AmberWallCrystal>();
                    break;

                //Evil Weapons
                case 21:
                    type = ModContent.ProjectileType<IchorBubble>();
                    velocity *= 0.45f;
                    damage *= 3;
                    break;
                case 30:
                    type = ModContent.ProjectileType<CursedFlames>();
                    velocity *= 0.8f;
                    damage = (int)(damage * 0.5f);
                    break;

                //Dungeon Weapons
                case 14:
                    type = ModContent.ProjectileType<InfernoFork>();
                    velocity *= 1.2f;
                    //velocity = new Vector2(6,-6);
                    break;
                case 15:
                    type = ModContent.ProjectileType<InfernoFlamethrower>();
                    damage = (int)(damage * 0.6f);
                    velocity *= 0.9f;
                    position += Vector2.Normalize(velocity) * 30;
                    break;
                case 16:
                    type = ModContent.ProjectileType<Firewall>();
                    velocity = Vector2.Normalize(velocity) * 0.01f;
                    damage = (int)(damage * 0.5f);
                    position = Main.MouseWorld;
                    break;
                case 18:
                    type = ModContent.ProjectileType<WaterGeyser>();
                    damage = (int)(damage * 0.5f);
                    position = Main.MouseWorld;
                    velocity = Vector2.Normalize(velocity) * 0.01f;
                    knockback *= 0.2f;
                    break;
                case 19:
                    type = ModContent.ProjectileType<AuraWave>();
                    position = Main.MouseWorld;
                    velocity = Vector2.Normalize(velocity) * 0.01f;
                    knockback *= 2f;
                    break;
                case 23:
                    type = ModContent.ProjectileType<BoneFragment>();
                    damage = (int)(damage * 0.5f);
                    velocity += velocity * 2f + new Vector2(0, Main.rand.NextFloat(-1f, 1f)).RotatedBy(velocity.ToRotation());
                    break;
                case 27:
                    type = ModContent.ProjectileType<HealingBubble>();
                    damage = (int)(damage * 0.1f);
                    break;
                case 28:
                    type = ModContent.ProjectileType<SkeletonHand>();
                    velocity = Vector2.Zero;
                    break;

                //Other Magic Weapons
                case 24:
                    type = ProjectileID.RainbowFront;
                    if (player.ownedProjectileCounts[ProjectileID.RainbowBack] > 0)
                    {
                        for (int j = 0; j < Main.projectile.Length; j++)
                        {
                            if (Main.projectile[j].active && Main.projectile[j].owner == player.whoAmI && (Main.projectile[j].type == ProjectileID.RainbowFront || Main.projectile[j].type == ProjectileID.RainbowBack))
                            {
                                Main.projectile[j].Kill();
                            }
                        }
                    }

                    break;
                case 25:
                    type = ModContent.ProjectileType<ColoredPrism>();
                    damage = (int)(damage * 1.1f);
                    velocity = velocity.RotatedByRandom(0.1f);
                    break;

                //Tempire
                case 32:
                    type = ModContent.ProjectileType<FantasticalDoubleHelix>();
                    break;
                case 33:
                    type = ModContent.ProjectileType<GlitterBomb>();
                    break;

                //Accessories
                case 31:
                    type = ModContent.ProjectileType<ManaBloomProj>();
                    SoundEngine.PlaySound(SoundID.Item8, position);
                    break;
            }

        }
    }
}
