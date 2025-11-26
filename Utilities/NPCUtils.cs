using Terrafirma.Common;
using Terraria;

namespace Terrafirma.Utilities
{
    public static class NPCUtils
    {
        public static NPCStats NPCStats(this NPC npc)
        {
            return npc.GetGlobalNPC<NPCStats>();
        }
    }
}
