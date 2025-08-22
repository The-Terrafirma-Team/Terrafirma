using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Terrafirma.Content.Tiles.BuildingBlocks
{
    public class SmoothStone : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<SmoothStoneBlock>());
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.Furnaces).AddIngredient(ItemID.StoneBlock).Register();
        }
    }
    public class SmoothStoneBlock : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;       
            DustType = DustID.Stone;
            AddMapEntry(new Color(144, 148, 144));
            HitSound = SoundID.Tink;
        }
    }
}
