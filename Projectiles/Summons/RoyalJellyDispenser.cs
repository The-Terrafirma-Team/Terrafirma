using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using TerrafirmaRedux.Projectiles.Ranged.Boomerangs;
using System.Collections.Generic;
using Terraria.ID;
using TerrafirmaRedux.Global;

namespace TerrafirmaRedux.Projectiles.Summons
{
    internal class RoyalJellyDispenser : ModProjectile
    {
        float turretradius = 200f;
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.height = 56;
            Projectile.width = 42;
            Projectile.DamageType = DamageClass.Summon;
            
            Projectile.tileCollide = true;
            Projectile.timeLeft = Projectile.SentryLifeTime;
            Projectile.penetrate = -1;
            Projectile.sentry = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            behindProjectiles.Add(index);
            base.DrawBehind(index, behindNPCsAndTiles, behindNPCs, behindProjectiles, overPlayers, overWiresUI);
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override void AI()
        {
            turretradius = 200f * Projectile.GetSentryRangeMultiplier();

            for(int i = 0; i < Main.player.Length; i++)
            {
                if (Projectile.Center.Distance(Main.player[i].MountedCenter) < turretradius && Projectile.ai[0] >= 20 * Projectile.GetSentryAttackCooldownMultiplier() && Main.player[i].team == Main.player[Projectile.owner].team)
                {
                    for(int j = 0; j < Projectile.Center.Distance(Main.player[i].MountedCenter); j += 6)
                    {
                        Dust newdust = Dust.NewDustPerfect(Projectile.Center + new Vector2(0,-12) + new Vector2(j, (float)Math.Sin( (j / 20f) - ((float)Main.timeForVisualEffects / 30) ) * 15f).RotatedBy((Projectile.Center + new Vector2(0, -12)).DirectionTo(Main.player[i].MountedCenter).ToRotation()), DustID.Honey2, Vector2.Zero, 140, Scale : 1f + (((float)Math.Sin(j / 20f) + 1f) / 5f) );
                        newdust.noGravity = true;
                    }

                    for (int j = 0; j < 4; j ++)
                    {
                        Dust newdust = Dust.NewDustPerfect(Main.player[i].MountedCenter, DustID.Honey2, Main.rand.NextVector2Circular(1,1), 140);
                    }

                    if (Projectile.ai[1] % 4 == 0)
                    {
                        Main.player[i].Heal(2);
                        Main.player[i].HealEffect(2); 
                    }

                    Projectile.ai[0] = 0;
                    Projectile.ai[1]++;
                }
            }

            Projectile.ai[0]++;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> SentryBorder = ModContent.Request<Texture2D>("TerrafirmaRedux/Projectiles/Summons/RoyalJellyDispenserBorder");

            for (int i = 0; i < (int)(turretradius / 10); i++)
            {
                float maxi = turretradius / 10;
                float rotationvar = 200f;
                Main.EntitySpriteDraw(SentryBorder.Value, 
                    Projectile.Center - Main.screenPosition + new Vector2(turretradius, 0).RotatedBy((((360 / maxi) * i) * (Math.PI / 180)) + ((float)Main.timeForVisualEffects / rotationvar)), 
                    new Rectangle(0, 0, SentryBorder.Width(), SentryBorder.Height()), 
                    new Color(100, 60, 0, 0) * 0.6f, 
                    ((360 / maxi) * i) * (float)(Math.PI / 180) + ((float)Main.timeForVisualEffects / rotationvar) + MathHelper.PiOver2, 
                    SentryBorder.Size() / 2, 
                    0.8f * (((float)Math.Sin( ((float)Main.timeForVisualEffects + 10 * i ) / 10f) / 2f) + 1f ), 
                    SpriteEffects.None, 0);
                Main.EntitySpriteDraw(SentryBorder.Value,
                    Projectile.Center - Main.screenPosition + new Vector2(turretradius, 0).RotatedBy((((360 / maxi) * i) * (Math.PI / 180)) + ((float)Main.timeForVisualEffects / rotationvar)),
                    new Rectangle(0, 0, SentryBorder.Width(), SentryBorder.Height()),
                    new Color(100, 60, 0, 0) * 0.2f,
                    ((360 / maxi) * i) * (float)(Math.PI / 180) + ((float)Main.timeForVisualEffects / rotationvar) + MathHelper.PiOver2,
                    SentryBorder.Size() / 2,
                    1f * (((float)Math.Sin(((float)Main.timeForVisualEffects + 10 * i + 40) / 10f) / 2f) + 1f),
                    SpriteEffects.None, 0);
            }

            return true;
        }
    }
}
