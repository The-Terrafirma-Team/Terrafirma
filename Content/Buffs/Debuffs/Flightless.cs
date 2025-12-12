using Terrafirma.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Content.Buffs.Debuffs
{
    public class Flightless : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.NPCStats().NoFlight = true;
        }
    }
}
