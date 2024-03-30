using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameInput;
using Terrafirma.Systems.MageClass;
using Terrafirma.Global.Structs;
using Terrafirma.Items.Weapons.Melee.Swords;
using Terrafirma.Projectiles.Melee;
using System;
using System.Drawing.Drawing2D;
using System.Linq;
using Terrafirma.Items.Weapons.Summoner.Wrench;
using Terrafirma.Systems.NPCQuests;
using Terrafirma.Reworks.VanillaMagic.Spells;

namespace Terrafirma.Global
{
    public class TerrafirmaGlobalPlayer : ModPlayer
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

        public bool SpellUI = false;
        internal Item HeldMagicItem = new Item(0);

        public Quest[] playerquests = new Quest[]{};

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

            if (ammo.ammo == AmmoID.Bullet && Main.rand.NextBool(2,3) && DrumMag)
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
                        SoundStyle boing = new SoundStyle("Terrafirma/Sounds/Boing",SoundType.Sound);
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
            //Main.NewText(playerquests.Quests[playerquests.Quests.Keys.ToArray()[0]]);
            if (playerquests.Length == 0) playerquests = QuestIndex.quests;

            if (SpellUI && HeldMagicItem.type != 0) { ModContent.GetInstance<SpellUISystem>().Show(); }
            else { ModContent.GetInstance<SpellUISystem>().Hide(); }            
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {

            if (triggersSet.MouseRight) {  
                
                if (SpellIndex.ItemCatalogue.ContainsKey(Player.inventory[Player.selectedItem].type) && Player.inventory[Player.selectedItem].damage != -1)
                {
                    if (!SpellUI)
                    {
                        ModContent.GetInstance<SpellUISystem>().Create(Player.HeldItem.type, Player);
                        HeldMagicItem = Player.HeldItem;
                    }

                    if (HeldMagicItem != Player.HeldItem)
                    {
                        ModContent.GetInstance<SpellUISystem>().Create(Player.HeldItem.type, Player);
                        HeldMagicItem = Player.HeldItem;
                    }
                    ModContent.GetInstance<SpellUISystem>().UpdateMana(Player.manaCost);
                }
                SpellUI = true;
             
            }
            else
            {  
                if (ModContent.GetInstance<SpellUISystem>().SelectedSpell != null && SpellUI == true)
                {
                    if (Player.HeldItem.type > 0 &&
                        SpellIndex.ItemCatalogue.ContainsKey(Player.HeldItem.type))
                    {
                        SpellUI = false;
                        Player.HeldItem.GetGlobalItem<GlobalItemInstanced>().Spell =
                        ModContent.GetInstance<SpellUISystem>().SelectedSpell;
                    }
                }

                ModContent.GetInstance<SpellUISystem>().Flush(); 
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
            if (Player.HeldItem.type == ModContent.ItemType<HeroSword>() )
            {
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<HeroSwordProjectile>()] < 1) return true;
                else return false;
            }
            return base.CanUseItem(item);
        }
    }
}
