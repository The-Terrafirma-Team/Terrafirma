using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Data;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Melee.Brawler
{
    public class ShadowBoxerBomb : ModProjectile
    {
        private static Asset<Texture2D> back;

        public override void SetStaticDefaults()
        {
            ProjectileSets.AutomaticallyGivenMeleeScaling[Type] = true;
            ProjectileSets.CanBeReflected[Type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.QuickDefaults(false, 16);
            Projectile.timeLeft = 40;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14);
            Projectile.Explode(16 * 8);
            for(int i = 0; i < 40; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.FireworksRGB);
                d.velocity = Main.rand.NextVector2Circular(7,7);
                d.color = new Color(60, 0, 150);
                d.noGravity = true;
                d.scale *= 1.3f;
                d.fadeIn = Main.rand.NextFloat(2);
                d.noLight = d.noLightEmittence = true;
            }
            for (int i = 0; i < 20; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Corruption);
                d.velocity = Main.rand.NextVector2Circular(5, 5);
                d.alpha = 128;
                d.scale *= 1.5f;
                d.noGravity = Main.rand.NextBool();
            }
            for (int i = 0; i < 20; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.CorruptTorch);
                d.velocity = Main.rand.NextVector2Circular(5, 5);
                d.scale *= 1.5f;
                d.noGravity = Main.rand.NextBool();
            }
            if (Projectile.reflected)
                return;
            Player player = Main.player[Projectile.owner];
            player.Teleport(Projectile.Center - new Vector2(player.width / 2, player.height),TeleportationStyleID.DebugTeleport);
            player.immune = true;
            player.immuneTime = Math.Max(player.immuneTime, 60 * 3);
            player.velocity = -Projectile.velocity * 0.3f;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = height = 2;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
        public override void AI()
        {
            if (Main.rand.NextBool(4))
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Corruption);
                d.velocity = Projectile.velocity;
                d.alpha = 128;
                d.noGravity = true;
                d.noLight = d.noLightEmittence = true;
            }
            Dust d2 = Dust.NewDustPerfect(Projectile.Center + new Vector2(11,-14).RotatedBy(Projectile.rotation), DustID.CorruptTorch);
            d2.velocity *= 0.1f;
            d2.noGravity = true;
            d2.scale = 1.2f;

            Projectile.rotation += Projectile.direction * 0.3f;

            Projectile.velocity.Y += 0.3f;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            TFUtils.EasyCenteredProjectileDraw(TextureAssets.Projectile[Type], Projectile, lightColor);
            return false;
        }
    }
}
