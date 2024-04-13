using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameInput;
using Terrafirma.Systems.MageClass;
using Terrafirma.Common.Structs;
using Terrafirma.Items.Weapons.Melee.Swords;
using Terrafirma.Projectiles.Melee;
using System;
using System.Drawing.Drawing2D;
using System.Linq;
using Terrafirma.Items.Weapons.Summoner.Wrench;
using Terrafirma.Systems.NPCQuests;
using Terrafirma.Reworks.VanillaMagic.Spells;
using Terrafirma.Common.Items;

namespace Terrafirma.Common.Players
{
    public class TerrafirmaModPlayer : ModPlayer
    {

        public bool Foregrip = false;
        public bool DrumMag = false;
        public bool AmmoCan = false;
        public bool CanUseAmmo = true;
        public bool BoxOfHighPiercingRounds = false;

        public bool SpringBoots = true;

        public Vector2 Momentum = Vector2.Zero;
        public int FloorTime = 0;
        public float JumpMultiplier = 1f;

        public static bool SpellUI = false;
        internal Item HeldMagicItem = new Item(0);

        public Quest[] playerquests = new Quest[] { };

        public override void ResetEffects()
        {
            Player.pickSpeed *= 0.8f;

            Foregrip = false;
            DrumMag = false;
            AmmoCan = false;
            CanUseAmmo = true;
            BoxOfHighPiercingRounds = false;

            SpringBoots = false;
        }
        public override bool CanConsumeAmmo(Item weapon, Item ammo)
        {
            if (!CanUseAmmo)
            {
                return false;
            }

            if (ammo.ammo == AmmoID.Bullet && Main.rand.NextBool(2, 3) && DrumMag)
            {
                return false;
            }
            return base.CanConsumeAmmo(weapon, ammo);

        }

        public override void PostUpdateRunSpeeds()
        {
            if (SpringBoots)
            {
                Player.runSlowdown = 0.2f;

                if (Player.velocity.Y != 0) FloorTime = 0;
                else FloorTime++;

                if (FloorTime > 10) JumpMultiplier = 1f;

            }
            if (Player.ItemAnimationActive && Player.HeldItem.type != 0)
            {
                if (Player.HeldItem.GetGlobalItem<GlobalItemInstanced>().Spell != null && Player.HeldItem.GetGlobalItem<GlobalItemInstanced>().Spell.GetSpellName() == "Mana Bloom")
                {
                    Player.accRunSpeed = 2f;
                }
            }
        }

        public override void PreUpdateMovement()
        {

            if (SpringBoots)
            {
                Player.frogLegJumpBoost = true;

                if (Player.justJumped)
                {

                    if (JumpMultiplier > 1)
                    {
                        SoundStyle boing = new SoundStyle("Terrafirma/Sounds/Boing", SoundType.Sound);
                        boing.Volume = 0.8f;
                        boing.PitchRange = (-0.1f, 0.1f);
                        boing.Pitch -= JumpMultiplier / 10;

                        SoundEngine.PlaySound(boing, Player.position);
                    }

                    JumpMultiplier = MathHelper.Clamp(JumpMultiplier * 1.25f, 1f, 3f);
                }
            }
        }

        public override void OnMissingMana(Item item, int neededMana)
        {
            Player.manaFlower = false;
        }

        public override void PostUpdate()
        {
            if (playerquests.Length == 0) playerquests = QuestIndex.quests;
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (HeldMagicItem != Player.HeldItem && triggersSet.MouseRight)
            {
                if (SpellIndex.ItemCatalogue.ContainsKey(Player.inventory[Player.selectedItem].type))
                {
                    ModContent.GetInstance<SpellUISystem>().Create(Player.HeldItem.type);
                    HeldMagicItem = Player.HeldItem;
                }
                else ModContent.GetInstance<SpellUISystem>().Flush();
            }

            if (triggersSet.MouseRight)
            {
                HeldMagicItem = Player.HeldItem;
                if (!SpellUI && SpellIndex.ItemCatalogue.ContainsKey(Player.inventory[Player.selectedItem].type) && Player.inventory[Player.selectedItem].damage != -1 && HeldMagicItem == Player.HeldItem)
                {
                    ModContent.GetInstance<SpellUISystem>().Create(Player.HeldItem.type);
                    SpellUI = true;
                }
            }
            else
            {        
                if (SpellUI)
                {
                    ModContent.GetInstance<SpellUISystem>().Flush();
                    if (ModContent.GetInstance<SpellUISystem>().SelectedSpell != null && 
                        SpellUI == true &&
                        Player.HeldItem.type > 0 &&
                        SpellIndex.ItemCatalogue.ContainsKey(Player.HeldItem.type))
                    {
                        SpellUI = false;
                        Player.HeldItem.GetGlobalItem<GlobalItemInstanced>().Spell =
                        ModContent.GetInstance<SpellUISystem>().SelectedSpell;
                    }
                }
            }

            //Item Right Click
            if (triggersSet.MouseRight)
            {
                if (Player.HeldItem.type == ModContent.ItemType<BookmarkerWrench>() &&
                    Player == Main.LocalPlayer)
                {
                    TFUtils.UpdateSentryPriority(null, Player);
                }
            }


        }
        public override bool CanUseItem(Item item)
        {
            
            if (Player.HeldItem.type == ModContent.ItemType<HeroSword>())
            {
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<HeroSwordProjectile>()] < 1) return true;
                else return false;
            }
            return base.CanUseItem(item);
        }
    }
}
