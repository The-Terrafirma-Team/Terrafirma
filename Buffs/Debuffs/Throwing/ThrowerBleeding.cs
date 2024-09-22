using Microsoft.Xna.Framework;
using Terrafirma.Common.NPCs;
using Terrafirma.Particles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Buffs.Debuffs.Throwing
{
    internal class ThrowerBleeding : ModBuff
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
                Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Blood);
                d.noGravity = Main.rand.NextBool(3);
                d.velocity.X *= 0.2f;
            }

            npc.GetGlobalNPC<TerrafirmaGlobalNPCInstance>().ThrowerDOT += (npc.buffTime[buffIndex] / 3f);
        }
    }
}
