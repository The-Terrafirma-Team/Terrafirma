using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Terrafirma.Global
{
    public class PlayerDrawEffects : ModPlayer
    {
        public bool SineDarken = false;
        public override void ResetEffects()
        {
            SineDarken = false;
        }
        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (SineDarken)
            {
                float sin = (MathF.Sin((float)Main.timeForVisualEffects * 0.1f) * 0.5f - 0.5f) * 0.22f;
                r += sin;
                g += sin;
                b += sin;
            }
        }
    }
}
