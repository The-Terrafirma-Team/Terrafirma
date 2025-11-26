using Microsoft.Xna.Framework;
using System;
using Terrafirma.Common;
using Terrafirma.Common.AIStyles;
using Terrafirma.Common.Interfaces;
using Terrafirma.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Content.NPCs.Vanilla.MeteorHead
{
    public class MeteorHead : GlobalNPC, ICustomBlockBehavior
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<ServerConfig>().CombatReworkEnabled;
        }
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.MeteorHead;
        }
        public override void SetStaticDefaults()
        {
            DataSets.NPCWhitelistedForStun[NPCID.MeteorHead] = true;
        }
        public override void SetDefaults(NPC entity)
        {
            entity.aiStyle = -1;
        }
        public void OnBlocked(Player player, float Power, NPC npc = null)
        {
        }
        public override void AI(NPC npc)
        {
            NPCStats stats = npc.NPCStats();

            if(stats.NoFlight || stats.Immobile)
            {
                npc.noGravity = npc.noTileCollide = false;
            }
            else
            {
                npc.noGravity = npc.noTileCollide = true;
            }
            if (stats.NoAnimation)
                npc.frameCounter = 0;

            if (!npc.noGravity || !npc.noTileCollide)
            {
                npc.rotation += npc.velocity.X * 0.1f;
                if (npc.collideY)
                    npc.velocity.X *= 0.99f;
                if (npc.collideX)
                    npc.velocity.X = -npc.oldVelocity.X * 0.7f;
                return;
            }
            if (!npc.HasValidTarget)
                npc.TargetClosest();
            var targetData = npc.GetTargetData();
            npc.SimpleFlyMovement(npc.Center.DirectionTo(targetData.Center) * stats.MoveSpeed * 3, stats.MoveSpeed * 0.03f);

            npc.spriteDirection = npc.velocity.X < 0 ? -1 : 1;
            npc.rotation = npc.velocity.ToRotation() + (npc.spriteDirection == 1? 0 : -MathHelper.Pi);
        }
    }
}
