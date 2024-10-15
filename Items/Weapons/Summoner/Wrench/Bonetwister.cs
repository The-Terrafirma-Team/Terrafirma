using Microsoft.Xna.Framework;
using Terrafirma.Buffs.Sentry;
using Terrafirma.Common;
using Terrafirma.Common.Templates;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Summoner.Wrench
{
    public class Bonetwister : WrenchItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToWrench(18, 30);
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 1);
            Buff = new BonetwisterBuff();
            BuffDuration = 3600 * 2;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ItemID.CrimtaneBar, 15).AddIngredient(ItemID.TissueSample, 15).AddTile(TileID.Anvils).Register();
        }
    }
}
