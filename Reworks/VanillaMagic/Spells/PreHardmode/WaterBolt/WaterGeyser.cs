using Microsoft.Xna.Framework;
using System;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.WaterBolt
{
    internal class WaterGeyser : Spell
    {
        public override int UseAnimation => 26;
        public override int UseTime => 26;
        public override int ManaCost => 16;
        public override int[] SpellItem => new int[] { ItemID.WaterBolt };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<WaterGeyserProj>();
            damage = (int)(damage * 0.5f);
            position = Main.MouseWorld;
            velocity = Vector2.Normalize(velocity) * 0.01f;
            knockback *= 0.2f;

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
    internal class WaterGeyserProj : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 8;
        }

        Vector2 OriginalPos;
        bool findtile = false;
        public override void SetDefaults()
        {
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.Size = new Vector2(28, 20);
            Projectile.timeLeft = 400;
            Projectile.Opacity = 0;
            Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * 0.7f;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.ai[2] % 2 == 0)
            {
                return base.CanHitNPC(target);
            }
            return false;

        }
        public override void AI()
        {

            Projectile.position.X += (float)Math.Sin(Projectile.ai[0] / 10) / 4;
            if (Projectile.timeLeft > 390)
            {
                Projectile.Opacity += 1 / 10f;
            }
            else if (Projectile.timeLeft < 60)
            {
                Projectile.Opacity -= 1 / 60f;
            }


            if (Projectile.ai[2] == 0 && !findtile)
            {
                float SetPos = 0;
                for (int i = 300; i > 0; i -= 4)
                {
                    if (Collision.SolidCollision(Projectile.Center + new Vector2(0, i * 2), Projectile.width, Projectile.height, true))
                    {
                        SetPos = new Vector2(0, Projectile.Bottom.Y + i * 2).ToTileCoordinates().Y * 16;
                    }
                }
                Projectile.position.Y = SetPos;
                findtile = true;
            }

            if (Projectile.ai[0] == 0)
            {
                OriginalPos = Projectile.Center;
            }

            if (Projectile.ai[0] % 3 == 0)
            {
                if (Projectile.frame == 3 + Projectile.ai[1] * 4)
                {
                    Projectile.frame = 0 + (int)(Projectile.ai[1] * 4);

                }
                else { Projectile.frame++; }
            }

            if (Projectile.ai[0] % 5 == 0)
            {

                Dust newdust = Dust.NewDustDirect(Projectile.Center, Projectile.width / 2, Projectile.height / 2, DustID.DungeonWater, 0, -2f, Projectile.alpha, Color.White, Projectile.Opacity);
                newdust.velocity.X *= 0.3f;
                newdust.noGravity = Main.rand.NextBool();
            }


            if (Projectile.ai[0] == 4 && Projectile.ai[2] <= 10 && Main.LocalPlayer.whoAmI == Projectile.owner)
            {
                if (Collision.SolidCollision(OriginalPos + new Vector2(0, -Projectile.height + 4), Projectile.width, Projectile.height))
                {
                    Projectile newproj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), OriginalPos + new Vector2(0, -Projectile.height + 6), Vector2.Zero, ModContent.ProjectileType<WaterGeyserProj>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 1, 11);
                    newproj.frame = Projectile.frame - 1;
                }
                else
                {
                    if (Projectile.ai[2] < 10)
                    {
                        Projectile newproj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), OriginalPos + new Vector2(0, -Projectile.height + 4), Vector2.Zero, ModContent.ProjectileType<WaterGeyserProj>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 0, Projectile.ai[2] + 1);
                        newproj.frame = Projectile.frame - 1;

                    }
                    else if (Projectile.ai[2] == 10)
                    {
                        Projectile newproj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), OriginalPos + new Vector2(0, -Projectile.height + 8), Vector2.Zero, ModContent.ProjectileType<WaterGeyserProj>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 1, Projectile.ai[2] + 1);
                        newproj.frame = Projectile.frame - 1;

                    }
                }
            }

            for (int i = 0; i < Main.player.Length; i++)
            {
                if (Main.player[i].Hitbox.Intersects(Projectile.Hitbox) && Main.player[i].velocity.Y > -45f)
                {
                    Main.player[i].AddBuff(BuffID.Wet, 120);
                    if (Math.Abs(Main.player[i].velocity.X) > 2f)
                    {
                        Main.player[i].velocity.Y -= Math.Abs(Main.player[i].velocity.X) / 20;
                    }
                    else
                    {
                        Main.player[i].velocity.Y -= 0.2f;
                    }
                }
            }
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].Hitbox.Intersects(Projectile.Hitbox) && Main.npc[i].velocity.Y > -45f && Main.npc[i].friendly)
                {
                    Main.npc[i].AddBuff(BuffID.Wet, 120);
                    if (Math.Abs(Main.npc[i].velocity.X) > 2f)
                    {
                        Main.npc[i].velocity.Y -= Math.Abs(Main.npc[i].velocity.X) / 20;
                    }
                    else
                    {
                        Main.npc[i].velocity.Y -= 0.2f;
                    }
                }
            }

            Projectile.ai[0]++;

        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
    }
}
