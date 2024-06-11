using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terrafirma.Common.Items;
using Terrafirma.Common.Players;
using Terrafirma.Particles;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.GemStaves
{
    internal class HealingEmeraldBeam : Spell
    {
        Projectile beamproj = null;
        public override int UseAnimation => 5;
        public override int UseTime => 5;
        public override int ManaCost => 3;
        public override int[] SpellItem => new int[] { ItemID.EmeraldStaff };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return false;
        }

        public override void OnLeftMousePressed(Item item, Player player)
        {
            if (player.statMana >= ManaCost && player.HeldItem == item)
            {
                beamproj = Projectile.NewProjectileDirect(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<HealingEmeraldBeamProj>(), 100, 0, player.whoAmI);
            }
        }

        public override void OnLeftMouseReleased(Item item, Player player)
        {
            if (beamproj != null) beamproj.Kill();
            beamproj = null;
        }

        public override void UpdateLeftMouse(Item item, Player player)
        {
            if (player.statMana >= ManaCost && player.HeldItem == item && beamproj == null)
            {
                beamproj = Projectile.NewProjectileDirect(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<HealingEmeraldBeamProj>(), 100, 0, player.whoAmI);
            }
            if (!beamproj.active) beamproj = null;
        }

        public override void Update(Item item, Player player)
        {
            if (player.statMana < ManaCost)
            {
                LeftMouseSwitch = false;
            }
            if (beamproj == null && player.statMana >= ManaCost) LeftMouseSwitch = false;

            if (player.statMana < ManaCost)
            {
                if (beamproj != null) beamproj.Kill();
                beamproj = null;
            }

            item.UseSound = null;
        }

        public override void OnRightClick(Item item, Player player)
        {
            if (beamproj != null) beamproj.Kill();
            beamproj = null;
            base.OnRightClick(item, player);
        }
    }

    public class HealingEmeraldBeamProj : ModProjectile
    {
        Vector2 MousePos = Vector2.Zero;
        Player TargetPlayer = null;
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.EmeraldBolt}";
        public override void SetDefaults()
        {
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
            Projectile.Size = new Vector2(16);
            Projectile.scale = 1f;
            Projectile.Opacity = 0;
        }

        public override bool? CanHitNPC(NPC target) => false;
        public override bool CanHitPvp(Player target) => false;
        public override void AI()
        {
            if (Main.player[Projectile.owner].HeldItem.type != ItemID.EmeraldStaff) Projectile.Kill();
            
            if (Main.LocalPlayer == Main.player[Projectile.owner]) Main.player[Projectile.owner].SendMouseWorld();
            MousePos = Main.player[Projectile.owner].PlayerStats().MouseWorld;

            //Find Closest NPC to mouse
            int PlayerNum = -1;
            float minDist = -1;
            for (int i = 0; i < Main.player.Length; i++)
            {
                float Distance = Main.player[i].Distance(MousePos);
                if ((Distance < minDist || minDist == -1) && 
                    Main.player[i].active && 
                    Main.player[Projectile.owner].Center.Distance(Main.player[i].Center) <= 300)
                {
                    minDist = Distance;
                    PlayerNum = i;
                }
            }

            if (PlayerNum > -1) TargetPlayer = Main.player[PlayerNum];

            if (TargetPlayer != null && Main.player[Projectile.owner].Center.Distance(MousePos) <= 600)
            {
                Projectile.scale = 2f;
                Projectile.position = TargetPlayer.Center;
                if (Projectile.ai[0] % 4 == 0) GenerateCoolBeam(MousePos, TargetPlayer, 7, 4, 1.5f, 3, new Color(30, 180, 120, 0) * 0.2f, new Color(30, 180, 120, 0) * 0.2f, Vector2.Zero, true);
                if (Projectile.ai[0] % 8 == 0) GenerateCoolBeam(MousePos, TargetPlayer, 10, 4, 2f, -1, new Color(30, 180, 120, 0) * 0.5f, new Color(30, 180, 120, 0) * 0.5f, new Vector2(10), false);
                if (Projectile.ai[0] % 16 == 0) GenerateCoolBeam(MousePos, TargetPlayer, 20, 5, 2f, -1, new Color(30, 180, 120, 0), new Color(30, 180, 120, 0), new Vector2(4), false);
                if (Projectile.ai[0] % 30 == 0 && Projectile.ai[0] != 0) TargetPlayer.Heal(5);
            }

            TargetPlayer = null;
            Projectile.ai[0]++;
        }

        public void GenerateCoolBeam(Vector2 MousePos, Player targetplayer, int DistanceBetweenSparkles, int fadeintime, float scale, int timeleft, Color color, Color secondaryColor, Vector2 velocity, bool MakeLight)
        {
            Vector2 weaponend = Main.player[Projectile.owner].Center + new Vector2(Main.player[Projectile.owner].HeldItem.Size.Length(), 0).RotatedBy(Main.player[Projectile.owner].Center.DirectionTo(MousePos).ToRotation());
            for (int i = 0; i < weaponend.Distance(targetplayer.Center) / DistanceBetweenSparkles; i++)
            {
                BigSparkle bigsparkle = new BigSparkle();
                bigsparkle.fadeInTime = fadeintime;
                bigsparkle.Rotation = Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2);
                bigsparkle.Scale = scale;
                bigsparkle.velocity = Main.player[Projectile.owner].velocity;
                bigsparkle.TimeLeft = timeleft;
                bigsparkle.color = color;

                bigsparkle.secondaryColor = secondaryColor;

                Vector2 blendedpos = Vector2.Lerp(weaponend + weaponend.DirectionTo(MousePos) * DistanceBetweenSparkles * i, weaponend + weaponend.DirectionTo(targetplayer.Center) * DistanceBetweenSparkles * i, i / (weaponend.Distance(targetplayer.Center) / DistanceBetweenSparkles));

                ParticleSystem.AddParticle(bigsparkle, blendedpos + Main.rand.NextVector2Circular(velocity.X, velocity.Y), Vector2.Zero, bigsparkle.color);
                if (MakeLight) Lighting.AddLight(blendedpos, new Vector3(30, 180, 120) * (1 / 255f) * 0.5f);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {

            return true;
        }
    }

}
