﻿using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Terrafirma.Projectiles.Summon.Swarm
{
    internal class ZombiemancedZombie : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 6;
        }

        int MovementDirection = 1;
        int Jumps = 0;
        float MaxSpeed = 0;
        float MaxHeight = 0;
        int Variant = 0;
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.damage = 8;
            Projectile.height = 46;
            Projectile.width = 30;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 600;
            Projectile.penetrate = 6;
            Projectile.CritChance = 15;

        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            NPC closestnpc = TFUtils.FindClosestNPC(600f, Projectile.position);
            fallThrough = !(closestnpc != null && closestnpc.Top.Y <= Projectile.Bottom.Y);
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
        public override void AI()
        {

            Projectile.velocity.Y += 0.5f;

            if (MovementDirection == 1)
            {
                if (Projectile.velocity.X < MaxSpeed) { Projectile.velocity.X += 0.2f; }
                Projectile.spriteDirection = -1;
            }
            else
            {
                if (Projectile.velocity.X > -MaxSpeed) { Projectile.velocity.X -= 0.2f; }
                Projectile.spriteDirection = 1;
            }

            Projectile.ai[1]++;

            if (Projectile.ai[1] % 5 == 0)
            {
                if (Projectile.velocity.Y > 0.5f || Projectile.velocity.Y < -0.5f)
                {
                    Projectile.frame = 2 + 3 * Variant;
                }
                else
                {
                    if (Projectile.frame == 2 + 3 * Variant)
                    {
                        Projectile.frame = 0 + 3 * Variant;
                    }
                    else
                    {
                        Projectile.frame++;
                    }
                }

            }

            NPC closestnpc = TFUtils.FindClosestNPC(600f, Projectile.position);

            if (closestnpc != null && Projectile.ai[1] % 10 == 0)
            {
                if (Math.Abs(closestnpc.position.X - Projectile.Center.X) < 100f)
                {
                    if (Jumps > 0)
                    {
                        if (-Math.Abs(closestnpc.position.Y - Projectile.Center.Y) / 10 < -MaxHeight)
                        {
                            Projectile.velocity.Y = -MaxHeight;
                        }
                        else
                        {
                            Projectile.velocity.Y = -Math.Abs(closestnpc.position.Y - Projectile.Center.Y) / 10;
                        }
                        Jumps--;
                    }
                }

                if (closestnpc.position.X - Projectile.Center.X < 0)
                {
                    MovementDirection = 0;
                }
                else
                {
                    MovementDirection = 1;
                }
            }



        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {

            if (Projectile.velocity.X != oldVelocity.X)
            {
                if (oldVelocity.X > 0)
                {
                    MovementDirection = 0;
                }
                else
                {
                    MovementDirection = 1;
                }

                Projectile.velocity.X = 0;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = 0;
                Jumps = 1;
            }

            return false;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write7BitEncodedInt((int)(MaxSpeed * 10f));
            writer.Write7BitEncodedInt((int)(MaxHeight * 10f));
            base.SendExtraAI(writer);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            MaxSpeed = reader.Read7BitEncodedInt() / 10f;
            MaxHeight = reader.Read7BitEncodedInt() / 10f;
            base.ReceiveExtraAI(reader);
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 12; i++)
            {
                Dust.NewDust(Projectile.Center, 10, 10, DustID.Smoke, Main.rand.Next(-2, 2), Main.rand.Next(-2, 2), 0, default, Main.rand.NextFloat(1.8f, 2.5f));
            }
        }

        public override void OnSpawn(IEntitySource source)
        {
            MaxSpeed = Main.rand.NextFloat(2.8f, 3.2f);
            MaxHeight = Main.rand.NextFloat(13.8f, 14.2f);


            for (int i = 0; i < 12; i++)
            {
                Dust.NewDust(Projectile.Center, 10, 10, DustID.Smoke, Main.rand.Next(-2, 2), Main.rand.Next(-2, 2), 0, default, Main.rand.NextFloat(1.8f, 2.5f));
            }

            if (Main.rand.Next(0, 21) == 20)
            {
                Variant = 1;
            }

        }

    }
}
