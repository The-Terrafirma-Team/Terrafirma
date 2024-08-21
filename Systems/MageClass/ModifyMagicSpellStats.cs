using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terrafirma.Common.Items;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;

namespace Terrafirma.Systems.MageClass
{
    internal class ModifyMagicSpellStats : GlobalItemInstanced
    {  
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.DamageType == DamageClass.Magic;
        }
        public override void SetDefaults(Item entity)
        {
            if (entity.GetGlobalItem<GlobalItemInstanced>().Spell != null) entity.GetGlobalItem<GlobalItemInstanced>().SetDefaults(entity);
        }
        public override void UpdateInventory(Item item, Player player)
        {

            if (Main.mouseRight && item.GetGlobalItem<GlobalItemInstanced>().Spell != null) item.GetGlobalItem<GlobalItemInstanced>().Spell.OnRightClick(item, player);

            if (Main.mouseLeft && item.GetGlobalItem<GlobalItemInstanced>().Spell != null && !item.GetGlobalItem<GlobalItemInstanced>().Spell.LeftMouseSwitch)
            {
                item.GetGlobalItem<GlobalItemInstanced>().Spell.OnLeftMousePressed(item, player);
                item.GetGlobalItem<GlobalItemInstanced>().Spell.LeftMouseSwitch = true;
            }
            else if (Main.mouseLeft && item.GetGlobalItem<GlobalItemInstanced>().Spell != null && item.GetGlobalItem<GlobalItemInstanced>().Spell.LeftMouseSwitch)
            {
                item.GetGlobalItem<GlobalItemInstanced>().Spell.UpdateLeftMouse(item, player);
            }
            else
            {
                if (item.GetGlobalItem<GlobalItemInstanced>().Spell != null && item.GetGlobalItem<GlobalItemInstanced>().Spell.LeftMouseSwitch)
                {
                    item.GetGlobalItem<GlobalItemInstanced>().Spell.OnLeftMouseReleased(item, player);
                    item.GetGlobalItem<GlobalItemInstanced>().Spell.LeftMouseSwitch = false;
                }
            }

            if (item.GetGlobalItem<GlobalItemInstanced>().Spell != null) item.GetGlobalItem<GlobalItemInstanced>().Spell.Update(item, player);

            if (item.GetGlobalItem<GlobalItemInstanced>().Spell == null && SpellID.itemcatalogue.ContainsKey(item.type))
            {
                item.GetGlobalItem<GlobalItemInstanced>().Spell = SpellID.itemcatalogue[item.type][0];
            }

            base.UpdateInventory(item, player);
        }
        public override bool CanUseItem(Item item, Player player)
        {

            if (item.GetGlobalItem<GlobalItemInstanced>().Spell != null)
            {
                if (item.GetGlobalItem<GlobalItemInstanced>().Spell.CanUseItem(item, player) == true) return true;
                else if (item.GetGlobalItem<GlobalItemInstanced>().Spell.CanUseItem(item, player) == false) return false;
            }
            return base.CanUseItem(item, player);
        }
        public override bool? UseItem(Item item, Player player)
        {
            if (item.GetGlobalItem<GlobalItemInstanced>().Spell != null)
            {
                if (item.GetGlobalItem<GlobalItemInstanced>().Spell.UseItem(item, player) == true) return true;
                else if (item.GetGlobalItem<GlobalItemInstanced>().Spell.UseItem(item, player) == false) return false;
            }
            return base.UseItem(item, player);
        }
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (item.GetGlobalItem<GlobalItemInstanced>().Spell != null)
            {
                if (item.GetGlobalItem<GlobalItemInstanced>().Spell.Shoot(player, source, position, velocity, type, damage, knockback) == true) item.GetGlobalItem<GlobalItemInstanced>().Spell.Shoot(player, source, position, velocity, type, damage, knockback);
                else return false;
            }
            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);

        }

    }

    internal class ModifyMagicSpellStatsPlayer : ModPlayer
    {
        public override float UseAnimationMultiplier(Item item)
        {
            float returnanimation = -1f;
            if (SpellID.itemcatalogue.ContainsKey(item.type) && item.GetGlobalItem<GlobalItemInstanced>().Spell != null)
            {
                returnanimation = item.GetGlobalItem<GlobalItemInstanced>().Spell.UseAnimation > 0 ? item.GetGlobalItem<GlobalItemInstanced>().Spell.UseAnimation / (float)item.useAnimation : -1f;
            }
            return returnanimation == -1f? base.UseAnimationMultiplier(item) : returnanimation;
        }

        public override float UseTimeMultiplier(Item item)
        {
            float returntime = -1f;
            if (SpellID.itemcatalogue.ContainsKey(item.type) && item.GetGlobalItem<GlobalItemInstanced>().Spell != null)
            {
                returntime = item.GetGlobalItem<GlobalItemInstanced>().Spell.UseTime > 0 ? item.GetGlobalItem<GlobalItemInstanced>().Spell.UseTime / (float)item.useTime : -1f;
            }
            return returntime == -1f ? base.UseTimeMultiplier(item) : returntime;
        }

        public override void ModifyManaCost(Item item, ref float reduce, ref float mult)
        {
            if (SpellID.itemcatalogue.ContainsKey(item.type) && item.GetGlobalItem<GlobalItemInstanced>().Spell != null)
            {
                mult = item.GetGlobalItem<GlobalItemInstanced>().Spell.ManaCost > 0 ? item.GetGlobalItem<GlobalItemInstanced>().Spell.ManaCost / (float)item.mana : 1f;
            }
            base.ModifyManaCost(item, ref reduce, ref mult);
        }

    }
}
