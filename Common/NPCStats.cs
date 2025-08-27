using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Common
{
    public class NPCStats : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public bool NoAnimation = false;
        public bool Immobile = false;
        public bool NoFlight = false;
        public bool Silenced = false;

        public float MoveSpeed = 1f;
        public float AttackSpeed = 1f;
        public override void ResetEffects(NPC npc)
        {
            NoAnimation = false;
            Immobile = false;
            NoFlight = false;
            Silenced = false;

            MoveSpeed = 1f;
            AttackSpeed = 1f;
        }

        public override bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot)
        {
            if (target.PlayerStats().ImmuneToContactDamage)
                return false;
            return base.CanHitPlayer(npc, target, ref cooldownSlot);
        }
    }
}
