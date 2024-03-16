using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terrafirma.Buffs.Minions;

namespace Terrafirma.Projectiles.Summon.Summons
{
    internal class Airship : ModProjectile
    {
        NPC targetnpc = null;
        float AngleToEnemy = 0f;   
        public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true;

            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.friendly = true;

            Projectile.height = 42;
            Projectile.width = 88;
            Projectile.DamageType = DamageClass.Summon;

            Projectile.tileCollide = false;
            Projectile.timeLeft = Projectile.SentryLifeTime;
            Projectile.penetrate = -1;

            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            

        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.velocity *= 0.95f;
            NPC closestnpc = TFUtils.FindClosestNPC(800f, Projectile.Center);

            if (Projectile.ai[1] == 0)
            {
                Projectile.ai[2] = Main.rand.NextFloat((float)-Math.PI, (float)Math.PI);
                
            }

            if (!player.HasBuff(ModContent.BuffType<AirshipBuff>())) Projectile.timeLeft = 1;

            if (player.HasMinionAttackTargetNPC)
            {
                if (targetnpc == null) Projectile.ai[2] = Main.rand.NextFloat((float)-Math.PI, (float)Math.PI);
                targetnpc = Main.npc[player.MinionAttackTargetNPC];
            }
            else if (closestnpc != null)
            {
                if (targetnpc == null) Projectile.ai[2] = Main.rand.NextFloat((float)-Math.PI, (float)Math.PI);
                targetnpc = closestnpc;
            }
            else
            { 
                targetnpc = null;
            }

            if (targetnpc == null)
            {
                AngleToEnemy = MathHelper.Lerp(AngleToEnemy, Projectile.spriteDirection == 1? 0.5f : (float)Math.PI - 0.5f, 0.02f);
                if (Projectile.Center.Distance(player.Center + new Vector2(120, 0).RotatedBy(Projectile.ai[2])) > 80f)
                {
                    Projectile.ai[0] += 0.01f;
                    Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.Center.DirectionTo(player.Center + new Vector2(120, 0).RotatedBy(Projectile.ai[2])) * 6f, Math.Clamp(Projectile.ai[0], 0f, 1f));
                }
                else
                {
                    Projectile.ai[0] = 0f;
                    if(Math.Abs(Projectile.velocity.Y) > 1f) Projectile.velocity.Y = MathHelper.Lerp(Projectile.velocity.Y, (float)Math.Sin(Projectile.ai[1] / 80f) / 2f, 0.005f);
                    else Projectile.velocity.Y = (float)Math.Sin(Projectile.ai[1] / 80f) / 6f;
                }
            }
            else
            {
                AngleToEnemy = (Projectile.Center + new Vector2(17 * Projectile.spriteDirection, 10).RotatedBy(Projectile.rotation)).AngleTo(targetnpc.Center);
                if (Projectile.Center.Distance(targetnpc.Center + new Vector2(360, 0).RotatedBy(Projectile.ai[2])) > 200f)
                {
                    Projectile.ai[0] += 0.01f;
                    Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.Center.DirectionTo(targetnpc.Center + new Vector2(360, 0).RotatedBy(Projectile.ai[2])) * 6f, Math.Clamp(Projectile.ai[0], 0f, 1f));

                }
                else
                {

                    Projectile.ai[0] = 0f;
                    if (Math.Abs(Projectile.velocity.Y) > 1f) Projectile.velocity.Y = MathHelper.Lerp(Projectile.velocity.Y, (float)Math.Sin(Projectile.ai[1] / 80f) / 2f, 0.005f);
                    else Projectile.velocity.Y = (float)Math.Sin(Projectile.ai[1] / 80f) / 6f;
                }

                if (Projectile.Center.Distance(targetnpc.Center + new Vector2(360, 0).RotatedBy(Projectile.ai[2])) <= 300f)
                {
                    if (Projectile.ai[1] % 14 == 0)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(),
                            Projectile.Center + new Vector2(17 * Projectile.spriteDirection, 10).RotatedBy(Projectile.rotation),
                            Projectile.DirectionTo(targetnpc.Center) * 18f,
                            ProjectileID.Bullet,
                            Projectile.damage,
                            Projectile.knockBack,
                            Projectile.owner,
                            0,
                            0,
                            0
                            );
                        Dust dust = Dust.NewDustPerfect(Projectile.Center + new Vector2(17 * Projectile.spriteDirection, 10).RotatedBy(Projectile.rotation) + new Vector2(18,0).RotatedBy(AngleToEnemy), DustID.Torch, Vector2.Zero, 1, Color.White, 1.2f);
                        dust.noGravity = true;
                        SoundEngine.PlaySound(SoundID.Item14 with { Volume = 0.2f }, Projectile.Center);
                    }
                }
            }

            Projectile.spriteDirection = Projectile.velocity.X >= 0 ? 1 : -1;
            Projectile.rotation = Math.Clamp(Projectile.velocity.X / 30f, (float)-Math.PI, (float)Math.PI);

            if (Math.Abs(Projectile.velocity.X) > 0.5f)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.Center - new Vector2(40 * Projectile.spriteDirection, -10).RotatedBy(Projectile.rotation), DustID.Torch, Vector2.Zero, 1, Color.White, 1.2f);
                dust.noGravity = true;
            }

            Projectile.ai[1]++;

        }
        public override bool PreDraw(ref Color lightColor)
        {
           


            Texture2D AirshipTexture = ModContent.Request<Texture2D>("Terrafirma/Projectiles/Summon/Summons/Airship").Value;

            Main.EntitySpriteDraw(AirshipTexture,
                Projectile.Center - Main.screenPosition,
                new Rectangle(0,0,88,42),
                Color.White,
                Projectile.rotation,
                new Vector2(44,21),
                Projectile.scale,
                Projectile.spriteDirection == 1? SpriteEffects.None : SpriteEffects.FlipHorizontally);

            Main.EntitySpriteDraw(AirshipTexture,
                Projectile.Center - Main.screenPosition + new Vector2(17 * Projectile.spriteDirection, 10).RotatedBy(Projectile.rotation),
                new Rectangle(88, 0, 22, 10),
                Color.White,
                Projectile.rotation + AngleToEnemy,
                new Vector2(5, 5),
                Projectile.scale,
                SpriteEffects.None);

            return false;
        }

    }
}
