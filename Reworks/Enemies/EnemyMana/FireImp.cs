//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Terraria.ID;
//using Terraria;
//using Terraria.ModLoader;
//using Microsoft.Xna.Framework;
//using Terraria.Audio;

//namespace Terrafirma.Reworks.Enemies.EnemyMana
//{
//    public class FireImp : GlobalNPC
//    {
//        public override bool AppliesToEntity(NPC npc, bool lateInstantiation)
//        {
//            return npc.type == NPCID.FireImp;
//        }
//        public override bool InstancePerEntity => true;
//        public override void SetDefaults(NPC npc)
//        {
//            npc.ApplyManaStats(200);
//            npc.aiStyle = -1;
//        }
//        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
//        {
//            base.OnHitByProjectile(npc, projectile, hit, damageDone);
//        }
//        public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
//        {
//            base.OnHitByItem(npc, player, item, hit, damageDone);
//        }
//        public override void AI(NPC npc)
//        {
//            Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Torch);
//            d.noGravity = true;
//            d.velocity *= 0.3f;
//            d.velocity.Y -= 0.7f;
//            if (Main.rand.NextBool(7))
//            {
//                Dust d3 = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Wraith);
//                d3.noGravity = true;
//                d3.velocity *= 0.3f;
//                d3.velocity.Y -= 0.7f;
//                d.alpha = 128;
//            }
//            if (npc.collideY)
//                npc.velocity.X *= 0.87f;

//            if (!npc.HasValidTarget)
//            {
//                npc.TargetClosest();
//                if (!npc.HasValidTarget)
//                {
//                    return;
//                }
//            }

//            Player target = Main.player[npc.target];
//            npc.direction = -Math.Sign(npc.Center.X - target.Center.X);
//            Vector2 tile = Vector2.Zero;
//            npc.ai[0]++;
//            npc.ai[1]++;
//            if (npc.CheckMana(100, false))
//            {
//                npc.ai[1] += 0.5f;
//            }

//            if (npc.ai[0] > Math.Max(npc.Center.Distance(target.Center),100) && npc.CheckMana(20))
//            {
//                npc.ai[0] = 0;
//                npc.AI_AttemptToFindTeleportSpot(ref tile, (int)(target.Center.X / 16), (int)(target.Center.Y / 16), 30);
//                npc.Bottom = tile * 16;
//                for(int i = 0; i < 80; i++)
//                {
//                    Dust d2 = Dust.NewDustDirect(i < 40? npc.oldPosition : npc.position, npc.width, npc.height, DustID.Torch);
//                    d2.noGravity = Main.rand.NextBool();
//                    d2.velocity = Main.rand.NextVector2Circular(1, 1) + new Vector2(0, -1);
//                    d2.scale = Main.rand.NextFloat(2);
//                    d2.customData = 0;

//                    if (i < 40)
//                        d2.velocity += npc.oldPosition.DirectionTo(npc.position) * 2;
//                }
//                SoundEngine.PlaySound(SoundID.Item8,npc.position);
//            }
//            if (npc.CheckMana(60, false))
//            {
//                if (npc.ai[1] > 120 && npc.ai[0] > 20)
//                {
//                    npc.CheckMana(5);
//                    npc.ai[1] = 0;
//                    SoundEngine.PlaySound(SoundID.DD2_FlameburstTowerShot, npc.position);
//                    if (Main.rand.NextBool())
//                    {
//                        NPC n = NPC.NewNPCDirect(npc.GetSource_FromThis(), (int)npc.Center.X, (int)npc.Center.Y, NPCID.BurningSphere, npc.whoAmI, 0, 0, 0, 0, npc.target);
//                        n.velocity = n.Center.DirectionTo(target.Center) * 8;
//                    }
//                    else
//                    {
//                        for (int i = -1; i < 2; i++)
//                        {
//                            NPC n = NPC.NewNPCDirect(npc.GetSource_FromThis(), (int)npc.Center.X, (int)npc.Center.Y, NPCID.BurningSphere, npc.whoAmI, 0, 0, 0, 0, npc.target);
//                            n.velocity = n.Center.DirectionTo(target.Center).RotatedBy(i * 0.1f) * 6;
//                        }
//                    }
//                }
//            }
//            else
//            {
//                npc.ai[1] -= 0.5f;
//            }
//        }
//    }
//}
