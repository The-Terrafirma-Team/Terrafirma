using Microsoft.Xna.Framework;
using Terrafirma.Buffs.Debuffs;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Global.NPCs
{
    internal class TerrafirmaGlobalNPCInstance : GlobalNPC
    {
        public bool PhantasmalBurn;
        public bool ElectricCharge;
        public override void ResetEffects(NPC npc)
        {
            PhantasmalBurn = false;
            ElectricCharge = false;
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
        public override bool InstancePerEntity => true;
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (npc.HasBuff(ModContent.BuffType<Inked>())) drawColor = new Color(179, 130, 237);
            base.DrawEffects(npc, ref drawColor);
        }
    }
}
