using Microsoft.Xna.Framework;
using Terrafirma.Common.Players;
using Terrafirma.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Terrafirma.Items.Tools
{
    public class CrystalPickaxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToSword(10, 22, 4);
            Item.pick = 150;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(gold: 2);
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.Anvils).AddIngredient(ItemID.PearlstoneBlock, 25).AddIngredient(ItemID.CrystalShard, 25).AddIngredient(ItemID.SoulofLight, 10).Register();
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.Lerp(lightColor, Color.White, 0.5f);
        }
    }
}
