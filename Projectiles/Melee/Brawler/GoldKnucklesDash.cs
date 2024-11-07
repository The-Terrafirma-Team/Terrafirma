using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Buffs.Debuffs;
using Terrafirma.Data;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Melee.Brawler
{
    public class GoldKnucklesDash : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileSets.TrueMeleeProjectiles[Type] = true;
            ProjectileSets.AutomaticallyGivenMeleeScaling[Type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.QuickDefaults(false, 32);
            Projectile.timeLeft = 50;
            Projectile.tileCollide = false;
            Projectile.Opacity = 0;
            Projectile.hide = true;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = height = 32;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            overPlayers.Add(index);
        }
        public override bool? CanDamage()
        {
            return Projectile.ai[0] > 0;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            player.SetDummyItemTime(2);

            Projectile.ai[0]++;

            Projectile.Center = player.Center + Vector2.Normalize(Projectile.velocity) * 4;
            if (Projectile.ai[0] == 0)
            {
                SoundEngine.PlaySound(SoundID.Item1 with { Volume = 2, Pitch = -0.5f}, Projectile.position);
            }

            if (Projectile.ai[0] < 0)
            {
                player.direction = Math.Sign(Projectile.velocity.X);
                Projectile.velocity = player.Center.DirectionTo(player.PlayerStats().MouseWorld) * 16;
                player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.None, Projectile.velocity.ToRotation() - MathHelper.PiOver2);
                player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, Projectile.velocity.ToRotation() - MathHelper.PiOver2);
                player.velocity *= 0.9f;
                player.velocity.Y -= player.gravity;

                Vector2 vector = Main.rand.NextVector2Circular(1, 1);
                Dust d = Dust.NewDustPerfect(player.GetFrontHandPosition(Player.CompositeArmStretchAmount.None, Projectile.velocity.ToRotation() - MathHelper.PiOver2) + vector * 20,DustID.Cloud,-vector * 3, 128);
                d.noGravity = true;
            }
            else
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
                player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.velocity.ToRotation() - MathHelper.PiOver2);
                player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.None, Projectile.velocity.ToRotation() - MathHelper.PiOver2);
                if (!Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                {
                    Projectile.tileCollide = true;
                }
                player.PlayerStats().TurnOffDownwardsMovementRestrictions = true;
                player.velocity = Projectile.velocity;
                if (Projectile.timeLeft == 1)
                {
                    player.velocity *= 0.5f;
                }
                Vector2 vector = Main.rand.NextVector2Circular(1, 1);
                Dust d = Dust.NewDustDirect(player.position,player.width,player.height, DustID.Cloud);
                d.alpha = 64;
                d.velocity = player.velocity;
                d.fadeIn = 1.2f;
                d.noGravity = true;
                if (Projectile.timeLeft < 10)
                {
                    Projectile.Opacity *= 0.9f;
                }
                else if (Projectile.ai[0] < 5)
                {
                    Projectile.Opacity += 0.1f;
                }
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {

            target.AddBuff(ModContent.BuffType<Stunned>(), 60 * 2);
            Main.player[Projectile.owner].velocity = -Projectile.velocity * 0.25f;
            Main.player[Projectile.owner].immune = true;
            Main.player[Projectile.owner].AddImmuneTime(ImmunityCooldownID.General,20);

            if (Terrafirma.ScreenshakeEnabled)
            {
                PunchCameraModifier modifier = new PunchCameraModifier(Main.LocalPlayer.MountedCenter, new Vector2(Main.rand.NextFloat(-1.5f, -0.7f), 0).RotatedBy(Main.LocalPlayer.MountedCenter.DirectionTo(Main.MouseWorld).ToRotation() + Main.rand.NextFloat(-0.1f, 0.1f)), 4, 12f, 20, 200f, Main.LocalPlayer.name);
                Main.instance.CameraModifiers.Add(modifier);
            }

            for (int i = 0; i < 10; i++)
            {
                Dust d = Dust.NewDustPerfect(target.Hitbox.ClosestPointInRect(Main.player[Projectile.owner].Center),DustID.Cloud,Main.rand.NextVector2Circular(8,8));
                d.noGravity = true;
                d.fadeIn = 1.5f;
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<Stunned>(), 60 * 2);
            Main.player[Projectile.owner].velocity = -Projectile.velocity * 0.25f;
            Main.player[Projectile.owner].immune = true;
            Main.player[Projectile.owner].AddImmuneTime(ImmunityCooldownID.General, 20);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            for (int i = 0; i < 3; i++)
            {
                TFUtils.EasyCenteredProjectileDraw(TextureAssets.Projectile[Type], Projectile, lightColor with { A = 64 } * 0.4f * Projectile.Opacity, (Projectile.Center - Projectile.velocity * i * 0.25f) - Main.screenPosition, Projectile.rotation, (1f - i * 0.2f) * Projectile.scale * Projectile.Opacity * 2);
            }
            TFUtils.EasyCenteredProjectileDraw(TextureAssets.Projectile[Type], Projectile, lightColor with { A = 64 } * 0.75f * Projectile.Opacity, Projectile.Center - Main.screenPosition, Projectile.rotation, Projectile.scale * Projectile.Opacity * 2);
            return false;
        }
    }
}
