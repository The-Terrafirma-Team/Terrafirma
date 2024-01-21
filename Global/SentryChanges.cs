using Terraria.ModLoader;
using Terraria;
using Terraria.ModLoader.IO;
using System.IO;
using Terraria.ID;
using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;

namespace TerrafirmaRedux.Global
{
    public class SentryBuffID
    {
        public const int MetalWrench = 0;
        public const int DemoniteWrench = 1;
        public const int CrimtaneWrench = 2;
    }
    public class SentryChanges : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.sentry;
        }

        public int[] BuffTime = new int[3];

        public float SpeedMultiplier = 1f;
        public float RangeMultiplier = 1f;
        public float DamageMultiplier = 1f;
        void ResetEffects(Projectile entity)
        {
            SpeedMultiplier = 1f;
            RangeMultiplier = 1f;
            DamageMultiplier = 1f;
        }
        public void UpdateBuffs(Projectile projectile)
        {
            for (int i = 0; i < BuffTime.Length; i++)
            {
                if (BuffTime[i] > 0)
                    BuffTime[i]--;
            }
            if (BuffTime[SentryBuffID.MetalWrench] > 0)
            {
                SpeedMultiplier -= 0.3f;
                if (Main.rand.NextBool(5))
                {
                    Dust d = Dust.NewDustDirect(projectile.BottomLeft + new Vector2(0,-4), projectile.width, 0, DustID.GemDiamond, 0, -projectile.height / 20);
                    d.noGravity = true;
                    d.velocity.X *= 0.1f;
                    d.noLightEmittence = true;
                    if (d.velocity.Y > 0)
                        d.velocity *= -1;
                }
            }
            if (BuffTime[SentryBuffID.DemoniteWrench] > 0 && Main.rand.NextBool(5))
            {
                Dust d = Dust.NewDustDirect(projectile.BottomLeft + new Vector2(0, -4), projectile.width, 0, DustID.Shadowflame, 0, -projectile.height / 10);
                d.noGravity = true;
                d.velocity.X *= 0.1f;
                d.scale *= 1.5f;
                d.alpha = 128;
                if (d.velocity.Y > 0)
                    d.velocity *= -1;
            }
            if (BuffTime[SentryBuffID.CrimtaneWrench] > 0 && Main.rand.NextBool(5))
            {
                DamageMultiplier += 0.15f;

                Dust d = Dust.NewDustDirect(projectile.BottomLeft + new Vector2(0, -4), projectile.width, 0, DustID.Crimson, 0, -projectile.height / 10);
                d.noGravity = true;
                d.velocity.X *= 0.1f;
                d.scale *= 1.5f;
                d.alpha = 128;
                if (d.velocity.Y > 0)
                    d.velocity *= -1;
            }
        }
        public override bool PreAI(Projectile projectile)
        {
            ResetEffects(projectile);
            UpdateBuffs(projectile);
            return base.PreAI(projectile);
        }
        public override void SendExtraAI(Projectile projectile, BitWriter bitWriter, BinaryWriter binaryWriter)
        {
            for (int i = 0; i < BuffTime.Length; i++)
            {
                binaryWriter.Write(BuffTime[i]);
            }
        }
        public override void ReceiveExtraAI(Projectile projectile, BitReader bitReader, BinaryReader binaryReader)
        {
            for (int i = 0; i < BuffTime.Length; i++)
            {
                binaryReader.ReadInt32();
            }
        }
    }
    public class SentryBulletBuff : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public bool Demonite = false;
        public override void PostAI(Projectile projectile)
        {
            if (Demonite)
            {
                Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Shadowflame, projectile.velocity.X, projectile.velocity.Y);
                d.noGravity = true;
                d.fadeIn = Main.rand.NextFloat(2);
                d.alpha = 128;
            }
        }
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Demonite && Main.rand.NextBool(2))
            {
                target.AddBuff(BuffID.ShadowFlame,60 * 3);
            }
        }
        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
        {
            base.OnHitPlayer(projectile, target, info);
        }
    }
}
