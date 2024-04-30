using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Tiles;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace Terrafirma.Items.Materials
{
    public class SteelBar : ModItem
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(16);
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.DefaultToPlaceableTile(ModContent.TileType<PlacedBars>(), 1);
        }
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25;
        }
    }
}
