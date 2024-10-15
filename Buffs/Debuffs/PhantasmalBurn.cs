using Microsoft.Xna.Framework;
using Terrafirma.Common.NPCs;
using Terrafirma.Particles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Buffs.Debuffs
{
    internal class PhantasmalBurn : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.pvpBuff[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCStats>().PhantasmalBurn = true;
            if (npc.buffTime[buffIndex] % 2 == 0)
            {
                Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.BlueFlare, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-6f, -5f), 0, default, Main.rand.NextFloat(1.5f, 1.7f));
                if (!Main.rand.NextBool(3))
                {
                    d.noGravity = true;
                    d.fadeIn = 1.2f;
                    d.scale *= 2;
                }
            }
            //if (Main.rand.NextBool(3))
            //{
            //    ParticleSystem.AddParticle(new ColorDot(), Main.rand.NextVector2FromRectangle(npc.Hitbox), Vector2.Zero + Main.rand.NextVector2Circular(4, 5), Color.Lerp(new Color(0,255,255,0),new Color(0,128,255,0),Main.rand.NextFloat()), 0.2f);
            //}
            //if (Main.rand.NextBool(3))
            //{
            //    ParticleSystem.AddParticle(new HiResFlame(), Main.rand.NextVector2FromRectangle(npc.Hitbox), Vector2.Zero, Color.Lerp(new Color(0, 255, 255, 0), new Color(0, 128, 255, 0), Main.rand.NextFloat()), 3f);
            //}
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
