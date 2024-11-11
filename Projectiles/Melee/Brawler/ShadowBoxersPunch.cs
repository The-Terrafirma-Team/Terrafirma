using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Data;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Melee.Brawler
{
    public class ShadowBoxersPunch : ModProjectile
    {
        private static Asset<Texture2D> back;
        public override void SetStaticDefaults()
        {
            ProjectileSets.TrueMeleeProjectiles[Type] = true;
            ProjectileSets.AutomaticallyGivenMeleeScaling[Type] = true;
            Main.projFrames[Type] = 6;
            back = ModContent.Request<Texture2D>("Terrafirma/Assets/Particles/GlowCircleGradient");
        }
        public override void SetDefaults()
        {
            Projectile.QuickDefaults(false, 16);
            Projectile.penetrate = 3;
            Projectile.stopsDealingDamageAfterPenetrateHits = true;
            Projectile.scale = 1.2f;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = height = 2;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.ai[2] = 1;
            Projectile.damage = 0;
            Projectile.velocity = Projectile.oldVelocity;
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.ai[2] == 0)
            {
                Projectile.ai[2] = 1;
                Main.player[Projectile.owner].velocity -= Projectile.velocity * (1f - target.knockBackResist) * 0.8f;
                Main.player[Projectile.owner].GiveTension(5);
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (Projectile.ai[2] == 0)
            {
                Projectile.ai[2] = 1;
                Main.player[Projectile.owner].velocity -= Projectile.velocity * 0.8f;
                Main.player[Projectile.owner].GiveTension(5);
            }
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (Projectile.ai[1] == 0)
            {
                SoundEngine.PlaySound(SoundID.Item1,Projectile.position);
                Projectile.timeLeft = player.itemAnimationMax;
            }
            player.SetDummyItemTime(Projectile.timeLeft);
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.ai[1] += 0.06f * player.GetWeaponAttackSpeed(player.HeldItem);
            Projectile.Center = (player.MountedCenter + new Vector2(0,player.gfxOffY)) + Vector2.Normalize(Projectile.velocity) * Projectile.ai[1] * Projectile.velocity.Length() * 19 * Math.Max(Projectile.scale,0.75f);

            if(Projectile.timeLeft < 10 || Projectile.ai[2] > 0)
            {
                Projectile.Opacity *= 0.9f;
            }

            if (Main.rand.NextBool(4))
            {
                Dust d = Dust.NewDustDirect(Projectile.position,Projectile.width,Projectile.height,DustID.FireworksRGB);
                d.velocity = Projectile.velocity;
                d.color = new Color(60, 0, 150);
                d.noGravity = true;
                d.scale *= 0.7f;
                d.noLight = d.noLightEmittence = true;
            }

            Projectile.frame = (int)(Projectile.timeLeft / 3) % 6;

            Player.CompositeArmStretchAmount stretch = Player.CompositeArmStretchAmount.None;
            Player.CompositeArmStretchAmount stretch2 = Player.CompositeArmStretchAmount.Full;
            if (Projectile.ai[1] < 0.1f)
            {
                stretch = Player.CompositeArmStretchAmount.ThreeQuarters;
                stretch2 = Player.CompositeArmStretchAmount.Quarter;
            }
            else if (Projectile.ai[1] < 0.15f)
            {
                stretch2 = Player.CompositeArmStretchAmount.None;
                stretch = Player.CompositeArmStretchAmount.Full;
            }
            else if (Projectile.ai[1] < 0.25f)
            {
                stretch = Player.CompositeArmStretchAmount.Quarter;
                stretch2 = Player.CompositeArmStretchAmount.ThreeQuarters;
            }
            
            if (Projectile.ai[0] == 0)
            {
                Projectile.spriteDirection = 1;
                player.SetCompositeArmFront(true, stretch, Projectile.velocity.ToRotation() - MathHelper.PiOver2);
                player.SetCompositeArmBack(true, stretch2, Projectile.velocity.ToRotation() - MathHelper.PiOver2);
            }
            else
            {
                Projectile.spriteDirection = -1;
                player.SetCompositeArmFront(true, stretch2, Projectile.velocity.ToRotation() - MathHelper.PiOver2);
                player.SetCompositeArmBack(true, stretch, Projectile.velocity.ToRotation() - MathHelper.PiOver2);
            }

            Projectile.velocity = Projectile.velocity.RotatedBy(Projectile.spriteDirection * 0.01f);
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

            TFUtils.EasyCenteredProjectileDraw(TextureAssets.Projectile[Type],Projectile,Color.White * Projectile.Opacity * 2);
            TFUtils.EasyCenteredProjectileDraw(TextureAssets.Projectile[Type], Projectile, Color.Lime * Projectile.Opacity * 2);
            
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);

            TFUtils.EasyCenteredProjectileDraw(TextureAssets.Projectile[Type], Projectile, Color.Magenta * Projectile.Opacity * 4);
            TFUtils.EasyCenteredProjectileDraw(TextureAssets.Projectile[Type], Projectile, Color.Magenta * Projectile.Opacity * 0.5f);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
            return false;
        }
    }
}
