using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Global
{
    internal class TerrafirmaGlobalNPCInstance : GlobalNPC
    {
        public bool PhantasmalBurn;
        public override void ResetEffects(NPC npc)
        {
            PhantasmalBurn = false;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (PhantasmalBurn) 
            {
                damage = 15;
                npc.lifeRegen -= 60;
            }

        }
        public override bool InstancePerEntity => true;

    }
}
