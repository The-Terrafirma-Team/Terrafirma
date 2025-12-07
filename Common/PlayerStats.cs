using System;
using System.Collections.Generic;
using Terrafirma.Common.Attributes;
using Terrafirma.Common.Interfaces;
using Terrafirma.Common.Templates;
using Terrafirma.Utilities;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common
{
    public class PlayerStats : TFModPlayer
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
            else if(!Main.debuff[type])
            {
                time1 = (int)(time1 * self.PlayerStats().BuffTimeMultiplier);
            }
            return orig(self, type, time1);
        }
        [ResetDefaults(1f)]
        public float DebuffTimeMultiplier = 1f;
        [ResetDefaults(1f)]
        public float BuffTimeMultiplier = 1f;

        [ResetDefaults(1f)]
        public float SkillCastTime = 1f;
        [ResetDefaults(1f)]
        public float SkillCooldown = 1f;
        [ResetDefaults(1f)]
        public float SkillManaCost = 1f;
        [ResetDefaults(1f)]
        public float SkillTensionCost = 1f;

        [ResetDefaults(false)]
        public bool ItemUseBlocked = false;
        [ResetDefaults(false)]
        public bool TurnOffDownwardsMovementRestrictions = false;
        [ResetDefaults(false)]
        public bool ImmuneToContactDamage = false;
        [ResetDefaults(1f)]
        public float KnockbackResist = 1f;
        [ResetDefaults(1f)]
        public float MeleeWeaponScale = 1f;
        [ResetDefaults(0f)]
        public float AmmoSaveChance = 0f;
        /// <summary>
        /// This defaults to 0.5f, multiply it down to make the player slow down less in the air.
        /// </summary>
        [ResetDefaults(0.5f)]
        public float AirResistenceMultiplier = 0.5f;

        [ResetDefaults(8)]
        public int ParryDamage = 8;
        [ResetDefaults(1f)]
        public float ParryPower = 1f;
        // Tension
        public int Tension = 0;
        [ResetDefaults(50)]
        public int TensionMax = 50;
        [ResetDefaults(1f)]
        public float TensionGainMultiplier = 1f;
        [ResetDefaults(1f)]
        public float TensionCostMultiplier = 1f;

        internal bool RightMouseSwitch = false;
        public override void ResetEffects()
        {
            if (TurnOffDownwardsMovementRestrictions)
            {
                Player.maxFallSpeed = 1000;
            }
            base.ResetEffects();
            Player.pickSpeed *= 0.8f;
            Player.autoJump = true;
        }
        public override void PostUpdateMiscEffects()
        {
            Tension = Math.Clamp(Tension, 0, TensionMax);
        }
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (KnockbackResist <= 0)
                Player.noKnockback = true;
            modifiers.Knockback *= KnockbackResist;
        }
        public override bool CanUseItem(Item item)
        {
            if (ItemUseBlocked) return false;
            return base.CanUseItem(item);
        }
        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            if (!mediumCoreDeath)
            {
                Player.ConsumedManaCrystals = 2;
            }
            return base.AddStartingItems(mediumCoreDeath);
        }
        public override bool CanConsumeAmmo(Item weapon, Item ammo)
        {
            if (Main.rand.NextFloat() < AmmoSaveChance)
                return false;

            return base.CanConsumeAmmo(weapon, ammo);
        }
        public override void ModifyItemScale(Item item, ref float scale)
        {
            if (item.DamageType.CountsAsClass(DamageClass.Melee))
                scale *= MeleeWeaponScale;
        }
        public override bool HoverSlot(Item[] inventory, int context, int slot)
        {
            if (Main.mouseRight && !RightMouseSwitch)
            {
                if (Main.mouseItem.ModItem is IUseOnItemInInventoryItem item)
                {
                    if (item.canBeUsedOnThisItem(Player, Main.mouseItem, inventory[slot], context))
                        item.useOnItem(Player, Main.mouseItem, inventory[slot], context);
                }
                RightMouseSwitch = true;
            }
            if (!Main.mouseRight) RightMouseSwitch = false;
            return base.HoverSlot(inventory, context, slot);
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (((Player.controlRight && Player.velocity.X > 0) || (Player.controlLeft && Player.velocity.X < 0)) && Player.velocity.Y != 0)
                Player.runSlowdown *= AirResistenceMultiplier;
        }
    }
}
