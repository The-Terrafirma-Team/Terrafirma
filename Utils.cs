using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using TerrafirmaRedux.Global;
using System;
using Terraria.Audio;
using System.Linq;

namespace TerrafirmaRedux
{
    public static class Utils
    {
        /// <summary>
        /// Clamps a Vector2 to be a specific length between max and min. Good for giving something a maximum speed.
        /// </summary>
        public static Vector2 LengthClamp(this Vector2 vector, float max, float min = 0)
        {
            if (vector.Length() > max) return Vector2.Normalize(vector) * max;
            else if (vector.Length() < min) return Vector2.Normalize(vector) * min;
            else return vector;
        }
        /// <summary>
        /// I should learn what this actually does at some point
        /// </summary>
        /// <param name="spriteWidth"></param>
        /// <param name="spriteHeight"></param>
        /// <param name="normalizedPointOnPath"></param>
        /// <param name="itemScale"></param>
        /// <param name="location"></param>
        /// <param name="outwardDirection"></param>
        /// <param name="player"></param>
        public static void GetPointOnSwungItemPath(float spriteWidth, float spriteHeight, float normalizedPointOnPath, float itemScale, out Vector2 location, out Vector2 outwardDirection, Player player)
        {
            float num = (float)Math.Sqrt(spriteWidth * spriteWidth + spriteHeight * spriteHeight);
            float num2 = (float)(player.direction == 1).ToInt() * ((float)Math.PI / 2f);
            if (player.gravDir == -1f)
            {
                num2 += (float)Math.PI / 2f * (float)player.direction;
            }
            outwardDirection = player.itemRotation.ToRotationVector2().RotatedBy(3.926991f + num2);
            location = player.RotatedRelativePoint(player.itemLocation + outwardDirection * num * normalizedPointOnPath * itemScale);
        }
        /// <summary>
        /// Sets defaults to regular sword stuff.
        /// item.useStyle = ItemUseStyleID.Swing;
        /// item.DamageType = DamageClass.Melee;
        /// item.damage = Damage;
        /// item.useTime = UseTime;
        /// item.useAnimation = UseTime;
        /// item.knockBack = Knockback;
        /// item.UseSound = SoundID.Item1;
        /// item.Size = new Vector2(16, 16);
        /// </summary>
        public static void DefaultToSword(this Item item, int Damage, int UseTime, float Knockback)
        {
            item.useStyle = ItemUseStyleID.Swing;
            item.DamageType = DamageClass.Melee;
            item.damage = Damage;
            item.useTime = UseTime;
            item.useAnimation = UseTime;
            item.knockBack = Knockback;
            item.UseSound = SoundID.Item1;
            item.Size = new Vector2(16, 16);
        }
        public static bool PlayerDoublePressedSetBonusActivateKey(this Player player)
        {
            return (player.doubleTapCardinalTimer[Main.ReversedUpDownArmorSetBonuses ? 1 : 0] < 15 && ((player.releaseUp && Main.ReversedUpDownArmorSetBonuses && player.controlUp) || (player.releaseDown && !Main.ReversedUpDownArmorSetBonuses && player.controlDown)));
        }
        public static bool IsTrueMeleeProjectile(this Projectile projectile)
        {
            return projectile.DamageType == DamageClass.Melee && (projectile.aiStyle == ProjAIStyleID.Spear || projectile.aiStyle == ProjAIStyleID.ShortSword || projectile.aiStyle == ProjAIStyleID.NightsEdge || projectile.type == ProjectileID.Terragrim || projectile.type == ProjectileID.Arkhalis || TrueMeleeArmorPenetrationGlobalProjectile.TrueMeleeProjectiles[projectile.type]);
        }
        public static void Explode(this Projectile projectile, int Diameter)
        {
            projectile.ResetLocalNPCHitImmunity();
            projectile.maxPenetrate = -1;
            projectile.penetrate = -1;
            projectile.Resize(Diameter, Diameter);
            projectile.Damage();
        }
        public static Color getAgnomalumFlameColor()
        {
            Color[] colors = new Color[] { new Color(255, 188, 122, 0), new Color(255, 128, 0, 0), new Color(252, 120, 111, 0), new Color(207,33,76,0)};
            if (Main.rand.NextBool(15))
                return Main.rand.NextBool() ? new Color(255, 128, 246,0) : new Color(91, 186, 229,0);
            else
                return colors[Main.rand.Next(colors.Length)];
        }
        public static int TypeCountNPC(int type)
        {
            int found = 0;
            for(int i = 0; i < Main.npc.Length; i++) 
            {
                if (Main.npc[i].type == type) found++;
            }
            return found;
        }
        public static int TypeCountProjectile(int type)
        {
            int found = 0;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].type == type) found++;
            }
            return found;
        }

        /// <summary>
        /// Finds the closest NPC to the given position and returns that NPC
        /// </summary>
        public static NPC FindClosestNPC(float maxDetectDistance, Vector2 position, bool HostileOnly = true, NPC[] excludedNPCs = null)
        {
            NPC closestNPC = null;

            float MaxDetectDistance = maxDetectDistance;

            for (int k = 0; k < Main.npc.Length; k++)
            {
                NPC target = Main.npc[k];

                if (target.CanBeChasedBy() && (!HostileOnly || !target.friendly))
                {
                    if (excludedNPCs != null && !excludedNPCs.Contains(target))
                    {
                        float DistanceToTarget = Vector2.Distance(target.Center, position);

                        if (DistanceToTarget < MaxDetectDistance)
                        {
                            MaxDetectDistance = DistanceToTarget;
                            closestNPC = target;
                        }
                    }
                    else
                    {
                        float DistanceToTarget = Vector2.Distance(target.Center, position);

                        if (DistanceToTarget < MaxDetectDistance)
                        {
                            MaxDetectDistance = DistanceToTarget;
                            closestNPC = target;
                        }
                    }
                    
                }
            }

            return closestNPC;

        }
    }
}
