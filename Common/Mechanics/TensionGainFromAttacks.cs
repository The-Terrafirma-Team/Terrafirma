using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Common.Mechanics
{
    public class TensionGainFromAttacks : ModPlayer
    {
        public float AccumulatedTension = 0;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(target.friendly || target.lifeMax <= 5)
                return;
            int maxDistance = 25;

            AccumulatedTension += Math.Max(((16 * maxDistance) - Player.Center.Distance(target.Hitbox.ClosestPointInRect(Player.Center))) / 50 * damageDone, 0);
            while (AccumulatedTension > 75)
            {
                AccumulatedTension -= 75;
                Player.GiveTension(1, false);
            }
        }
    }
}
