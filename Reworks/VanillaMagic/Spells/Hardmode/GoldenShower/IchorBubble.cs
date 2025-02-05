using Microsoft.Xna.Framework;
using System;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.Hardmode.GoldenShower
{
    internal class IchorBubble : Spell
    {
        public override int UseAnimation => 40;
        public override int UseTime => 40;
        public override int ManaCost => 7;
        public override int[] SpellItem => new int[] { ItemID.GoldenShower };

        public override bool OverrideSoundstyle => true;
        public override SoundStyle? UseSound => SoundID.NPCDeath19;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            velocity *= 0.45f;
            damage *= 3;
            type = ModContent.ProjectileType<IchorBubbleProj>();

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
    public class IchorBubbleProj : ModProjectile
    {
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(1f, 1f + (float)Math.Sin(Projectile.timeLeft * 0.14f) * 0.3f, 1f + (float)Math.Sin(Projectile.timeLeft * 0.12f) * 0.3f, 0.4f) * (0.8f + (float)Math.Sin(Projectile.timeLeft * 0.1f) * 0.2f);
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.ToxicBubble);
            Projectile.timeLeft = 150;
            DrawOffsetX = 5;
            DrawOriginOffsetY = 5;
        }
        public override void AI()
        {
            if (Projectile.ai[0] == 0)
                Projectile.ai[0] = 1;

            Projectile.ai[0] *= 1.02f;
            Projectile.scale = 1 + (float)Math.Sin(Projectile.ai[0] * 0.1f) * 0.1f;

            Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(10, 10), DustID.Ichor, Projectile.velocity, 0);
            d.noGravity = true;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14);
            for (int i = 0; i < 24; i++)
            {
                float rot = MathHelper.TwoPi / 24 * Main.rand.NextFloat(0.9f, 1.1f) * i;
                Dust d = Dust.NewDustPerfect(Projectile.Center + new Vector2(0, 10).RotatedBy(rot), DustID.Ichor, new Vector2(0, Main.rand.NextFloat(2, 4)).RotatedBy(rot));
                d.noGravity = !Main.rand.NextBool(6);
            }
            for (int i = 0; i < 12; i++)
            {
                Dust d2 = Dust.NewDustPerfect(Projectile.Center, DustID.Torch, Main.rand.NextVector2Circular(6, 6));
                d2.noGravity = true;
                d2.fadeIn = 1.2f;
            }
            Projectile.Explode(100);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Ichor, 60 * 17);
            if (Main.rand.NextBool())
            {
                target.AddBuff(BuffID.OnFire3, 60 * 6);
            }
        }
    }
}
