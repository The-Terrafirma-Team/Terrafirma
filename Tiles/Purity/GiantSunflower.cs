using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terrafirma.Particles;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Terrafirma.Tiles.Purity
{
    public class GiantSunflower : ModTile
    {
        Asset<Texture2D> GlowTex;
        public override void SetStaticDefaults()
        {
            GlowTex = ModContent.Request<Texture2D>(Texture + "Glow");
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.Width = 4;
            TileObjectData.newTile.Height = 7;
            TileObjectData.newTile.Origin = new Point16(2, 6);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateHeights = new int[7] { 16, 16, 16, 16, 16, 16, 16 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.AnchorValidTiles = new int[6] { 2, 477, 109, 60, 492, 633 };
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.RandomStyleRange = 2;
            TileObjectData.newTile.LavaDeath = true;
            TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
            TileObjectData.addTile(Type);
            Main.tileLighted[Type] = true;
        }
        public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
        {
            offsetY = 2;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GlowTex.Value, new Vector2(i * 16,j * 16) - Main.screenPosition + new Vector2(12 * 16,12 * 16 + 2), new Rectangle(Main.tile[i,j].TileFrameX, Main.tile[i, j].TileFrameY,16,16),Color.White,0,Vector2.Zero,1,SpriteEffects.None,0);

            spriteBatch.Draw(GlowTex.Value, new Vector2(i * 16, j * 16) - Main.screenPosition + new Vector2(12 * 16, 12 * 16 + 2), new Rectangle(Main.tile[i, j].TileFrameX, Main.tile[i, j].TileFrameY, 16, 16), new Color(1f,1f,1f,0f) * (MathF.Sin((float)Main.timeForVisualEffects * 0.01f) * 0.2f + 0.2f), 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 1f;
            g = 0.95f;
            b = 0.65f;
            if (Main.tile[i, j].TileFrameY < 18 * 4)
            {
                r = 0.3f;
                g = 0.27f;
            }
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (!closer || !(Main.tile[i, j].TileFrameX == 0 || Main.tile[i, j].TileFrameX != 18*5) || Main.tile[i, j].TileFrameY != 0 || !Main.rand.NextBool(120))
                return;

            Vector2 coords = new Point(i + 1, j + 2).ToWorldCoordinates(0, 0);

            //Dust.NewDustPerfect(coords, DustID.Terra);
            SunflowerBeam p = new SunflowerBeam();
            ParticleSystem.AddParticle(p, coords + Main.rand.NextVector2Circular(32, 16), Main.rand.NextVector2Circular(0.3f, 0.1f));
        }
    }
}
