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
    public class BookmarkerWrench : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToWrench(14, 25);
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.buyPrice(0, 4, 0);
        }   
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            player.WrenchHitSentry(hitbox, SentryBuffID.SentryPriority, 30);
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddRecipeGroup(RecipeGroupID.IronBar, 15).AddTile(TileID.Anvils).Register();
        }

        public override bool MeleePrefix()
        {
            return true;
        }
    }
}
