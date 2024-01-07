using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TerrafirmaRedux.Global;
using TerrafirmaRedux.Projectiles.Magic;
using Terraria.DataStructures;
using Terraria.Audio;

namespace TerrafirmaRedux.Reworks.VanillaMagic
{
    public class DungeonStaves : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type is >= 1444 and <= 1446;
        }
        public override void SetDefaults(Item entity)
        {
            if (entity.type == ItemID.InfernoFork)
                entity.UseSound = null;
        }
        public override void ModifyManaCost(Item item, Player player, ref float reduce, ref float mult)
        {
            byte spell = item.GetGlobalItem<GlobalItemInstanced>().Spell;
            base.ModifyManaCost(item, player, ref reduce, ref mult);
        }
        public override float UseAnimationMultiplier(Item item, Player player)
        {
            byte spell = item.GetGlobalItem<GlobalItemInstanced>().Spell;
            switch (spell)
            {
                case 2:
                    return 1.2f;
            }
            return base.UseAnimationMultiplier(item, player);
        }
        public override float UseTimeMultiplier(Item item, Player player)
        {
            byte spell = item.GetGlobalItem<GlobalItemInstanced>().Spell;
            switch (spell)
            {
                case 1:
                    return 0.17f;
                case 2:
                    return 0.3f;
            }

            return base.UseTimeMultiplier(item, player);
        }
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            byte spell = item.GetGlobalItem<GlobalItemInstanced>().Spell;
            switch (spell)
            {
                case 0:
                    SoundEngine.PlaySound(SoundID.Item73, player.position);
                    break;
                case 1:
                    if(player.ItemAnimationJustStarted) 
                        SoundEngine.PlaySound(SoundID.Item34, player.position);
                    break;
                case 2:
                    break;
            }

            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }
        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            byte spell = item.GetGlobalItem<GlobalItemInstanced>().Spell;
            switch (spell)
            {
                case 0:
                    type = ModContent.ProjectileType<InfernoFork>();
                    velocity *= 1.2f;
                    //velocity = new Vector2(6,-6);
                    break;
                case 1:
                    type = ProjectileID.Flames;
                    damage = (int)(damage * 0.6f);
                    velocity *= 0.7f;
                    position += Vector2.Normalize(velocity) * 30;
                    break;
                case 2:
                    type = ModContent.ProjectileType<Firewall>();
                    velocity = Vector2.Normalize(velocity) * 0.01f;
                    damage = (int)(damage * 0.5f);
                    position = Main.MouseWorld;
                    break;
            }
        }
    }
}
