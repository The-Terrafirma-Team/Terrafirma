using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Data;
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

        public int MeleeFlatDamage = 0;
        public int RangedFlatDamage = 0;
        public int MagicFlatDamage = 0;
        public int SummonFlatDamage = 0;
        public override void ResetEffects()
        {
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
