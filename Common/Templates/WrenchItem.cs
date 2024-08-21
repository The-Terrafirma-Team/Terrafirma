using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Players;
using Terrafirma.Particles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common.Templates
{
    public abstract class WrenchItem : ModItem
    {
        public static void checkForSentriesToHitWithWrenchAndApplyBuff(Player player, WrenchItem wrench, Rectangle hitbox)
        {
            foreach(Projectile sentry in Main.ActiveProjectiles)
            {
                if (sentry.sentry && sentry.Hitbox.Intersects(hitbox))
                {
                    if(sentry.GetGlobalProjectile<SentryStats>().BuffTime < (int)(wrench.BuffDuration * player.GetModPlayer<PlayerStats>().WrenchBuffTimeMultiplier) - player.HeldItem.useTime) 
                    {
                        if (Main.netMode == NetmodeID.SinglePlayer) ApplySentryBuff(sentry, wrench.Buff, (int)(wrench.BuffDuration * player.PlayerStats().WrenchBuffTimeMultiplier));
                        SyncSentryBuff(sentry, wrench.Buff, (int)(wrench.BuffDuration * player.PlayerStats().WrenchBuffTimeMultiplier));
                    }
                }
            }
        }
        public SentryBuff Buff = null;
        public int BuffDuration = 0;

        public static void ApplySentryBuff(Projectile proj, SentryBuff buff, int duration, bool doeffects = true)
        {
            SoundEngine.PlaySound(SoundID.Item37, proj.position);
            proj.netUpdate = true;

            BigSparkle bigsparkle = new BigSparkle();
            bigsparkle.fadeInTime = 6;
            bigsparkle.Rotation = Main.rand.NextFloat(-0.4f, 0.4f);
            bigsparkle.Scale = 3f;
            ParticleSystem.AddParticle(bigsparkle, proj.Hitbox.ClosestPointInRect(proj.Center), Vector2.Zero, new Color(1f, 1f, 0.6f, 0f) * 0.3f);
            proj.GetGlobalProjectile<SentryStats>().BuffType = buff;
            proj.GetGlobalProjectile<SentryStats>().BuffTime = duration;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            checkForSentriesToHitWithWrenchAndApplyBuff(player, this, hitbox);
        }
        public override bool MeleePrefix()
        {
            return true;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            player.MinionAttackTargetNPC = target.whoAmI;
            base.OnHitNPC(player, target, hit, damageDone);
        }

        public static void SyncSentryBuff(Projectile proj, SentryBuff buff, int BuffDuration)
        {
            if (Main.netMode == NetmodeID.SinglePlayer) return; //Why are you trying to sync a sentry buff in singleplayer?

            ModPacket packet = ModContent.GetInstance<Terrafirma>().GetPacket();
            packet.Write(NetSendIDs.syncSentryBuff);
            packet.Write(proj.identity);
            packet.Write(buff.ID);
            packet.Write(BuffDuration);
            packet.Send(-1, -1);
        }
    }
}
