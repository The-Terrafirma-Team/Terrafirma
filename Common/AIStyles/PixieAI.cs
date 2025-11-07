using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Utilities;
using Terraria;

namespace Terrafirma.Common.AIStyles
{
    public static class PixieAI
    {
        public static void AI(NPC npc, NPCStats stats, bool shouldBeDespawning = false,float maxSpeedX = 2, float maxSpeedY = 1, float acceleration = 0.1f)
        {
            npc.noGravity = true;
            if (!shouldBeDespawning)
                npc.TargetClosest();
            maxSpeedX *= stats.MoveSpeed;
            maxSpeedY *= stats.MoveSpeed;
            acceleration *= stats.MoveSpeed;

            if (stats.Immobile | stats.NoFlight)
            {
                npc.velocity.X *= 0.99f;
                npc.noGravity = false;
                return;
            }

            Point coords = npc.Center.ToTileCoordinates();
            bool targetAbove = npc.Bottom.Y > npc.targetRect.Center().Y;
            if (CollisionUtils.SolidTiles(coords.X - 2, coords.X + 2, coords.Y, coords.Y + (targetAbove ? 6 : 3), targetAbove) || shouldBeDespawning)
                npc.directionY = -1;
            else
                npc.directionY = 1;

            if (!npc.HasValidTarget && npc.collideX)
            {
                npc.direction *= -1;
            }

            if (npc.collideX)
            {
                npc.velocity.X = -1 * Math.Sign(npc.velocity.X) * stats.MoveSpeed;
            }

            if (npc.collideY)
            {
                npc.velocity.Y = -1 * Math.Sign(npc.velocity.Y) * stats.MoveSpeed;
            }

            if (npc.direction == 1 && npc.velocity.X < maxSpeedX)
                npc.velocity.X += acceleration;
            else if (npc.direction == -1 && npc.velocity.X > -maxSpeedX)
                npc.velocity.X -= acceleration;

            if (npc.directionY == 1 && npc.velocity.Y < maxSpeedY)
                npc.velocity.Y += acceleration * 0.5f;
            else if (npc.directionY == -1 && npc.velocity.Y > -maxSpeedY)
                npc.velocity.Y -= acceleration * 0.5f;
        }
    }
}
