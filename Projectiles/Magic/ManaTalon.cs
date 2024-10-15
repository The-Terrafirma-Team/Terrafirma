using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terrafirma.Common;
using Terrafirma.Particles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Magic
{
    public class ManaTalon : ModProjectile
    {
        public override string Texture => Terrafirma.AssetPath + "Bullet";
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.Size = new Vector2(16);
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 60 * 4;
        }
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 24;
            ProjectileID.Sets.TrailingMode[Type] = 0;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            BulletVisuals.drawBullet(Projectile, new Color(1f,1f,1f,0f), new Color(0f, 0.75f, 1f, 0f), 1.25f + (float)Math.Sin(Main.timeForVisualEffects * 0.2f) * 0.25f);
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            ParticleSystem.AddParticle(new SliceSparkle() {Rotation = Projectile.oldVelocity.ToRotation(), Scale = 0.4f, fadeInTime = 3 }, Projectile.Center - Projectile.oldVelocity, Projectile.oldVelocity * 0.5f, new Color(0f, 0.75f, 1f, 0f));
        }
        public override void AI()
        {
            Projectile.velocity.Y += 0.1f;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];

            int manaGain = target.NPCStats().Mana > 2 ? 4 : 2;
            if (target.NPCStats().Mana == 1)
                manaGain = 3;

            CombatText.NewText(player.Hitbox, CombatText.HealMana, manaGain, false);
            player.statMana += manaGain;
            target.DealManaDamage(player, 6);
        }
    }
}
