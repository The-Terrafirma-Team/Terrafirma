using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Terrafirma.Common.Interfaces;
using Terrafirma.Content.Buffs.Cooldowns;
using Terrafirma.Content.Buffs.Debuffs;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.Player;

namespace Terrafirma.Common.Mechanics
{
    public class BlockingPlayer : ModPlayer
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<ServerConfig>().CombatReworkEnabled;
        }
        public static ModKeybind BlockKey { get; set; }
        public override void Load()
        {
            BlockKey = KeybindLoader.RegisterKeybind(Mod, "BlockKey", "Mouse3");
        }
        public override void Unload()
        {
            BlockKey = null;
        }

        public bool Shattered = false;
        public bool Blocking = false;
        public int BlockConsumeTensionTimer = 0;
        public float blockAmount = 0;
        public override void ResetEffects()
        {
            Shattered = false;
        }

        public bool CanBlock(Player player)
        {
            if (!player.ItemAnimationActive && !Shattered && player.PlayerStats().Tension > 0)
            {
                return true;
            }
            return false;
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (BlockKey.Current && CanBlock(Player))
            {
                blockAmount = MathHelper.Lerp(blockAmount, 1f, 0.5f);
                Blocking = true;
                BlockConsumeTensionTimer++;
                if(BlockConsumeTensionTimer > 5)
                {
                    BlockConsumeTensionTimer = 0;
                    Player.PlayerStats().Tension -= 1;
                }
            }
            else
            {
                blockAmount = MathHelper.Lerp(blockAmount, 0f, 0.3f);
                Blocking = false;
            }
            if (Player.ItemAnimationActive || (!Blocking && blockAmount < 0.1f))
            {
                blockAmount = 0;
            }
        }
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            if (blockAmount > 0)
            {
                CompositeArmStretchAmount amt = CompositeArmStretchAmount.ThreeQuarters;

                drawInfo.drawPlayer.SetCompositeArmFront(true, amt, Player.direction * -2 * blockAmount);
                drawInfo.drawPlayer.SetCompositeArmBack(true, amt, Player.direction * -2.3f * blockAmount);
            }
        }
        public override bool FreeDodge(HurtInfo info)
        {
            int tensionCost = 10;
            if (Blocking)
            {
                if (!Player.CheckTension(tensionCost))
                {
                    Player.AddBuff(ModContent.BuffType<Shattered>(), 60 * 10);
                    CombatText.NewText(Player.Hitbox, Color.LightGray, "Shattered!", true);
                    Player.PlayerStats().Tension = 0;
                }
                Player.immune = true;
                Player.AddImmuneTime(ImmunityCooldownID.General, 60 * 1);
                SoundEngine.PlaySound(SoundID.Item37, Player.position);
                
                info.DamageSource.TryGetCausingEntity(out var causingEntity);
                if (causingEntity is NPC n)
                {
                    Player.ParryStrike(n);
                }
                else if(causingEntity is Projectile p && p.ModProjectile is ICustomBlockBehavior i)
                {
                    i.OnBlocked(Player, Player.PlayerStats().ParryPower);
                }
                return true;
            }
            return base.FreeDodge(info);
        }
    }
}
