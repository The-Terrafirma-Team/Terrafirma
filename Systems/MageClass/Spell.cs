using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Terrafirma.Systems.MageClass
{
    
    public abstract class Spell : ModType
    {
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
        /// Array of all items that can use this spell. Works for all Weapons and Accessories, doesn't have any effect on other items.
        /// </summary>
        public virtual int[] SpellItem => new int[]{1};

        public override void Load()
        {
            SpellIndex.SpellID = SpellIndex.SpellID.Append(this).ToArray();
            for (int i = 0;  i < SpellItem.Length; i++)
            {
                if (SpellIndex.ItemCatalogue.ContainsKey(SpellItem[i]))
                {
                    SpellIndex.ItemCatalogue[SpellItem[i]] = SpellIndex.ItemCatalogue[SpellItem[i]].Append(this).ToArray();
                }
                else
                {
                    SpellIndex.ItemCatalogue.Add(SpellItem[i], new Spell[] { this });
                }

            }
            
        }
        public string GetSpellName()
        {
            return Language.GetTextValue("Mods.Terrafirma.Spells.Name." + $"{this.GetType().Name}");
        }
        public string GetSpellDesc()
        {
            return Language.GetTextValue("Mods.Terrafirma.Spells.Desc." + $"{this.GetType().Name}");
        }


        public virtual void SetDefaults(Item entity)
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

    public class SpellIndex : ModSystem
    {
        public static Spell[] SpellID = new Spell[] { };
        public static Dictionary<int, Spell[]> ItemCatalogue = new Dictionary<int, Spell[]>();
    }
}
