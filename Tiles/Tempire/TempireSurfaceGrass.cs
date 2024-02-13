using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Global;
using Terrafirma.Items.Materials;
using Terrafirma.Items.Placeable.Tempire;
using Terraria;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.ObjectData;

namespace Terrafirma.Tiles.Tempire
{
    internal class TempireSurfaceGrass : ModTile
    {
        public override void SetStaticDefaults()
        {

            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;
            Main.tileCut[Type] = true;
            Main.tileSolid[Type] = false;
            Main.tileNoFail[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            

            TileID.Sets.ReplaceTileBreakUp[Type] = true;
            TileID.Sets.IgnoredInHouseScore[Type] = true;
            TileID.Sets.IgnoredByGrowingSaplings[Type] = true;
            TileMaterials.SetForTileId(Type, TileMaterials._materialsByName["Plant"]); // Make this tile interact with golf balls in the same way other plants do

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(95, 89, 70));

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1xX);
            TileObjectData.newTile.AnchorValidTiles = new int[] {
                ModContent.TileType<TempireGrass>()
            };
            TileObjectData.addTile(Type);

            


            HitSound = SoundID.Grass;
            DustType = DustID.Ambient_DarkBrown;
        }

        

        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            return true;
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (Main.tile[i, j].TileFrameY == (short)(10 * ( 18 * 3 )) )
            {
                Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, new Item(ModContent.ItemType<Mistcap>()));
            }
            base.KillTile(i, j, ref fail, ref effectOnly, ref noItem);
        }

        public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
        {
            offsetY = 6;
        }
    }
}
