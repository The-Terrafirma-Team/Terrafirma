using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Interfaces;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common
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
            else if(!Main.debuff[type])
            {
                time1 = (int)(time1 * self.PlayerStats().BuffTimeMultiplier);
            }
            return orig(self, type, time1);
        }

        public float DebuffTimeMultiplier = 1f;
        public float BuffTimeMultiplier = 1f;

        public float SkillCastTime = 1f;
        public float SkillCooldown = 1f;
        public float SkillManaCost = 1f;
        public float SkillTensionCost = 1f;

        public bool ItemUseBlocked = false;
        public bool TurnOffDownwardsMovementRestrictions = false;
        public bool ImmuneToContactDamage = false;
        public float KnockbackResist = 1f;
        public float MeleeWeaponScale = 0f;
        public float AmmoSaveChance = 0f;
        /// <summary>
        /// This defaults to 0.5f, multiply it down to make the player slow down less in the air.
        /// </summary>
        public float AirResistenceMultiplier = 0.5f;

        public int ParryDamage = 0;
        public float ParryPower = 1f;
        // Tension
        public int Tension = 50;
        /// <summary>
        /// The base tension max. Buffs and things should change TensionMax2 instead.
        /// </summary>
        public int TensionMax = 50;
        public int TensionMax2 = 0;
        public float TensionGainMultiplier = 1f;
        public float TensionCostMultiplier = 1f;
        public int FlatTensionGain = 0;
        public int FlatTensionCost = 0;

        internal bool RightMouseSwitch = false;
        public override void ResetEffects()
        {
            SkillCastTime = 1f;
            SkillCooldown = 1f;
            SkillManaCost = 1f;
            SkillTensionCost = 1f;

            DebuffTimeMultiplier = 1f;
            BuffTimeMultiplier = 1f;
            MeleeWeaponScale = 0f;
            AmmoSaveChance = 0f;

            if (TurnOffDownwardsMovementRestrictions)
            {
                Player.maxFallSpeed = 1000;
            }
            TurnOffDownwardsMovementRestrictions = false;
            ImmuneToContactDamage = false;
            KnockbackResist = 1f;
            AirResistenceMultiplier = 0.5f;
            Player.pickSpeed *= 0.8f;
            Player.autoJump = true;

            ItemUseBlocked = false;

            ParryDamage = 8;
            ParryPower = 1f;

            Tension = Math.Clamp(Tension, 0, TensionMax2);
            TensionMax2 = TensionMax;
            TensionGainMultiplier = 1f;
            TensionCostMultiplier = 1f;
            FlatTensionGain = 0;
            FlatTensionCost = 0;
        }
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
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
                scale += MeleeWeaponScale;
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
