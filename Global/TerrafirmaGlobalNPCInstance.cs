using Microsoft.Xna.Framework;
using Terrafirma.Buffs.Debuffs;
using Terrafirma.Items.Consumable;
using Terrafirma.Items.Weapons.Ranged.Bows;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Global
{
    internal class TerrafirmaGlobalNPCInstance : GlobalNPC
    {
        public bool PhantasmalBurn;
        public bool AgnomalumBurns;
        public bool ElectricCharge;
        public override void ResetEffects(NPC npc)
        {
            PhantasmalBurn = false;
            AgnomalumBurns = false;
            ElectricCharge = false;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (PhantasmalBurn) 
            {
                damage += 15;
                npc.lifeRegen -= 60;
            }

            if (AgnomalumBurns)
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

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LuckyBlock>(), 20, 1, 1));

            if(npc.type == NPCID.PirateCrossbower)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PirateCrossbow>(), 25, 1, 1));
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
