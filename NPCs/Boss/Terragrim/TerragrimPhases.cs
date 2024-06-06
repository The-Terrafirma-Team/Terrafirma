using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Particles;
using Terrafirma.Projectiles.Hostile;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace Terrafirma.NPCs.Boss.Terragrim
{
    public partial class Terragrim
    {
        private void Phase0_Intro()
        {
            if (NPC.ai[0] == 0)
            {
                NPC.ai[0] = 30;
                NPC.velocity.Y = -10;
                NPC.rotation = MathHelper.PiOver4 * 3;
            }
            NPC.ai[0]++;
            NPC.velocity.Y *= 0.9f;
            if (NPC.ai[0] is >= 60 and <= 60*2)
            {
                NPC.rotation = MathHelper.SmoothStep(MathHelper.PiOver4 * 3, MathHelper.PiOver4 * 3 + (MathHelper.TwoPi * 3), (NPC.ai[0] - 60) / 60);
            }
            else if (NPC.ai[0] == 121)
            {
                NPC.TargetClosest();
                SoundEngine.PlaySound(SoundID.Roar,NPC.position);
                phase = 1;
                NPC.ai[0] = 0;
                NPC.ai[1] = -200;
                NPC.netUpdate = true;

                for(int i = 0; i < 24; i++)
                {
                    Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Terragrim);
                    d.velocity = new Vector2(3).RotatedBy(i * MathHelper.TwoPi / 24);
                    d.scale = 2f;
                    d.noGravity = true;
                }
            }
        }
        private void Phase1_Dash()
        {
            NPC.ai[0]++;
            NPC.ai[1]++;

            if (NPC.ai[1] > 30 && NPC.ai[1] < 50)
            {
                Dust d = Dust.NewDustPerfect(NPC.Center, DustID.GemDiamond);
                d.velocity *= 0.5f;
                d.scale = 1f;
                d.noGravity = true;
            }

            if (NPC.ai[1] < 0)
            {
                Vector2 targetPos = target.Center + new Vector2(0, -100);
                NPC.rotation = Utils.AngleLerp(NPC.rotation, NPC.Center.DirectionTo(target.Center).ToRotation() + MathHelper.PiOver4, 0.1f);
                NPC.velocity += NPC.Center.DirectionTo(targetPos) * 0.5f;
                NPC.velocity = NPC.velocity.LengthClamp(6);
            }
            else if (NPC.ai[1] is > 0 and < 20)
            {
                NPC.velocity *= 0.85f;
            }
            else if (NPC.ai[1] == 20)
            {
                NPC.TargetClosest();
                SoundEngine.PlaySound(SoundID.Item4, NPC.position);
                ParticleSystem.AddParticle(new BigSparkle() {Scale = 1f, fadeInTime = 10,  },NPC.Center, null, new Color(128,255,170,0));
                NPC.rotation = NPC.Center.DirectionTo(target.Center).ToRotation() + MathHelper.PiOver4;
            }
            else if (NPC.ai[1] == 30)
            {
                SoundEngine.PlaySound(SoundID.Item9, NPC.position);
                NPC.velocity = NPC.Center.DirectionTo(target.Center) * (16 + (NPC.ai[2] * (!Main.expertMode? 0.25f : 0.5f)));
                NPC.rotation = NPC.Center.DirectionTo(target.Center).ToRotation() + MathHelper.PiOver4;
                canHitPlayer = true;
                NPC.ai[2]++;
            }
            else if (NPC.ai[1] == 50)
            {
                canHitPlayer = false;
                if (NPC.ai[2] == 8)
                {
                    NPC.ai[0] = 0.3f;
                    NPC.ai[1] = -200;
                    NPC.ai[2] = 0;
                    phase++;
                    NPC.netUpdate = true;
                    return;
                }
                if (NPC.ai[2] == 3)
                {
                    NPC.ai[1] = -80;
                }
                else
                {
                    NPC.ai[1] = -10;
                }
            }
        }
        private void Phase2_SpinFireball()
        {
            NPC.ai[1]++;
            if (NPC.ai[1] < 0)
            {
                Vector2 targetPos = target.Center + new Vector2(0, -200);
                NPC.rotation = Utils.AngleLerp(NPC.rotation, NPC.Center.DirectionTo(target.Center).ToRotation() + MathHelper.PiOver4, 0.1f);
                NPC.velocity += NPC.Center.DirectionTo(targetPos) * 0.5f;
                NPC.velocity = NPC.velocity.LengthClamp(6);
            }
            else if (NPC.ai[1] is > 0 and < 30)
            {
                NPC.velocity *= 0.85f;
            }
            if (NPC.ai[1] is > 30)
            {
                spinnyMode = true;
                NPC.knockBackResist = 0.2f;
                canHitPlayer = true;
                NPC.velocity += NPC.Center.DirectionTo(target.Center) * 0.6f;
                NPC.velocity = NPC.velocity.LengthClamp(Main.expertMode ? 8 : 5);

                NPC.ai[0] += 0.0005f;
                NPC.rotation += NPC.ai[0];
                if (NPC.ai[1] % 10 == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item7, NPC.position);
                }
                if (NPC.ai[1] % (60*3) == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item9, NPC.position);
                    if(Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(6, -6).RotatedBy(NPC.rotation), ModContent.ProjectileType<TerragrimBouncyFlame>(), 10, 0);
                }
            }
            if (NPC.ai[1] > 60 * 10)
            {
                NPC.TargetClosest();
                spinnyMode = false;
                NPC.knockBackResist = 0;
                canHitPlayer = false;
                phase = 1;
                NPC.ai[0] = 0;
                NPC.ai[2] = 0;
                NPC.ai[1] = -200;
                NPC.netUpdate = true;
            }
        }
        private void Phase3_PhaseTwoIntro()
        {
            NPC.ai[0]++;
            NPC.velocity *= 0.9f;

            if (NPC.ai[0] > 60)
            {
                NPC.rotation = MathHelper.SmoothStep(MathHelper.PiOver4 * 3, MathHelper.PiOver4 * 3 + (MathHelper.TwoPi * 3), (NPC.ai[0] - 60) / 60);
            }
            else if(NPC.ai[0] < 30)
            {
                NPC.Center = Vector2.Lerp(NPC.Center,target.Center + new Vector2(0, -200), (NPC.ai[0] + 60) / 90);
                NPC.rotation = Utils.AngleLerp(MathHelper.PiOver4 * 3,NPC.rotation,0.9f);
            }

            if (NPC.ai[0] is > 120 and < 150)
            {
                NPC.velocity.Y -= 0.5f;
            }
            else if (NPC.ai[0] > 150)
            {
                NPC.velocity.Y += 3f;
                if(NPC.velocity.Y > 16)
                {
                    NPC.velocity.Y = 16;
                }
                if ((!Collision.SolidCollision(NPC.position + NPC.velocity, 8, 16) && NPC.noTileCollide && NPC.velocity.Y > 8) || NPC.ai[2] > 0)
                {
                    NPC.noTileCollide = false;
                    NPC.behindTiles = true;
                    if (NPC.ai[2] < 1)
                        NPC.ai[2] = 1;
                    if (NPC.collideY || NPC.velocity.Y == 0)
                    {
                        if (NPC.ai[2] == 1)
                        {
                            SoundEngine.PlaySound(SoundID.DD2_MonkStaffGroundImpact);
                            NPC.ai[2] = 2;
                            for (int i = 0; i < 23; i++)
                            {
                                Dust d2 = Dust.NewDustPerfect(NPC.Bottom, DustID.Stone, Main.rand.NextVector2Circular(6, 6) + new Vector2(0, -6));
                                d2.scale *= 2;
                            }
                        }

                        if (NPC.ai[1] % 10 == 0)
                        {
                            SoundEngine.PlaySound(SoundID.Item24, NPC.position);
                        }

                        NPC.velocity.Y = 0;

                        NPC.ai[1]++;

                        Vector2 rand = Main.rand.NextVector2CircularEdge(1,1);
                        if (rand.Y > 0)
                            rand.Y *= -1;

                        Dust d = Dust.NewDustPerfect(NPC.Bottom + rand * 50, DustID.Terragrim, -rand * 5);
                        d.noGravity = true;
                        d.scale *= 1.3f;

                        if (NPC.ai[1] > 60)
                        {
                            NPC.Size = new Vector2(32);
                            NPC.position += new Vector2(-8,-32);
                            NPC.behindTiles = false;
                            NPC.noTileCollide = true;
                            NPC.ai[0] = -100;
                            NPC.ai[1] = 0;
                            NPC.ai[2] = 0;
                            NPC.velocity.Y = -12;
                            SoundEngine.PlaySound(SoundID.Item69);
                            phase = 4;
                        }
                    }
                }
            }
        }
        private void Phase4_Dash()
        {
            NPC.ai[0]++;
            NPC.ai[1]++;

            if (NPC.ai[1] > 30 && NPC.ai[1] < 50)
            {
                Dust d = Dust.NewDustPerfect(NPC.Center, DustID.GemDiamond);
                d.velocity *= 0.5f;
                d.scale = 1f;
                d.noGravity = true;
            }

            if (NPC.ai[1] < 0)
            {
                Vector2 targetPos = target.Center + new Vector2(0, -200);
                NPC.rotation = Utils.AngleLerp(NPC.rotation, NPC.Center.DirectionTo(target.Center).ToRotation() + MathHelper.PiOver4, 0.1f);
                NPC.velocity += NPC.Center.DirectionTo(targetPos) * 0.5f;
                NPC.velocity = NPC.velocity.LengthClamp(6);
            }
            else if (NPC.ai[1] > 30)
            {
                NPC.velocity += NPC.Center.DirectionTo(target.Center) * (Main.expertMode ? 1.5f : 1);
                NPC.rotation = NPC.velocity.ToRotation() + MathHelper.PiOver4;
                NPC.velocity = NPC.velocity.LengthClamp(22);
            }

            if (NPC.ai[1] is > 0 and < 20)
            {
                NPC.velocity *= 0.85f;
            }
            else if (NPC.ai[1] == 20)
            {
                NPC.TargetClosest();
                SoundEngine.PlaySound(SoundID.Item4, NPC.position);
                NPC.rotation = NPC.Center.DirectionTo(target.Center).ToRotation() + MathHelper.PiOver4;
                ParticleSystem.AddParticle(new BigSparkle() { Scale = 1.2f, fadeInTime = 10, }, NPC.Center, null, new Color(128, 255, 170, 0));
            }
            else if (NPC.ai[1] == 30)
            {
                SoundEngine.PlaySound(SoundID.Item9, NPC.position);
                NPC.velocity = NPC.Center.DirectionTo(target.Center) * (10 + (NPC.ai[2] * (!Main.expertMode ? 0.5f : 0.75f)));
                canHitPlayer = true;
                NPC.ai[2]++;
            }
            else if (NPC.ai[1] == 45)
            {
                canHitPlayer = false;
                if (NPC.ai[2] == 6)
                {
                    NPC.ai[0] = 0.3f;
                    NPC.ai[1] = -200;
                    NPC.ai[2] = 0;
                    phase++;
                    NPC.netUpdate = true;
                    NPC.TargetClosest();
                    return;
                }
                if (NPC.ai[2] == 3)
                {
                    NPC.ai[1] = Main.expertMode ? -60 : -80;
                }
                else
                {
                    NPC.ai[1] = Main.expertMode ? 0 : 10;
                    if (NPC.ai[2] > 3)
                    {
                        NPC.ai[1] += 5;
                    }
                }
            }
        }
        private void Phase5_BladeRing()
        {
            NPC.ai[1]++;
            if (NPC.ai[1] == -30)
            {
                NPC.TargetClosest();
                SoundEngine.PlaySound(SoundID.Item4, NPC.position);
                ParticleSystem.AddParticle(new BigSparkle() { Scale = 1f, fadeInTime = 10, }, NPC.Center, null, new Color(128, 255, 170, 0));
            }
            if (NPC.ai[1] < -30)
            {
                canHitPlayer = false;
                Vector2 targetPos = target.Center + new Vector2(0, -100);
                NPC.rotation = Utils.AngleLerp(NPC.rotation, NPC.Center.DirectionTo(target.Center).ToRotation() + MathHelper.PiOver4, 0.1f);
                NPC.velocity += NPC.Center.DirectionTo(targetPos) * 0.8f;
                NPC.velocity = NPC.velocity.LengthClamp(7);
            }
            else if (NPC.ai[1] < 0)
            {
                NPC.velocity = Vector2.Zero;
            }
            else
            {
                //NPC.Center = target.Center;
                NPC.velocity = (NPC.rotation + MathHelper.PiOver4).ToRotationVector2() * -12;
                NPC.rotation += 0.05f;
                canHitPlayer = true;
                if (NPC.ai[1] % Math.Round(MathHelper.TwoPi / 0.05f / 16) == 0)
                {
                    if(Main.netMode != NetmodeID.MultiplayerClient && !Main.rand.NextBool(5))
                    Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center, (NPC.rotation - MathHelper.PiOver4).ToRotationVector2() * 0.1f, ModContent.ProjectileType<TerragrimSpiritBlade>(), 12, 0, -1, NPC.ai[1],Main.rand.NextFloat(-1,1));
                }

                if (NPC.ai[1] > (MathHelper.TwoPi / 0.05f))
                {
                    NPC.ai[2]++;
                    NPC.ai[1] = -100;
                }

                if (NPC.ai[2] == 3)
                {
                    canHitPlayer = false;
                    NPC.ai[0] = 0.3f;
                    NPC.ai[1] = -200;
                    NPC.ai[2] = 0;
                    phase++;
                    NPC.netUpdate = true;
                }
            }
        }
        private void Phase6_Spin()
        {
            NPC.ai[1]++;
            if (NPC.ai[1] < 0)
            {
                Vector2 targetPos = target.Center + new Vector2(0, -200);
                NPC.rotation = Utils.AngleLerp(NPC.rotation, NPC.Center.DirectionTo(target.Center).ToRotation() + MathHelper.PiOver4, 0.1f);
                NPC.velocity += NPC.Center.DirectionTo(targetPos) * 0.5f;
                NPC.velocity = NPC.velocity.LengthClamp(7);
            }
            else if (NPC.ai[1] is > 0 and < 30)
            {
                NPC.velocity *= 0.85f;
            }
            if (NPC.ai[1] is > 30)
            {
                spinnyMode = true;
                NPC.knockBackResist = 0.2f;
                canHitPlayer = true;
                NPC.velocity += NPC.Center.DirectionTo(target.Center) * 0.6f;
                NPC.velocity = NPC.velocity.LengthClamp(Main.expertMode ? 9 : 7);

                NPC.ai[0] += 0.0005f;
                NPC.rotation += NPC.ai[0];
                if (NPC.ai[1] % 10 == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item7, NPC.position);
                }
                if (NPC.ai[1] % (60 * 2) == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item9, NPC.position);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(6, -6).RotatedBy(NPC.rotation), ModContent.ProjectileType<TerragrimBouncyFlame>(), 10, 0);
                }
            }
            if (NPC.ai[1] > 60 * 10)
            {
                NPC.TargetClosest();
                spinnyMode = false;
                NPC.knockBackResist = 0;
                canHitPlayer = false;
                phase = 4;
                NPC.ai[0] = 0;
                NPC.ai[2] = 0;
                NPC.ai[1] = -200;
                NPC.netUpdate = true;
            }
        }
    }
}
