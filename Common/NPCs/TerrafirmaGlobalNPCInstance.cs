using Microsoft.Xna.Framework;
using Terrafirma.Buffs.Debuffs;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Common.NPCs
{
    internal class TerrafirmaGlobalNPCInstance : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public bool PhantasmalBurn;
        public bool ElectricCharge;
        public bool Stunned;
        public bool Inked;
        public override void ResetEffects(NPC npc)
        {
            PhantasmalBurn = false;
            ElectricCharge = false;
            Stunned = false;
            Inked = false;
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (PhantasmalBurn)
            {
                damage += 15;
                npc.lifeRegen -= 60;
            }
            if (ElectricCharge)
            {
                damage = 5;
                npc.lifeRegen -= (int)(npc.velocity.Length() * 4f) - 1;
            }
        }
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (Inked) drawColor = new Color(179, 130, 237);
        }
    }
}
