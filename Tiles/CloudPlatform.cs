using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Enums;
using Terraria.Localization;


namespace Terrafirma.Tiles
{
    public class CloudPlatform : ModTile
    {
        public Asset<Texture2D> PotHighlight;
        public override void Load()
        {
            PotHighlight = ModContent.Request<Texture2D>("Terrafirma/Tiles/CloudPlatform");
        }

        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileSolidTop[Type] = true;

            TileObjectData.newTile.Width = 3;
            TileObjectData.newTile.Height = 1;

            TileObjectData.newTile.CoordinateHeights = new int[1] { 16 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;

            TileObjectData.addTile(Type);
            DustType = DustID.Cloud;
        }

    }
}
