using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terrafirma.Common.Items;
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
        public override int ManaCost => 1;
        public override int[] SpellItem => new int[] { ItemID.EmeraldStaff };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return false;
        }

        public override void OnLeftMousePressed(Item item, Player player)
        {
            if (player.statMana >= ManaCost && player.HeldItem == item)
            {
                beamproj = Projectile.NewProjectileDirect(player.GetSource_FromThis(), player.Center, Main.rand.NextVector2Circular(2f,2f), ModContent.ProjectileType<HealingEmeraldBeamProj>(), 100, 0, player.whoAmI);
            }
        }

        public override void OnLeftMouseReleased(Item item, Player player)
        {
            if (beamproj != null) beamproj.Kill();
            beamproj = null;
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
        Player targetplayer = null;
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.EmeraldBolt}";
        public override void SetDefaults()
        {
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.Size = new Vector2(16);
        }

        public override bool? CanHitNPC(NPC target) => false;
        public override bool CanHitPvp(Player target) => false;
        public override void AI()
        {
            Vector2 weaponend = Main.player[Projectile.owner].Center + new Vector2(Main.player[Projectile.owner].HeldItem.Size.Length(), 0).RotatedBy(Main.player[Projectile.owner].Center.DirectionTo(Main.MouseWorld).ToRotation());

            if (targetplayer != null)
            {
                Projectile.Center = targetplayer.Center;

                if ((Projectile.ai[0]-1) % 24 == 0)
                {
                    int healamount = (int)(2 * ((500f - Main.player[Projectile.owner].Center.Distance(targetplayer.Center)) / 500f));
                    targetplayer.Heal(healamount);
                }

                if ((Projectile.ai[0] - 1) % 4 == 0)
                {
                    for (int i = 0; i < weaponend.Distance(targetplayer.Center) / 7; i++)
                    {
                        BigSparkle bigsparkle = new BigSparkle();
                        bigsparkle.fadeInTime = 4;
                        bigsparkle.Rotation = Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2);
                        bigsparkle.Scale = 1.5f;
                        bigsparkle.velocity = Main.player[Projectile.owner].velocity;
                        bigsparkle.TimeLeft = 3;
                        bigsparkle.color = new Color(30, 180, 120, 0) * 0.2f;
                        
                        bigsparkle.secondaryColor = new Color(30, 180, 120, 0) * 0.2f;

                        Vector2 blendedpos = Vector2.Lerp(weaponend + weaponend.DirectionTo(Main.MouseWorld) * 7 * i, weaponend + weaponend.DirectionTo(targetplayer.Center) * 7 * i, i / (weaponend.Distance(targetplayer.Center) / 7));

                        ParticleSystem.AddParticle(bigsparkle, blendedpos, Vector2.Zero, bigsparkle.color);
                    }
                }

                if ((Projectile.ai[0]-1) % 8 == 0)
                {
                    for (int i = 0; i < weaponend.Distance(targetplayer.Center) / 10; i++)
                    {
                        BigSparkle bigsparkle = new BigSparkle();
                        bigsparkle.fadeInTime = 4;
                        bigsparkle.Rotation = Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2);
                        bigsparkle.Scale = 2f;
                        bigsparkle.velocity = Main.player[Projectile.owner].velocity;
                        bigsparkle.color = new Color(30, 180, 120, 0) * 0.5f;
                        bigsparkle.secondaryColor = new Color(30, 180, 120, 0) * 0.5f;

                        Vector2 blendedpos = Vector2.Lerp(weaponend + weaponend.DirectionTo(Main.MouseWorld) * 10 * i, weaponend + weaponend.DirectionTo(targetplayer.Center) * 10 * i, i / (weaponend.Distance(targetplayer.Center) / 10));

                        ParticleSystem.AddParticle(bigsparkle, blendedpos + Main.rand.NextVector2Circular(10, 10), Vector2.Zero, bigsparkle.color);
                    }    
                }

                if ((Projectile.ai[0] - 1) % 16 == 0)
                {
                    for (int i = 0; i < weaponend.Distance(targetplayer.Center) / 20; i++)
                    {
                        BigSparkle bigsparkle = new BigSparkle();
                        bigsparkle.fadeInTime = 5;
                        bigsparkle.Rotation = Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2);
                        bigsparkle.velocity = Main.player[Projectile.owner].velocity;
                        bigsparkle.Scale = 2f;
                        bigsparkle.color = new Color(30, 180, 120, 0);

                        Vector2 blendedpos = Vector2.Lerp(weaponend + weaponend.DirectionTo(Main.MouseWorld) * 20 * i, weaponend + weaponend.DirectionTo(targetplayer.Center) * 20 * i, i / (weaponend.Distance(targetplayer.Center) / 20));

                        ParticleSystem.AddParticle(bigsparkle, blendedpos + Main.rand.NextVector2Circular(4, 4), Vector2.Zero, bigsparkle.color);
                    }
                }

                if (Main.LocalPlayer.whoAmI == Projectile.owner) targetplayer = GetMousePlayer();
            }
            else
            {
                if (Main.LocalPlayer.whoAmI == Projectile.owner) targetplayer = GetMousePlayer();

                if ((Projectile.ai[0] - 1) % 4 == 0)
                {
                    Vector2 vel = weaponend.DirectionTo(Main.MouseWorld) * 3;
                    Dust.NewDust(weaponend, 4, 4, DustID.GemEmerald, vel.X, vel.Y, 0, new Color(255,255,255,0));            
                }


            }

            Projectile.ai[0]++;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((byte)targetplayer.whoAmI);
            base.SendExtraAI(writer);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            targetplayer = Main.player[reader.ReadByte()];
            base.ReceiveExtraAI(reader);
        }

        public Player GetMousePlayer()
        {
            float MinDist = 300f;
            Player target = null;   

            for (int i = 0; i < Main.player.Length; i++)
            {
                float calculatedDist = Main.player[i].Center.Distance(Main.MouseWorld);
                if (calculatedDist <= MinDist && Main.player[i].active && Main.player[i].team == Main.player[Projectile.owner].team)
                {
                    MinDist = calculatedDist;
                    target = Main.player[i];
                }
            }

            if (target != null && Main.player[Projectile.owner].Center.Distance(target.Center) > 500f) return null;

            return target;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> ProjTex = ModContent.Request<Texture2D>(Texture);

            Main.EntitySpriteDraw(ProjTex.Value,
                Projectile.Center,
                ProjTex.Value.Bounds,
                Color.White,
                0f,
                ProjTex.Size() / 2,
                1f,
                SpriteEffects.None);
            return false;
        }
    }

}
