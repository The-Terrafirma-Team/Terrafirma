using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Rarities
{
    internal class FinalQuestRarity : ModRarity
    {
        //Color.Lerp(Main.DiscoColor, Color.Lerp(Color.LightBlue, Color.White, (float) Math.Sin(Main.timeForVisualEffects / 10f)), 0.5f)
        //Color.Lerp(Main.DiscoColor, Color.White, (float) Math.Sin(Main.timeForVisualEffects / 30f) + 0.5f);
        //Color.Lerp(Main.DiscoColor, Color.White, Math.Clamp((float) Math.Sin(Main.timeForVisualEffects / 40f) + 0.5f, 0.2f, 0.7f))
        public override Color RarityColor => Color.Lerp(Main.DiscoColor, Color.White, 0.5f);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            return Type; // no 'lower' tier to go to, so return the type of this rarity.
        }
    }
}
