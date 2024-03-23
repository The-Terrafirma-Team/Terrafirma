using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terrafirma.Systems.Cooking;

namespace Terrafirma.Tiles
{
    public class CookingPotsTiles : GlobalTile
    {
        public Asset<Texture2D> PotHighlight;
        public override void Load()
        {
            // We'll need these textures for later, it's best practice to cache them on load instead of continually requesting every draw call.
            PotHighlight = ModContent.Request<Texture2D>("Terrafirma/Tiles/CookingPotsTiles");
        }

        public override void SetStaticDefaults()
        {
            TileID.Sets.InteractibleByNPCs[TileID.CookingPots] = true;
            TileID.Sets.DisableSmartCursor[TileID.CookingPots] = false;
        }

        public override void MouseOver(int i, int j, int type)
        {
            if (type == TileID.CookingPots)
            {
                if (Main.tile[i, j].TileFrameX >= 34) 
                {
                    Main.LocalPlayer.cursorItemIconEnabled = true;
                    Main.LocalPlayer.cursorItemIconID = ItemID.Cauldron;
                }
                else
                {
                    Main.LocalPlayer.cursorItemIconEnabled = true;
                    Main.LocalPlayer.cursorItemIconID = ItemID.CookingPot;
                }
                
            }
            base.MouseOver(i, j, type);
        }

        public override void RightClick(int i, int j, int type)
        {
            if (type == TileID.CookingPots)
            {
                if (Main.tile[i, j].TileFrameX >= 34)
                {
                    Vector2 Tileframe = new Vector2(Main.tile[i, j].TileFrameX - 34, Main.tile[i, j].TileFrameY);
                    Vector2 offset = new Vector2(Tileframe.X - (Tileframe.X % 16) * 1,
                                                 Tileframe.Y - (Tileframe.Y % 16) * 1);
                    ModContent.GetInstance<CookingPotUISystem>().Create(new Vector2(i * 16 - offset.X, j * 16 - offset.Y));
                }
                else
                {
                    Vector2 offset = new Vector2(Main.tile[i, j].TileFrameX - (Main.tile[i, j].TileFrameX % 16) * 1,
                                                 Main.tile[i, j].TileFrameY - (Main.tile[i, j].TileFrameY % 16) * 1);
                    ModContent.GetInstance<CookingPotUISystem>().Create(new Vector2(i * 16 - offset.X, j * 16 - offset.Y));
                }

            }
            base.RightClick(i, j, type);
        }
            
        //public override void PostDraw(int i, int j, int type, SpriteBatch spriteBatch)
        //{
        //    if (type != TileID.CookingPots) return;

        //    if ( Main.LocalPlayer.IsWithinSnappngRangeToTile(i,j, PlayerSittingHelper.ChairSittingMaxDistance) && Main.LocalPlayer.controlSmart)
        //    {

        //        Tile tile = Main.tile[i, j];
        //        Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);

        //        Color selectcolor = Color.White;

        //        spriteBatch.Draw(
        //        PotHighlight.Value,
        //        new Vector2(i * 16 - Main.screenPosition.X, j * 16 - Main.screenPosition.Y) + zero,
        //        new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16),
        //        Lighting.GetColor(i, j),
        //        0f,
        //        Vector2.Zero,
        //        1f,
        //        SpriteEffects.None,
        //        0f);
        //    }

        //    base.PostDraw(i, j, type, spriteBatch);
        //}


        public override void Unload()
        {
            TileID.Sets.InteractibleByNPCs[TileID.CookingPots] = false;
        }
    }
}
