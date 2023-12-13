using TerrafirmaRedux.Global;
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


            npc.GetGlobalNPC<TerrafirmaGlobalNPCInstance>().PhantasmalBurn = true;


            if (npc.buffTime[buffIndex] % 2 == 0)
            {
                Dust.NewDust(npc.position, 0, 0, DustID.BlueFlare, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-6f, 5f), 0, default, Main.rand.NextFloat(2.5f, 2.7f));
            }
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.lifeRegen > 0)
            {
                player.lifeRegen = 0;
            }
            player.lifeRegen -= 60;

            if (player.buffTime[buffIndex] % 2 == 0)
            {
                Dust.NewDust(player.position, 0, 0, DustID.BlueFlare, Main.rand.NextFloat(-0.6f, 0.6f), Main.rand.NextFloat(-3f, 4f), 0, default, Main.rand.NextFloat(2f, 2.3f)); ;
            }
        }
    }
}
