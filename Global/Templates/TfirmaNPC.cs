using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Global.Templates
{
    public abstract class TfirmaNPC : ModNPC
    {
        public Player target
        {
            get { return Main.player[NPC.target]; }
        }
        public void FindClosestOnlyIfCurrentTargetIsInvalid(int maxTilesRange = 100)
        {
            if (target.dead || !NPC.HasValidTarget || target.Center.Distance(NPC.Center) < 16 * maxTilesRange)
            {
                NPC.TargetClosest();
            }
            else if (!NPC.HasValidTarget)
            {
                NPC.active = false;
            }
        }
    }
}
