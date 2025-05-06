using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Biomes.CaveHouse;
using Terraria.ID;
using Terraria.ModLoader;
using static System.Net.Mime.MediaTypeNames;

namespace Terrafirma.Items.Weapons.Ranged.Bows
{
    public class SteelGreatbow : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Item.DefaultToDrawnBow(ModContent.ProjectileType<Projectiles.Ranged.Bows.SteelGreatbowProj>(), 30, 20,5,10);
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 0, 45, 0);
            Item.autoReuse = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(source, position, velocity, Item.shoot, damage, knockback, player.whoAmI, 0f, source.AmmoItemIdUsed, ai2:1);
            }
            else
            {
                Projectile.NewProjectile(source, position, velocity, Item.shoot, damage, knockback, player.whoAmI, 0f, source.AmmoItemIdUsed);
            }
            return false;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
    }
}
