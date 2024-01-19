using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrafirmaRedux.Data;
using Terraria;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Global
{
    public class PlayerStats : ModPlayer
    {
        public float SentrySpeedMultiplier = 0f;
        public float SwarmSpeedMultiplier = 1f;
        public override void ResetEffects()
        {
            SentrySpeedMultiplier = 0f;
            SwarmSpeedMultiplier = 1f;
        }
        public override float UseSpeedMultiplier(Item item)
        {
            if (ItemSets.isSwarmSummonItem[item.type])
            {
                return SwarmSpeedMultiplier;
            }
            return base.UseSpeedMultiplier(item);
        }
    }
}
