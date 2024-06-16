using Microsoft.Xna.Framework;
using Terrafirma.Common.Templates.Melee;
using Terrafirma.Items.Materials;
using Terrafirma.Projectiles.Melee;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Melee.Knight
{
    public class ShadowflameSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToSword(90, 25, 8);
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.ShadowflameSword>();
            Item.UseSound = SoundID.DD2_FlameburstTowerShot;
            Item.shootSpeed = 8;
            Item.crit = 11;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(0, 2, 0, 0);
        }
        public override bool MeleePrefix()
        {
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.Anvils).AddIngredient(ModContent.ItemType<SteelBar>(), 8).AddRecipeGroup(RecipeGroupID.Wood, 4).Register();
        }
    }
}
