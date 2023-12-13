using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Buffs
{
    internal class PhantasmalBurn : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.pvpBuff[Type] = true; 
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.lifeRegen = -40;
            NPC.HitInfo hit = new NPC.HitInfo();
            hit.Damage = 20;
            hit.Knockback = 0;
            hit.HitDirection = 0;


            if (npc.buffTime[buffIndex] % 2 == 0)
            {
                Dust.NewDust(npc.position, 0, 0, DustID.BlueFlare, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-6f, 5f), 0, default, Main.rand.NextFloat(2.5f, 2.7f));
            }
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen = -20;
            Player.HurtInfo hit = new Player.HurtInfo();
            hit.Damage = 20;
            hit.Knockback = 0;
            hit.HitDirection = 0;

            if (player.buffTime[buffIndex] % 2 == 0)
            {
                Dust.NewDust(player.position, 0, 0, DustID.BlueFlare, Main.rand.NextFloat(-0.6f, 0.6f), Main.rand.NextFloat(-3f, 4f), 0, default, Main.rand.NextFloat(2f, 2.3f)); ;
            }
        }
    }
}
