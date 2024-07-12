using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Terrafirma.Systems.Trees
{
    public enum TreeTileType
    {
        Trunk = 0,
        Base = 1,
        Crown = 2,
    }
    public class ModTree_Terrafirma : ModTile
    {
        public byte Variant = 0;
        public override string Texture => "Terrafirma/Systems/Trees/BirchTree";
        public override void SetStaticDefaults()
        {
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.addTile(Type);
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Texture2D Tex = ModContent.Request<Texture2D>("Terrafirma/Systems/Trees/BirchTree").Value;

            spriteBatch.Draw(Tex,
                new Vector2(i * 16, j * 16) - Main.screenPosition,
                new Rectangle(0,32 * Variant,32,32),
                Color.White,
                0f,
                - Tex.Size() - new Vector2(- 8,Tex.Height - 16),
                1f,
                SpriteEffects.None,
                0f);
            return false;
        }

        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            Variant = 2;
            Main.NewText(noBreak);
            return base.TileFrame(i, j, ref resetFrame, ref noBreak);
        }
    }
}
