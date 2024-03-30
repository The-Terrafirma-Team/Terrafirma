using Microsoft.Xna.Framework;
using Terrafirma.Global.NPC;
using Terrafirma.Particles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Buffs.Debuffs
{
    internal class ElectricCharge : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.pvpBuff[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<TerrafirmaGlobalNPCInstance>().ElectricCharge = true;
            Dust d = Dust.NewDustPerfect(Main.rand.NextVector2FromRectangle(npc.Hitbox), DustID.Electric, Main.rand.NextVector2CircularEdge(6,6));
            d.noGravity = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.lifeRegen > 0)
            {
                player.lifeRegen = 0;
            }
            player.lifeRegen -= (int)(player.velocity.Length()) - 1;
        }
    }
}
