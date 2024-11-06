using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terrafirma.Buffs.Debuffs;
using Terrafirma.Common.Templates;
using Terrafirma.Common.Templates.Melee;
using Terrafirma.Data;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common.Players
{
    public class PlayerStats : ModPlayer
    {
        public override void Load()
        {
            On_Player.AddBuff_DetermineBuffTimeToAdd += On_Player_AddBuff_DetermineBuffTimeToAdd;
        }
        private int On_Player_AddBuff_DetermineBuffTimeToAdd(On_Player.orig_AddBuff_DetermineBuffTimeToAdd orig, Player self, int type, int time1)
        {
            if (Main.debuff[type] && !BuffID.Sets.NurseCannotRemoveDebuff[type])
            {
                time1 = (int)(time1 * self.PlayerStats().DebuffTimeMultiplier);
            }
            else
            {
                time1 = (int)(time1 * self.PlayerStats().buffTimeMultiplier);
            }
            return orig(self, type, time1);
        }

        Item lastHeldItem = null;
        public bool hasSwappedItems = false;
        public uint TimesHeldWeaponHasBeenSwung = 0;
        public int ParryProjectile = -1;
        public bool TurnOffDownwardsMovementRestrictions = false;

        public float HealingMultiplier = 1f;
        public float PotionHealingMultiplier = 1f;
        public bool ManaPotionSickness = false;

        public float FeralCharge;
        public float FeralChargeMax;
        public float FeralChargeSpeed;
        public float MeleeWeaponScale = 0;
        public float ParryBuffDurationMultiplier = 1f;
        public float ParryImmunityDurationMultiplier = 1f;
        public int Tension = 0;
        public int TensionMax = 60;
        public int TensionMax2 = 60;
        public float TensionGainMultiplier = 1f;
        public float TensionCostMultiplier = 1f;

        public float SentrySpeedMultiplier = 0f;
        public float SentryRangeMultiplier = 0f;
        public float SentryDamageMultiplier = 0f;
        public float WrenchBuffTimeMultiplier = 1f;
        public bool CanThrowWrenches = false;

        public float KnockbackResist = 1f;
        public float ExtraWeaponPierceMultiplier = 1;
        public float DebuffTimeMultiplier = 1f;
        public float buffTimeMultiplier = 1f;
        public float NecromancerWeaponScale = 0;
        public float NecromancerChargeBonus = 1f;
        public float NecromancerAttackSpeed = 1f;
        public float AmmoSaveChance = 0;
        public float ThrowerDebuffPower = 1f;
        public float ThrowerGrabRange = 1f;
        public float ThrowerRecoveryChance = 0f;
        public float ThrowerVelocity = 1f;
        public float BowChargeTimeMultipler = 1f;

        public int MeleeFlatDamage = 0;
        public int RangedFlatDamage = 0;
        public int MagicFlatDamage = 0;
        public int SummonFlatDamage = 0;
        public int GenericFlatDamage = 0;

        public float GenericCritDamage = 0f;

        public byte SteelBladeHits;
        public static readonly float defaultFeralChargeSpeed = 0.66f / 60f;

        public bool newSwim;

        public Vector2 MouseWorld = Vector2.Zero;
        public bool LeftMouse = false;

        //Movement
        public float maxRunSpeedMultiplier = 1f;
        public float maxRunSpeedFlat = 0f;
        public override void ResetEffects()
        {
            if (TurnOffDownwardsMovementRestrictions)
            {
                Player.maxFallSpeed = 1000;
            }
            TurnOffDownwardsMovementRestrictions = false;

            if (ParryProjectile >= 0 && !Main.projectile[ParryProjectile].active)
                ParryProjectile = -1;

            ManaPotionSickness = false;
            HealingMultiplier = 1f;
            BowChargeTimeMultipler = 1f;
            ThrowerVelocity = 1f;
            ThrowerRecoveryChance = 0.2f;
            ThrowerGrabRange = 1f;
            ThrowerDebuffPower = 1f;
            AmmoSaveChance = 0;
            CanThrowWrenches = false;
            newSwim = false;
            FeralChargeMax = 0;
            FeralChargeSpeed = defaultFeralChargeSpeed;
            hasSwappedItems = false;
            MeleeWeaponScale = 0;

            Tension = Math.Clamp(Tension,0,TensionMax + TensionMax2);
            TensionMax2 = 0;

            DebuffTimeMultiplier = 1f;
            buffTimeMultiplier = 1f;

            NecromancerWeaponScale = 0;
            NecromancerChargeBonus = 1f;
            NecromancerAttackSpeed = 1f;

            MeleeFlatDamage = 0;
            RangedFlatDamage = 0;
            MagicFlatDamage = 0;
            SummonFlatDamage = 0;
            GenericFlatDamage = 0;

            GenericCritDamage = 0f;

            SentrySpeedMultiplier = 0f;
            SentryRangeMultiplier = 0f;
            SentryDamageMultiplier = 0f;
            KnockbackResist = 1f;
            ExtraWeaponPierceMultiplier = 1f;
            WrenchBuffTimeMultiplier = 1f;

            maxRunSpeedMultiplier = 1f;
            maxRunSpeedFlat = 0f;

            if (Player.HeldItem != lastHeldItem)
            {
                hasSwappedItems = true;
            }

            lastHeldItem = Player.HeldItem;

            if ((hasSwappedItems || !Player.controlUseItem) && Player.ItemAnimationEndingOrEnded)
                TimesHeldWeaponHasBeenSwung = 0;

            if (Main.LocalPlayer == Player) MouseWorld = Main.MouseWorld;
            //Main.NewText($"{Tension}" + "/" + $"{TensionMax + TensionMax2}");
        }

        public override void PostUpdateRunSpeeds()
        {
            Player.maxRunSpeed += maxRunSpeedFlat;
            Player.maxRunSpeed *= maxRunSpeedMultiplier;
            base.PostUpdateRunSpeeds();
        }
        public override void PostUpdateEquips()
        {
            FeralCharge += FeralChargeSpeed;

            if(FeralCharge > FeralChargeMax)
                FeralCharge = FeralChargeMax;
            if (Player.itemAnimation == 1 && Player.altFunctionUse != 2)
                FeralCharge = 0;
            Player.GetDamage(DamageClass.Melee) += (FeralCharge);

        }

        public override void UpdateLifeRegen()
        {
            if(Player.lifeRegenCount >= (int)(120f / HealingMultiplier))
            {
                Player.lifeRegenCount = 120;
            }
        }
        public override void PostUpdateMiscEffects()
        {
            Player.lifeRegen = (int)(Player.lifeRegen * HealingMultiplier);
        }
        public override bool CanConsumeAmmo(Item weapon, Item ammo)
        {
            if(Main.rand.NextFloat() < AmmoSaveChance)
                return false;

            return base.CanConsumeAmmo(weapon, ammo);
        }
        public override void ModifyItemScale(Item item, ref float scale)
        {
            if(item.DamageType == DamageClass.Melee)
                scale += MeleeWeaponScale;
            else if (item.ModItem is NecromancerScythe)
                scale += NecromancerWeaponScale;
        }
        public override float UseSpeedMultiplier(Item item)
        {
            if (item.ModItem is NecromancerScythe)
                return NecromancerAttackSpeed;
            else if (item.useAmmo == AmmoID.Arrow && ContentSamples.ProjectilesByType[item.shoot].ModProjectile is DrawnBowTemplate)
                return BowChargeTimeMultipler;
            else
                return base.UseSpeedMultiplier(item);
        }
        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            #region Flat Damage
            if (item.DamageType.CountsAsClass(DamageClass.Melee))
            {
                damage.Flat += MeleeFlatDamage;
            }
            else if (item.DamageType.CountsAsClass(DamageClass.Ranged))
            {
                damage.Flat += RangedFlatDamage;
            }
            else if (item.DamageType.CountsAsClass(DamageClass.Magic))
            {
                damage.Flat += MagicFlatDamage;
            }
            else if (item.DamageType.CountsAsClass(DamageClass.Summon))
            {
                damage.Flat += SummonFlatDamage;
            }

            damage.Flat += GenericFlatDamage;
            #endregion Flat Damage

            if (item.DamageType == DamageClass.Summon && item.sentry)
            {
                damage += SentryDamageMultiplier;
            }
        }
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (KnockbackResist <= 0)
            {
                Player.noKnockback = true;
            }
            modifiers.Knockback *= MathHelper.Clamp(KnockbackResist, 0, 10);
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.CritDamage += GenericCritDamage;
            base.ModifyHitNPC(target, ref modifiers);
        }
        public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (ItemSets.ThrowerWeapon[item.type])
            {
                velocity *= ThrowerVelocity;
            }
        }
        public override bool CanUseItem(Item item)
        {
            if (ManaPotionSickness && item.healMana > 0)
                return false;
            return base.CanUseItem(item);
        }
        public override bool FreeDodge(Player.HurtInfo info)
        {
            if (ParryProjectile == -1)
                return false;
            Entity e;
            info.DamageSource.TryGetCausingEntity(out e);

            if (Main.projectile[ParryProjectile].ModProjectile is MeleeParry p && e.Hitbox.Intersects(Main.projectile[ParryProjectile].Hitbox))
            {
                if (e is NPC npc)
                    p.OnParryNPC(npc, Player);
                else if (e is Projectile proj)
                    p.OnParryProjectile(proj, Player);
                return true;
            }
            return base.FreeDodge(info);
        }
        //public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        //{
        //    if (ParryProjectile == -1)
        //        return;
        //    if (Main.projectile[ParryProjectile].ModProjectile is MeleeParry p && npc.Hitbox.Intersects(Main.projectile[ParryProjectile].Hitbox))
        //    {
        //        p.OnParryNPC(npc);
        //    }
        //}
        //public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        //{
        //    if (ParryProjectile == -1)
        //        return;
        //    if (Main.projectile[ParryProjectile].ModProjectile is MeleeParry p && proj.Hitbox.Intersects(Main.projectile[ParryProjectile].Hitbox))
        //    {
        //        p.OnParryProjectile(proj);
        //    }
        //}
    }

    public static class PlayerMethods
    {
        /// <summary>
        /// Sends a packet containing the Player's MouseWorld position
        /// </summary>
        public static void SendMouseWorld(this Player player)
        {
            if (Main.netMode == NetmodeID.SinglePlayer) return; //Why are you trying to sync your mouse position in singleplayer?

            ModPacket packet = ModContent.GetInstance<Terrafirma>().GetPacket();
            packet.Write(NetSendIDs.syncCursor);
            packet.Write(player.whoAmI);
            packet.WriteVector2(Main.MouseWorld);
            packet.Send(-1, -1);
        }
    }
}
