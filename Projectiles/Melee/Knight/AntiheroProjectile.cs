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
using System.Collections.Generic;

namespace Terrafirma.Projectiles.Melee.Knight
{
    public class AntiheroProjectile : ModProjectile
    {
        NPC targetNPC = null;
        Vector2 targetOffset = Vector2.Zero;
        public override string Texture => "Terrafirma/Projectiles/Melee/Knight/AntiheroProjectile";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
        }

        bool returnbool = false;
        public override void SetDefaults()
        {          
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 10000;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.extraUpdates = 2;
            Projectile.hide = true;
        }
        public override void AI()
        {
            if (Projectile.velocity != Vector2.Zero) Projectile.rotation = MathHelper.Lerp(Projectile.rotation, Projectile.velocity.ToRotation(), 0.2f);
            if (Projectile.ai[1] == 1)
            {
                Projectile.tileCollide = false;
                Projectile.spriteDirection = -1;              
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.Center.DirectionTo(Main.player[Projectile.owner].Center) * 16f, 0.04f);                
                targetNPC = null;
                if (Main.player[Projectile.owner].Center.Distance(Projectile.Center) <= 50) Projectile.Kill();
            }

            if (Main.player[Projectile.owner].Center.Distance(Projectile.Center) > 600f) Projectile.ai[1] = 1;
            if (Main.player[Projectile.owner].Center.Distance(Projectile.Center) > 900f) Projectile.Kill();

            if (targetNPC != null)
            {
                Projectile.Center = targetNPC.Center + targetOffset.RotatedBy(targetNPC.rotation);
                Projectile.rotation = Projectile.Center.AngleTo(targetNPC.Center);

                if (targetNPC.immune[Projectile.owner] == 0)
                {
                    Main.player[Projectile.owner].ApplyDamageToNPC(targetNPC, Projectile.damage, 0f, 1, damageType: DamageClass.Melee);
                    targetNPC.immune[Projectile.owner] = 20;

                    int healAmount = (int)(4 * (((600 - (int)Main.player[Projectile.owner].Center.Distance(Projectile.Center)) / 300f) + 1));

                    if (healAmount > 0 && targetNPC.type != NPCID.TargetDummy) Main.player[Projectile.owner].Heal(healAmount);

                    for (int i = 0; i < Projectile.Center.Distance(Main.player[Projectile.owner].Center); i += 5)
                    {
                        float dist = i / Projectile.Center.Distance(Main.player[Projectile.owner].Center);
                        Vector2 pos = new Vector2(
                            MathHelper.Lerp(Projectile.Center.X, Main.player[Projectile.owner].Center.X, dist),
                            MathHelper.Lerp(Projectile.Center.Y, Main.player[Projectile.owner].Center.Y, dist * dist)
                        );
                        Dust d = Dust.NewDustPerfect(pos, DustID.LifeDrain, Vector2.Zero);
                        d.noGravity = true;
                    }

                    for (int i = 0; i < 10; i++)
                    {
                        Vector2 vel = Main.rand.NextVector2Circular(4f, 4f);
                        Dust.NewDust(Projectile.Center + Projectile.Center.DirectionTo(targetNPC.Center) * 40, 4, 4, DustID.Blood, vel.X, vel.Y, Scale: 1.5f);
                    }

                }
                if (!targetNPC.active) Projectile.ai[1] = 1;
            }

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.ai[1] == 0) Projectile.velocity = Vector2.Zero;
            if (targetNPC == null || !targetNPC.active)
            {
                targetNPC = target;
                targetOffset = (Projectile.Center - target.Center).RotatedBy(-targetNPC.rotation);
            }
            base.OnHitNPC(target, hit, damageDone);
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (targetNPC == null)
            {
                return base.CanHitNPC(target);
            }
            else if (target == targetNPC)
            {
                return false;
            }
            return false;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.ai[1] = 1;
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            if (Main.player[Projectile.owner].HeldItem.ModItem is Antihero item )
            {
                item.auraPresence = 1f;
                item.auraFadeTimer = 30;
            }
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            behindNPCsAndTiles.Add(index);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D SwordTexture = TextureAssets.Projectile[Type].Value;
            for (int i = 0; i < Projectile.oldPos.Length; i+=2)
            {
                Main.EntitySpriteDraw(SwordTexture,
                    (Projectile.oldPos[i] + Projectile.Size/2f) - Main.screenPosition,
                    SwordTexture.Frame(),
                    new Color(1f,0f,0f,0f) * (1f - (i / (float)Projectile.oldPos.Length)) * 0.5f,
                    Projectile.rotation + MathHelper.PiOver4 * Projectile.spriteDirection,
                    SwordTexture.Size() / 2,
                    Projectile.scale + (float)((Math.Sin(Main.timeForVisualEffects / 10f) + 1f) / 10f),
                    Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
            }
            Main.EntitySpriteDraw(SwordTexture,
                Projectile.Center - Main.screenPosition,
                SwordTexture.Frame(),
                lightColor,
                Projectile.rotation + MathHelper.PiOver4 * Projectile.spriteDirection,
                SwordTexture.Size() / 2,
                Projectile.scale,
                Projectile.spriteDirection == 1? SpriteEffects.None : SpriteEffects.FlipHorizontally);
            Main.EntitySpriteDraw(SwordTexture,
                Projectile.Center - Main.screenPosition,
                SwordTexture.Frame(),
                new Color(1f, 1f, 1f, 0f) * 0.5f,
                Projectile.rotation + MathHelper.PiOver4 * Projectile.spriteDirection,
                SwordTexture.Size() / 2,
                Projectile.scale + (float)((Math.Sin(Main.timeForVisualEffects / 10f) + 1f) / 10f),
                Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally);

            return false;
        }

    }
}
