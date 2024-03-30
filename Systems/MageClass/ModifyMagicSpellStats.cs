using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terrafirma.Global.Items;

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
        public override void ModifyManaCost(Item item, Player player, ref float reduce, ref float mult)
        {
            if (item.GetGlobalItem<GlobalItemInstanced>().Spell != null) mult = (float)item.GetGlobalItem<GlobalItemInstanced>().Spell.ManaCost / item.mana;
        }
        public override float UseTimeMultiplier(Item item, Player player)
        {
            if (item.GetGlobalItem<GlobalItemInstanced>().Spell != null) return (float)item.GetGlobalItem<GlobalItemInstanced>().Spell.UseTime / (float)item.useTime;
            return base.UseTimeMultiplier(item, player);
        }
        public override float UseAnimationMultiplier(Item item, Player player)
        {
            if (item.GetGlobalItem<GlobalItemInstanced>().Spell != null) return (float)item.GetGlobalItem<GlobalItemInstanced>().Spell.UseAnimation / (float)item.useAnimation;
            return base.UseAnimationMultiplier(item, player);
        }
        public override void UpdateInventory(Item item, Player player)
        {
            if (item.GetGlobalItem<GlobalItemInstanced>().Spell != null)
            {
                Item newitem = new Item(item.type);

                if (item.GetGlobalItem<GlobalItemInstanced>().Spell.ReuseDelay != -1)
                { if (item.reuseDelay != item.GetGlobalItem<GlobalItemInstanced>().Spell.ReuseDelay) item.reuseDelay = item.GetGlobalItem<GlobalItemInstanced>().Spell.ReuseDelay; }
                else
                { item.reuseDelay = newitem.reuseDelay; }

                if (item.GetGlobalItem<GlobalItemInstanced>().Spell.UseAnimation != -1)
                { if (item.useAnimation != item.GetGlobalItem<GlobalItemInstanced>().Spell.ReuseDelay) item.useAnimation = item.GetGlobalItem<GlobalItemInstanced>().Spell.UseAnimation; }
                else
                { item.useAnimation = newitem.useAnimation; }

                if (item.GetGlobalItem<GlobalItemInstanced>().Spell.UseTime != -1)
                { if (item.useTime != item.GetGlobalItem<GlobalItemInstanced>().Spell.UseTime) item.useTime = item.GetGlobalItem<GlobalItemInstanced>().Spell.UseTime; }
                else
                { item.useTime = newitem.useTime; }

                if (item.GetGlobalItem<GlobalItemInstanced>().Spell.ManaCost != -1)
                { if (item.mana != item.GetGlobalItem<GlobalItemInstanced>().Spell.ManaCost) item.mana = item.GetGlobalItem<GlobalItemInstanced>().Spell.ManaCost; }
                else
                { item.mana = newitem.mana; }

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
}
