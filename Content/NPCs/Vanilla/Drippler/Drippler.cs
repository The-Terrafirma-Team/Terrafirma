using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common;
using Terrafirma.Common.AIStyles;
using Terrafirma.Common.Interfaces;
using Terrafirma.Common.Systems;
using Terrafirma.Content.Buffs;
using Terrafirma.Content.Dusts;
using Terrafirma.Content.Particles;
using Terrafirma.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Content.NPCs.Vanilla.Drippler
{
    public class Drippler : GlobalNPC, ICustomBlockBehavior
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<ServerConfig>().CombatReworkEnabled;
        }
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.Drippler;
        }
        public override void SetStaticDefaults()
        {
            DataSets.NPCWhitelistedForStun[NPCID.Drippler] = true;
        }
        public override void SetDefaults(NPC entity)
        {
            entity.aiStyle = -1;
        }
        public void OnBlocked(Player player, float Power, NPC npc = null)
        {
            int maxEye = Main.rand.Next(2, 5);
            for (int i = 0; i < maxEye; i++)
            {
                NPC n = NPC.NewNPCDirect(npc.GetSource_OnHurt(null), (int)npc.Center.X, (int)npc.Center.Y, 1);
                n.SetDefaults(Main.rand.Next([NPCID.DemonEye, NPCID.DemonEye2, NPCID.CataractEye, NPCID.CataractEye2, NPCID.SleepyEye, NPCID.SleepyEye2, NPCID.DialatedEye, NPCID.DialatedEye2]));
                n.velocity.X = npc.velocity.X * 2f;
                n.velocity.Y = npc.velocity.Y;
                n.velocity += Main.rand.NextVector2Circular(5, 5);
                n.ai[2] = Main.rand.Next(-600, -100);
                if (Main.netMode == NetmodeID.Server && n.whoAmI < 200)
                {
                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n.whoAmI);
                }
            }
            var hit = new NPC.HitInfo() { Damage = 9999, HideCombatText = true};
            npc.StrikeNPC(hit);
            if (Main.netMode != NetmodeID.SinglePlayer)
                NetMessage.SendStrikeNPC(npc, hit);
        }
        public override void HitEffect(NPC npc, NPC.HitInfo hit)
        {
            if (npc.life < 0)
            {
                SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode);
                ParticleSystem.NewParticle(new ScalingCircle(0, 200, 50, new Color(0f, 0f, 0f, 1f)), npc.Center);
                ParticleSystem.NewParticle(new ScalingCircle(0, 200, 45, new Color(1f, 0f, 0f, 0.5f)), npc.Center);
                int type = ModContent.DustType<SimpleColorableGlowyDust>();
                for (int i = 0; i < 35; i++)
                {
                    ParticleSystem.NewParticle(new Smoke(Main.rand.NextVector2Circular(3f, 2), new Color(Main.rand.NextFloat(0.5f, 1f), 0f, 0f) * Main.rand.NextFloat(0.1f, 1.5f), Color.Transparent, Main.rand.NextFloat(0.5f, 1f)), npc.Center);
                    Dust d = Dust.NewDustPerfect(npc.Center, DustID.Blood, Main.rand.NextVector2Circular(6, 6) + new Vector2(0, -3), Main.rand.Next(128));
                    d.fadeIn = Main.rand.NextFloat(2);
                    d.scale = Main.rand.NextFloat(1, 2);

                    Dust d2 = Dust.NewDustPerfect(npc.Center, type, Main.rand.NextVector2Circular(6, 6) + new Vector2(0, -3), newColor: new Color(Main.rand.NextFloat(), 0f, 0f, 0f));
                    d2.noGravity = Main.rand.NextBool();
                }
            }
        }
        public override void OnKill(NPC npc)
        {
            foreach (NPC n in Main.ActiveNPCs)
            {
                if (n.Center.Distance(npc.Center) < 200)
                {
                    n.AddBuff(ModContent.BuffType<Bloodlust>(), 60 * 10);
                }
            }
        }
        public override void FindFrame(NPC npc, int frameHeight)
        {
            npc.rotation = npc.localAI[0];
        }
        public override void AI(NPC npc)
        {

            NPCStats stats = npc.NPCStats();

            if (!stats.NoParticles)
            {
                Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.CrimtaneWeapons);
                d.alpha = Main.rand.Next(200);
                d.velocity = npc.velocity * 0.6f;
                d.velocity += new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(3));
                d.noGravity = true;
            }

            if (stats.NoAnimation)
                npc.frameCounter = 0;

            npc.ai[2] += 0.001f;
            if (npc.ai[2] > 5)
                npc.ai[2] = 5;

            PixieAI.AI(npc, stats, Main.dayTime, 2 + npc.ai[2]);
            if (npc.noGravity)
            {
                npc.localAI[0] = Utils.AngleLerp(npc.localAI[0], Math.Clamp(npc.velocity.X * 0.1f, -1f,1f), 0.06f * stats.MoveSpeed);
            }
            else
            {
                npc.localAI[0] += npc.velocity.X * 0.07f;
                if (!stats.Immobile && npc.collideY)
                {
                    float maxSpeedX = stats.MoveSpeed * (1f + (npc.ai[2] / 2));
                    float acceleration = 0.03f * stats.MoveSpeed;

                    if (npc.direction == 1 && npc.velocity.X < maxSpeedX)
                        npc.velocity.X += acceleration;
                    else if (npc.direction == -1 && npc.velocity.X > -maxSpeedX)
                        npc.velocity.X -= acceleration;
                }
            }
        }
    }
}
