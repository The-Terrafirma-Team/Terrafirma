using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terrafirma.Common;

namespace Terrafirma.Buffs.Buffs
{
    public class Malice : ModBuff
    { 
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MalicePlayer>().active = true;
        }
    }
    public class MalicePlayer : ModPlayer
    {
        public bool active;
        public override bool? CanHitNPCWithItem(Item item, NPC target)
        {
            if (target.friendly && active)
            {
                return true;
            }
            return base.CanHitNPCWithItem(item, target);
        }
        public override bool? CanHitNPCWithProj(Projectile proj, NPC target)
        {
            if (target.friendly && active)
            {
                return true;
            }
            return base.CanHitNPCWithProj(proj, target);
        }
        public override bool CanHitNPC(NPC target)
        {
            if (target.friendly)
            {
                return true;
            }
            return base.CanHitNPC(target);
        }
    }
}

