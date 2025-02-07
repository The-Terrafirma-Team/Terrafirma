﻿using System.Collections.Generic;
using System.Linq;
using Terraria.DataStructures;
using Terraria;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Humanizer;
using Terrafirma.Systems.AccessorySynergy;
using Microsoft.Xna.Framework.Graphics;
using Terrafirma.Systems.UIElements;
using System;
using Terrafirma.Common.Items;
using Terraria.Audio;

namespace Terrafirma.Systems.MageClass
{
    
    public abstract class Spell : ModType
    {
        /// <summary>
        /// This gets set to true once OnLeftMousePressed() is run and set to false once OnLeftMouseReleased() is run, this is to prevent that method from running multiple times.
        /// Set this to false to make OnLeftMousePressed() run again. This is useful for things like the Ice Boulder spell which can repeat even if the mouse is constantly held down.
        /// </summary>
        public bool LeftMouseSwitch = false;
        public virtual string TexurePath => (base.GetType().Namespace + "." + this.Name).Replace('.', '/');
        /// <summary>
        /// Sets the ReuseDelay of the spell's ManaCost. Set to -1 to use the item's default ManaCost
        /// </summary>
        public virtual int ManaCost => -1;
        /// <summary>
        /// Sets the ReuseDelay of the spell's item. Set to -1 to use the item's default ReuseDelay
        /// </summary>
        public virtual int UseTime => -1;
        /// <summary>
        /// Sets the UseTime of the spell's item. Set to -1 to use the item's default UseTime
        /// </summary>
        public virtual int ReuseDelay => -1;
        /// <summary>
        /// Sets the UseAnimation of the spell's item. Set to -1 to use the item's default UseAnimation
        /// </summary>
        public virtual int UseAnimation => -1;

        /// <summary>
        ///  Sets the use sound of the spell's item
        /// </summary>
        public virtual bool OverrideSoundstyle => false;

        /// <summary>
        ///  Sets the use sound of the spell's item, set OverrideSoundstyle for this to do anything.
        /// </summary>
        public virtual SoundStyle? UseSound => null;

        /// <summary>
        /// Whether or not the item needs channeling for the spell to work
        /// </summary>
        public virtual bool Channeled => false;
        /// <summary>
        /// Array of all items that can use this spell. Works for all Weapons and Accessories, doesn't have any effect on other items.
        /// </summary>
        public virtual int[] SpellItem => new int[]{1};

        public override void Load()
        {
            SpellID.spells = SpellID.spells.Append(this).ToArray();
            for (int i = 0;  i < SpellItem.Length; i++)
            {
                if (SpellID.itemcatalogue.ContainsKey(SpellItem[i]))
                {
                    SpellID.itemcatalogue[SpellItem[i]] = SpellID.itemcatalogue[SpellItem[i]].Append(this).ToArray();
                }
                else
                {
                    SpellID.itemcatalogue.Add(SpellItem[i], new Spell[] { this });
                }

            }

            Language.GetOrRegister("Mods.Terrafirma.Spells." + $"{this.GetType().Name}" + ".Name", CreateSpellName);
            Language.GetOrRegister("Mods.Terrafirma.Spells." + $"{this.GetType().Name}" + ".Description", CreateSpellDescription);       

        }

        string CreateSpellName() => this.GetType().Name.Titleize();

        static string CreateSpellDescription() => "";


        public string GetSpellName()
        {
            return Language.GetTextValue("Mods.Terrafirma.Spells." + $"{this.GetType().Name}" + ".Name");
        }
        public string GetSpellDesc()
        {
            return Language.GetTextValue("Mods.Terrafirma.Spells." + $"{this.GetType().Name}" + ".Description");
        }

        public virtual void SetDefaults(Item item)
        {
        }

        /// <summary>
        /// Update every tick while this weapon is in the Player's Inventory
        /// </summary>
        public virtual void Update(Item item, Player player)
        {
        }

        /// <summary>
        /// Runs once when the left mouse is pressed
        /// </summary>
        public virtual void OnLeftMousePressed(Item item, Player player)
        {
        }

        /// <summary>
        /// Update every tick while the left mouse is down
        /// </summary>
        public virtual void UpdateLeftMouse(Item item, Player player)
        {
        }

        //Runs once when the left mouse is released
        public virtual void OnLeftMouseReleased(Item item, Player player)
        {
        }

