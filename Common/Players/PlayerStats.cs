using Microsoft.Xna.Framework;
using Terrafirma.Data;
using Terrafirma.Systems.Elements;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Terrafirma.Common.Players
{
    public class PlayerStats : ModPlayer
    {
        Item lastHeldItem = null;
        public bool hasSwappedItems = false;
        public uint TimesHeldWeaponHasBeenSwung = 0;

        public float SentrySpeedMultiplier = 0f;
        public float SentryRangeMultiplier = 0f;
        public float WrenchBuffTimeMultiplier = 1f;
        public float SwarmSpeedMultiplier = 1f;
        public float KnockbackResist = 1f;
        public float ExtraWeaponPierceMultiplier = 1;

        // Elemental
        public float ElementalDamageVariance = 1f;
        public float FireDamage = 1f;
        public float WaterDamage = 1f;
        public float EarthDamage = 1f;
        public float AirDamage = 1f;
        public float LightDamage = 1f;
        public float DarkDamage = 1f;
        public float IceDamage = 1f;
        public float PoisonDamage = 1f;
        public float ElectricDamage = 1f;
        public float ArcaneDamage = 1f;

        public bool FireEnhancement;
        public bool WaterEnhancement;
        public bool EarthEnhancement;
        public bool AirEnhancement;
        public bool LightEnhancement;
        public bool DarkEnhancement;
        public bool IceEnhancement;
        public bool PoisonEnhancement;
        public bool ElectricEnhancement;
        public bool ArcaneEnhancement;

        public int MeleeFlatDamage = 0;
        public int RangedFlatDamage = 0;
        public int MagicFlatDamage = 0;
        public int SummonFlatDamage = 0;

        public override void ResetEffects()
        {
            hasSwappedItems = false;

            ElementalDamageVariance = 1f;
            FireDamage = 1;
            WaterDamage = 1;
            EarthDamage = 1;
            AirDamage = 1;
            LightDamage = 1;
            DarkDamage = 1;
            IceDamage = 1;
            PoisonDamage = 1;
            ElectricDamage = 1;
            ArcaneDamage = 1;

            FireEnhancement = false;
            WaterEnhancement = false;
            EarthEnhancement = false;
            AirEnhancement = false;
            LightEnhancement = false;
            DarkEnhancement = false;
            IceEnhancement = false;
            PoisonEnhancement = false;
            ElectricEnhancement = false;
            ArcaneEnhancement = false;

            MeleeFlatDamage = 0;
            RangedFlatDamage = 0;
            MagicFlatDamage = 0;
            SummonFlatDamage = 0;

            SentrySpeedMultiplier = 0f;
            SentryRangeMultiplier = 0f;
            SwarmSpeedMultiplier = 1f;
            KnockbackResist = 1f;
            ExtraWeaponPierceMultiplier = 1f;
            WrenchBuffTimeMultiplier = 1f;

            if(Player.HeldItem != lastHeldItem)
                hasSwappedItems = true;

            lastHeldItem = Player.HeldItem;

            if (hasSwappedItems || !Player.controlUseItem)
                TimesHeldWeaponHasBeenSwung = 0;
        }
        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            #region Elemental Damage
            if (item.GetElementItem().elementData.Fire)
            {
                damage *= FireDamage;
            }
            if (item.GetElementItem().elementData.Water)
            {
                damage *= WaterDamage;
            }
            if (item.GetElementItem().elementData.Earth)
            {
                damage *= EarthDamage;
            }
            if (item.GetElementItem().elementData.Air)
            {
                damage *= AirDamage;
            }
            if (item.GetElementItem().elementData.Light)
            {
                damage *= LightDamage;
            }
            if (item.GetElementItem().elementData.Dark)
            {
                damage *= DarkDamage;
            }
            if (item.GetElementItem().elementData.Ice)
            {
                damage *= IceDamage;
            }
            if (item.GetElementItem().elementData.Toxin)
            {
                damage *= PoisonDamage;
            }
            if (item.GetElementItem().elementData.Electric)
            {
                damage *= ElectricDamage;
            }
            #endregion Elemental Damage
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
        public override float UseSpeedMultiplier(Item item)
        {
            if (ItemSets.isSwarmSummonItem[item.type])
            {
                return SwarmSpeedMultiplier;
            }
            return base.UseSpeedMultiplier(item);
        }

    }
}
