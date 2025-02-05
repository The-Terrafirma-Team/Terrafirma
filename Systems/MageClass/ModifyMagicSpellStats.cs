using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terrafirma.Common.Items;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;

namespace Terrafirma.Systems.MageClass
{
    internal class ModifyMagicSpellStats : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.DamageType == DamageClass.Magic;
        }
        public static void ApplyDefaults(Item item)
        {
            if (item.Spell() != null)
            {
                item.Spell().SetDefaults(item);
                item.channel = item.Spell().Channeled;
                item.UseSound = !item.Spell().OverrideSoundstyle ? ContentSamples.ItemsByType[item.type].UseSound : item.Spell().UseSound;
            }
        }
        public override bool? CanAutoReuseItem(Item item, Player player)
        {
            if (item.Spell() != null) return item.Spell().CanAutoReuse();
            return base.CanAutoReuseItem(item, player);
        }
        public override void UpdateInventory(Item item, Player player)
        {

            if (Main.mouseRight && item.Spell() != null) item.Spell().OnRightClick(item, player);

            if (player.channel && item.Spell() != null && !item.Spell().LeftMouseSwitch)
            {
                item.Spell().OnLeftMousePressed(item, player);
                item.Spell().LeftMouseSwitch = true;
            }
            else if (player.channel && item.Spell() != null && item.Spell().LeftMouseSwitch)
            {
                item.Spell().UpdateLeftMouse(item, player);
            }
            else
            {
                if (item.Spell() != null && item.Spell().LeftMouseSwitch)
                {
                    item.Spell().OnLeftMouseReleased(item, player);
                    item.Spell().LeftMouseSwitch = false;
                }
            }

            if (item.Spell() != null) item.Spell().Update(item, player);

            if (item.Spell() == null && SpellID.itemcatalogue.ContainsKey(item.type))
            {
                item.GetGlobalItem<GlobalItemInstanced>().Spell = SpellID.itemcatalogue[item.type][0];
            }

            base.UpdateInventory(item, player);
        }
        public override bool CanUseItem(Item item, Player player)
        {

            if (item.Spell() != null)
            {
                if (item.Spell().CanUseItem(item, player) == true) return true;
                else if (item.Spell().CanUseItem(item, player) == false) return false;
            }
            return base.CanUseItem(item, player);
        }
        public override bool? UseItem(Item item, Player player)
        {
            if (item.Spell() != null)
            {
                if (item.Spell().UseItem(item, player) == true) return true;
                else if (item.Spell().UseItem(item, player) == false) return false;
            }
            return base.UseItem(item, player);
        }
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (item.Spell() != null)
            {
                if (item.Spell().Shoot(player, source, position, velocity, type, damage, knockback) == true) item.Spell().Shoot(player, source, position, velocity, type, damage, knockback);
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
            if (SpellID.itemcatalogue.ContainsKey(item.type) && item.Spell() != null)
            {
                returnanimation = item.Spell().UseAnimation > 0 ? item.Spell().UseAnimation / (float)item.useAnimation : -1f;
            }
            return returnanimation == -1f? base.UseAnimationMultiplier(item) : returnanimation;
        }

        public override float UseTimeMultiplier(Item item)
        {
            float returntime = -1f;
            if (SpellID.itemcatalogue.ContainsKey(item.type) && item.Spell() != null)
            {
                returntime = item.Spell().UseTime > 0 ? item.Spell().UseTime / (float)item.useTime : -1f;
            }
            return returntime == -1f ? base.UseTimeMultiplier(item) : returntime;
        }

        public override void ModifyManaCost(Item item, ref float reduce, ref float mult)
        {
            if (SpellID.itemcatalogue.ContainsKey(item.type) && item.Spell() != null)
            {
                mult = item.Spell().ManaCost > 0 ? item.Spell().ManaCost / (float)item.mana : 1f;
            }
            base.ModifyManaCost(item, ref reduce, ref mult);
        }

    }
}
