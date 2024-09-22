using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Data;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Ranged.Thrower
{
    public class DarkenedDiamond : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemSets.ThrowerWeapon[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.DefaultToThrownWeapon(ModContent.ProjectileType<Projectiles.Ranged.Throwing.DarkenedDiamond>(),20,12,true);
            Item.damage = 10;
            Item.UseSound = SoundID.Item1;
            Item.noUseGraphic = true;
            Item.value = ContentSamples.ItemsByType[ItemID.DemoniteBar].value / 255;
            Item.rare = ItemRarityID.Blue;
        }
        public override void AddRecipes()
        {
            CreateRecipe(125).AddTile(TileID.Anvils).AddIngredient(ItemID.DemoniteBar,2).Register();
        }
    }
}
