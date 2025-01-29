using Microsoft.Xna.Framework;
using Terrafirma.Buffs.Sentry;
using Terrafirma.Common;
using Terrafirma.Common.Templates;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Summoner.Wrench
{
    public class UmbraWrench : WrenchItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToWrench(16, 30);
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 1);
            Buff = new UmbraWrenchBuff();
            BuffDuration = 60 * 15;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ItemID.DemoniteBar, 15).AddIngredient(ItemID.ShadowScale, 15).AddTile(TileID.Anvils).Register();
        }
    }
}
