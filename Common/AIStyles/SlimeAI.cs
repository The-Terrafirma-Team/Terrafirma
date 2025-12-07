using Microsoft.Xna.Framework;
using Terrafirma.Utilities;
using Terraria;

namespace Terrafirma.Common.AIStyles
{
    public static class SlimeAI
    {
        public static bool CanFallThrough(NPC npc)
        {
            return npc.HasValidTarget && npc.ai[3] > 0 && (npc.Bottom.Y + npc.velocity.Y < npc.targetRect.Bottom);
        }
        public static void FindFrame(NPC npc, int frameHeight, ref float frameCounter, ref int frame, int maxFrames = 3, int jumpUpFrame = 5, int jumpDownFrame = 4)
        {
            //npc.frame.Y = frame * frameHeight;
            //if (npc.NPCStats().NoAnimation)
            //{
            //    frameCounter = 0;
            //    return;
            //}
            //if (npc.ai[2] == 0)
            //{
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
            //}
        }
        public static bool CanAttack(NPC npc)
        {
            return npc.velocity.Y != 0 && npc.ai[3] == 1 && !npc.NPCStats().Immobile;
        }
        public static void AI(NPC npc, NPCStats stats, ref float frameCounter, bool canTarget = true, float jumpHeight = -12f, float lowJumpHeight = -6, float maxHorizontalSpeed = 4, float JumpInterval = 100)
        {
            npc.targetRect = npc.GetTargetData().Hitbox;
            npc.rotation = npc.velocity.X * 0.05f * npc.velocity.Y * -0.05f;
            npc.rotation = MathHelper.Clamp(npc.rotation, -0.2f, 0.2f);
            if (npc.direction == 0)
            {
                npc.direction = Main.rand.NextBool() ? 1 : -1;
                npc.netUpdate = true;
            }
            if (npc.ai[0] < -500)
            {
                npc.ai[0] = Main.rand.Next(20);
                npc.netUpdate = true;
            }
            if (npc.wet)
            {
                npc.ai[0] = 0;
                npc.FaceTarget();
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
                npc.ai[0] = 0;
                frameCounter -= npc.NPCStats().MoveSpeed * 0.5f;
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
            else if (npc.ai[3] >= 1 && npc.ai[2] < 1)
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
                if (canTarget)
                {
                    npc.TargetClosest(false);
                }
                if (npc.ai[0] > JumpInterval)
                {
                    if (Main.rand.NextBool(3))
                    {
                        npc.direction = Main.rand.NextBool() ? -1 : 1;
                    }
                    npc.ai[3] = 1;
                    npc.ai[0] = Main.rand.Next(-20, 0);
                    npc.velocity.X += npc.direction * Main.rand.NextFloat(2, maxHorizontalSpeed) * stats.MoveSpeed;
                    npc.velocity.Y += Main.rand.NextFloat(jumpHeight,lowJumpHeight);
                    npc.netUpdate = true;
                }
            }
            else
            {
                if (npc.ai[0] > JumpInterval)
                {
                    npc.FaceTarget();
                    npc.ai[3] = 1;
                    npc.ai[0] = Main.rand.Next(40);
                    npc.velocity.X += MathHelper.Clamp((npc.targetRect.Center.X - npc.Center.X) * 0.04f, -maxHorizontalSpeed * stats.MoveSpeed, maxHorizontalSpeed * stats.MoveSpeed);
                    npc.velocity.Y += MathHelper.Clamp((npc.targetRect.Center.Y - npc.Center.Y) * 0.06f, jumpHeight, lowJumpHeight);
                    npc.netUpdate = true;
                }
            }
        }
    }
}
