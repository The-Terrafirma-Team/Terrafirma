using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace Terrafirma.Rarities
{
    internal class LuckyRarity : ModRarity
    {
        public override Color RarityColor => new Color(255, 217, 10);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            return Type; // no 'lower' tier to go to, so return the type of this rarity.
        }
    }
}
