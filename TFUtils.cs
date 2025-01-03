using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using System;
using Terraria.Audio;
using System.Linq;
using Terraria.DataStructures;
using Terrafirma.Data;
using Terrafirma.Common.Players;
using Terrafirma.Particles;
using Terrafirma.Systems.AccessorySynergy;
using Terraria.GameContent;
using ReLogic.Graphics;
using Terrafirma.Common;
using Terrafirma.Systems.MageClass;
using Terrafirma.Common.Items;
using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using Terrafirma.Common.NPCs;
using Terraria.Chat;
using Terraria.Localization;
using System.Collections.Generic;
using Terraria.GameInput;

namespace Terrafirma
{
    public static class TFUtils
    {
        public static void HealWithAdjustments(this Player reciever, int life)
        {
            reciever.Heal((int)(life * reciever.PlayerStats().HealingMultiplier));
        }
        public static void HealWithAdjustments(this Player reciever, Player giver, int life)
        {
            reciever.Heal((int)(life * reciever.PlayerStats().HealingMultiplier * giver.PlayerStats().OutgoingHealingMultiplier));
        }
        public static float GetAdjustedWeaponSpeedPercent(this Player player, Item item)
        {
            return player.GetWeaponAttackSpeed(item) * ((float)ContentSamples.ItemsByType[item.type].useTime / item.useTime);
        }
        public static bool CheckTension(this Player player, int Tension, bool Consume = true)
        {
            PlayerStats pStats = player.PlayerStats();
            if (pStats.Tension >= Tension)
            {
                if (Consume)
                {
                    pStats.Tension -= player.ApplyTensionBonusScaling(Tension,false,true);
                }
                return true;
            }
            return false;
        }
        public static void GiveTension(this Player player, int Tension, bool Numbers = true)
        {
            PlayerStats pStats = player.PlayerStats();
            pStats.Tension += (int)(Tension * pStats.TensionGainMultiplier);
        }
        public static int ApplyTensionBonusScaling(this Player player, int number, bool parry = false, bool drain = false)
        {
            PlayerStats stats = player.PlayerStats();
            if (!drain)
            {
                return (int)(number * stats.TensionGainMultiplier) + stats.FlatTensionGain;
            }
            else
            {
                return (int)(number * stats.TensionCostMultiplier) + stats.FlatTensionCost;
            }
        }
        public static string NicenUpKeybindNameIfApplicable(string name)
        {
            switch (name)
            {
                case "Mouse1":
                    return Language.GetText("Mods.Terrafirma.KeybindReplacements.Mouse1").Value;
                case "Mouse2":
                    return Language.GetText("Mods.Terrafirma.KeybindReplacements.Mouse2").Value;
                case "Mouse3":
                    return Language.GetText("Mods.Terrafirma.KeybindReplacements.Mouse3").Value;
                case "OemOpenBrackets":
                    return "[";
                case "OemCloseBrackets":
                    return "]";
                case "OemSemiColon":
                    return ";";
                case "OemColon":
                    return ":";
                case "OemQuotes":
                    return "'";
                case "OemComma":
                    return ",";
                case "OemPeriod":
                    return ".";
                case "OemQuestion":
                    return "?";
                case "OemPipe":
                    return "/";
                case "OemPlus":
                    return "+";
                case "OemMinus":
                    return "-";
            }
            return name;
        }
        public static bool CanUseDash(this Player player)
        {
            return player.dashType == DashID.None && !player.setSolar && !player.mount.Active;
        }
        public static void SendImportantStatusMessage(string Key, Color color)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText(Language.GetText(Key), color);
            }
            else if (Main.netMode == NetmodeID.Server)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Key), color);
            }
        }
        public static void ApplyManaStats(this NPC npc, int MaxMana)
        {
            NPCStats gNPC = npc.GetGlobalNPC<NPCStats>();
            gNPC.MaxMana = MaxMana;
            gNPC.Mana = MaxMana;
        }
        public static bool CheckMana(this NPC npc, int Mana, bool Consume = true)
        {
            NPCStats gNPC = npc.GetGlobalNPC<NPCStats>();
            if (gNPC.Silenced)
                return false;
            if(gNPC.Mana > Mana)
            {
                if (Consume)
                {
                    gNPC.Mana -= Mana;
                    gNPC.ManaRegenTimer = 0;
                }
                return true;
            }
            return false;
        }
        public static void DealManaDamage(this NPC npc, Player player, int Mana)
        {
            NPCStats gNPC = npc.GetGlobalNPC<NPCStats>();
            if (gNPC.MaxMana == 0)
                return;

            gNPC.Mana -= Mana;
            gNPC.ManaRegenTimer = 0;
            CombatText.NewText(npc.Hitbox,Common.NPCs.NPCStats.EnemyManaDamageColor,Mana);
        }
        public static void DealManaDamage(this Player player, NPC npc, int Mana)
        {
            player.statMana -= Mana;
            CombatText.NewText(npc.Hitbox, Common.NPCs.NPCStats.FriendlyManaDamageColor, Mana);
        }
        public static Color TryApplyingPlayerStringColor(int playerStringColor, Color stringColor)
        {
            return (Color)typeof(Main).GetMethod("TryApplyingPlayerStringColor", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, [playerStringColor, stringColor]);
            //if (playerStringColor > 0)
            //{
            //    stringColor = WorldGen.paintColor(playerStringColor);
            //    if (stringColor.R < 75)
            //    {
            //        stringColor.R = 75;
            //    }
            //    if (stringColor.G < 75)
            //    {
            //        stringColor.G = 75;
            //    }
            //    if (stringColor.B < 75)
            //    {
            //        stringColor.B = 75;
            //    }
            //    switch (playerStringColor)
            //    {
            //        case 13:
            //            stringColor = new Color(20, 20, 20);
            //            break;
            //        case 0:
            //        case 14:
            //            stringColor = new Color(200, 200, 200);
            //            break;
            //        case 28:
            //            stringColor = new Color(163, 116, 91);
            //            break;
            //        case 27:
            //            stringColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
            //            break;
            //    }
            //    stringColor.A = (byte)((float)(int)stringColor.A * 0.4f);
            //}
            //return stringColor;
        }
        public static bool ThrowerSpawnDroppedItem(this Projectile projectile, int player, int type)
        {
            return projectile.ThrowerSpawnDroppedItem(Main.player[player], type);
        }
        public static bool ThrowerSpawnDroppedItem(this Projectile projectile, Player player, int type)
        {
            if(Main.LocalPlayer == player && Main.rand.NextFloat() < player.PlayerStats().ThrowerRecoveryChance)
            {
                int i = Item.NewItem(projectile.GetSource_DropAsItem(),projectile.Hitbox,type,1,true);
                Main.item[i].GetGlobalItem<GlobalItemInstanced>().droppedForRecovery = true;
                return true;
            }
            return false;
        }
        public static void AddBuffThrower(this NPC target, int type, int duration, Player player, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(type, (int)(duration * player.PlayerStats().ThrowerDebuffPower));
        }
        public static void EasyCenteredProjectileDraw(Asset<Texture2D> tex, Projectile p, Color color)
        {
            Main.EntitySpriteDraw(tex.Value, p.Center - Main.screenPosition, tex.Frame(1, Main.projFrames[p.type],0,p.frame),color * p.Opacity,p.rotation,new Vector2(tex.Width() / 2, tex.Height() / 2 / Main.projFrames[p.type]),p.scale,p.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
        }
        public static void EasyCenteredProjectileDraw(Asset<Texture2D> tex, Projectile p, Color color, Vector2 position, float rotation, float scale)
        {
            Main.EntitySpriteDraw(tex.Value, position, tex.Frame(1, Main.projFrames[p.type], 0, p.frame), color, rotation, new Vector2(tex.Width() / 2, tex.Height() / 2 / Main.projFrames[p.type]), scale, p.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
        }
        public static void EasyCenteredProjectileDrawVerticalFlip(Asset<Texture2D> tex, Projectile p, Color color)
        {
            Main.EntitySpriteDraw(tex.Value, p.Center - Main.screenPosition, tex.Frame(1, Main.projFrames[p.type], 0, p.frame), color * p.Opacity, p.rotation, new Vector2(tex.Width() / 2, tex.Height() / 2 / Main.projFrames[p.type]), p.scale, p.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically);
        }
        public static void EasyCenteredProjectileDrawVerticalFlip(Asset<Texture2D> tex, Projectile p, Color color, Vector2 position, float rotation, float scale)
        {
            Main.EntitySpriteDraw(tex.Value, position, tex.Frame(1, Main.projFrames[p.type], 0, p.frame), color, rotation, new Vector2(tex.Width() / 2, tex.Height() / 2 / Main.projFrames[p.type]), scale, p.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically);
        }
        public static void QuickDefaults(this Projectile proj, bool hostile = false, int size = 8, int aiStyle = -1)
        {
            proj.aiStyle = aiStyle;
            proj.hostile = hostile;
            proj.friendly = !hostile;
            proj.width = size;
            proj.height = size;
        }
        public static Color ToColor(this Vector3 vect, float alpha = 1f)
        {
            return new Color(vect.X, vect.Y, vect.Z, alpha);
        }

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
        public static void DefaultToDrawnBow(this Item item,int Projectile, int Damage, int UseTime, float Knockback = 5, float shootSpeed = 6f)
        {
            item.useStyle = ItemUseStyleID.Shoot;
            item.DamageType = DamageClass.Ranged;
            item.useTime = UseTime;
            item.useAnimation = UseTime;
            item.damage = Damage;
            item.knockBack = Knockback;
            item.useAmmo = AmmoID.Arrow;
            item.Size = new Vector2(16, 16);
            item.shoot = Projectile;
            item.shootSpeed = 6f;
            item.channel = true;
            item.noUseGraphic = true;
            item.noMelee = true;
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
        public static PlayerStats PlayerStats(this Player player)
        {
            return player.GetModPlayer<PlayerStats>();
        }
        public static NPCStats NPCStats(this NPC npc)
        {
            return npc.GetGlobalNPC<NPCStats>();
        }
        public static Spell Spell(this Item item)
        {
            return item.GetGlobalItem<GlobalItemInstanced>().Spell;
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
        /// Finds the closest NPC to the given position and returns that NPC. If no NPC can be found, returns null
        /// </summary>
        public static NPC FindClosestNPC(float maxDetectDistance, Vector2 position, bool HostileOnly = true, NPC[] excludedNPCs = null, bool TargetThroughWalls = true)
        {
            NPC closestNPC = null;

            float MaxDetectDistance = maxDetectDistance;

            for (int k = 0; k < Main.npc.Length; k++)
            {
                NPC target = Main.npc[k];

                if (target.CanBeChasedBy() && (!HostileOnly || !target.friendly) && target != null && target.lifeMax > 5 && (!TargetThroughWalls ? Collision.CanHitLine(position - new Vector2(4), 8, 8, target.position, target.width, target.height) : true))
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

        public static NPC FindSummonTarget(this Projectile proj, float maxDetectDistance, Vector2 position, bool TargetThroughWalls = true)
        {
            NPC TargetNPC = null;
            if (Main.player[proj.owner].HasMinionAttackTargetNPC && Main.npc[Main.player[proj.owner].MinionAttackTargetNPC].Center.Distance(position) < maxDetectDistance)
                TargetNPC = Main.npc[Main.player[proj.owner].MinionAttackTargetNPC];
            else 
                TargetNPC = FindClosestNPC(maxDetectDistance, position, TargetThroughWalls);
            return TargetNPC;
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

        public static Vector2 CommonBounceLogic(this Projectile p, Vector2 oldVelocity)
        {
            if (p.velocity.Y != oldVelocity.Y)
            {
                p.velocity.Y = -oldVelocity.Y;
            }
            if (p.velocity.X != oldVelocity.X)
            {
                p.velocity.X = -oldVelocity.X;
            }
            return p.velocity;
        }

        public static bool NotPreBoss(bool includeKingSlime = true)
        {
            if (includeKingSlime)
                return (NPC.downedBoss1 || NPC.downedBoss2 || NPC.downedBoss3 || NPC.downedSlimeKing || Main.hardMode);
            return (NPC.downedBoss1 || NPC.downedBoss2 || NPC.downedBoss3 || Main.hardMode);
        }
        // Sentry  Methods ____________________________________________________________________________________________________
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
        //public static void WrenchHitSentry(this Player player, Rectangle hitbox, int WrenchBuffID, int Duration)
        //{
        //    for(int i = 0; i < Main.projectile.Length; i++)
        //    {
        //        if (Main.projectile[i].sentry && Main.projectile[i].GetGlobalProjectile<SentryStats>().BuffTime[WrenchBuffID] < (int)(Duration * player.GetModPlayer<PlayerStats>().WrenchBuffTimeMultiplier) - player.HeldItem.useTime && hitbox.Intersects(Main.projectile[i].Hitbox) && Main.projectile[i].active)
        //        {
        //            Main.projectile[i].GetGlobalProjectile<SentryStats>().BuffTime[WrenchBuffID] = (int)(Duration * player.GetModPlayer<PlayerStats>().WrenchBuffTimeMultiplier);
        //            SoundEngine.PlaySound(SoundID.Item37, player.position);
        //            Main.projectile[i].netUpdate = true;

        //            BigSparkle bigsparkle = new BigSparkle();
        //            bigsparkle.fadeInTime = 6;
        //            bigsparkle.Rotation = Main.rand.NextFloat(-0.4f, 0.4f);
        //            bigsparkle.Scale = 3f;
        //            ParticleSystem.AddParticle(bigsparkle, hitbox.ClosestPointInRect(Main.projectile[i].Center), Vector2.Zero, new Color(1f, 1f, 0.6f, 0f) * 0.3f);
        //            //LegacyParticleSystem.AddParticle(new BigSparkle(), hitbox.ClosestPointInRect(Main.projectile[i].Center), Vector2.Zero, new Color(1f, 1f, 0.6f, 0f) * 0.3f,0,6,20,3,Main.rand.NextFloat(-0.4f,0.4f));
        //            for(int j = 0; j < 3; j++)
        //            {
        //                Dust d = Dust.NewDustPerfect(hitbox.ClosestPointInRect(Main.projectile[i].Center), DustID.Torch, -Vector2.UnitY.RotatedByRandom(0.6f) * Main.rand.NextFloat(5));
        //                d.noGravity = true;
        //                d.scale *= 1.3f;
        //            }
        //        }
        //    }
        //}
        public static Projectile NewProjectileButWithChangesFromSentryBuffs(this Projectile sentry, IEntitySource source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, int owner,float ai0 = 0, float ai1 = 0, float ai2 = 0, bool RangeDoesNotAffectVelocity = false)
        {
            //Do Stuff in here for buffs it's like modify shoot stats
            SentryStats sentryGlobal = sentry.GetGlobalProjectile<SentryStats>();

            if(!RangeDoesNotAffectVelocity)
                velocity *= sentry.GetSentryRangeMultiplier();
            damage = (int)(damage * sentryGlobal.DamageMultiplier);
            Projectile p = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, owner, ai0, ai1, ai2);
            SentryBulletBuff bulletGlobal = p.GetGlobalProjectile<SentryBulletBuff>();
            if(sentryGlobal.BuffType != null)
            {
                sentryGlobal.BuffType.modifyBullet(bulletGlobal, p);
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
