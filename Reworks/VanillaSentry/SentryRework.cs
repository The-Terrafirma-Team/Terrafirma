using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace TerrafirmaRedux.Reworks.VanillaSentry
{
    public class SentryRework : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.sentry && entity.type <= 1021;
        }
        public override void SetDefaults(Projectile entity)
        {
        }
        public override bool PreAI(Projectile projectile)
        {
            return false;
        }
        public override void PostAI(Projectile projectile)
        {
            // Frost and Spider
            if(projectile.aiStyle == 53)
            {
                if (projectile.localAI[0] == 0f)
                {
                    projectile.localAI[1] = 1f;
                    projectile.localAI[0] = 1f;
                    projectile.ai[0] = 120f;
                    int num430 = 80;
                    SoundEngine.PlaySound(SoundID.Item46, projectile.position);
                    if (projectile.type == 308)
                    {
                        for (int num431 = 0; num431 < num430; num431++)
                        {
                            int num432 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 16f), projectile.width, projectile.height - 16, 185);
                            Dust dust2 = Main.dust[num432];
                            dust2.velocity *= 2f;
                            Main.dust[num432].noGravity = true;
                            dust2 = Main.dust[num432];
                            dust2.scale *= 1.15f;
                        }
                    }
                    if (projectile.type == 377)
                    {
                        projectile.frame = 4;
                        num430 = 40;
                        for (int num433 = 0; num433 < num430; num433++)
                        {
                            int num434 = Dust.NewDust(projectile.position + Vector2.UnitY * 16f, projectile.width, projectile.height - 16, 171, 0f, 0f, 100);
                            Main.dust[num434].scale = (float)Main.rand.Next(1, 10) * 0.1f;
                            Main.dust[num434].noGravity = true;
                            Main.dust[num434].fadeIn = 1.5f;
                            Dust dust2 = Main.dust[num434];
                            dust2.velocity *= 0.75f;
                        }
                    }
                    if (projectile.type == 966)
                    {
                        projectile.ai[1] = -1f;
                        projectile.frame = 0;
                        num430 = 30;
                        int num435 = 25;
                        int num436 = 30;
                        for (int num437 = 0; num437 < num430; num437++)
                        {
                            int num438 = Dust.NewDust(projectile.Center - new Vector2(num435, num436), num435 * 2, num436 * 2, 219);
                            Dust dust2 = Main.dust[num438];
                            dust2.velocity *= 2f;
                            Main.dust[num438].noGravity = true;
                            dust2 = Main.dust[num438];
                            dust2.scale *= 0.5f;
                        }
                    }
                }
                projectile.velocity.X = 0f;
                projectile.velocity.Y += 0.2f;
                if (projectile.velocity.Y > 16f)
                {
                    projectile.velocity.Y = 16f;
                }
                bool flag20 = false;
                float num439 = projectile.Center.X;
                float num440 = projectile.Center.Y;
                float num441 = 1000f * projectile.GetSentryRangeMultiplier();
                int num442 = -1;
                NPC ownerMinionAttackTargetNPC = projectile.OwnerMinionAttackTargetNPC;
                if (ownerMinionAttackTargetNPC != null && ownerMinionAttackTargetNPC.CanBeChasedBy(projectile))
                {
                    float num443 = ownerMinionAttackTargetNPC.position.X + (float)(ownerMinionAttackTargetNPC.width / 2);
                    float num444 = ownerMinionAttackTargetNPC.position.Y + (float)(ownerMinionAttackTargetNPC.height / 2);
                    float num445 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num443) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num444);
                    if (num445 < num441 && Collision.CanHit(projectile.position, projectile.width, projectile.height, ownerMinionAttackTargetNPC.position, ownerMinionAttackTargetNPC.width, ownerMinionAttackTargetNPC.height))
                    {
                        num441 = num445;
                        num439 = num443;
                        num440 = num444;
                        flag20 = true;
                        num442 = ownerMinionAttackTargetNPC.whoAmI;
                    }
                }
                if (!flag20)
                {
                    for (int num446 = 0; num446 < 200; num446++)
                    {
                        if (Main.npc[num446].CanBeChasedBy(projectile))
                        {
                            float num447 = Main.npc[num446].position.X + (float)(Main.npc[num446].width / 2);
                            float num448 = Main.npc[num446].position.Y + (float)(Main.npc[num446].height / 2);
                            float num449 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num447) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num448);
                            if (num449 < num441 && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[num446].position, Main.npc[num446].width, Main.npc[num446].height))
                            {
                                num441 = num449;
                                num439 = num447;
                                num440 = num448;
                                flag20 = true;
                                num442 = Main.npc[num446].whoAmI;
                            }
                        }
                    }
                }
                if (flag20)
                {
                    if (projectile.type == 966 && projectile.ai[1] != (float)num442)
                    {
                        projectile.ai[1] = num442;
                        projectile.netUpdate = true;
                    }
                    float num450 = num439;
                    float num451 = num440;
                    num439 -= projectile.Center.X;
                    num440 -= projectile.Center.Y;
                    int num452 = 0;
                    if (projectile.type != 966)
                    {
                        if (projectile.frameCounter > 0)
                        {
                            projectile.frameCounter--;
                        }
                        if (projectile.frameCounter <= 0)
                        {
                            int num453 = projectile.spriteDirection;
                            if (num439 < 0f)
                            {
                                projectile.spriteDirection = -1;
                            }
                            else
                            {
                                projectile.spriteDirection = 1;
                            }
                            num452 = ((!(num440 > 0f)) ? ((Math.Abs(num440) > Math.Abs(num439) * 3f) ? 4 : ((Math.Abs(num440) > Math.Abs(num439) * 2f) ? 3 : ((!(Math.Abs(num439) > Math.Abs(num440) * 3f)) ? ((Math.Abs(num439) > Math.Abs(num440) * 2f) ? 1 : 2) : 0))) : 0);
                            int num454 = projectile.frame;
                            if (projectile.type == 308)
                            {
                                projectile.frame = num452 * 2;
                            }
                            else if (projectile.type == 377)
                            {
                                projectile.frame = num452;
                            }
                            if (projectile.ai[0] > 40f && projectile.localAI[1] == 0f && projectile.type == 308)
                            {
                                projectile.frame++;
                            }
                            if (num454 != projectile.frame || num453 != projectile.spriteDirection)
                            {
                                projectile.frameCounter = 8;
                                if (projectile.ai[0] <= 0f)
                                {
                                    projectile.frameCounter = 4;
                                }
                            }
                        }
                    }
                    if (projectile.ai[0] <= 0f)
                    {
                        float num455 = 60f;
                        if (projectile.type == 966)
                        {
                            num455 = 90f;
                        }
                        projectile.localAI[1] = 0f;
                        projectile.ai[0] = num455 * projectile.GetSentryAttackCooldownMultiplier();
                        projectile.netUpdate = true;
                        if (Main.myPlayer == projectile.owner)
                        {
                            float num456 = 6f;
                            int num457 = 309;
                            if (projectile.type == 308)
                            {
                                num457 = 309;
                                num456 = 9f;
                            }
                            if (projectile.type == 377)
                            {
                                num457 = 378;
                                num456 = 9f;
                            }
                            if (projectile.type == 966)
                            {
                                num457 = 967;
                                num456 = 12.5f;
                            }
                            Vector2 vector37 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                            if (projectile.type == 966)
                            {
                                vector37.Y -= 16f;
                            }
                            else
                            {
                                switch (num452)
                                {
                                    case 0:
                                        vector37.Y += 12f;
                                        vector37.X += 24 * projectile.spriteDirection;
                                        break;
                                    case 1:
                                        vector37.Y += 0f;
                                        vector37.X += 24 * projectile.spriteDirection;
                                        break;
                                    case 2:
                                        vector37.Y -= 2f;
                                        vector37.X += 24 * projectile.spriteDirection;
                                        break;
                                    case 3:
                                        vector37.Y -= 6f;
                                        vector37.X += 14 * projectile.spriteDirection;
                                        break;
                                    case 4:
                                        vector37.Y -= 14f;
                                        vector37.X += 2 * projectile.spriteDirection;
                                        break;
                                }
                            }
                            if (projectile.type != 966 && projectile.spriteDirection < 0)
                            {
                                vector37.X += 10f;
                            }
                            float num458 = num450 - vector37.X;
                            float num459 = num451 - vector37.Y;
                            float num460 = (float)Math.Sqrt(num458 * num458 + num459 * num459);
                            float num461 = num460;
                            num460 = num456 / num460;
                            num458 *= num460;
                            num459 *= num460;
                            int num462 = projectile.damage;
                            projectile.NewProjectileButWithChangesFromSentryBuffs(projectile.GetSource_FromThis(), vector37, new Vector2(num458, num459), num457, num462, projectile.knockBack, Main.myPlayer);

                        }
                    }
                }
                else
                {
                    if (projectile.type == 966 && projectile.ai[1] != -1f)
                    {
                        projectile.ai[1] = -1f;
                        projectile.netUpdate = true;
                    }
                    if (projectile.type != 966 && projectile.ai[0] <= 60f && (projectile.frame == 1 || projectile.frame == 3 || projectile.frame == 5 || projectile.frame == 7 || projectile.frame == 9))
                    {
                        projectile.frame--;
                    }
                }
                if (projectile.ai[0] > 0f)
                {
                    projectile.ai[0] -= 1f;
                }
            }
            // ML drops
            else if (projectile.aiStyle == 123)
            {
                bool flag60 = projectile.type == 641;
                bool flag61 = projectile.type == 643;
                float num953 = 1000f;
                projectile.velocity = Vector2.Zero;
                if (flag60)
                {
                    projectile.alpha -= 5;
                    if (projectile.alpha < 0)
                    {
                        projectile.alpha = 0;
                    }
                    if (projectile.direction == 0)
                    {
                        projectile.direction = Main.player[projectile.owner].direction;
                    }
                    projectile.rotation -= (float)projectile.direction * ((float)Math.PI * 2f) / 120f;
                    projectile.scale = projectile.Opacity;
                    Lighting.AddLight(projectile.Center, new Vector3(0.3f, 0.9f, 0.7f) * projectile.Opacity);
                    if (Main.rand.NextBool(2))
                    {
                        Vector2 vector152 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
                        Dust dust54 = Main.dust[Dust.NewDust(projectile.Center - vector152 * 30f, 0, 0, 229)];
                        dust54.noGravity = true;
                        dust54.position = projectile.Center - vector152 * Main.rand.Next(10, 21);
                        dust54.velocity = vector152.RotatedBy(1.5707963705062866) * 6f;
                        dust54.scale = 0.5f + Main.rand.NextFloat();
                        dust54.fadeIn = 0.5f;
                        dust54.customData = projectile.Center;
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        Vector2 vector153 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
                        Dust dust55 = Main.dust[Dust.NewDust(projectile.Center - vector153 * 30f, 0, 0, 240)];
                        dust55.noGravity = true;
                        dust55.position = projectile.Center - vector153 * 30f;
                        dust55.velocity = vector153.RotatedBy(-1.5707963705062866) * 3f;
                        dust55.scale = 0.5f + Main.rand.NextFloat();
                        dust55.fadeIn = 0.5f;
                        dust55.customData = projectile.Center;
                    }
                    if (projectile.ai[0] < 0f)
                    {
                        Vector2 center18 = projectile.Center;
                        int num954 = Dust.NewDust(center18 - Vector2.One * 8f, 16, 16, 229, projectile.velocity.X / 2f, projectile.velocity.Y / 2f);
                        Dust dust2 = Main.dust[num954];
                        dust2.velocity *= 2f;
                        Main.dust[num954].noGravity = true;
                        Main.dust[num954].scale = Terraria.Utils.SelectRandom<float>(Main.rand, 0.8f, 1.65f);
                        Main.dust[num954].customData = projectile;
                    }
                }
                if (flag61)
                {
                    projectile.alpha -= 5;
                    if (projectile.alpha < 0)
                    {
                        projectile.alpha = 0;
                    }
                    if (projectile.direction == 0)
                    {
                        projectile.direction = Main.player[projectile.owner].direction;
                    }
                    if (++projectile.frameCounter >= 3)
                    {
                        projectile.frameCounter = 0;
                        if (++projectile.frame >= Main.projFrames[projectile.type])
                        {
                            projectile.frame = 0;
                        }
                    }
                    if (projectile.alpha == 0 && Main.rand.NextBool(15))
                    {
                        Dust dust56 = Main.dust[Dust.NewDust(projectile.Top, 0, 0, 261, 0f, 0f, 100)];
                        dust56.velocity.X = 0f;
                        dust56.noGravity = true;
                        dust56.fadeIn = 1f;
                        dust56.position = projectile.Center + Vector2.UnitY.RotatedByRandom(6.2831854820251465) * (4f * Main.rand.NextFloat() + 26f);
                        dust56.scale = 0.5f;
                    }
                    projectile.localAI[0]++;
                    if (projectile.localAI[0] >= 60f)
                    {
                        projectile.localAI[0] = 0f;
                    }
                }
                if (projectile.ai[0] < 0f)
                {
                    projectile.ai[0]++;
                    if (flag60)
                    {
                        projectile.ai[1] -= (float)projectile.direction * ((float)Math.PI / 8f) / 50f;
                    }
                }
                if (projectile.ai[0] == 0f)
                {
                    int num955 = -1;
                    float num956 = num953;
                    NPC ownerMinionAttackTargetNPC4 = projectile.OwnerMinionAttackTargetNPC;
                    if (ownerMinionAttackTargetNPC4 != null && ownerMinionAttackTargetNPC4.CanBeChasedBy(projectile))
                    {
                        float num957 = projectile.Distance(ownerMinionAttackTargetNPC4.Center);
                        if (num957 < num956 && Collision.CanHitLine(projectile.Center, 0, 0, ownerMinionAttackTargetNPC4.Center, 0, 0))
                        {
                            num956 = num957;
                            num955 = ownerMinionAttackTargetNPC4.whoAmI;
                        }
                    }
                    if (num955 < 0)
                    {
                        for (int num958 = 0; num958 < 200; num958++)
                        {
                            NPC nPC12 = Main.npc[num958];
                            if (nPC12.CanBeChasedBy(projectile))
                            {
                                float num959 = projectile.Distance(nPC12.Center);
                                if (num959 < num956 && Collision.CanHitLine(projectile.Center, 0, 0, nPC12.Center, 0, 0))
                                {
                                    num956 = num959;
                                    num955 = num958;
                                }
                            }
                        }
                    }
                    if (num955 != -1)
                    {
                        projectile.ai[0] = 1f;
                        projectile.ai[1] = num955;
                        projectile.netUpdate = true;
                        return;
                    }
                }
                if (!(projectile.ai[0] > 0f))
                {
                    return;
                }
                int num960 = (int)projectile.ai[1];
                if (!Main.npc[num960].CanBeChasedBy(projectile))
                {
                    projectile.ai[0] = 0f;
                    projectile.ai[1] = 0f;
                    projectile.netUpdate = true;
                    return;
                }
                projectile.ai[0]++;
                float num961 = 30f;
                if (flag60)
                {
                    num961 = 10f * projectile.GetSentryAttackCooldownMultiplier();
                }
                if (flag61)
                {
                    num961 = 5f * projectile.GetSentryAttackCooldownMultiplier();
                }
                if (!(projectile.ai[0] >= num961))
                {
                    return;
                }
                Vector2 vector154 = projectile.DirectionTo(Main.npc[num960].Center);
                if (vector154.HasNaNs())
                {
                    vector154 = Vector2.UnitY;
                }
                float num962 = vector154.ToRotation();
                int num963 = ((vector154.X > 0f) ? 1 : (-1));
                if (flag60)
                {
                    projectile.direction = num963;
                    projectile.ai[0] = -20f;
                    projectile.ai[1] = num962 + (float)num963 * (float)Math.PI / 6f;
                    projectile.netUpdate = true;
                    if (projectile.owner == Main.myPlayer)
                    {
                        projectile.NewProjectileButWithChangesFromSentryBuffs(projectile.GetSource_FromThis(), projectile.Center, vector154, 642, projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[1], projectile.whoAmI);
                    }
                }
                if (!flag61)
                {
                    return;
                }
                projectile.direction = num963;
                projectile.ai[0] = -20f;
                projectile.netUpdate = true;
                if (projectile.owner != Main.myPlayer)
                {
                    return;
                }
                NPC nPC13 = Main.npc[num960];
                Vector2 vector155 = nPC13.position + nPC13.Size * Terraria.Utils.RandomVector2(Main.rand, 0f, 1f) - projectile.Center;
                for (int num964 = 0; num964 < 3; num964++)
                {
                    Vector2 other = projectile.Center + vector155;
                    Vector2 vector156 = nPC13.velocity * 30f;
                    other += vector156;
                    float num965 = MathHelper.Lerp(0.1f, 0.75f, Terraria.Utils.GetLerpValue(800f, 200f, projectile.Distance(other)));
                    if (num964 > 0)
                    {
                        other = projectile.Center + vector155.RotatedByRandom(0.78539818525314331) * (Main.rand.NextFloat() * num965 + 0.5f);
                    }
                    float x7 = Main.rgbToHsl(new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB)).X;
                    projectile.NewProjectileButWithChangesFromSentryBuffs(projectile.GetSource_FromThis(), other, Vector2.Zero, 644, projectile.damage, projectile.knockBack, projectile.owner, x7, projectile.whoAmI);
                }
            }
            else if (projectile.aiStyle == 130)
            {
                AI_130_FlameBurstTower(projectile); // Why is the timer like that :((
            }
            else if (projectile.aiStyle == 134)
            {
                AI_134_Ballista(projectile); // Good boy :3
            }
        }

        private void AI_130_FlameBurstTower(Projectile projectile)
        {
            //Main.NewText($"{projectile.ai[0]}" + $" {projectile.ai[1]}" + $" {projectile.ai[2]}" + "|" + $"{projectile.localAI[0]}" + $" {projectile.localAI[1]}" + $" {projectile.localAI[2]}", Main.DiscoColor);

            float num = 900f;
            float angleRatioMax = 1f;
            Vector2 vector = projectile.Center;
            int num2 = 664;
            float num4 = 12f;
            int num5 = 1;
            int num6 = 6;
            int num7 = 4;
            int num8 = 80;
            switch (projectile.type)
            {
                case 663:
                    {
                        Lighting.AddLight(projectile.Center, new Vector3(0.4f, 0.2f, 0.1f));
                        Lighting.AddLight(projectile.Bottom + new Vector2(0f, -10f), new Vector3(0.4f, 0.2f, 0.1f));
                        vector = projectile.Bottom + new Vector2(projectile.direction * 6, -40f);
                        if ((projectile.localAI[0] += 1f) >= 300f)
                        {
                            projectile.localAI[0] = 0f;
                        }
                        Rectangle r3 = new Rectangle((int)projectile.position.X + projectile.width / 4, (int)projectile.position.Y + projectile.height - 16, projectile.width / 4 * 3, 6);
                        if (projectile.direction == 1)
                        {
                            r3.X -= projectile.width / 4;
                        }
                        for (int m = 0; m < 1; m++)
                        {
                            if (Main.rand.NextBool(2))
                            {
                                Dust dust5 = Dust.NewDustDirect(r3.TopLeft() + new Vector2(-2f, -2f), r3.Width + 4, r3.Height + 4, 270, -projectile.direction * 2, -2f, 200, new Color(255, 255, 255, 0));
                                dust5.fadeIn = 0.6f + Main.rand.NextFloat() * 0.6f;
                                dust5.scale = 0.4f;
                                dust5.noGravity = true;
                                dust5.noLight = true;
                                dust5.velocity = Vector2.Zero;
                                dust5.velocity.X = (float)(-projectile.direction) * Main.rand.NextFloat() * dust5.fadeIn;
                            }
                        }
                        r3 = new Rectangle((int)projectile.Center.X, (int)projectile.Bottom.Y, projectile.width / 4, 10);
                        if (projectile.direction == -1)
                        {
                            r3.X -= r3.Width;
                        }
                        r3.X += projectile.direction * 4;
                        r3.Y -= projectile.height - 10;
                        for (int n = 0; n < 1; n++)
                        {
                            if (Main.rand.NextBool(5))
                            {
                                Dust dust6 = Dust.NewDustDirect(r3.TopLeft(), r3.Width, r3.Height, 6);
                                dust6.fadeIn = 1f;
                                dust6.scale = 1f;
                                dust6.noGravity = true;
                                dust6.noLight = true;
                                dust6.velocity *= 2f;
                            }
                        }
                        break;
                    }
                case 665:
                    {
                        Lighting.AddLight(projectile.Center, new Vector3(0.4f, 0.2f, 0.1f) * 1.2f);
                        Lighting.AddLight(projectile.Bottom + new Vector2(0f, -10f), new Vector3(0.4f, 0.2f, 0.1f) * 1.2f);
                        num8 = 70;
                        num4 += 3f;
                        num6 = 8;
                        num2 = 666;
                        vector = projectile.Bottom + new Vector2(projectile.direction * 6, -44f);
                        if ((projectile.localAI[0] += 1f) >= 300f)
                        {
                            projectile.localAI[0] = 0f;
                        }
                        Rectangle r2 = new Rectangle((int)projectile.position.X + projectile.width / 4, (int)projectile.position.Y + projectile.height - 16, projectile.width / 4 * 2, 6);
                        if (projectile.direction == 1)
                        {
                            r2.X -= projectile.width / 4;
                        }
                        for (int k = 0; k < 1; k++)
                        {
                            if (Main.rand.NextBool(2))
                            {
                                Dust dust3 = Dust.NewDustDirect(r2.TopLeft() + new Vector2(-2f, -2f), r2.Width + 4, r2.Height + 4, 270, -projectile.direction * 2, -2f, 200, new Color(255, 255, 255, 0));
                                dust3.fadeIn = 0.6f + Main.rand.NextFloat() * 0.6f;
                                dust3.scale = 0.4f;
                                dust3.noGravity = true;
                                dust3.noLight = true;
                                dust3.velocity = Vector2.Zero;
                                dust3.velocity.X = (float)(-projectile.direction) * Main.rand.NextFloat() * dust3.fadeIn;
                            }
                        }
                        r2 = new Rectangle((int)projectile.Center.X, (int)projectile.Bottom.Y, projectile.width / 4, 10);
                        if (projectile.direction == -1)
                        {
                            r2.X -= r2.Width;
                        }
                        r2.X += projectile.direction * 4;
                        r2.Y -= projectile.height - 10;
                        for (int l = 0; l < 2; l++)
                        {
                            if (Main.rand.NextBool(5))
                            {
                                Dust dust4 = Dust.NewDustDirect(r2.TopLeft(), r2.Width, r2.Height, 6);
                                dust4.fadeIn = 1f;
                                dust4.scale = 1f;
                                dust4.noGravity = true;
                                dust4.noLight = true;
                                dust4.velocity *= 2f;
                            }
                        }
                        break;
                    }
                case 667:
                    {
                        Lighting.AddLight(projectile.Center, new Vector3(0.4f, 0.2f, 0.1f) * 1.5f);
                        Lighting.AddLight(projectile.Bottom + new Vector2(0f, -10f), new Vector3(0.4f, 0.2f, 0.1f) * 1.5f);
                        num8 = 60;
                        num4 += 6f;
                        num6 = 8;
                        num2 = 668;
                        vector = projectile.Bottom + new Vector2(projectile.direction * 6, -46f);
                        if ((projectile.localAI[0] += 1f) >= 300f)
                        {
                            projectile.localAI[0] = 0f;
                        }
                        Rectangle r = new Rectangle((int)projectile.position.X + projectile.width / 4, (int)projectile.position.Y + projectile.height - 16, projectile.width / 4 * 2, 6);
                        if (projectile.direction == 1)
                        {
                            r.X -= projectile.width / 4;
                        }
                        for (int i = 0; i < 1; i++)
                        {
                            if (Main.rand.NextBool(2))
                            {
                                Dust dust = Dust.NewDustDirect(r.TopLeft() + new Vector2(-2f, -2f), r.Width + 4, r.Height + 4, 270, -projectile.direction * 2, -2f, 200, new Color(255, 255, 255, 0));
                                dust.fadeIn = 0.6f + Main.rand.NextFloat() * 0.6f;
                                dust.scale = 0.4f;
                                dust.noGravity = true;
                                dust.noLight = true;
                                dust.velocity = Vector2.Zero;
                                dust.velocity.X = (float)(-projectile.direction) * Main.rand.NextFloat() * dust.fadeIn;
                            }
                        }
                        r = new Rectangle((int)projectile.Center.X, (int)projectile.Bottom.Y, projectile.width / 4, 10);
                        if (projectile.direction == -1)
                        {
                            r.X -= r.Width;
                        }
                        r.X += projectile.direction * 4;
                        r.Y -= projectile.height - 10;
                        for (int j = 0; j < 3; j++)
                        {
                            if (Main.rand.NextBool(5))
                            {
                                Dust dust2 = Dust.NewDustDirect(r.TopLeft(), r.Width, r.Height, 6);
                                dust2.fadeIn = 1.1f;
                                dust2.scale = 1f;
                                dust2.noGravity = true;
                                dust2.noLight = true;
                                dust2.velocity *= 2.4f;
                            }
                        }
                        break;
                    }
            }
            if (Main.player[projectile.owner].setApprenticeT2)
            {
                angleRatioMax = 0.1f;
                num *= 1.5f;
                num4 *= 1.4f;
            }
            if (projectile.ai[0] == 0f)
            {
                projectile.direction = (projectile.spriteDirection = Main.player[projectile.owner].direction);
                projectile.ai[0] = 1f;
                projectile.ai[1] = 0f;
                projectile.netUpdate = true;
            }
            if (projectile.ai[0] == 1f)
            {
                projectile.frame = 0;
                bool flag = false;
                if (projectile.ai[1] > 0f)
                {
                    projectile.ai[1] -= 1f * projectile.GetSentryAttackCooldownMultiplierInverse();
                }
                else
                {
                    flag = true;
                }
                if (flag && projectile.owner == Main.myPlayer)
                {
                    int num9 = AI_130_FlameBurstTower_FindTarget(num, angleRatioMax, vector,projectile);
                    if (num9 != -1)
                    {
                        projectile.direction = Math.Sign(projectile.DirectionTo(Main.npc[num9].Center).X);
                        projectile.ai[0] = 2f;
                        projectile.ai[1] = 0f;
                        projectile.netUpdate = true;
                    }
                }
            }
            else if (projectile.ai[0] == 2f)
            {
                projectile.frame = num5 + (int)(projectile.ai[1] / (float)num7);
                if (projectile.ai[1] <= 0)
                {
                    Vector2 vector2 = new Vector2(projectile.direction, 0f);
                    int num10 = AI_130_FlameBurstTower_FindTarget(num, angleRatioMax, vector, projectile, canChangeDirection: false);
                    if (num10 != -1)
                    {
                        vector2 = (Main.npc[num10].Center - vector).SafeNormalize(Vector2.UnitX * projectile.direction);
                    }
                    Vector2 vector3 = vector2 * num4;
                    if (projectile.owner == Main.myPlayer)
                    {
                        projectile.NewProjectileButWithChangesFromSentryBuffs(projectile.GetSource_FromThis(), vector, vector3, num2, projectile.damage, projectile.knockBack, projectile.owner);
                    }
                }
                if ((projectile.ai[1] += 1f * projectile.GetSentryAttackCooldownMultiplierInverse()) >= (float)(num6 * num7))
                {
                    projectile.ai[0] = 1f;
                    projectile.ai[1] = num8;
                }
            }
            projectile.spriteDirection = projectile.direction;
            projectile.tileCollide = true;
            projectile.velocity.Y += 0.2f;
        }
        private int AI_130_FlameBurstTower_FindTarget(float shot_range, float angleRatioMax, Vector2 shootingSpot, Projectile projectile, bool canChangeDirection = true)
        {
            shot_range *= projectile.GetSentryRangeMultiplier();
            int num = -1;
            NPC ownerMinionAttackTargetNPC = projectile.OwnerMinionAttackTargetNPC;
            if (ownerMinionAttackTargetNPC != null && ownerMinionAttackTargetNPC.CanBeChasedBy(projectile))
            {
                for (int i = 0; i < 1; i++)
                {
                    if (!ownerMinionAttackTargetNPC.CanBeChasedBy(projectile))
                    {
                        continue;
                    }
                    float num2 = Vector2.Distance(shootingSpot, ownerMinionAttackTargetNPC.Center);
                    if (!(num2 > shot_range))
                    {
                        Vector2 vector = (ownerMinionAttackTargetNPC.Center - shootingSpot).SafeNormalize(Vector2.UnitY);
                        if (!(Math.Abs(vector.X) < Math.Abs(vector.Y) * angleRatioMax) && (canChangeDirection || !((float)projectile.direction * vector.X < 0f)) && (num == -1 || num2 < Vector2.Distance(shootingSpot, Main.npc[num].Center)) && Collision.CanHitLine(shootingSpot, 0, 0, ownerMinionAttackTargetNPC.Center, 0, 0))
                        {
                            num = ownerMinionAttackTargetNPC.whoAmI;
                        }
                    }
                }
                if (num != -1)
                {
                    return num;
                }
            }
            for (int j = 0; j < 200; j++)
            {
                NPC nPC = Main.npc[j];
                if (!nPC.CanBeChasedBy(projectile))
                {
                    continue;
                }
                float num3 = Vector2.Distance(shootingSpot, nPC.Center);
                if (!(num3 > shot_range))
                {
                    Vector2 vector2 = (nPC.Center - shootingSpot).SafeNormalize(Vector2.UnitY);
                    if (!(Math.Abs(vector2.X) < Math.Abs(vector2.Y) * angleRatioMax) && (canChangeDirection || !((float)projectile.direction * vector2.X < 0f)) && (num == -1 || num3 < Vector2.Distance(shootingSpot, Main.npc[num].Center)) && Collision.CanHitLine(shootingSpot, 0, 0, nPC.Center, 0, 0))
                    {
                        num = j;
                    }
                }
            }
            return num;
        }

        private void AI_134_Ballista(Projectile projectile)
        {
            //IL_02dd: Unknown result type (might be due to invalid IL or missing references)
            float shot_range = 900f;
            float deadBottomAngle = 0.75f;
            Vector2 center = projectile.Center;
            int num = 680;
            int num2 = 12;
            float num3 = 16f;
            int num4 = 1;
            int num5 = 5;
            int num6 = 5;
            if (Main.player[projectile.owner].setSquireT2)
            {
                num3 = 21f;
            }
            int ballistraShotDelay = (int)(Projectile.GetBallistraShotDelay(Main.player[projectile.owner]) * projectile.GetSentryAttackCooldownMultiplier());
            num2 = num6;
            int num7 = projectile.type;
            if (num7 == 677)
            {
                center.Y -= 4f;
            }
            if (projectile.ai[0] == 0f)
            {
                projectile.direction = (projectile.spriteDirection = Main.player[projectile.owner].direction);
                projectile.ai[0] = 1f;
                projectile.ai[1] = 0f;
                projectile.netUpdate = true;
                if (projectile.direction == -1)
                {
                    projectile.rotation = (float)Math.PI;
                }
            }
            if (projectile.ai[0] == 1f)
            {
                projectile.frame = 0;
                bool flag = false;
                if (Main.player[projectile.owner].ballistaPanic && projectile.ai[1] > 60f)
                {
                    projectile.ai[1] = 60f;
                }
                if (Main.player[projectile.owner].ballistaPanic && Main.player[projectile.owner].setSquireT3 && projectile.ai[1] > 30f)
                {
                    projectile.ai[1] = 30f;
                }
                if (projectile.ai[1] > 0f)
                {
                    projectile.ai[1] -= 1f;
                }
                else
                {
                    flag = true;
                }
                int num8 = AI_134_Ballista_FindTarget(shot_range, deadBottomAngle, center, projectile);
                if (num8 != -1)
                {
                    Vector2 vector = (vector = (Main.npc[num8].Center - center).SafeNormalize(Vector2.UnitY));
                    projectile.rotation = projectile.rotation.AngleLerp(vector.ToRotation(), 0.08f);
                    if (projectile.rotation > (float)Math.PI / 2f || projectile.rotation < -(float)Math.PI / 2f)
                    {
                        projectile.direction = -1;
                    }
                    else
                    {
                        projectile.direction = 1;
                    }
                    if (flag && projectile.owner == Main.myPlayer)
                    {
                        projectile.direction = Math.Sign(vector.X);
                        projectile.ai[0] = 2f;
                        projectile.ai[1] = 0f;
                        projectile.netUpdate = true;
                    }
                }
                else
                {
                    float targetAngle = 0f;
                    if (projectile.direction == -1)
                    {
                        targetAngle = (float)Math.PI;
                    }
                    projectile.rotation = projectile.rotation.AngleLerp(targetAngle, 0.05f);
                }
            }
            else if (projectile.ai[0] == 2f)
            {
                projectile.frame = num4 + (int)(projectile.ai[1] / (float)num6);
                if (projectile.ai[1] == (float)num2)
                {
                    SoundEngine.PlaySound(SoundID.DD2_BallistaTowerShot, projectile.Center);
                    Vector2 vector2 = new Vector2(projectile.direction, 0f);
                    int num9 = AI_134_Ballista_FindTarget(shot_range, deadBottomAngle, center, projectile);
                    if (num9 != -1)
                    {
                        vector2 = (Main.npc[num9].Center - center).SafeNormalize(Vector2.UnitX * projectile.direction);
                    }
                    projectile.rotation = vector2.ToRotation();
                    if (projectile.rotation > (float)Math.PI / 2f || projectile.rotation < -(float)Math.PI / 2f)
                    {
                        projectile.direction = -1;
                    }
                    else
                    {
                        projectile.direction = 1;
                    }
                    Vector2 vector3 = vector2 * num3;
                    if (projectile.owner == Main.myPlayer)
                    {
                        projectile.NewProjectileButWithChangesFromSentryBuffs(projectile.GetSource_FromThis(), center, vector3, num, projectile.damage, projectile.knockBack, projectile.owner);
                    }
                }
                if ((projectile.ai[1] += 1f) >= (float)(num5 * num6))
                {
                    projectile.ai[0] = 1f;
                    projectile.ai[1] = ballistraShotDelay;
                }
            }
            projectile.spriteDirection = projectile.direction;
            projectile.tileCollide = true;
            projectile.velocity.Y += 0.2f;
        }
        private int AI_134_Ballista_FindTarget(float shot_range, float deadBottomAngle, Vector2 shootingSpot, Projectile projectile)
        {
            shot_range *= projectile.GetSentryRangeMultiplier();
            int num = -1;
            NPC ownerMinionAttackTargetNPC = projectile.OwnerMinionAttackTargetNPC;
            if (ownerMinionAttackTargetNPC != null && ownerMinionAttackTargetNPC.CanBeChasedBy(this))
            {
                for (int i = 0; i < 1; i++)
                {
                    if (!ownerMinionAttackTargetNPC.CanBeChasedBy(this))
                    {
                        continue;
                    }
                    float num2 = Vector2.Distance(shootingSpot, ownerMinionAttackTargetNPC.Center);
                    if (!(num2 > shot_range))
                    {
                        Vector2 vector = (ownerMinionAttackTargetNPC.Center - shootingSpot).SafeNormalize(Vector2.UnitY);
                        if ((!(Math.Abs(vector.X) < Math.Abs(vector.Y) * deadBottomAngle) || !(vector.Y > 0f)) && (num == -1 || num2 < Vector2.Distance(shootingSpot, Main.npc[num].Center)) && Collision.CanHitLine(shootingSpot, 0, 0, ownerMinionAttackTargetNPC.Center, 0, 0))
                        {
                            num = ownerMinionAttackTargetNPC.whoAmI;
                        }
                    }
                }
                if (num != -1)
                {
                    return num;
                }
            }
            for (int j = 0; j < 200; j++)
            {
                NPC nPC = Main.npc[j];
                if (!nPC.CanBeChasedBy(this))
                {
                    continue;
                }
                float num3 = Vector2.Distance(shootingSpot, nPC.Center);
                if (!(num3 > shot_range))
                {
                    Vector2 vector2 = (nPC.Center - shootingSpot).SafeNormalize(Vector2.UnitY);
                    if ((!(Math.Abs(vector2.X) < Math.Abs(vector2.Y) * deadBottomAngle) || !(vector2.Y > 0f)) && (num == -1 || num3 < Vector2.Distance(shootingSpot, Main.npc[num].Center)) && Collision.CanHitLine(shootingSpot, 0, 0, nPC.Center, 0, 0))
                    {
                        num = j;
                    }
                }
            }
            return num;
        }
    }
}
