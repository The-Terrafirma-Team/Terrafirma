using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terrafirma.Items.Placeable.Statues;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Terrafirma.Tiles
{
    public class SunstoneCrystals : ModTile
    {
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
        {
            drawData.finalColor.A = 128;
        }
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            DustType = DustID.GemTopaz;
            AddMapEntry(new Color(255, 248, 236));
            AddMapEntry(new Color(255, 197, 29));
        }
        public override ushort GetMapOption(int i, int j)
        {
            return (ushort)((i + j) % 2);
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.3f + ((float)Math.Sin(Main.timeForVisualEffects * 0.01f + j) * 0.1f);
            g = 0.2f + ((float)Math.Sin(Main.timeForVisualEffects * 0.01f + j) * 0.1f);

            r += 0.3f + ((float)Math.Sin(Main.timeForVisualEffects * 0.02f + j) * 0.1f);
            g += 0.2f + ((float)Math.Sin(Main.timeForVisualEffects * 0.02f + j) * 0.1f);

            r += 0.3f + ((float)Math.Sin(Main.timeForVisualEffects * 0.03f + j) * 0.2f);
            g += 0.2f + ((float)Math.Sin(Main.timeForVisualEffects * 0.03f + j) * 0.2f);
            base.ModifyLight(i, j, ref r, ref g, ref b);
        }
        public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
        {
            frameXOffset = i % 2 * 288;
            frameYOffset = j % 2 * 270;
        }
    }
}
