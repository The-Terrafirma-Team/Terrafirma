using Microsoft.Xna.Framework;
using Terrafirma.Common;
using Terrafirma.Content.Dusts;
using Terrafirma.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Content.Buffs
{
    public class Bloodlust : ModBuff
    {
        public override void Update(NPC npc, ref int buffIndex)
        {
            NPCStats stats = npc.NPCStats();
            stats.AttackSpeed += 2f;
            stats.MoveSpeed += 1f;
            Dust d = Dust.NewDustDirect(npc.position,npc.width,npc.height,DustID.Blood);
            d.velocity = npc.velocity * Main.rand.NextFloat(0.1f,0.3f);

            if (Main.rand.NextBool(5))
            {
                Dust d2 = Dust.NewDustDirect(npc.position, npc.width, npc.height, ModContent.DustType<SimpleColorableGlowyDust>());
                d2.color = new Color(0.7f, 0.1f, 0.1f, 0f);
                d2.noGravity = true;
                d2.noLight = false;
                d2.velocity = npc.velocity * Main.rand.NextFloat(0.4f, 0.1f);
            }
        }
    }
}
