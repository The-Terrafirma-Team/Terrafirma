using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameInput;
using TerrafirmaRedux.Systems.MageClass;
using TerrafirmaRedux.Global.Structs;
using TerrafirmaRedux.Items.Weapons.Melee.Swords;
using TerrafirmaRedux.Projectiles.Melee;
using System;
using System.Drawing.Drawing2D;
using System.Linq;
using TerrafirmaRedux.Items.Weapons.Summoner.Wrench;

namespace TerrafirmaRedux.Global
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


        internal float[] ArmAnimationTimer = new float[] { 0, -1, -1};

        public QuestList playerquests = new QuestList();

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
                if (Player.HeldItem.GetGlobalItem<GlobalItemInstanced>().Spell == 31)
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
                        SoundStyle boing = new SoundStyle("TerrafirmaRedux/Sounds/Boing",SoundType.Sound);
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
            if (playerquests.Quests.Count == 0) playerquests = QuestList.ImportQuestList();

            if (SpellUI && HeldMagicItem.type != 0) { ModContent.GetInstance<SpellUISystem>().Show(); }
            else { ModContent.GetInstance<SpellUISystem>().Hide(); }

            //ArmAnimationTimer Work
            if (ArmAnimationTimer[1] > 0)
            {
                ArmAnimationTimer[0] += 1f;
                Player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, ArmAnimationTimer[2]);
                Player.direction = (Player.MountedCenter - Main.MouseWorld).X > 0 ? -1 : 1;
            }
            if (ArmAnimationTimer[0] >= ArmAnimationTimer[1])
            {
                ArmAnimationTimer = new float[] { 0, -1, -1 };
                Player.SetCompositeArmFront(false, Player.CompositeArmStretchAmount.None, 0);
            }

            
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (triggersSet.MouseRight) {  
                
                if (ModContent.GetInstance<SpellIndex>().ItemCatalogue.ContainsKey(Player.inventory[Player.selectedItem].type) && Player.inventory[Player.selectedItem].damage != -1)
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
                SpellUI = false;
                if (ModContent.GetInstance<SpellUISystem>().SelectedSpell[0] != -1)
                {
                    if (Player.HeldItem.type > 0 &&
                        ModContent.GetInstance<SpellIndex>().ItemCatalogue.ContainsKey(Player.HeldItem.type) &&
                        ModContent.GetInstance<SpellIndex>().ItemCatalogue[Player.HeldItem.type].Length >= ModContent.GetInstance<SpellUISystem>().Index[0] + 1 &&
                        ModContent.GetInstance<SpellIndex>().SpellCatalogue.ContainsKey(ModContent.GetInstance<SpellIndex>().ItemCatalogue[Player.HeldItem.type][ModContent.GetInstance<SpellUISystem>().Index[0]]) &&
                        ModContent.GetInstance<SpellUISystem>().Index[0] <= ModContent.GetInstance<SpellIndex>().ItemCatalogue[Player.HeldItem.type].Length )
                    {
                        if (ModContent.GetInstance<SpellIndex>().ItemCatalogue[Player.HeldItem.type].Contains(ModContent.GetInstance<SpellUISystem>().SelectedSpell[0]) && ModContent.GetInstance<SpellUISystem>().SelectedSpell[1] == 0 || ModContent.GetInstance<SpellUISystem>().SelectedSpell[1] == 1)
                        {
                            Player.HeldItem.GetGlobalItem<GlobalItemInstanced>().Spell =
                            ModContent.GetInstance<SpellIndex>().SpellCatalogue[ModContent.GetInstance<SpellIndex>().ItemCatalogue[ModContent.GetInstance<SpellUISystem>().Index[1]][ModContent.GetInstance<SpellUISystem>().Index[0]]].Item1;
                        }
                    }
                }

                ModContent.GetInstance<SpellUISystem>().Flush(); 
            }


            //Item Right Click
            if (triggersSet.MouseRight)
            {
                if (Player.HeldItem.type == ModContent.ItemType<HeroSword>() &&
                    Player.ownedProjectileCounts[ModContent.ProjectileType<HeroSwordProjectile>()] < 1 &&
                    Player == Main.LocalPlayer)
                {
                    Item sword = new Item(ModContent.ItemType<HeroSword>());
                    Projectile.NewProjectile(sword.GetSource_FromThis(), Player.MountedCenter, -Vector2.Normalize(Player.MountedCenter - Main.MouseWorld) * 16f, ModContent.ProjectileType<HeroSwordProjectile>(), sword.damage, sword.knockBack, Player.whoAmI, 0, 0, 0);
                    StretchArm(20f, Player.MountedCenter.DirectionTo(Main.MouseWorld).ToRotation() - MathHelper.PiOver2);
                }
                if (Player.HeldItem.type == ModContent.ItemType<BookmarkerWrench>() &&
                    Player == Main.LocalPlayer)
                {
                    TFUtils.UpdateSentryPriority(null, Player);
                }
            }
                
            
        }

        /// <summary>
        /// Stretches the Player's arm towards a specific point for a certain amount of time (obvious)
        /// </summary>
        public void StretchArm(float time, float rotation)
        {
            ArmAnimationTimer = new float[] { 0f, time, rotation };
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
