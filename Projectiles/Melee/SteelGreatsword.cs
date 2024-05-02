using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terrafirma.Common.Templates.Melee;
using Terrafirma.Particles;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Melee
{
    public class SteelGreatsword : UpDownSwing
    {
        public override string Texture => "Terrafirma/Items/Weapons/Melee/Swords/SteelGreatsword";
        private static Asset<Texture2D> afterImage;

        public override void Load()
        {
            afterImage = Mod.Assets.Request<Texture2D>("Projectiles/Melee/SteelGreatswordAfter");
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (player.PlayerStats().SteelBladeHits == 12)
                modifiers.FinalDamage *= 1.2f;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (player.PlayerStats().SteelBladeHits < 12)
                player.PlayerStats().SteelBladeHits++;
            bool fullCharge = player.PlayerStats().SteelBladeHits == 12;

            if (fullCharge) SoundEngine.PlaySound(SoundID.Item8, Projectile.position);

            BigSparkle p = new BigSparkle();
            p.Scale = fullCharge? 4 : 2;
            p.fadeInTime = 6;
            p.smallestSize = 0.01f;
            p.secondaryColor = Color.Transparent;

            Color trailColor = (!fullCharge) ? new Color(180, 180, 250, 0) : new Color(128, 255,255, 0);

            ParticleSystem.AddParticle(p, target.Hitbox.ClosestPointInRect(Projectile.Center),null,trailColor);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            float extend = MathF.Sin((Projectile.timeLeft / Projectile.ai[1]) * MathHelper.Pi);

            if ((player.PlayerStats().SteelBladeHits != 12))
            {
                for (int i = 0; i < 6; i++)
                {
                    if (Projectile.oldRot[i] != 0)
                        commonDiagonalItemDrawManualRotation(new Color(lightColor.R, lightColor.G, lightColor.B, 0) * (0.3f - (i * 0.05f)), afterImage, Projectile.scale + extend * 0.2f, Projectile.oldRot[i]);
                }
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    if (Projectile.oldRot[i] != 0)
                        commonDiagonalItemDrawManualRotation(new Color(128,255,255,0) * (1f - (i * 0.2f)), afterImage, Projectile.scale + extend * 0.3f, Projectile.oldRot[i]);
                }
            }

            commonDiagonalItemDraw(lightColor, TextureAssets.Projectile[Type], Projectile.scale + extend * 0.2f);
            return false;
        }
    }
    public class SteelGreatswordSlash : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.Size = new Vector2(80);
            Projectile.aiStyle = -1;
            Projectile.alpha = 254;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.extraUpdates = 2;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0; i < 4; i++)
            {
                BigSparkle p = new BigSparkle();
                p.Scale = Main.rand.NextFloat(3,5);
                p.fadeInTime = Main.rand.NextFloat(4, 7);
                p.smallestSize = 0.01f;
                p.secondaryColor = Color.Transparent;
                p.Rotation = Main.rand.NextFloat(-1f,1f);
                ParticleSystem.AddParticle(p, Main.rand.NextVector2FromRectangle(target.Hitbox), null, new Color(128, 255, 255, 0));
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            for (int i = 0; i < 5; i++)
            {
                Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.Center - (Projectile.velocity * i * 3) - Main.screenPosition, null, new Color(128, 200, 255, 0) * Projectile.Opacity * (0.7f - i * 0.1f), Projectile.rotation, new Vector2(40, 24), 1.3f - (i*0.1f), SpriteEffects.None);
            }

            //Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.Center - Main.screenPosition, null, new Color(255,255,255,0) * Projectile.Opacity, Projectile.rotation, new Vector2(40, 24), Projectile.scale, SpriteEffects.None);
            
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (!Collision.CanHitLine(Main.player[Projectile.owner].Center,1,1,Projectile.Center,1,1))
                return false;
            return base.CanHitNPC(target);
        }
        public override void AI()
        {
            if (Projectile.timeLeft == 600)
                SoundEngine.PlaySound(SoundID.Item71, Projectile.position);

            Projectile.velocity *= 0.99f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            if (Projectile.timeLeft > 540)
                Projectile.alpha -= 20;
            else
                Projectile.alpha += 12;
            if (Projectile.alpha > 255)
                Projectile.Kill();
        }
    }
}
