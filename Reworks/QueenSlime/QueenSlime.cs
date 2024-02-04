using Microsoft.Xna.Framework;
using System;
using Terrafirma.Items.Materials;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.QueenSlime
{
    public class QueenSlime : GlobalNPC
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.QueenSlimeBoss;
        }
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<MajesticGel>(), 1, 32, 54));
        }
        public override void FindFrame(NPC npc, int frameHeight)
        {
        }
        byte phase = 0;
        Player Target = Main.player[0];
        bool shouldTryToDespawn = false;
        public override bool? CanFallThroughPlatforms(NPC npc)
        {
            if (phase == 1)
                return false;
            return base.CanFallThroughPlatforms(npc);
        }
        public override void AI(NPC npc)
        {
            if (Main.player[npc.target].dead || Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) / 16f > 500f)
            {
                npc.TargetClosest();
                if (Main.player[npc.target].dead || Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) / 16f > 500f)
                {
                    npc.EncourageDespawn(10);
                    shouldTryToDespawn = true;
                    if (Main.player[npc.target].Center.X < npc.Center.X)
                    {
                        npc.direction = 1;
                    }
                    else
                    {
                        npc.direction = -1;
                    }
                }
            }
            else
            {
                shouldTryToDespawn = false;
            }

            if (phase == 0)
            {
                if (npc.collideY && npc.velocity.Y == 0)
                {
                    npc.ai[0]++;
                    npc.velocity.X *= 0.9f;
                }
                else
                {
                    if(!shouldTryToDespawn)
                        npc.velocity.X += Math.Sign((Target.Center.X - npc.Center.X)) * MathHelper.Clamp(0.01f * Math.Abs(Target.Center.X - npc.Center.X),-0.2f,0.2f);
                    else
                        npc.velocity.X += npc.direction * 0.2f;
                    npc.velocity.X = MathHelper.Clamp(npc.velocity.X, -8, 8);
                    if (Math.Abs(Target.Center.X - npc.Center.X) < npc.width / 2f && npc.Center.Y < Target.Center.Y)
                    {
                        npc.velocity.X *= 0.94f;
                        npc.velocity.Y += 0.2f;
                    }
                }
                if ((npc.ai[0] + 1) % 60 == 0)
                {
                    npc.ai[1]++;
                    if (npc.ai[1] > 6)
                        return;
                    npc.velocity.Y -= (npc.ai[1] % 3 == 0) ? 15 : 12;
                    npc.ai[0]++;
                }
                if (npc.ai[1] > 6 && (npc.collideY || npc.velocity.Y == 0))
                {
                    phase++;
                    npc.ai[0] = 0;
                    npc.ai[1] = 0;
                }
            }
            else if(phase == 1)
            {
                if (npc.collideY && npc.velocity.Y == 0)
                {
                    npc.ai[0]++;
                    npc.velocity.X *= 0.9f;
                }
                npc.velocity.X *= 0.9f;
                int BeamTime = 120;
                npc.ai[0]++;
                if (npc.ai[0] % BeamTime == 0)
                {
                    if (npc.ai[0] % BeamTime == 0 && !(npc.ai[0] % (BeamTime * 2) == 0))
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, Vector2.UnitY.RotatedBy(MathHelper.TwoPi * i / 8 + Main.rand.NextFloat(0.3f)), ModContent.ProjectileType<QueenSlimeBeam>(), 60, 1, Main.myPlayer, 2000, Main.rand.NextFloat(0.7f, 1.1f));
                        }
                    }
                    else if (npc.ai[0] % BeamTime == 0)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, Vector2.UnitY.RotatedBy((MathHelper.TwoPi * i / 8) + MathHelper.TwoPi / 16f + Main.rand.NextFloat(0.3f)), ModContent.ProjectileType<QueenSlimeBeam>(), 60, 1, Main.myPlayer, 2000, Main.rand.NextFloat(0.7f, 1.1f));
                        }
                    }
                }
                else if (npc.ai[0] % (BeamTime / 3) == 0)
                {
                    Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, Vector2.UnitX.RotatedBy(npc.Center.DirectionTo(Target.Center).ToRotation() + Main.rand.NextFloat(-0.5f, 0.5f)) * Main.rand.NextFloat(6, 8), ModContent.ProjectileType<MajesticGelShot>(), 20, 1, Main.myPlayer);
                }

                if (npc.ai[0] > BeamTime * 3)
                {
                    //phase--;
                    //npc.ai[0] = 0;
                    //npc.ai[1] = 0;
                }
            }
        }
        public override void SetDefaults(NPC entity)
        {
            entity.lifeMax = 14000;
            entity.aiStyle = -1;
        }
    }
    public class QueenSlimeProjectileNerf : GlobalProjectile
    {
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (source is EntitySource_Parent parent && parent.Entity is NPC npc && npc.type is >= 658 and <= 660)
            {
                projectile.damage = (int)(projectile.damage * 0.7f);
            }
        }
    }
    public class QueenSlimeLootBag : GlobalItem
    {
        public override bool InstancePerEntity => base.InstancePerEntity;
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.QueenSlimeBossBag;
        }
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<MajesticGel>(), 1, 32, 54));
        }
    }
}
