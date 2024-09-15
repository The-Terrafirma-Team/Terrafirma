using Microsoft.Xna.Framework;
using System;
using System.IO;
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

        public float FeralCharge;
        public float FeralChargeMax;
        public float FeralChargeSpeed;

        public float SentrySpeedMultiplier = 0f;
        public float SentryRangeMultiplier = 0f;
        public float SentryDamageMultiplier = 0f;
        public float WrenchBuffTimeMultiplier = 1f;
        public bool CanThrowWrenches = false;

        public float KnockbackResist = 1f;
        public float ExtraWeaponPierceMultiplier = 1;
        public float MeleeWeaponScale = 0;
        public float DebuffTimeMultiplier = 1f;
        public float buffTimeMultiplier = 1f;
        public float NecromancerWeaponScale = 0;
        public float NecromancerChargeBonus = 1f;
        public float NecromancerSwingSpeed = 1f;
        public float AmmoSaveChance = 0;

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
        public override void ResetEffects()
        {
            AmmoSaveChance = 0;
            CanThrowWrenches = false;
            newSwim = false;
            FeralChargeMax = 0;
            FeralChargeSpeed = defaultFeralChargeSpeed;
            hasSwappedItems = false;
            MeleeWeaponScale = 0;

            DebuffTimeMultiplier = 1f;
            buffTimeMultiplier = 1f;

            NecromancerWeaponScale = 0;
            NecromancerChargeBonus = 1f;
            NecromancerSwingSpeed = 1f;

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

            if (Player.HeldItem != lastHeldItem)
            {
                hasSwappedItems = true;
            }

            lastHeldItem = Player.HeldItem;

            if (hasSwappedItems || !Player.controlUseItem)
                TimesHeldWeaponHasBeenSwung = 0;

            if (Main.LocalPlayer == Player) MouseWorld = Main.MouseWorld;
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
        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            base.DrawEffects(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);
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

            damage.Flat += GenericFlatDamage;
            #endregion Flat Damage

            if (item.DamageType == DamageClass.Summon && item.sentry)
            {
                damage.Flat += (item.damage * (SentryDamageMultiplier + 1f)) - item.OriginalDamage;
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
