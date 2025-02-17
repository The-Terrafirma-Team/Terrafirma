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

namespace Terrafirma.NPCs.Miniboss
{
    public partial class YinYangSlime
    {
        private void Phase0_Sleepy()
        {
            if (Main.timeForVisualEffects % 24 == 0)
            {
                ParticleSystem.AddParticle(new Sleepy(), NPC.Center + new Vector2(15, -15), new Vector2(1f, -1f) * 0.5f);
            }
            if (NPC.justHit)
            {
                NPC.velocity.Y = -4;
                phase = 1;
                NPC.TargetClosest();
            }
        }
        private void Phase1()
        {
            NPC.ai[2]--;
            if (NPC.velocity.Y == 0)
            {
                NPC.velocity.X *= 0.8f;
                NPC.ai[0]++;
                NPC.ai[1]--;
                if (NPC.ai[0] == 60)
                {
                    NPC.velocity.Y = -8;
                    NPC.velocity.X = -5 * Math.Sign(NPC.Center.X - target.Center.X);
                }
                else if (NPC.ai[0] == 90)
                {
                    NPC.velocity.Y = -8;
                    NPC.velocity.X = -5 * Math.Sign(NPC.Center.X - target.Center.X);
                }
                else if (NPC.ai[0] == 110)
                {
                    NPC.velocity.Y = -13;
                    NPC.velocity.X = -8 * Math.Sign(NPC.Center.X - target.Center.X);
                    NPC.ai[0] = 0;
                    NPC.ai[1] = 2;
                }
                if (NPC.ai[1] == 1)
                {
                    NPC.velocity = Vector2.Zero;
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 60;
                    SoundEngine.PlaySound(SoundID.Item167, NPC.position);
                    for (int i = 0; i < 15; i++)
                    {
                        Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.NextBool() ? DustID.Ghost : DustID.Wraith);
                        d.velocity = Main.rand.NextVector2Circular(6, 3) + new Vector2(0, -4);
                    }
                }
                if (NPC.ai[2] > 0 && NPC.ai[2] % 10 == 0)
                {
                    for(int i = -1; i < 2; i += 2)
                    {
                        if(Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Bottom + new Vector2(i * (70 - NPC.ai[2]) * 2,0), Vector2.Zero, ModContent.ProjectileType<YinYangRiser>(), 10, 1, -1, NPC.ai[2]);
                        }
                    }
                }
            }
            else
            {
                if(Math.Abs(NPC.Center.X - target.Center.X) < 60 && NPC.Bottom.Y < target.position.Y)
                {
                    NPC.velocity.X *= 0.94f;
                }
            }
        }
        private void Phase2()
        {
        }
        private void Phase3()
        {
        }
    }
}
