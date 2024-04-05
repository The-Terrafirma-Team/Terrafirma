using Microsoft.Xna.Framework;
using Terrafirma.Data;
using Terrafirma.Systems.Elements;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Global.Players
{
    public class PlayerStats : ModPlayer
    {
        public float SentrySpeedMultiplier = 0f;
        public float SentryRangeMultiplier = 0f;
        public float WrenchBuffTimeMultiplier = 1f;
        public float SwarmSpeedMultiplier = 1f;
        public float KnockbackResist = 1f;
        public float ExtraWeaponPierceMultiplier = 1;

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

        public int MeleeFlatDamage = 0;
        public int RangedFlatDamage = 0;
        public int MagicFlatDamage = 0;
        public int SummonFlatDamage = 0;
        public override void ResetEffects()
        {
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
        }
        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            #region Elemental Damage
            if (FireDamage != 1 && AddElementsToVanillaContent.fireItem.Contains(item.type))
            {
                damage *= FireDamage;
            }
            if (WaterDamage != 1 && AddElementsToVanillaContent.waterItem.Contains(item.type))
            {
                damage *= WaterDamage;
            }
            if (EarthDamage != 1 && AddElementsToVanillaContent.earthItem.Contains(item.type))
            {
                damage *= EarthDamage;
            }
            if (AirDamage != 1 && AddElementsToVanillaContent.airItem.Contains(item.type))
            {
                damage *= AirDamage;
            }
            if (LightDamage != 1 && AddElementsToVanillaContent.lightItem.Contains(item.type))
            {
                damage *= LightDamage;
            }
            if (DarkDamage != 1 && AddElementsToVanillaContent.darkItem.Contains(item.type))
            {
                damage *= DarkDamage;
            }
            if (IceDamage != 1 && AddElementsToVanillaContent.iceItem.Contains(item.type))
            {
                damage *= IceDamage;
            }
            if (PoisonDamage != 1 && AddElementsToVanillaContent.poisonItem.Contains(item.type))
            {
                damage *= PoisonDamage;
            }
            if (ElectricDamage != 1 && AddElementsToVanillaContent.electricItem.Contains(item.type))
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
