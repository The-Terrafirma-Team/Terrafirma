using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Global.Templates;
using Terrafirma.Systems.Elements;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.NPCs
{
    public class FlameElemental : TfirmaNPC
    {
        public override Color? GetAlpha(Color drawColor)
        {
            return new Color(1f, 1f, MathF.Sin((float)Main.timeForVisualEffects * 0.3f) * 0.5f + 0.5f, 0.5f);
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 5;
            Elements.fireNPC.Add(Type);
        }
        public override void SetDefaults()
        {
            NPC.lifeMax = 360;
            NPC.defense = 2;
            NPC.knockBackResist = 0.2f;
            NPC.noGravity = true;
            NPC.Size = new Vector2(30);
            NPC.damage = 20;
            NPC.HitSound = SoundID.NPCHit54;
            NPC.DeathSound = SoundID.NPCDeath52;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if(NPC.life <= 0)
            {
                for(int i = 0; i < 30; i++)
                {
                    Dust d = Dust.NewDustPerfect(NPC.Center, DustID.InfernoFork, Main.rand.NextVector2Circular(6, 6));
                    d.noGravity = !Main.rand.NextBool(3);
                    if(d.noGravity)
                        d.fadeIn = 1.4f;
                }
            }
        }
        public override void AI()
        {
            FindClosestOnlyIfCurrentTargetIsInvalid();
            NPC.ai[0]++;

            if (NPC.collideX)
                NPC.velocity.X = NPC.oldVelocity.X * -1f;
            if (NPC.collideY)
                NPC.velocity.Y = NPC.oldVelocity.Y * -1f;

            if (NPC.ai[1] == 0)
                NPC.velocity += NPC.Center.DirectionTo(target.Center + new Vector2(160).RotatedBy(NPC.ai[0] * 0.01f)) * 0.1f;
            NPC.velocity = NPC.velocity.LengthClamp(8);

            if (NPC.ai[0] % 300 == 0)
            {
                NPC.ai[1] = 1;
            }
            if (NPC.ai[1] == 1)
            {
                NPC.velocity *= 0.9f;
                NPC.ai[2]++;

                if (NPC.ai[2] == 30)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y = 46 * 2;
                }
                else if (NPC.ai[2] > 30)
                {
                    if ((NPC.ai[2] - 20) % 30 == 0 && target == Main.LocalPlayer)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.Center.DirectionTo(target.Center).RotatedByRandom(0.2f) * 8,ProjectileID.Fireball,30,3);
                    }
                    if (NPC.ai[2] > 110)
                    {
                        NPC.ai[2] = 0;
                        NPC.ai[1] = 0;
                    }
                }
            }

            NPC.spriteDirection = -MathF.Sign(NPC.Center.X - target.Center.X);
            NPC.rotation = NPC.velocity.X * 0.05f;
            Dust d = Dust.NewDustDirect(NPC.Bottom + new Vector2(-10,-10), 20, 3, DustID.Torch);
            d.velocity = new Vector2(0, Main.rand.NextFloat(1,3) + NPC.velocity.Y);
            d.noGravity = true;
            d.scale = 1.2f;
            d.customData = 1;
        }
        public override void FindFrame(int frameHeight)
        {
            if (NPC.ai[2] <= 30)
            {
                NPC.frameCounter++;
                if (NPC.frameCounter == 10)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y = NPC.frame.Y == 0? frameHeight: 0;
                }
            }
            else
            {
                NPC.frameCounter++;
                if (NPC.frameCounter == 10)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += frameHeight;
                    if (NPC.frame.Y == 230)
                        NPC.frame.Y = frameHeight * 2;
                }
            }
        }
    }
}
