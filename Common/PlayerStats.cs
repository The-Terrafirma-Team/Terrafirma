using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Common
{
    public class PlayerStats : ModPlayer
    {
        public bool ItemUseBlocked = false;

        public int ParryDamage = 0;
        public float ParryPower = 1f;
        // Tension
        public int Tension = 50;
        public int TensionMax = 50;
        public int TensionMax2 = 0;
        public float TensionGainMultiplier = 1f;
        public float TensionCostMultiplier = 1f;
        public int FlatTensionGain = 0;
        public int FlatTensionCost = 0;
        public override void ResetEffects()
        {
            ItemUseBlocked = false;

            ParryDamage = 8;
            ParryPower = 1f;

            TensionMax2 = TensionMax;
            Tension = Math.Clamp(Tension, 0, TensionMax2);
            TensionGainMultiplier = 1f;
            TensionCostMultiplier = 1f;
            FlatTensionGain = 0;
            FlatTensionCost = 0;
        }
        public override bool CanUseItem(Item item)
        {
            if (ItemUseBlocked) return false;
            return base.CanUseItem(item);
        }
    }
}
