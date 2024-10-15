using Microsoft.Xna.Framework;
using Terrafirma.Buffs.Sentry;
using Terrafirma.Common;
using Terrafirma.Common.Templates;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Summoner.Wrench
{
    public class CoolWrench : WrenchItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToWrench(12, 28);
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 2, 0);
            Buff = new CoolWrenchBuff();
            BuffDuration = 3600 * 3;
        }
        public override void AddRecipes()
        {

            CreateRecipe()
            .AddIngredient(ItemID.IceBlock, 10)
            .AddRecipeGroup("Terrafirma:GoldBar", 6)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
