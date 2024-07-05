using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terrafirma.Systems.MageClass;
using Terrafirma.Items.Weapons.Summoner.Wrench;
using Terrafirma.Common.Items;
using Terrafirma.Items.Consumable;
using Terraria.GameContent.UI;
using Terrafirma.Systems.NewNPCQuests;
using Terrafirma.Common.Interfaces;
using Terrafirma.Items.Weapons.Melee.Knight;
using Terrafirma.Projectiles.Melee.Knight;
using Microsoft.Xna.Framework.Input;


namespace Terrafirma.Common.Players
{
    public class TerrafirmaModPlayer : ModPlayer
    {
        //Accessories

        public bool Foregrip = false;
        public bool DrumMag = false;
        public bool AmmoCan = false;
        public bool CanUseAmmo = true;
        public bool BoxOfHighPiercingRounds = false;

        public bool SpringBoots = true;

        //Movement Variables

        public Vector2 Momentum = Vector2.Zero;
        public int FloorTime = 0;
        public float JumpMultiplier = 1f;

        //UI Variables

        public static bool SpellUI = false;
        internal Item HeldMagicItem = new Item(0);

        public static bool SpellSideMenu = false;

        //

        public Quest[] playerquests = new Quest[] { };

        internal bool RightMouseSwitch = false;

        public override void ResetEffects()
        {
            Player.pickSpeed *= 0.8f;
            Player.accFlipper = true;
            //Player.runAcceleration *= 2;

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
            if (playerquests.Length == 0) playerquests = QuestID.quests;
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            //Check if Mouse
            Tile tile = Main.tile[(Main.MouseWorld / 16).ToPoint()];
            bool TileInteract = TileID.Sets.InteractibleByNPCs[tile.TileType] || TileID.Sets.HasOutlines[tile.TileType];

            if (HeldMagicItem != Player.HeldItem && triggersSet.MouseRight && Player.inventory[Player.selectedItem].damage != -1 && !Main.HoveringOverAnNPC && Main.SmartInteractTileCoordsSelected.Count == 0 && !TileInteract)
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
                if (!SpellUI && SpellIndex.ItemCatalogue.ContainsKey(Player.inventory[Player.selectedItem].type) && Player.inventory[Player.selectedItem].damage != -1 && HeldMagicItem == Player.HeldItem && !Main.HoveringOverAnNPC && Main.SmartInteractTileCoordsSelected.Count == 0 && !TileInteract)
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

            //Spell Side menu
            if (!SpellSideMenu && SpellIndex.ItemCatalogue.ContainsKey(Player.HeldItem.type))
            {
                ModContent.GetInstance<SpellSideMenuUISystem>().Create(Player.HeldItem);
                SpellSideMenu = true;
            }
            if (!SpellIndex.ItemCatalogue.ContainsKey(Player.HeldItem.type) || ModContent.GetInstance<SpellSideMenuUISystem>().spellitem.type != Player.HeldItem.type || Player.HeldItem.type == 0)
            {
                ModContent.GetInstance<SpellSideMenuUISystem>().Flush();
                SpellSideMenu = false;
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

        public override void OnEnterWorld()
        {
            SpellSideMenu = false;
            base.OnEnterWorld();
        }

        public override bool HoverSlot(Item[] inventory, int context, int slot)
        {
            if (Main.mouseRight && !RightMouseSwitch)
            {
                if(Main.mouseItem.ModItem is IUseOnItemInInventoryItem item)
                {
                    if (item.canBeUsedOnThisItem(Player,Main.mouseItem, inventory[slot],context))
                        item.useOnItem(Player,Main.mouseItem,inventory[slot],context);
                }
                RightMouseSwitch = true;
            }
            if (!Main.mouseRight) RightMouseSwitch = false;
            return base.HoverSlot(inventory, context, slot);
        }
    }
}
