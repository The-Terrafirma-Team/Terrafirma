using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Terrafirma.Common.Players
{
    public class PlayerDrawEffects : ModPlayer
    {
        public bool SineDarken = false;
        public bool CrystalAfterimages = false;
        public override void ResetEffects()
        {
            SineDarken = false;
            CrystalAfterimages = false;
        }
        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (CrystalAfterimages && drawInfo.shadow > 0)
            {
                r *= 0.75f;
                g = 0;
                a = 0;
            }
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
