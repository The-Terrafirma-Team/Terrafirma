using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using Terrafirma.Buffs.Buffs;
using Terrafirma.Common.Structs;
using Terrafirma.Common.Templates;
using Terrafirma.Common.Templates.Melee;
using Terrafirma.Data;
using Terrafirma.Projectiles.Summon.Sentry.PreHardmode;
using Terrafirma.Systems.MageClass.ManaTypes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

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

        public int SpiritCrystals = 0;

        Item lastHeldItem = null;
        public bool hasSwappedItems = false;
        public uint TimesHeldWeaponHasBeenSwung = 0;
        public int ParryProjectile = -1;
        public bool TurnOffDownwardsMovementRestrictions = false;
        public float EnemySpawnRateMultiplier = 1f;
        public float MaxEnemySpawnMultiplier = 1f;

        public float HealingMultiplier = 1f;
        public float PotionHealingMultiplier = 1f;
        public float OutgoingHealingMultiplier = 1f;
        public bool ManaPotionSickness = false;

        public float StoredMeleeCharge = 0f;
        public float FeralCharge;
        public float FeralChargeMax;
        public float FeralChargeSpeed;
        public float MeleeWeaponScale = 0;
        public float ParryBuffDurationMultiplier = 1f;
        public float ParryImmunityDurationMultiplier = 1f;
        public float ParryDurationMultiplier = 1f;
        public int Tension = 0;
        public int TensionMax = 50;
        public int TensionMax2 = 0;
        public float TensionGainMultiplier = 1f;
        public float TensionCostMultiplier = 1f;
        public int FlatTensionGain = 0;
        public int FlatTensionCost = 0;
        public bool Whiffed = false;

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

        public static readonly float defaultFeralChargeSpeed = 0.66f / 60f;

        public bool newSwim;

        public Vector2 MouseWorld = Vector2.Zero;
        public bool LeftMouse = false;

        //Movement
        public float maxRunSpeedMultiplier = 1f;
        public float maxRunSpeedFlat = 0f;

        //Mana Types
        public Dictionary<ManaType, NumberRange> playerManaTypes = new Dictionary<ManaType, NumberRange>();
        public bool manaUsed = false;
        public bool consumeManaType = true;

        public override void OnConsumeMana(Item item, int manaConsumed)
        {
            manaUsed = true;
        }

        public override void PreUpdateBuffs()
        {
            //Mana Types
            if (Player.statMana == 0 || (!Player.CheckMana(Player.HeldItem.mana))) playerManaTypes.Clear();

            for (int i = playerManaTypes.Count - 1; i >= 0; i--)
            {
                playerManaTypes.Keys.ToArray()[i].TickEffect(Player, playerManaTypes[playerManaTypes.Keys.ToArray()[i]]);

                //Use Effect
                if (playerManaTypes.Values.ToArray()[i].ContainsInt(Player.statMana) && manaUsed)
                {
                    playerManaTypes.Keys.ToArray()[i].UseEffect(Player, playerManaTypes[playerManaTypes.Keys.ToArray()[i]]);
                }
                //Not in Use Effect
                else if (!manaUsed && !Player.ItemAnimationActive) playerManaTypes.Keys.ToArray()[i].NotInUseEffect(Player, playerManaTypes[playerManaTypes.Keys.ToArray()[i]]);

                //Consume when mana is used
                if (playerManaTypes.Values.ToArray()[i].end > Player.statMana && consumeManaType && playerManaTypes.Keys.ToArray()[i].consumable)
                {
                    playerManaTypes[playerManaTypes.Keys.ToArray()[i]] = new NumberRange(playerManaTypes.Values.ToArray()[i].start, Player.statMana);
                }
                //Remove when end and start are the same of if it has been completely consumed
                if ((playerManaTypes.Values.ToArray()[i].start > Player.statMana && consumeManaType && playerManaTypes.Keys.ToArray()[i].consumable) ||
                    playerManaTypes.Values.ToArray()[i].start == playerManaTypes.Values.ToArray()[i].end)
                {
                    playerManaTypes.Remove(playerManaTypes.Keys.ToArray()[i]);
                }
            }
        }

        public override void ResetEffects()
        {
            EnemySpawnRateMultiplier = 1f;
            MaxEnemySpawnMultiplier = 1f;

            ParryBuffDurationMultiplier = 1f;
            ParryImmunityDurationMultiplier = 1f;
            ParryDurationMultiplier = 1f;
            Whiffed = false;
            if (StoredMeleeCharge > 1)
            {
                StoredMeleeCharge = 1;
            }
            if (TurnOffDownwardsMovementRestrictions)
            {
                Player.maxFallSpeed = 1000;
            }
            TurnOffDownwardsMovementRestrictions = false;

            if (ParryProjectile >= 0 && !Main.projectile[ParryProjectile].active)
                ParryProjectile = -1;

            ManaPotionSickness = false;
            HealingMultiplier = 1f;
            OutgoingHealingMultiplier = 1f;
            PotionHealingMultiplier = 1f;
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

            TensionMax2 = TensionMax;
            Tension = Math.Clamp(Tension, 0, TensionMax2);
            TensionGainMultiplier = 1f;
            TensionCostMultiplier = 1f;
            FlatTensionGain = 0;
            FlatTensionCost = 0;

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
            if (manaUsed && Player.ItemAnimationEndingOrEnded)
            {
                manaUsed = false;
            }
            consumeManaType = false;
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

            if (FeralCharge > FeralChargeMax)
                FeralCharge = FeralChargeMax;
            if (Player.itemAnimation == 1 && Player.altFunctionUse != 2)
                FeralCharge = 0;
            Player.GetDamage(DamageClass.Melee) += (FeralCharge);

        }

        public override void UpdateLifeRegen()
        {
            if (Player.lifeRegenCount >= (int)(120f / HealingMultiplier))
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
            if (Main.rand.NextFloat() < AmmoSaveChance)
                return false;

            return base.CanConsumeAmmo(weapon, ammo);
        }
        public override void ModifyItemScale(Item item, ref float scale)
        {
            if (item.ModItem is TowerShield)
            {
                scale -= scale * 0.25f;
            }
            if (item.DamageType == DamageClass.Melee)
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
            modifiers.DamageVariationScale *= 0;
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

                if (Player.GetModPlayer<ShockingPlayer>().active)
                {
                    Projectile.NewProjectile(Player.GetSource_Buff(Player.GetModPlayer<ShockingPlayer>().index), Player.Center, Vector2.Zero, ModContent.ProjectileType<ShockPotionLightning>(), info.SourceDamage / 3, 0, Player.whoAmI, 0, -1);
                }

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
        public override void SaveData(TagCompound tag)
        {
            tag["Terrafirma:TensionMax"] = TensionMax;
            tag["Terrafirma:SpiritCrystals"] = SpiritCrystals;
        }
        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("Terrafirma:TensionMax"))
            {
                TensionMax = tag.Get<int>("Terrafirma:TensionMax");
            }
            if (tag.ContainsKey("Terrafirma:SpiritCrystals"))
            {
                SpiritCrystals = tag.Get<int>("Terrafirma:SpiritCrystals");
            }
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

        public static void AddManaType(this Player player, ManaType type, float startPercent, float endPercent)
        {
            Dictionary<ManaType, NumberRange> manaTypes = player.PlayerStats().playerManaTypes;

            int convertedStart = (int)(startPercent * player.statManaMax);
            int convertedEnd = (int)(endPercent * player.statManaMax);

            if (convertedStart >= player.statMana && convertedEnd >= player.statMana) return;

            for (int i = manaTypes.Count - 1; i >= 0; i--)
            {
                //Remove mana type is the new one consumes the old one
                if (new NumberRange(convertedStart, convertedEnd).ContainsRange(manaTypes.Values.ToArray()[i]))
                {
                    manaTypes.Remove(manaTypes.Keys.ToArray()[i]);
                }
            }

            bool overlap = false;
            for (int i = manaTypes.Count - 1; i >= 0; i--)
            {

                //Shrink the end if it overlaps with the start of the new type
                if (manaTypes.Values.ToArray()[i].ContainsInt(convertedStart))
                {
                    manaTypes[manaTypes.Keys.ToArray()[i]] = new NumberRange(manaTypes.Values.ToArray()[i].start, convertedStart);
                }
                //Shrink the start if it overlaps with the end of the new type
                else if (manaTypes.Values.ToArray()[i].ContainsInt(convertedEnd))
                {
                    manaTypes[manaTypes.Keys.ToArray()[i]] = new NumberRange(convertedEnd, manaTypes.Values.ToArray()[i].end);
                }
                //Merge the same types as one if they overlap
                if (manaTypes.Keys.ToArray()[i].GetType() == type.GetType() &&
                    (manaTypes.Values.ToArray()[i].ContainsInt(convertedStart) || manaTypes.Values.ToArray()[i].ContainsInt(convertedEnd)))
                {
                    overlap = true;
                    manaTypes[manaTypes.Keys.ToArray()[i]] = new NumberRange(Math.Min(convertedStart, manaTypes.Values.ToArray()[i].start), Math.Max(convertedEnd, manaTypes.Values.ToArray()[i].end));
                }
            }
            if (!overlap) manaTypes.Add(type, new NumberRange(convertedStart, Math.Min(convertedEnd, player.statMana)));

        }

        public static void AddManaType(this Player player, ManaType type)
        {
            player.PlayerStats().playerManaTypes.Clear();
            player.PlayerStats().playerManaTypes.Add(type, new NumberRange(0, player.statManaMax));
        }

        public static void ResetManaTypes(this Player player)
        {
            player.PlayerStats().playerManaTypes.Clear();
        }

        /// <summary>
        /// Return the current mana type that player can use, returns null if there's none
        /// </summary>
        public static ManaType GetCurrentManaType(this Player player)
        {
            Dictionary<ManaType, NumberRange> playerManaTypes = player.PlayerStats().playerManaTypes;
            for (int i = 0; i < playerManaTypes.Count; i++)
            {
                if (playerManaTypes.Values.ToArray()[i].ContainsInt(player.statMana)) return playerManaTypes.Keys.ToArray()[i];
            }
            return null;
        }
    }
}
