using Microsoft.Xna.Framework;
using Terrafirma.Common;
using Terrafirma.Common.Mechanics;
using Terrafirma.Content.Dusts;
using Terrafirma.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Content.Prefixes
{
    public class Elite : EnemyPrefix
    {
        public override void Apply(NPC npc)
        {
            npc.lifeMax = (int)(npc.lifeMax * 3);
            npc.life = npc.lifeMax;
            npc.defense += 12;
            npc.value *= 10;
            npc.defDefense = npc.defense;
            npc.scale *= 1.3f;
            npc.Size *= 1.3f;
        }
        public override int Weight => 10;
        public override void Update(NPC npc)
        {
            NPCStats stats = npc.NPCStats();

            float percent = npc.life / (float)npc.lifeMax;

            stats.MoveSpeed *= (1.3f + (percent * 0.4f));
            stats.AttackSpeed *= 1.25f;
            stats.AttackDamage *= 1.5f;
            Lighting.AddLight(npc.Center, 0.5f, Main.masterColor * 0.25f, 0);

            if (!Main.rand.NextBool(10))
                return;
            Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.height, ModContent.DustType<SimpleColorableGlowyDust>());
            d.position += Main.rand.NextVector2Circular(npc.width / 3, npc.height / 3);
            d.velocity *= 0.3f;
            d.velocity += npc.velocity * 0.5f;
            d.noGravity = true;
            d.color = new Color(1f,Main.masterColor * 0.75f,0f,0f);
            d.noLight = true;
        }
    }
}
