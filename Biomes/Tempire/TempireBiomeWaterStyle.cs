using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Terrafirma.Biomes.Tempire
{
    internal class TempireBiomeWaterStyle : ModWaterStyle
    {
        public override int ChooseWaterfallStyle()
        {
            return ModContent.GetInstance<TempireBiomeWaterfallStyle>().Slot;
        }

        public override void LightColorMultiplier(ref float r, ref float g, ref float b)
        {
            r = 1f;
            g = 1f;
            b = 1f;
        }

        public override int GetDropletGore()
        {
            return GoreID.WaterDripUnderground;
        }
        public override Color BiomeHairColor()
        {
            return Color.White;
        }

        public override int GetSplashDust()
        {
            return 104;
        }
        public override byte GetRainVariant()
        {
            return (byte)Main.rand.Next(3);
        }
    }
}
