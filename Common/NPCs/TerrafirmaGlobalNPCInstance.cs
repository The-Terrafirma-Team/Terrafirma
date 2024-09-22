using Microsoft.Xna.Framework;
using Terrafirma.Buffs.Debuffs;
using Terraria;
using Terraria.ID;
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
        public bool Chilled;
        public float ThrowerDOT;
        public override void ResetEffects(NPC npc)
        {
            ThrowerDOT = 0;
            PhantasmalBurn = false;
            ElectricCharge = false;
            Stunned = false;
            Inked = false;
            Chilled = false;
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
                damage += 5;
                npc.lifeRegen -= (int)(npc.velocity.Length() * 4f) - 1;
            }
            npc.lifeRegen -= (int)ThrowerDOT;
            damage += (int)(ThrowerDOT / 5f);
        }
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (Inked) drawColor = new Color(179, 130, 237);
            if (Chilled)
            {
                drawColor = new Color(128, 200, 255);
                if (Main.rand.NextBool(3))
                {
                    Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Snow);
                    d.noGravity = true;
                    d.velocity *= 0.2f;
                    d.scale = 0.6f;
                }
            }
        }
    }
}
