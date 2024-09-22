using Microsoft.Xna.Framework;
using Terrafirma.Common.NPCs;
using Terrafirma.Particles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Buffs.Debuffs.Throwing
{
    internal class Corrupting : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.pvpBuff[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (Main.rand.NextBool(3))
            {
                Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Corruption);
                d.noGravity = !Main.rand.NextBool(3);
            }

            npc.GetGlobalNPC<TerrafirmaGlobalNPCInstance>().ThrowerDOT += (npc.buffTime[buffIndex] / 2f);
        }
    }
}
