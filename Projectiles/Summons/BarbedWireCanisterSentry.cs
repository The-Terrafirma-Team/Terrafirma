using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terrafirma.Projectiles.Ranged.Boomerangs;
using System.Collections.Generic;
using Terraria.ID;
using System.Linq;
using Terrafirma.Global;

namespace Terrafirma.Projectiles.Summons
{
    internal class BarbedWireCanisterSentry : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.height = 50;
            Projectile.width = 36;
            Projectile.DamageType = DamageClass.Summon;

            Projectile.tileCollide = true;
            Projectile.timeLeft = Projectile.SentryLifeTime;
            Projectile.penetrate = -1;
            Projectile.sentry = true;
            Projectile.hide = true;
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
            return base.CanHitNPC(target);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float collsionPoint = 0;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].type == Type && Main.projectile[i].active && Projectile.Center.Distance(Main.projectile[i].Center) < 400f)
                {
                    if (Collision.CheckAABBvLineCollision(targetHitbox.Center(), targetHitbox.Size(), Projectile.Center , Main.projectile[i].Center , 16, ref collsionPoint)) return true;
                }
            }
            return false;
        }

        public override void AI()
        {

        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D SentryBase = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Summons/BarbedWireCanisterSentry").Value;
            Texture2D BarbedWire = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Summons/BarbedWire").Value;
            for (int i = 0; i < Projectile.whoAmI; i++)
            {
                if (Main.projectile[i].sentry && Main.projectile[i].type == Type && Main.projectile[i].whoAmI < Projectile.whoAmI && Main.projectile[i].active && Projectile.Center.Distance(Main.projectile[i].Center) < 400f)
                {
                    for (int j = 0; j < Projectile.Center.Distance(Main.projectile[i].Center) / 17f; j++)
                    {
                        Main.EntitySpriteDraw(BarbedWire,
                        Projectile.Center + (Projectile.Center.DirectionTo(Main.projectile[i].Center) * j * 17f) - Main.screenPosition,
                        BarbedWire.Bounds,
                        lightColor,
                        Projectile.Center.AngleTo(Main.projectile[i].Center),
                        BarbedWire.Size() / 2,
                        1f,
                        SpriteEffects.None);
                    }

                }
            }

            Main.EntitySpriteDraw(SentryBase,
                            Projectile.Center + new Vector2(0,6) - Main.screenPosition,
                            SentryBase.Bounds,
                            lightColor,
                            0,
                            SentryBase.Size() / 2,
                            1f,
                            SpriteEffects.None);

            return false;
        }
    }
}
