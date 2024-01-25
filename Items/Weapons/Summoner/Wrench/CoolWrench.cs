using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrafirmaRedux.Global;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Items.Weapons.Summoner.Wrench
{
    public class CoolWrench : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToWrench(12, 28);
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 2, 0);
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            player.WrenchHitSentry(hitbox, SentryBuffID.CoolWrench, 60 * 4);
        }
        public override void AddRecipes()
        {

            CreateRecipe()
            .AddIngredient(ItemID.IceBlock, 10)
            .AddRecipeGroup(nameof(ItemID.GoldBar), 6)
            .AddTile(TileID.Anvils)
            .Register();
        }

        public override bool MeleePrefix()
        {
            return true;
        }
    }
}
