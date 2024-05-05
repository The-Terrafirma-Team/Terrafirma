using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Linq;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode
{
    internal class Thunderbeam : Spell
    {
        public override int UseAnimation => 12;
        public override int UseTime => 12;
        public override int ManaCost => 8;
        public override int ReuseDelay => -1;
        public override int[] SpellItem => new int[] { ItemID.ThunderStaff };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<ThunderbeamProj>();
            position += new Vector2(54,54) * player.Center.DirectionTo(Main.MouseWorld);
            velocity = position.DirectionTo(Main.MouseWorld) * 3f;           
            damage = (int)(damage * 0.5f);

            for (int i = 0; i < 3; i++)
            {
                Projectile proj = Projectile.NewProjectileDirect(player.GetSource_FromThis(), position, velocity, ModContent.ProjectileType<ThunderbeamProj>(), damage, knockback, player.whoAmI);
                proj.scale = 0.5f - 0.1f * i;
            }            

            return false;
        }
    }

    public class ThunderbeamProj : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.ThunderStaffShot}";

        Vector2 OriginalPos = Vector2.Zero;
        NPC TargetNPC = null;
        Vector2[] LightPoints = new Vector2[]{};

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(4);
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.knockBack = 0f;
            Projectile.timeLeft = 500;
            Projectile.stopsDealingDamageAfterPenetrateHits = true;

            Projectile.extraUpdates = 20;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.timeLeft = 320;
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void AI()
        {
            //Patented by Jeo himself
            if (Projectile.timeLeft > 300)
            {
                int ChaosFormula = (int)Math.Abs(Math.Clamp((int)(Math.Sin(Projectile.ai[0] + 1) * Math.Tan(Projectile.timeLeft) * 6), -6f, 6f));

                if (Projectile.ai[0] == 0)
                {
                    OriginalPos = Projectile.Center;
                    TargetNPC = TFUtils.FindClosestNPC(300f, Projectile.Center, TargetThroughWalls: false);
                }

                if (TargetNPC != null)
                {
                    float LerpValue = 0f;
                    if (OriginalPos.Distance(TargetNPC.Center) != 0) LerpValue = OriginalPos.Distance(Projectile.Center) / OriginalPos.Distance(TargetNPC.Center);

                    if (Projectile.ai[0] % (10 - ChaosFormula) == 0)
                    {
                        Projectile.velocity = Vector2.Lerp(Projectile.velocity.RotatedByRandom(MathHelper.PiOver4), Projectile.DirectionTo(TargetNPC.Center) * 3f, LerpValue);
                        LightPoints = LightPoints.Append(Projectile.Center).ToArray();
                    }
                }
                else
                {
                    if (Projectile.ai[0] % (10 - ChaosFormula) == 0)
                    {
                        Projectile.velocity = Projectile.velocity.RotatedByRandom(MathHelper.PiOver4);
                        LightPoints = LightPoints.Append(Projectile.Center).ToArray();
                    }
                }
            }
            else
            {
                Projectile.velocity = Vector2.Zero;
            }

            for (int i = 0; i < LightPoints.Length; i++)
            {
                Lighting.AddLight(LightPoints[i], new Vector3(0.6f, 0.6f, 0.6f));
            }
            if (LightPoints.Length == 0) Lighting.AddLight(Projectile.Center, new Vector3(0.6f, 0.6f, 0.6f));
            Lighting.AddLight(Main.player[Projectile.owner].Center, new Vector3(0.6f, 0.6f, 0.6f));
            Projectile.ai[0]++;
        }

        //Abbandoned Piece of lost media
        private static readonly BlendState maxBlend = new BlendState()
        {
            AlphaBlendFunction = BlendFunction.Max,
            ColorBlendFunction = BlendFunction.Max,
            AlphaDestinationBlend = Blend.One,
            AlphaSourceBlend = Blend.One,
            ColorDestinationBlend = Blend.One,
            ColorSourceBlend = Blend.One

        };
        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> Tex = ModContent.Request<Texture2D>(Texture);
            Asset<Texture2D> LightningTex = ModContent.Request<Texture2D>("Terrafirma/Assets/Particles/LightningParts");

            float Fadeout = 1f;
            if (Projectile.timeLeft <= 300)
            {
                Fadeout =  Projectile.timeLeft / 300f;
            }

            Color glowcolor = new Color(200, 240, 255, 0) * Projectile.Opacity * Fadeout;

            for (int i = 0; i < LightPoints.Length - 1; i++)
            {
                float scalevalue = 1.5f - 0.028f * i;
                //Main.EntitySpriteDraw(Tex.Value,
                //    LightPoints[i] - Main.screenPosition,
                //    new Rectangle(0,0,Tex.Width(),Tex.Width()),
                //    Color.White,
                //    0f,
                //    new Vector2(Tex.Width() / 2, Tex.Width() / 2),
                //    1f,
                //    SpriteEffects.None);
                Main.EntitySpriteDraw(LightningTex.Value,
                    LightPoints[i] - Main.screenPosition,
                    new Rectangle(0, 0, 2, LightningTex.Height()),
                    glowcolor,
                    LightPoints[i].DirectionTo(LightPoints[i+1]).ToRotation(),
                    new Vector2(0, LightningTex.Height() / 2),
                    new Vector2(LightPoints[i].Distance(LightPoints[i+1]) / 2f, Projectile.scale / 3f * scalevalue),
                    SpriteEffects.None);
                Main.EntitySpriteDraw(LightningTex.Value,
                    LightPoints[i+1] - Main.screenPosition,
                    new Rectangle(4, 0, 18, LightningTex.Height()),
                    glowcolor,
                    LightPoints[i].DirectionTo(LightPoints[i + 1]).ToRotation(),
                    new Vector2(0, LightningTex.Height() / 2),
                    new Vector2(Projectile.scale / 3f, Projectile.scale / 3f * scalevalue),
                    SpriteEffects.None);
                Main.EntitySpriteDraw(LightningTex.Value,
                    LightPoints[i] - Main.screenPosition,
                    new Rectangle(4, 0, 18, LightningTex.Height()),
                    glowcolor,
                    LightPoints[i].DirectionTo(LightPoints[i + 1]).ToRotation() + MathHelper.Pi,
                    new Vector2(0, LightningTex.Height() / 2),
                    new Vector2(Projectile.scale / 3f, Projectile.scale / 3f * scalevalue),
                    SpriteEffects.None);

                Main.EntitySpriteDraw(LightningTex.Value,
                    LightPoints[i] - Main.screenPosition,
                    new Rectangle(0, 0, 2, LightningTex.Height()),
                    glowcolor * 0.4f,
                    LightPoints[i].DirectionTo(LightPoints[i + 1]).ToRotation(),
                    new Vector2(0, LightningTex.Height() / 2),
                    new Vector2(LightPoints[i].Distance(LightPoints[i + 1]) / 2f, Projectile.scale * scalevalue),
                    SpriteEffects.None);
                Main.EntitySpriteDraw(LightningTex.Value,
                    LightPoints[i + 1] - Main.screenPosition,
                    new Rectangle(4, 0, 18, LightningTex.Height()),
                    glowcolor * 0.4f,
                    LightPoints[i].DirectionTo(LightPoints[i + 1]).ToRotation(),
                    new Vector2(0, LightningTex.Height() / 2),
                    new Vector2(Projectile.scale, Projectile.scale * scalevalue),
                    SpriteEffects.None);
                Main.EntitySpriteDraw(LightningTex.Value,
                    LightPoints[i] - Main.screenPosition,
                    new Rectangle(4, 0, 18, LightningTex.Height()),
                    glowcolor * 0.4f,
                    LightPoints[i].DirectionTo(LightPoints[i + 1]).ToRotation() + MathHelper.Pi,
                    new Vector2(0, LightningTex.Height() / 2),
                    new Vector2(Projectile.scale, Projectile.scale * scalevalue),
                    SpriteEffects.None);
            }
            return false;
        }
        public override void OnKill(int timeLeft)
        {

        }
    }
}
