using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace Terrafirma.Common
{
    public static class DataSets
    {
        public static bool[] NPCWhitelistedForStun = NPCID.Sets.Factory.CreateBoolSet();
        public static int[] StunDebuff = [];
        public static float[] ItemTensionGainMultiplier = ItemID.Sets.Factory.CreateFloatSet(1f);
        public static float[] ProjectileTensionGainMultiplier = ProjectileID.Sets.Factory.CreateFloatSet(1f);
        public static void RegisterStunDebuff(int Type)
        {
            StunDebuff = StunDebuff.Append(Type).ToArray();
        }
    }
}
