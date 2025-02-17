﻿using Microsoft.Xna.Framework;
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
    public class GoldKnucklesPunch : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileSets.TrueMeleeProjectiles[Type] = true;
            ProjectileSets.AutomaticallyGivenMeleeScaling[Type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.QuickDefaults(false, 16);
            Projectile.penetrate = 3;
            Projectile.stopsDealingDamageAfterPenetrateHits = true;
            Projectile.scale = 1.3f;
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
                //Main.player[Projectile.owner].velocity -= Projectile.velocity * (1f - target.knockBackResist) * 1.2f;
                Main.player[Projectile.owner].GiveTension(5);
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (Projectile.ai[2] == 0)
            {
                Projectile.ai[2] = 1;
                //Main.player[Projectile.owner].velocity -= Projectile.velocity * 1.2f;
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
            Projectile.velocity *= 0.96f;
            Projectile.Center = (player.MountedCenter + new Vector2(0,player.gfxOffY)) + Vector2.Normalize(Projectile.velocity) * Projectile.ai[1] * Projectile.velocity.Length() * 19 * Math.Max(Projectile.scale,0.75f);

            if(Projectile.timeLeft < 10 || Projectile.ai[2] > 0)
            {
                Projectile.Opacity *= 0.9f;
            }

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
                player.SetCompositeArmFront(true, stretch, Projectile.velocity.ToRotation() - MathHelper.PiOver2);
                player.SetCompositeArmBack(true, stretch2, Projectile.velocity.ToRotation() - MathHelper.PiOver2);
            }
            else
            {
                player.SetCompositeArmFront(true, stretch2, Projectile.velocity.ToRotation() - MathHelper.PiOver2);
                player.SetCompositeArmBack(true, stretch, Projectile.velocity.ToRotation() - MathHelper.PiOver2);
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            for(int i = 1; i < 3; i++)
            {
                TFUtils.EasyCenteredProjectileDraw(TextureAssets.Projectile[Type], Projectile, lightColor with { A = 64 } * 0.2f * Projectile.Opacity, (Projectile.Center - Projectile.velocity * i * 1.5f) - Main.screenPosition, Projectile.rotation, (1f - i * 0.1f) * Projectile.scale);
            }
            TFUtils.EasyCenteredProjectileDraw(TextureAssets.Projectile[Type],Projectile,lightColor with { A = 64} * 0.5f * Projectile.Opacity);
            return false;
        }
    }
}
