using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using TerrafirmaRedux.Projectiles.Ranged.Boomerangs;
using System.Collections.Generic;

namespace TerrafirmaRedux.Projectiles.Summons
{
    internal class PortableSlimeBakery : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.height = 36;
            Projectile.width = 40;
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
            return false;
        }
        public override void AI()
        {
            Projectile.velocity.Y += 0.5f;
            Projectile.ai[0]++;
            if (Projectile.ai[0] % 160 == 0 && Main.myPlayer == Projectile.owner)
            {
                if (Projectile.ai[0] % (160 * 3) == 0)
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center,Vector2.Zero,ModContent.ProjectileType<OrangeSlimeFriend>(),Projectile.damage,Projectile.knockBack,Projectile.owner);
                else
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BlueSlimeFriend>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
        }
    }
}
