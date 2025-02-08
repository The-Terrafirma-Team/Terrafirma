using Microsoft.Xna.Framework;
using Terrafirma.Common.NPCs;
using Terrafirma.Particles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Buffs.Debuffs
{
    internal class HallowedFlames : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.pvpBuff[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            //npc.GetGlobalNPC<NPCStats>().PhantasmalBurn = true;
            for(int i = 0; i < 2; i++)
                ParticleSystem.AddParticle(new ColorDot() { Size = Main.rand.NextFloat(0.2f, 1f), TimeInWorld = 40, Waviness = Main.rand.NextFloat(0.04f), gravity = -0.1f, secondaryColor = Color.Lerp(new Color(0.3f, 1f, 1f, 0f), new Color(0.8f, 0.3f, 1f, 0f), Main.rand.NextFloat()) }, Main.rand.NextVector2FromRectangle(npc.Hitbox), Main.rand.NextVector2Circular(1, 1), Color.Lerp(new Color(0.3f, 1f, 1f, 0f), new Color(0.8f, 0.3f, 1f, 0f), Main.rand.NextFloat()));

        }

        public override void Update(Player player, ref int buffIndex)
        {
            ParticleSystem.AddParticle(new ColorDot() { Size = Main.rand.NextFloat(0.2f, 1f), TimeInWorld = 40, Waviness = Main.rand.NextFloat(0.04f), gravity = -0.1f, secondaryColor = Color.Lerp(new Color(0.3f, 1f, 1f, 0f), new Color(0.8f, 0.3f, 1f, 0f), Main.rand.NextFloat()) }, Main.rand.NextVector2FromRectangle(player.Hitbox), Main.rand.NextVector2Circular(1, 1), Color.Lerp(new Color(0.3f, 1f, 1f, 0f), new Color(0.8f, 0.3f, 1f, 0f), Main.rand.NextFloat()));

            if (player.lifeRegen > 0)
            {
                player.lifeRegen = 0;
            }
            player.lifeRegen -= 20;
        }
    }
}
