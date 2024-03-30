﻿using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terrafirma.Global;
using System;
using Terraria.Audio;
using System.Linq;
using Terraria.DataStructures;
using Terrafirma.Projectiles.Ranged;
using Terraria.GameContent.RGB;
using Terrafirma.Particles.LegacyParticles;
using Terrafirma.Data;

namespace Terrafirma
{
    public static class TFUtils
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
        public static void DefaultToWrench(this Item item, int Damage, int UseTime, float Knockback = 7)
        {
            item.useStyle = ItemUseStyleID.Swing;
            item.DamageType = DamageClass.SummonMeleeSpeed;
            item.useTime = UseTime;
            item.useAnimation = UseTime;
            item.damage = Damage;
            item.knockBack = Knockback;
            item.UseSound = SoundID.Item1;
            item.Size = new Vector2(16, 16);
        }
        public static bool LegFrameIsOneThatRaisesTheBody(this Player player)
        {
            return (player.bodyFrame.Y >= 392 && player.bodyFrame.Y < 560) || (player.bodyFrame.Y >= 784 && player.bodyFrame.Y < 952);
        }
        public static PlayerStats PlayerStats(this Player player)
        {
            return player.GetModPlayer<PlayerStats>();
        }
        public static bool PlayerDoublePressedSetBonusActivateKey(this Player player)
        {
            return (player.doubleTapCardinalTimer[Main.ReversedUpDownArmorSetBonuses ? 1 : 0] < 15 && ((player.releaseUp && Main.ReversedUpDownArmorSetBonuses && player.controlUp) || (player.releaseDown && !Main.ReversedUpDownArmorSetBonuses && player.controlDown)));
        }
        public static bool IsTrueMeleeProjectile(this Projectile projectile)
        {
            return projectile.DamageType == DamageClass.Melee && (projectile.aiStyle == ProjAIStyleID.Spear || projectile.aiStyle == ProjAIStyleID.ShortSword || projectile.aiStyle == ProjAIStyleID.NightsEdge || projectile.type == ProjectileID.Terragrim || projectile.type == ProjectileID.Arkhalis || ProjectileSets.TrueMeleeProjectiles[projectile.type]);
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

                if (target.CanBeChasedBy() && (!HostileOnly || !target.friendly) && target != null && target.lifeMax > 5)
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
                    else if (excludedNPCs == null)
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

        /// <summary>
        /// Gets all the NPCs in an Area
        /// </summary>
        public static NPC[] GetAllNPCsInArea(float AreaSize, Vector2 position, bool HostileOnly = true, NPC[] excludedNPCs = null)
        {
            NPC[] AreaNPCs = new NPC[] {};

            for (int k = 0; k < Main.npc.Length; k++)
            {
                NPC target = Main.npc[k];

                if (target.CanBeChasedBy() && (!HostileOnly || !target.friendly) && target != null)
                {
                    if (excludedNPCs != null && !excludedNPCs.Contains(target))
                    {
                        if (Vector2.Distance(target.Center, position) < AreaSize)
                        {
                            AreaNPCs = AreaNPCs.Append(target).ToArray();
                        }
                    }
                    else if (excludedNPCs == null)
                    {
                        if (Vector2.Distance(target.Center, position) < AreaSize)
                        {
                            AreaNPCs = AreaNPCs.Append(target).ToArray();
                        }
                    }

                }
            }

            return AreaNPCs;

        }

        // Sentry  Methods ____________________________________________________________________________________________________

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectile"></param>
        /// <returns></returns>

        public static float GetSentryAttackCooldownMultiplier(this Projectile projectile)
        {
            return Math.Clamp(projectile.GetGlobalProjectile<SentryStats>().SpeedMultiplier + Main.player[projectile.owner].GetModPlayer<PlayerStats>().SentrySpeedMultiplier ,0.1f , 100f);
        }
        public static float GetSentryAttackCooldownMultiplierInverse(this Projectile projectile)
        {
            return Math.Clamp((1 - projectile.GetGlobalProjectile<SentryStats>().SpeedMultiplier - Main.player[projectile.owner].GetModPlayer<PlayerStats>().SentrySpeedMultiplier) + 1, 0.1f, 100f);
        }
        public static float GetSentryRangeMultiplier(this Projectile projectile)
        {
            return projectile.GetGlobalProjectile<SentryStats>().RangeMultiplier + Main.player[projectile.owner].GetModPlayer<PlayerStats>().SentryRangeMultiplier;
        }
        public static void WrenchHitSentry(this Player player, Rectangle hitbox, int WrenchBuffID, int Duration)
        {
            for(int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].sentry && Main.projectile[i].GetGlobalProjectile<SentryStats>().BuffTime[WrenchBuffID] < Duration - player.HeldItem.useTime && hitbox.Intersects(Main.projectile[i].Hitbox) && Main.projectile[i].active)
                {
                    Main.projectile[i].GetGlobalProjectile<SentryStats>().BuffTime[WrenchBuffID] = (int)(Duration * player.GetModPlayer<PlayerStats>().WrenchBuffTimeMultiplier);
                    SoundEngine.PlaySound(SoundID.Item37, player.position);
                    Main.projectile[i].netUpdate = true;

                    LegacyParticleSystem.AddParticle(new BigSparkle(), hitbox.ClosestPointInRect(Main.projectile[i].Center), Vector2.Zero, new Color(1f, 1f, 0.6f, 0f) * 0.3f,0,6,20,3,Main.rand.NextFloat(-0.4f,0.4f));
                    for(int j = 0; j < 3; j++)
                    {
                        Dust d = Dust.NewDustPerfect(hitbox.ClosestPointInRect(Main.projectile[i].Center), DustID.Torch, -Vector2.UnitY.RotatedByRandom(0.6f) * Main.rand.NextFloat(5));
                        d.noGravity = true;
                        d.scale *= 1.3f;
                    }
                }
            }
        }
        public static Projectile NewProjectileButWithChangesFromSentryBuffs(this Projectile sentry, IEntitySource source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, int owner,float ai0 = 0, float ai1 = 0, float ai2 = 0, bool RangeDoesNotAffectVelocity = false)
        {
            //Do Stuff in here for buffs it's like modify shoot stats
            SentryStats sentryGlobal = sentry.GetGlobalProjectile<SentryStats>();

            if(!RangeDoesNotAffectVelocity)
                velocity *= sentry.GetSentryRangeMultiplier();
            damage = (int)(damage * sentryGlobal.DamageMultiplier);
            Projectile p = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, owner, ai0, ai1, ai2);
            SentryBulletBuff bulletGlobal = p.GetGlobalProjectile<SentryBulletBuff>();

            if (sentryGlobal.BuffTime[SentryBuffID.InflictShadowflame] > 0)
            {
                bulletGlobal.Demonite = true;
            }
            return p;
        }

        /// <summary>
        /// Updates the sentry Projectile to have priority over other sentries (Bookmark Wrench Effect)
        /// </summary>
        public static void UpdateSentryPriority(Projectile projectile, Player player = null)
        {
            if (projectile != null)
            {
                projectile.GetGlobalProjectile<SentryStats>().Priority = true;
                for (int i = 0; i < Main.projectile.Length; ++i)
                {
                    if (Main.projectile[i] != projectile && Main.projectile[i].owner == projectile.owner && Main.projectile[i].WipableTurret) Main.projectile[i].GetGlobalProjectile<SentryStats>().Priority = false;
                }
            }
            else if (player != null)
            {
                for (int i = 0; i < Main.projectile.Length; ++i)
                {
                    if (Main.projectile[i].WipableTurret && Main.player[Main.projectile[i].owner] == player) Main.projectile[i].GetGlobalProjectile<SentryStats>().Priority = false;
                }
            }
            
        }

        //____________________________________________________________________________________________________

        public static bool AnyWingsEquipped(this Player player)
        {
            int[] EquippedAccessories = player.GetModPlayer<AccessorySynergyPlayer>().EquippedAccessories;

            for (int i = 0; i < EquippedAccessories.Length; i++)
            {
                if (new Item(EquippedAccessories[i]).wingSlot != -1) return true;
            }

            return false;
            
        }
    }
}
