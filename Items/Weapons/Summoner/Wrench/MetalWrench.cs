using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Buffs.Sentry;
using Terrafirma.Common;
using Terrafirma.Common.Templates;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Summoner.Wrench
{
    public class MetalWrench : WrenchItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToWrench(8, 25);
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 10);
            Buff = new MetalWrenchBuff();
            BuffDuration = 60 * 30;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddRecipeGroup(RecipeGroupID.IronBar, 15).AddTile(TileID.Anvils).Register();
        }
    }
}
