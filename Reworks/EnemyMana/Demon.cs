using Microsoft.Xna.Framework;
using MonoMod.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.NPCs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.EnemyMana
{
    public class Demon : GlobalNPC
    {

        public override bool AppliesToEntity(NPC npc, bool lateInstantiation)
        {
            return npc.type == NPCID.Demon || npc.type == NPCID.VoodooDemon;
        }
        public override bool InstancePerEntity => true;
        public override void SetDefaults(NPC npc)
        {
            npc.ApplyManaStats(200);
            npc.aiStyle = -1;
            npc.noGravity = true;
        }

        public override bool? CanFallThroughPlatforms(NPC npc)
        {
            return true;
        }
        public override void AI(NPC npc)
        {
            NPCStats stats = npc.NPCStats();
            Player target = Main.player[npc.target];
            if (npc.HasValidTarget)
            {
                npc.direction = -Math.Sign(npc.Center.X - target.Center.X);
                npc.directionY = -Math.Sign(npc.Center.Y - target.Center.Y);
            }
            else
            {
                npc.TargetClosest();

                if (Main.rand.NextBool(80))
                    npc.direction *= -1;
                if (Main.rand.NextBool(80))
                    npc.directionY *= -1;
            }

            npc.ai[0]++;
            if (npc.ai[1] == 0)
            {
                npc.velocity += new Vector2(npc.direction, npc.directionY * 0.2f) * 0.1f;
                npc.velocity = npc.velocity.LengthClamp(6);

                if(npc.HasValidTarget && stats.Mana > 45 && npc.ai[0] > 60 && Collision.CanHitLine(npc.Center,1,1,target.Center,1,1) && Main.rand.NextBool(20))
                {
                    npc.ai[0] = 0;
                    npc.ai[1] = 1;
                    npc.netUpdate = true;
                }
            }
            else if (npc.ai[1] == 1)
            {
                Lighting.AddLight(npc.Center, new Vector3(0.6f, 0f, 1f) * Math.Clamp(npc.ai[0], 0, 15) / 15f);
                npc.velocity *= 0.95f;
                if (npc.ai[0] is 30 or 50 or 70 && npc.CheckMana(15))
                {
                    if(Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center, npc.Center.DirectionTo(target.Center),ProjectileID.DemonSickle,21,1);
                }

                if (npc.ai[0] > 90)
                {
                    npc.ai[0] = -128;
                    npc.ai[1] = 0;
                }
            }
            if (npc.collideY)
            {
                npc.velocity.Y = npc.oldVelocity.Y * -0.5f;
                if(npc.velocity.Y < -2)
                {
                    npc.velocity.Y = -2;
                }
                else if (npc.velocity.Y > 2)
                {
                    npc.velocity.Y = 2;
                }
            }
            if (npc.collideX)
            {
                npc.velocity.X = npc.oldVelocity.X * -0.5f;
                if (npc.velocity.X < -2)
                {
                    npc.velocity.X = -2;
                }
                else if (npc.velocity.X > 2)
                {
                    npc.velocity.X = 2;
                }
            }
            if (npc.wet || npc.lavaWet || npc.honeyWet)
                npc.velocity.Y -= 0.4f;
        }
        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
        {
            target.DealManaDamage(npc, 50);
            CombatText.NewText(npc.Hitbox, CombatText.HealMana, 50);
            npc.NPCStats().Mana += 25;
        }
    }
}
