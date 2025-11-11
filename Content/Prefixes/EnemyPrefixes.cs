using Terrafirma.Common;
using Terrafirma.Common.Mechanics;
using Terrafirma.Utilities;
using Terraria;

namespace Terrafirma.Content.Prefixes
{
    public class Bulky : EnemyPrefix
    {
        public override void Apply(NPC npc)
        {
            npc.scale *= 1.15f;
            npc.Size *= 1.15f;
            npc.lifeMax = (int)(npc.lifeMax * 1.25f);
            npc.life = npc.lifeMax;
            npc.defense += 5;
            npc.defDefense = npc.defense;
            npc.value = (int)npc.value * 1.2f;
        }
        public override void Update(NPC npc)
        {
            npc.NPCStats().MoveSpeed *= 0.95f;
        }
    }
    public class Scrawny : EnemyPrefix
    {
        public override void Apply(NPC npc)
        {
            npc.scale *= 0.9f;
            npc.Size *= 0.9f;
            npc.lifeMax = (int)(npc.lifeMax * 0.9f);
            npc.life = npc.lifeMax;
            npc.knockBackResist *= 1.15f;
            npc.value = (int)npc.value * 0.9f;
        }
    }
    public class Tiny : EnemyPrefix
    {
        public override void Apply(NPC npc)
        {
            npc.scale *= 0.75f;
            npc.Size *= 0.75f;
            npc.lifeMax = (int)(npc.lifeMax * 0.9f);
            npc.life = npc.lifeMax;
            npc.knockBackResist *= 1.3f;
            npc.value = (int)npc.value * 0.85f;
        }
        public override int Weight => 30;
        public override void Update(NPC npc)
        {
            NPCStats stats = npc.NPCStats();
            stats.MoveSpeed *= 1.25f;
            npc.GravityMultiplier *= 1.15f;
        }
    }
    public class Speedy : EnemyPrefix
    {
        public override void Apply(NPC npc)
        {
            npc.value = (int)npc.value * 1.1f;
        }
        public override void Update(NPC npc)
        {
            NPCStats stats = npc.NPCStats();
            stats.MoveSpeed *= 1.15f;
            stats.AttackSpeed *= 1.15f;
        }
    }
    public class Jacked : EnemyPrefix
    {
        public override void Apply(NPC npc)
        {
            npc.scale *= 1.25f;
            npc.Size *= 1.25f;
            npc.lifeMax = (int)(npc.lifeMax * 1.35f);
            npc.life = npc.lifeMax;
            npc.value = (int)npc.value * 1.3f;
        }
        public override int Weight => 50;
        public override void Update(NPC npc)
        {
            NPCStats stats = npc.NPCStats();
            stats.MoveSpeed *= 0.95f;
            stats.AttackSpeed *= 1.15f;
            stats.AttackDamage *= 1.15f;
        }
    }
    public class Weak : EnemyPrefix
    {
        public override void Apply(NPC npc)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.85);
            npc.life = npc.lifeMax;
            npc.defense += -3;
            npc.defDefense = npc.defense;
            npc.value = (int)npc.value * 0.9f;
        }
        public override void Update(NPC npc)
        {
            NPCStats stats = npc.NPCStats();
            stats.MoveSpeed *= 0.95f;
            stats.AttackSpeed *= 1.15f;
            stats.AttackDamage *= 1.15f;
        }
    }
    public class Angry : EnemyPrefix
    {
        public override void Apply(NPC npc)
        {
            npc.defense += 6;
            npc.defDefense = npc.defense;
            npc.knockBackResist *= 0.5f;
            npc.value = (int)npc.value * 1.3f;
        }
        public override int Weight => 60;
        public override void Update(NPC npc)
        {
            NPCStats stats = npc.NPCStats();
            stats.MoveSpeed *= 1.15f;
            stats.AttackSpeed *= 1.15f;
            stats.AttackDamage *= 1.25f;
        }
    }
    public class Slow : EnemyPrefix
    {
        public override void Apply(NPC npc)
        {
            npc.defense += 4;
            npc.defDefense = npc.defense;
            npc.value = (int)npc.value * 0.8f;
        }
        public override void Update(NPC npc)
        {
            NPCStats stats = npc.NPCStats();
            stats.MoveSpeed *= 0.75f;
            stats.AttackSpeed *= 0.75f;
        }
    }
}
