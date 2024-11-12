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
            Main.projFrames[Type] = 4;
            back = ModContent.Request<Texture2D>("Terrafirma/Assets/Particles/GlowCircleGradient");
        }
        public override void SetDefaults()
        {
            Projectile.QuickDefaults(false, 16);
            Projectile.timeLeft = 60;
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
                d.fadeIn = 2f;
                d.noLight = d.noLightEmittence = true;
            }
            Player player = Main.player[Projectile.owner];
            player.Teleport(Projectile.Center - new Vector2(player.width / 2, player.height),TeleportationStyleID.DebugTeleport);
            player.immune = true;
            player.AddImmuneTime(ImmunityCooldownID.General,60 * 3);
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
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.FireworksRGB);
                d.velocity = Projectile.velocity;
                d.color = new Color(60, 0, 150);
                d.noGravity = true;
                d.scale *= 0.7f;
                d.noLight = d.noLightEmittence = true;
            }

            Projectile.frame = (int)(Projectile.timeLeft / 3) % 4;

            Projectile.velocity.Y += 0.3f;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.spriteBatch.End();
            BlendState BlendS = new BlendState
            {
                ColorBlendFunction = BlendFunction.ReverseSubtract,
                ColorDestinationBlend = Blend.One,
                ColorSourceBlend = Blend.SourceAlpha,
                AlphaBlendFunction = BlendFunction.ReverseSubtract,
                AlphaDestinationBlend = Blend.One,
                AlphaSourceBlend = Blend.SourceAlpha
            };
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendS, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);

            Main.EntitySpriteDraw(back.Value, Projectile.Center - Main.screenPosition + Vector2.Normalize(Projectile.velocity) * 13, null, Color.LightGreen * 0.3f * Projectile.Opacity, 0, back.Size() / 2, 0.2f, SpriteEffects.None);

            TFUtils.EasyCenteredProjectileDraw(TextureAssets.Projectile[Type], Projectile, Color.White * Projectile.Opacity);
            TFUtils.EasyCenteredProjectileDraw(TextureAssets.Projectile[Type], Projectile, Color.Lime * Projectile.Opacity);

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);

            TFUtils.EasyCenteredProjectileDraw(TextureAssets.Projectile[Type], Projectile, Color.Magenta * Projectile.Opacity);
            TFUtils.EasyCenteredProjectileDraw(TextureAssets.Projectile[Type], Projectile, Color.Magenta * Projectile.Opacity);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
            return false;
        }
    }
}
