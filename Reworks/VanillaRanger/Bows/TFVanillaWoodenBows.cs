using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Reworks.VanillaRanger.Bows.PreHardmode;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaRanger.Bows
{
    internal class TFVanillaWoodenBows : GlobalItem
    {
        private static HashSet<int> types = new HashSet<int> { 
            ItemID.WoodenBow, ItemID.BorealWoodBow
        };
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return types.Contains(entity.type);
        }

        public override void SetStaticDefaults()
        {
            foreach(int type in types)
            {
                ItemID.Sets.ItemsThatAllowRepeatedRightClick[type] = true;
            }
            base.SetStaticDefaults();
        }

        public override void SetDefaults(Item entity)
        {
            base.SetDefaults(entity);
            entity.channel = true;
            entity.noUseGraphic = true;
            entity.noMelee = true;
            entity.autoReuse = true;
            entity.UseSound = null;
        }
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int shoot = ProjectileID.WoodenArrowFriendly;

            switch (item.type)
            {
                case ItemID.WoodenBow: shoot = ModContent.ProjectileType<WoodenBowProj>(); break;
                case ItemID.BorealWoodBow: shoot = ModContent.ProjectileType<BorealWoodBowProj>(); break;
                default: shoot = -1; break;
            }

            if (shoot == -1) return base.Shoot(item, player, source, position, velocity, type, damage, knockback);

            if (player.altFunctionUse == 2) Projectile.NewProjectile(source, position, velocity, shoot, damage, knockback, player.whoAmI, 0f, source.AmmoItemIdUsed, ai2: 1);
            else Projectile.NewProjectile(source, position, velocity, shoot, damage, knockback, player.whoAmI, 0f, source.AmmoItemIdUsed);

            return false;
        }

        public override bool? CanAutoReuseItem(Item item, Player player) => true;
        public override bool AltFunctionUse(Item item, Player player) => true;
    }
}
