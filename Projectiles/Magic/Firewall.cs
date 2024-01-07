using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;

namespace TerrafirmaRedux.Projectiles.Magic
{
    public class Firewall : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.Size = new Vector2(16);
            Projectile.aiStyle = -1;
            Projectile.hide = true;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(1f, 1f, 1f, 0f) * Projectile.Opacity;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override void AI()
        {
            if (Projectile.ai[0] == 0)
                SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
            Projectile.ai[0]++;
            Player player = Main.player[Projectile.owner];

            if (player.ItemAnimationEndingOrEnded && Projectile.timeLeft > 60)
                Projectile.timeLeft = 60;
            Vector2 rand = Main.rand.NextVector2Circular(20, 20);
            Dust d = Dust.NewDustPerfect(Projectile.Center + rand, DustID.InfernoFork, -rand * 0.1f);
            d.noGravity = true;


        }

        public override void OnKill(int timeLeft)
        {
            Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ProjectileID.InfernoFriendlyBlast, Projectile.damage, Projectile.owner);
        }
    }
}
