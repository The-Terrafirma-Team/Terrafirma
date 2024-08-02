using Microsoft.Xna.Framework;
using Terrafirma.Common;
using Terrafirma.Common.NPCs;
using Terrafirma.Particles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Buffs.Debuffs
{
    internal class ChilledForEnemies : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<TerrafirmaGlobalNPCInstance>().Chilled = true;
            npc.position -= npc.velocity * (npc.knockBackResist * 0.5f);
        } 
    }
}
