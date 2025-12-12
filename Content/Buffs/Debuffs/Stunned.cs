using Terrafirma.Common;
using Terrafirma.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Content.Buffs.Debuffs
{
    public class Stunned : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            DataSets.RegisterStunDebuff(Type);
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            NPCStats stats = npc.NPCStats();
            stats.NoFlight = true;
            stats.Immobile = true;
            stats.StunStarEffects = true;
        }
    }
}
