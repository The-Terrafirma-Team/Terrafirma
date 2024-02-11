using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Graphics.Capture;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using SubworldLibrary;
using Terrafirma.Subworlds.Tempire;

namespace Terrafirma.Biomes.Tempire
{
    internal class TempireBiomeBackgroundStyle : ModSurfaceBackgroundStyle
    {
        // Use this to keep far Backgrounds like the mountains.
        public override void ModifyFarFades(float[] fades, float transitionSpeed)
        {
            for (int i = 0; i < fades.Length; i++)
            {
                if (i == Slot)
                {
                    fades[i] += transitionSpeed;
                    if (fades[i] > 1f)
                    {
                        fades[i] = 1f;
                    }
                }
                else
                {
                    fades[i] -= transitionSpeed;
                    if (fades[i] < 0f)
                    {
                        fades[i] = 0f;
                    }
                }
            }
        }

        public override int ChooseFarTexture()
        {
            return ModContent.GetModBackgroundSlot("Terrafirma/Backgrounds/TempireBiomeFarBackground0");
        }
        public override int ChooseMiddleTexture()
        {
            return ModContent.GetModBackgroundSlot("Terrafirma/Backgrounds/TempireBiomeMidBackground0");
        }

        public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
        {
            return ModContent.GetModBackgroundSlot("Terrafirma/Backgrounds/TempireBiomeCloseBackground0");
        }
    }
}

