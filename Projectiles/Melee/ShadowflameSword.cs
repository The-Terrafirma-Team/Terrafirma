using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Templates.Melee;
using Terraria.GameContent;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Terrafirma.Projectiles.Melee
{
    public class ShadowflameSword : MeleeSlice
    {
        private static Asset<Texture2D> glowTex;
        public override void Load()
        {
            glowTex = ModContent.Request<Texture2D>(Texture + "Glow");
        }
        public override Color DarkSlashColor => new Color(182, 27, 248, 0);
        public override Color LightSlashColor => new Color(58, 29, 190, 128);
        public override float slashSize => 1.3f;

        public override void PostAI()
        {
            if (Main.rand.NextBool((int)(extend * 10) + 1))
                return;

            for (int i = 0; i < 3; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center + (new Vector2(Main.rand.NextFloat(Length * Projectile.scale * 0.8f)).RotatedBy(Projectile.rotation - MathHelper.PiOver2)), DustID.Shadowflame);
                d.velocity = new Vector2(player.direction * Main.rand.NextFloat(-1,4) * extend).RotatedBy(Projectile.rotation);
                d.velocity += new Vector2(player.direction * 2 * extend,-2);
                d.alpha = 255;
                d.scale = extend * 1.2f;
                d.noGravity = !Main.rand.NextBool(5);
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            target.AddBuff(BuffID.ShadowFlame, 60 * 3);
            modifiers.CritDamage += 1;
            Vector2 spawnPos = player.Center + new Vector2(Main.rand.Next(-200,200), -700);
            if(Projectile.localAI[0] != 1)
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), spawnPos, spawnPos.DirectionTo(Main.MouseWorld) * 8, ModContent.ProjectileType<ShadowflameBall>(), Projectile.damage / 2, Projectile.knockBack, Projectile.owner, Main.MouseWorld.Y);
            Projectile.localAI[0] = 1;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            float extend2 = MathF.Sin((Projectile.timeLeft / Projectile.ai[1]) * MathHelper.Pi);

            Vector2 scaleVector = Vector2.SmoothStep(player.direction == 1 ? new Vector2(1.2f, 0.6f) : new Vector2(0.6f, 1.2f), new Vector2(1.1f), extend2);

            int slashLength = (int)(slashTex.Height() / 8 * (extend * 1.4f));

            for (int i = 0; i < 4; i++)
            {
                //Main.EntitySpriteDraw(tex.Value, Projectile.Center - Main.screenPosition, new Rectangle(0, tex.Height() / 4 * i, tex.Width(), (int)((tex.Height() / 4) * extend)), Color.White * extend2 * extend, (Projectile.rotation + (i * 0.1f * extend) - MathHelper.PiOver4 * 1.1f), new Vector2(tex.Width() / 2, tex.Height() / 8 ), Projectile.scale * 1.1f, player.direction == 1 ? SpriteEffects.None: SpriteEffects.FlipVertically);

                Main.EntitySpriteDraw(
                    slashTex.Value,
                    Projectile.Center - Main.screenPosition,
                    new Rectangle(0, slashTex.Height() / 4 * i, slashTex.Width(), slashLength),
                    Color.Lerp(DarkSlashColor, LightSlashColor, i / 4f) * extend2 * 0.3f,
                    Projectile.rotation + (player.direction * extend * 0.1f) - MathHelper.PiOver4 - (player.direction == 1 ? 0 : MathHelper.TwoPi),
                    new Vector2(slashTex.Width() / 2 + (slashLength * 0.1f), player.direction == 1 ? slashLength : 0),
                    (Projectile.scale + (extend2 * 0.2f)) * slashSize,
                    player.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically); ;
            }
            commonDiagonalItemDraw(lightColor, TextureAssets.Projectile[Type], scaleVector * Projectile.scale);
            commonDiagonalItemDraw(new Color(255,255,255,128) * extend, glowTex, scaleVector * Projectile.scale);
            commonDiagonalItemDraw(new Color(255, 255, 255, 0) * extend * 0.5f, glowTex, scaleVector * Projectile.scale * 1.1f);
            return false;
        }
    }
}
