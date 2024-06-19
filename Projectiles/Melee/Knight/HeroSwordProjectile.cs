using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using System;
using Terrafirma.Particles;
using Terrafirma.Items.Weapons.Melee.Knight;
using Terraria.GameContent;
using ReLogic.Content;

namespace Terrafirma.Projectiles.Melee.Knight
{
    public class HeroSwordProjectile : ModProjectile
    {
        public override string Texture => "Terrafirma/Items/Weapons/Melee/Knight/HeroSword";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
        }
        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 800;
            DrawOriginOffsetX = -9;
            DrawOriginOffsetY = -9;
            Projectile.friendly = true;
            Projectile.scale = 0.1f;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.CritChance = 4;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.timeLeft == 800)
            {
                Projectile.localAI[1] = -MathHelper.PiOver2;
            }
            if (Projectile.Center.Distance(player.MountedCenter) < 200)
            {
                if (Projectile.localAI[0] < 30)
                    Projectile.localAI[0] = 30;
                Projectile.localAI[1] = Projectile.localAI[1].AngleLerp(player.MountedCenter.DirectionTo(Projectile.Center + Projectile.velocity).ToRotation(), 0.1f);
                Projectile.localAI[2] = Math.Sign(Projectile.Center.X - player.Center.X);
            }
            else
            {
                Projectile.localAI[1] = Projectile.localAI[1].AngleLerp(MathHelper.PiOver2, 1 / 15f);
            }
            if (Projectile.localAI[0] > 0)
            {
                player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.localAI[1] - MathHelper.PiOver2);
                Projectile.localAI[0]--;
                player.direction = (int)Projectile.localAI[2];
            }

            Projectile.ai[0]++;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;

            if (Projectile.ai[0] > 30)
            {
                Projectile.scale = 1f;
                Projectile.rotation += 0.6f * (Projectile.ai[0] / 2f);
                if (Projectile.ai[0] > 600) Projectile.velocity = Vector2.Normalize(Main.player[Projectile.owner].MountedCenter - Projectile.Center) * (Projectile.ai[0] * 0.1f);
                else Projectile.velocity += Vector2.Normalize(Main.player[Projectile.owner].MountedCenter - Projectile.Center);
                //Projectile.velocity += Vector2.Lerp(Projectile.velocity, Main.player[Projectile.owner].MountedCenter - Projectile.Center, 0.5f);
                if (Projectile.Hitbox.Intersects(Main.player[Projectile.owner].Hitbox) || Projectile.Center.Distance(Main.player[Projectile.owner].MountedCenter) < Projectile.ai[0] / 4f) Projectile.Kill();
            }
            else
            {
                Projectile.scale = Math.Clamp(Projectile.scale + (1f - Projectile.scale) / 4f, 0f, 1f);
            }

            Projectile.velocity *= Projectile.ai[0] > 30 ? 1f : 0.9f;
            if (Projectile.ai[0] == 30) Projectile.velocity *= -1;

            if (Projectile.Center.Distance(Main.player[Projectile.owner].MountedCenter) > 1000f) Projectile.Kill();

            if (Projectile.ai[0] % 8 == 0) SoundEngine.PlaySound(SoundID.Item7, Projectile.Center);

            if (Projectile.ai[0] % 10 == 0 && Main.LocalPlayer == Main.player[Projectile.owner])
            {
                Projectile newproj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(20f, 0f).RotatedBy(Projectile.Center.DirectionTo(Main.MouseWorld).ToRotation()), ModContent.ProjectileType<HeroSwordShot>(), Projectile.damage + Projectile.damage * (int)Math.Clamp(Projectile.ai[0] / 150f, 0f, 4f), Projectile.knockBack, Projectile.owner, 0, 0, 0);
                newproj.scale = 1f + Math.Clamp(Projectile.ai[0] / 600f, 0f, 1f);
            }


            if (player.HeldItem.type != ModContent.ItemType<HeroSword>()) Projectile.Kill();


        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            BigSparkle bigsparkle = new BigSparkle();
            bigsparkle.fadeInTime = 10;
            bigsparkle.Rotation = Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2);
            ParticleSystem.AddParticle(bigsparkle, Projectile.Center, Vector2.Zero, new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0));
            //LegacyParticleSystem.AddParticle(new BigSparkle(), Projectile.Center, Vector2.Zero, new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0), 0, 10, 1, 1f, Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2));

            base.OnHitNPC(target, hit, damageDone);
        }

        public override void OnKill(int timeLeft)
        {
            BigSparkle bigsparkle = new BigSparkle();
            bigsparkle.fadeInTime = 10;
            bigsparkle.Rotation = Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2);
            bigsparkle.Scale = 2f;
            ParticleSystem.AddParticle(bigsparkle, Projectile.Center, Vector2.Zero, new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0));
            //LegacyParticleSystem.AddParticle(new BigSparkle(), Projectile.Center, Vector2.Zero, new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0), 0, 10, 1, 2f, Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2));
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D SwordTexture = TextureAssets.Projectile[Type].Value;

            Main.EntitySpriteDraw(SwordTexture, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, 56, 56), new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0) * 0.2f, Projectile.rotation, new Vector2(28), Projectile.scale * 2f, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(SwordTexture, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, 56, 56), new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0), Projectile.rotation, new Vector2(28), Projectile.scale * 1.2f, SpriteEffects.None, 0);
            for (int i = 0; i < 10; i++)
            {
                Main.EntitySpriteDraw(SwordTexture, Projectile.oldPos[i] + Projectile.Size / 2 - Main.screenPosition, new Rectangle(0, 0, 56, 56), new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0) * (1f - i / 10f), Projectile.oldRot[i], new Vector2(28), Projectile.scale * 1.2f, SpriteEffects.None, 0);
            }
            Main.EntitySpriteDraw(SwordTexture, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, 56, 56), Color.White, Projectile.rotation, new Vector2(28), Projectile.scale * 1f, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(SwordTexture, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, 56, 56), new Color(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B, 0) * 0.5f, Projectile.rotation, new Vector2(28), Projectile.scale * 0.8f, SpriteEffects.None, 0);


            return false;
        }

    }
}
