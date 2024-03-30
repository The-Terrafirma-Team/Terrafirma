using Microsoft.Xna.Framework;
using Terrafirma.Global.NPCs;
using Terrafirma.Particles.LegacyParticles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Buffs.Debuffs
{
    internal class AgnomalumBurns : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<TerrafirmaGlobalNPCInstance>().AgnomalumBurns = true;

            LegacyParticleSystem.AddParticle(new HiResFlame(), Main.rand.NextVector2FromRectangle(npc.Hitbox), Vector2.Zero, TFUtils.getAgnomalumFlameColor(),2);
            if (Main.rand.NextBool(5))
            {
                LegacyParticleSystem.AddParticle(new ColorDot(), Main.rand.NextVector2FromRectangle(npc.Hitbox), Vector2.Zero + Main.rand.NextVector2Circular(4, 5), TFUtils.getAgnomalumFlameColor(), 0.2f);
            }
        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.lifeRegen > 0)
            {
                player.lifeRegen = 0;
            }
            player.lifeRegen -= 60;

            LegacyParticleSystem.AddParticle(new HiResFlame(), Main.rand.NextVector2FromRectangle(player.Hitbox), Vector2.Zero, TFUtils.getAgnomalumFlameColor(),2);
            if(Main.rand.NextBool(5)) 
            {
                LegacyParticleSystem.AddParticle(new ColorDot(), Main.rand.NextVector2FromRectangle(player.Hitbox), Vector2.Zero + Main.rand.NextVector2Circular(4,5), TFUtils.getAgnomalumFlameColor(),0.2f);
            }
        }
    }
}
