using Microsoft.Xna.Framework;
using Terrafirma.Utilities;
using Terraria;

namespace Terrafirma.Common.AIStyles
{
    public static class SlimeAI
    {
        public static void FindFrame(NPC npc, int frameHeight, ref float frameCounter, ref int frame, int maxFrames = 3, int jumpUpFrame = 5, int jumpDownFrame = 4)
        {
            npc.frame.Y = frame * frameHeight;
            if (npc.NPCStats().NoAnimation)
                return;
            if (npc.ai[2] == 0)
            {
                frameCounter += npc.NPCStats().MoveSpeed;
                if (frameCounter > 8)
                {
                    frameCounter = 0;
                    frame++;
                }

                if (frame > maxFrames)
                    frame = 0;
                if (npc.velocity.Y < 0)
                    frame = jumpUpFrame;
                else if (npc.velocity.Y > 0)
                    frame = jumpDownFrame;
            }
        }
        public static bool CanAttack(NPC npc)
        {
            return npc.velocity.Y != 0 && npc.ai[3] == 1 && !npc.NPCStats().Immobile;
        }
        public static void AI(NPC npc, NPCStats stats, ref float frameCounter)
        {
            npc.rotation = npc.velocity.X * 0.1f * npc.velocity.Y * -0.1f;

            if (npc.direction == 0)
            {
                npc.direction = Main.rand.NextBool() ? 1 : -1;
            }
            if (npc.ai[0] < -500)
            {
                npc.ai[0] = Main.rand.Next(20);
                npc.netUpdate = true;
            }
            if (npc.wet)
            {

                npc.velocity.X += npc.direction * 0.1f * stats.MoveSpeed;
                if (npc.velocity.Y == 0)
                {
                    npc.velocity.Y = -2f;
                }
                if (npc.velocity.Y > 2f)
                {
                    npc.velocity.Y *= 0.9f;
                }
                npc.velocity.Y -= 0.5f;
                if (npc.velocity.Y < -4f)
                {
                    npc.velocity.Y = -4f;
                }
                npc.ai[3] = 1;
            }
            if (stats.Immobile)
            {
                npc.ai[3] = 0;
                npc.ai[2] = 0;
                npc.ai[0] = 0;
                frameCounter += 2;
                if (npc.velocity.Y == 0)
                {
                    npc.velocity.X *= 0.8f;
                }
                return;
            }

            if (npc.velocity.Y == 0)
            {
                npc.velocity.X *= 0.8f;
                npc.ai[0] += stats.AttackSpeed;
                npc.ai[3] = 0;
            }
            else if (npc.ai[3] == 1)
            {
                if (npc.direction == 1 && npc.velocity.X < 0.1f || npc.direction == -1 && npc.velocity.X > -0.1f && CanAttack(npc))
                    npc.velocity.X += npc.direction * 0.6f * stats.MoveSpeed;
            }
            if (npc.ai[0] > 50)
                frameCounter += stats.MoveSpeed;
            if (npc.ai[0] > 75)
                frameCounter += stats.MoveSpeed;

            if (!npc.HasValidTarget)
            {
                if (npc.life < npc.lifeMax || !Main.dayTime || npc.position.Y > Main.worldSurface * 16)
                {
                    npc.TargetClosest(false);
                }

                npc.ai[2] = 0;
                if (npc.ai[0] > 100)
                {
                    if (Main.rand.NextBool(3))
                    {
                        npc.direction = Main.rand.NextBool() ? -1 : 1;
                    }
                    npc.ai[3] = 1;
                    npc.ai[0] = Main.rand.Next(-20, 0);
                    npc.velocity.X += npc.direction * Main.rand.NextFloat(2, 4) * stats.MoveSpeed;
                    npc.velocity.Y += Main.rand.NextFloat(-8, -5);
                    npc.netUpdate = true;
                }
            }
            else
            {
                Player p = Main.player[npc.target];

                if (npc.ai[0] > 100)
                {
                    npc.direction = p.Center.X < npc.Center.X ? -1 : 1;
                    if (npc.confused)
                        npc.direction *= -1;
                    npc.ai[3] = 1;
                    npc.ai[0] = Main.rand.Next(40);
                    npc.velocity.X += MathHelper.Clamp((p.Center.X - npc.Center.X) * 0.04f, -4, 4) * (npc.confused ? -1 : 1) * stats.MoveSpeed;
                    npc.velocity.Y += MathHelper.Clamp((p.Center.Y - npc.Center.Y) * 0.06f, -12, Main.rand.NextFloat(-8, -5));
                    npc.netUpdate = true;
                }
            }
        }
    }
}
