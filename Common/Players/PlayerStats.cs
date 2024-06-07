using Microsoft.Xna.Framework;
using System;
using Terrafirma.Common.Templates;
using Terrafirma.Data;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common.Players
{
    public class PlayerStats : ModPlayer
    {
        Item lastHeldItem = null;
        public bool hasSwappedItems = false;
        public uint TimesHeldWeaponHasBeenSwung = 0;

        public float FeralCharge;
        public float FeralChargeMax;
        public float FeralChargeSpeed;

        public float SentrySpeedMultiplier = 0f;
        public float SentryRangeMultiplier = 0f;
        public float WrenchBuffTimeMultiplier = 1f;
        public float KnockbackResist = 1f;
        public float ExtraWeaponPierceMultiplier = 1;
        public float MeleeWeaponScale = 0;
        public float NecromancerWeaponScale = 0;
        public float NecromancerChargeBonus = 1f;
        public float NecromancerSwingSpeed = 1f;
        public float AmmoSaveChance;

        public int MeleeFlatDamage = 0;
        public int RangedFlatDamage = 0;
        public int MagicFlatDamage = 0;
        public int SummonFlatDamage = 0;

        public byte SteelBladeHits;
        public static readonly float defaultFeralChargeSpeed = 0.66f / 60f;

        public bool newSwim;
        public override void ResetEffects()
        {
            newSwim = false;
            FeralChargeMax = 0;
            FeralChargeSpeed = defaultFeralChargeSpeed;
            hasSwappedItems = false;
            MeleeWeaponScale = 0;
            NecromancerWeaponScale = 0;
            NecromancerChargeBonus = 1f;
            NecromancerSwingSpeed = 1f;

            MeleeFlatDamage = 0;
            RangedFlatDamage = 0;
            MagicFlatDamage = 0;
            SummonFlatDamage = 0;

            SentrySpeedMultiplier = 0f;
            SentryRangeMultiplier = 0f;
            KnockbackResist = 1f;
            ExtraWeaponPierceMultiplier = 1f;
            WrenchBuffTimeMultiplier = 1f;

            if (Player.HeldItem != lastHeldItem)
            {
                hasSwappedItems = true;
            }

            lastHeldItem = Player.HeldItem;

            if (hasSwappedItems || !Player.controlUseItem)
                TimesHeldWeaponHasBeenSwung = 0;
        }
        public override void PostUpdateEquips()
        {
            FeralCharge += FeralChargeSpeed;

            if(FeralCharge > FeralChargeMax)
                FeralCharge = FeralChargeMax;
            if (Player.itemAnimation == 1)
                FeralCharge = 0;
            Player.GetDamage(DamageClass.Melee) += (FeralCharge);
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
                return NecromancerSwingSpeed;
            else
                return base.UseSpeedMultiplier(item);
        }
        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            #region Flat Damage
            if (item.DamageType == DamageClass.Melee || item.DamageType == DamageClass.MeleeNoSpeed)
            {
                damage.Flat += MeleeFlatDamage;
            }
            else if (item.DamageType == DamageClass.Ranged)
            {
                damage.Flat += RangedFlatDamage;
            }
            else if (item.DamageType == DamageClass.Magic)
            {
                damage.Flat += MagicFlatDamage;
            }
            else if (item.DamageType == DamageClass.Summon)
            {
                damage.Flat += SummonFlatDamage;
            }
            #endregion Flat Damage
        }
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (KnockbackResist <= 0)
            {
                Player.noKnockback = true;
            }
            modifiers.Knockback *= MathHelper.Clamp(KnockbackResist, 0, 10);
        }
    }
}