        public virtual void OnRightClick(Item item, Player player)
        {
        }

        public virtual bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return true;
        }
        public virtual bool? UseItem(Item item, Player player)
        {
            return true;
        }
        public virtual bool CanUseItem(Item item, Player player)
        {
            return true;
        }

        public virtual bool CanAutoReuse()
        {
            return true;
        }
        protected override void Register()
        {
            ModTypeLookup<Spell>.Register(this);
        }


    }

    public static class SpellMethods
    {
        //Compares two spells. Returns true if both spells match.
        public static bool IsEqualsTo(this Spell basespell, Spell spell)
        {
            if (basespell.GetSpellName() == spell.GetSpellName()) return true;
            return false;
        }
        public static bool ContainsSpell(this Spell[] spellarray, Spell spell)
        {
            if (spell != null)
            {
                for (int i = 0; i < spellarray.Length; i++)
                {
                    if (spellarray[i].GetSpellName() == spell.GetSpellName()) return true;
                }
                return false;
            }
            return false;
        }

        public static bool ContainsSpell(this Spell[] spellarray, string spellName)
        {
            for (int i = 0; i < spellarray.Length; i++)
            {
                if (spellarray[i].GetSpellName() == spellName) return true;
            }
            return false;
        }
    }

    public class SpellID : ModSystem
    {
        public static Spell[] spells = new Spell[] { };
        public static Dictionary<int, Spell[]> itemcatalogue = new Dictionary<int, Spell[]>();

        public static int GetWeaponSpellIndex(Spell spell, int itemid)
        {
            for (int i = 0; i < itemcatalogue[itemid].Length; i++)
            {
                if (itemcatalogue[itemid][i].IsEqualsTo(spell)) return i;
            }
            return 0;
        }

        public static int GetWeaponSpellIndexWithAccessory(Spell spell, int itemid)
        {
            for (int i = 0; i < itemcatalogue[itemid].Length; i++)
            {
                if (itemcatalogue[itemid][i].IsEqualsTo(spell)) return i;
            }

            int AccumulatedAccessorySpells = 0;

            for (int i = 0; i < Main.LocalPlayer.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories.Length; i++)
            {
                if (itemcatalogue.ContainsKey(Main.LocalPlayer.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories[i]))
                {
                    int accessory = Main.LocalPlayer.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories[i];
                    for (int j = 0; j < itemcatalogue[accessory].Length; j++)
                    {
                        if (itemcatalogue[Main.LocalPlayer.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories[i]][j].IsEqualsTo(spell)) return itemcatalogue[itemid].Length + AccumulatedAccessorySpells;
                        else AccumulatedAccessorySpells++;
                    }
                }
            }
            return 0;
        }

        public static Spell GetWeaponSpellFromIndex(int index, int itemid)
        {
            if (index < itemcatalogue[itemid].Length)
            {
                return itemcatalogue[itemid][index];
            }
            else
            {
                int AccumulatedAccessorySpells = 0;
                for (int i = 0; i < Main.LocalPlayer.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories.Length; i++)
                {
                    if (itemcatalogue.ContainsKey(Main.LocalPlayer.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories[i]))
                    {
                        int accessory = Main.LocalPlayer.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories[i];
                        for (int j = 0; j < itemcatalogue[accessory].Length; j++)
                        {
                            if (index == itemcatalogue[itemid].Length + AccumulatedAccessorySpells) return itemcatalogue[Main.LocalPlayer.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories[i]][j];
                            else AccumulatedAccessorySpells++;
                        }
                    }
                }
            }
            return null;
        }

        public static int GetMaxSpellsforWeapon(int itemid)
        {
            return itemcatalogue[itemid].Length;
        }

        public static int GetMaxSpellsforWeaponwithAccessory(int itemid)
        {
            if (!itemcatalogue.ContainsKey(itemid)) return 0;

            int AccumulatedAccessorySpells = 0;
            for (int i = 0; i < Main.LocalPlayer.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories.Length; i++)
            {
                if (itemcatalogue.ContainsKey(Main.LocalPlayer.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories[i]))
                {
                    int accessory = Main.LocalPlayer.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories[i];
                    AccumulatedAccessorySpells += itemcatalogue[accessory].Length;
                }
            }

            return itemcatalogue[itemid].Length + AccumulatedAccessorySpells;
        }
    }
}
