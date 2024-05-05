using Microsoft.Xna.Framework;
using Terrafirma.Common.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Summoner.Wrench
{
    public class Bonetwister : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToWrench(18, 30);
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 1);
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            player.WrenchHitSentry(hitbox, SentryBuffID.CrimtaneWrench, 60 * 5);
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ItemID.CrimtaneBar, 15).AddIngredient(ItemID.TissueSample, 15).AddTile(TileID.Anvils).Register();
        }
        public override bool MeleePrefix()
        {
            return true;
        }
    }
}
