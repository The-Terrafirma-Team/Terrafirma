using Microsoft.Xna.Framework;
using Terrafirma.Global;
using Terrafirma.Particles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Buffs.Debuffs
{
    internal class Inked : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.position.X -= npc.velocity.X * (npc.knockBackResist * 0.5f);
            if (Main.rand.NextBool(5))Dust.NewDust(npc.Center + Main.rand.NextVector2Circular(npc.width / 2f,npc.height / 2f), 2, 2, DustID.Poop, npc.velocity.X * Main.rand.NextFloat(0.2f, 0.3f), npc.velocity.Y * Main.rand.NextFloat(0.2f, 0.3f), 0, Color.Black, Main.rand.NextFloat(0.8f, 1f));
        } 
    }
}
