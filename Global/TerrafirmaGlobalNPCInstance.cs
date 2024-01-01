using TerrafirmaRedux.Items.Consumable;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Global
{
    internal class TerrafirmaGlobalNPCInstance : GlobalNPC
    {
        public bool PhantasmalBurn;
        public bool AgnomalumBurns;
        public override void ResetEffects(NPC npc)
        {
            PhantasmalBurn = false;
            AgnomalumBurns = false;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (PhantasmalBurn) 
            {
                damage = 15;
                npc.lifeRegen -= 60;
            }

            if (AgnomalumBurns)
            {
                damage = 15;
                npc.lifeRegen -= 60;
            }

        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LuckyBlock>(), 20, 1, 1));
        }
        public override bool InstancePerEntity => true;

    }
}
