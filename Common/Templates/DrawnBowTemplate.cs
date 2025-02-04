using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using System;
using Terrafirma.Particles;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common.Templates
{
    public abstract class DrawnBowTemplate : HeldProjectile
    {
        public virtual SoundStyle shootSound => SoundID.Item5;
        public virtual Vector2 TopString => Vector2.Zero;
        public virtual Vector2 BottomString => Vector2.Zero;
        public virtual Color StringColor => Color.White;
        public virtual bool StringGlows => false;
        public override void SetDefaults()
        {
            Projectile.QuickDefaults();
            Projectile.tileCollide = false;
        }
        public override bool? CanHitNPC(NPC target) => false;
        public override bool CanHitPlayer(Player target) => false;

        public virtual void Shoot(IEntitySource source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, float power)
        {
            Projectile p = Projectile.NewProjectileDirect(source, position, velocity * power * 3, type, (int)MathHelper.Lerp(damage * 0.1f, damage * 2,power*power*power), knockback / 2f + (knockback * power * 1.5f), player.whoAmI);
            if (power == 1)
            {
                p.ai[0] = -20;
                p.extraUpdates++;
            }
        }
        SlotId BowPullSound = SlotId.Invalid;
        public override void AI()
        {
            commonHeldLogic(30);

            Player.CompositeArmStretchAmount stretch = Player.CompositeArmStretchAmount.None;
            if (player.channel && !stoppedChanneling)
            {
                if (Projectile.ai[0] == 0)
                {
                    SoundStyle PullSound = new SoundStyle("Terrafirma/Sounds/LongbowPull")
                    {
                        Volume = 0.3f,
                        Pitch = -(float)(player.itemAnimationMax * 0.014f),
                        MaxInstances = 2,
                    };
                    BowPullSound = SoundEngine.PlaySound(PullSound, Projectile.position);
                }

                Projectile.ai[0] += 1 / (float)player.itemAnimationMax;
                Vector2 direction = player.Center.DirectionTo(player.PlayerStats().MouseWorld);
                Projectile.rotation = direction.ToRotation();
                player.direction = MathF.Sign(direction.X);
                if (Projectile.ai[0] < 0.3f)
                    stretch = Player.CompositeArmStretchAmount.Full;
                else if (Projectile.ai[0] < 0.6f)
                    stretch = Player.CompositeArmStretchAmount.ThreeQuarters;
                else if (Projectile.ai[0] < 0.9f)
                    stretch = Player.CompositeArmStretchAmount.Quarter;
                if (Projectile.ai[0] > 1f)
                {
                    if (Projectile.localAI[1] == 0 && player == Main.LocalPlayer)
                    {
                        SoundEngine.PlaySound(SoundID.MaxMana, player.Center);
                        BigSparkle bigsparkle = new BigSparkle();
                        bigsparkle.fadeInTime = 6;
                        bigsparkle.Rotation = Main.rand.NextFloat(-0.4f, 0.4f);
                        bigsparkle.Scale = 2f;
                        ParticleSystem.AddParticle(bigsparkle, Projectile.Center + player.Center.DirectionTo(player.PlayerStats().MouseWorld) * 5, null, new Color(128, 255, 255, 128), ParticleLayer.NormalPixel);
                    }
                    if(SoundEngine.TryGetActiveSound(BowPullSound, out ActiveSound sound) && sound != null && sound.IsPlaying)
                    {
                        sound.Volume = 0;
                    }
                    Projectile.localAI[1]++;
                    Projectile.ai[0] = 1f;
                }
            }
            else
            {
                if (Projectile.localAI[0] == 0)
                {
                    Shoot(Projectile.GetSource_FromThis(),Projectile.Center, player.Center.DirectionTo(player.PlayerStats().MouseWorld) * player.HeldItem.shootSpeed, ContentSamples.ItemsByType[(int)Projectile.ai[1]].shoot, Projectile.damage, Projectile.knockBack, Projectile.ai[0]);
                    SoundEngine.PlaySound(shootSound, player.Center);
                }
                Projectile.localAI[0]++;
                stretch = Player.CompositeArmStretchAmount.None;

                if ((Projectile.ai[0] > 1 || Projectile.localAI[0] > 0) && SoundEngine.TryGetActiveSound(BowPullSound, out ActiveSound sound) && sound != null && sound.IsPlaying)
                {
                    sound.Volume = 0;
                }
            }
            //player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation - MathHelper.PiOver2);
            player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, (player.Center + new Vector2(4 * player.direction, player.gfxOffY)).DirectionTo(Projectile.Center + player.velocity).ToRotation() - MathHelper.PiOver2);
            player.SetCompositeArmFront(true,stretch, (player.Center + new Vector2(-4 * player.direction, player.gfxOffY)).DirectionTo(Projectile.Center + player.velocity).ToRotation() - MathHelper.PiOver2);
            Projectile.spriteDirection = player.direction;
            //Projectile.Center = player.GetBackHandPosition(Player.CompositeArmStretchAmount.Full, Projectile.rotation - MathHelper.PiOver2);
            //Projectile.Center = player.Center + (new Vector2(13,0).RotatedBy(Projectile.rotation) * new Vector2(1f,1.2f));
            Projectile.Center = player.Center + (new Vector2(13, 0).RotatedBy(Projectile.rotation) * new Vector2(1f, 1.2f)) + new Vector2(0,player.gfxOffY);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Player.CompositeArmStretchAmount stretch = Player.CompositeArmStretchAmount.None;
            if (Projectile.ai[0] < 0.3f)
                stretch = Player.CompositeArmStretchAmount.Full;
            else if (Projectile.ai[0] < 0.6f)
                stretch = Player.CompositeArmStretchAmount.ThreeQuarters;
            else if (Projectile.ai[0] < 0.9f)
                stretch = Player.CompositeArmStretchAmount.Quarter;

            DrawString(TopString, StringColor, StringGlows, stretch);
            DrawString(BottomString, StringColor, StringGlows, stretch);
            TFUtils.EasyCenteredProjectileDrawVerticalFlip(TextureAssets.Projectile[Type],Projectile,lightColor,Projectile.Center - Main.screenPosition,Projectile.rotation,1f);
            if(player.channel && !stoppedChanneling)
                DrawArrow(lightColor, stretch);
            return false;
        }
        public void DrawArrow(Color lightColor, Player.CompositeArmStretchAmount stretch, Vector2 offset = default, float scale = 1f)
        {
            Asset<Texture2D> arrowTex = TextureAssets.Item[(int)Projectile.ai[1]];
            Main.EntitySpriteDraw(arrowTex.Value, new Vector2(0,player.gfxOffY) + player.GetFrontHandPosition(stretch, player.Center.DirectionTo(Projectile.Center).ToRotation() - MathHelper.PiOver2) - Main.screenPosition + offset.RotatedBy(Projectile.rotation), null, ContentSamples.ItemsByType[(int)Projectile.ai[1]].GetAlpha(lightColor),Projectile.rotation - MathHelper.PiOver2,new Vector2(arrowTex.Width() / 2,4f),scale, player.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
        }
        public void DrawString(Vector2 startPosition, Color defaultColor, bool glow, Player.CompositeArmStretchAmount stretch)
        {
            Color stringColor = defaultColor;
            if (player.stringColor > 1)
                stringColor = TFUtils.TryApplyingPlayerStringColor(player.stringColor, Color.White);
            if (!glow)
                stringColor = Lighting.GetColor(player.Center.ToTileCoordinates(), stringColor);

            Vector2 modifiedStart = Projectile.Center + startPosition.RotatedBy(Projectile.rotation);
            Vector2 stringEnd = player.GetFrontHandPosition(stretch, player.Center.DirectionTo(Projectile.Center + player.velocity).ToRotation() - MathHelper.PiOver2);
            float rotation = modifiedStart.DirectionTo(stringEnd).ToRotation();
            if (!player.channel && !stoppedChanneling)
            {
                stringEnd = Projectile.Center + new Vector2(startPosition.X, 0).RotatedBy(Projectile.rotation);
            }

            Main.EntitySpriteDraw(Terrafirma.Pixel.Value, modifiedStart - Main.screenPosition,null, stringColor, modifiedStart.DirectionTo(stringEnd).ToRotation() - MathHelper.PiOver2,new Vector2(0.5f,0f),new Vector2(2,modifiedStart.Distance(stringEnd)),SpriteEffects.None);
        }
    }
}
