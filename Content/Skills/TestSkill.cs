using Terrafirma.Common.Mechanics;
using Terraria;
using Terraria.ID;

namespace Terrafirma.Content.Skills
{
    internal class TestSkill : Skill
    {
        public override int ManaCost => 15;
        public override int CastTimeMax => 60;

        public override void Casting(Player player)
        {
            for (int i = 0; i < 3; i++)
            {
                Dust d = Dust.NewDustPerfect(player.Center, DustID.GemEmerald, Main.rand.NextVector2Circular(6, 6));
                d.noGravity = true;
            }
        }
        public override void Use(Player player)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDustPerfect(player.Center, DustID.GemSapphire, Main.rand.NextVector2Circular(6, 6));
            }
        }
        public override void Update(Player player, bool OnCooldown)
        {
        }
    }
    internal class TestSkill2 : Skill
    {
        public override int TensionCost => 5;

        public override void Use(Player player)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDustPerfect(player.Center, DustID.GemEmerald, Main.rand.NextVector2Circular(6, 6));
            }
        }
    }
    internal class TestSkill3 : Skill
    {
        public override int TensionCost => 5;
        public override int ManaCost => 5;
        public override void Use(Player player)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDustPerfect(player.Center, DustID.GemAmber, Main.rand.NextVector2Circular(6, 6));
            }
        }
        public override int CooldownMax => 30;
        public override void Update(Player player, bool OnCooldown)
        {
            if (!OnCooldown)
                return;
            Main.NewText(Cooldown);
            Dust.NewDustPerfect(player.Center, DustID.GemAmber, Main.rand.NextVector2Circular(6, 6));
        }
    }
    internal class TestSkill4 : Skill
    {
        public override void Use(Player player)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDustPerfect(player.Center, DustID.GemAmber, Main.rand.NextVector2Circular(6, 6));
            }
        }
        public override int CooldownMax => 0;
        public override void Update(Player player, bool OnCooldown)
        {
            Dust d = Dust.NewDustPerfect(player.Center, DustID.GemAmethyst, Main.rand.NextVector2Circular(1, 1));
            d.noGravity = true;
        }
    }
}
