using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terrafirma.Common;
using Terrafirma.Common.AIStyles;
using Terrafirma.Common.Interfaces;
using Terrafirma.Common.Mechanics;
using Terrafirma.Common.Systems;
using Terrafirma.Content.Buffs.Debuffs;
using Terrafirma.Content.Dusts;
using Terrafirma.Content.Particles;
using Terrafirma.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Content.NPCs.Vanilla.LavaSlime
{
    public class LavaSlime : GlobalNPC, ICustomBlockBehavior
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<ServerConfig>().CombatReworkEnabled;
        }
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.LavaSlime;
        }
        public override void Unload()
        {
            TextureAssets.Npc[NPCID.LavaSlime] = ModContent.Request<Texture2D>($"Terraria/Images/NPC_{NPCID.LavaSlime}");
            Main.npcFrameCount[NPCID.LavaSlime] = 2;
        }
        public override void SetStaticDefaults()
        {
            TextureAssets.Npc[NPCID.LavaSlime] = Mod.Assets.Request<Texture2D>("Content/NPCs/Vanilla/LavaSlime/LavaSlime");
            Glow = Mod.Assets.Request<Texture2D>("Content/NPCs/Vanilla/LavaSlime/LavaSlimeGlow");
            Main.npcFrameCount[NPCID.LavaSlime] = 8;
            DataSets.NPCWhitelistedForStun[NPCID.LavaSlime] = true;
        }
        private static Asset<Texture2D> Glow;
        private float frameCounter;
        private int frame;
        public void OnBlocked(Player player, float Power, NPC npc = null)
        {
            npc.ai[2] = 0;
            npc.AddBuff(ModContent.BuffType<Stunned>(), (int)(60 * 1.5f * Power));
        }
        public override void FindFrame(NPC npc, int frameHeight)
        {
            SlimeAI.FindFrame(npc, frameHeight, ref frameCounter, ref frame);
        }
        public override Color? GetAlpha(NPC npc, Color drawColor)
        {
            float percent = (float)Math.Sin((Main.timeForVisualEffects + (npc.whoAmI * 10)) * 0.1f) * 0.5f + 0.5f;
            percent *= 0.4f;
            percent += 0.6f;
            return npc.GetNPCColorTintedByBuffs(new Color(1f, percent, percent * percent * percent, 1f));
        }
        public override void SetDefaults(NPC npc)
        {
            npc.alpha = 0;
            npc.aiStyle = -1;
            npc.width = 34;
            npc.height = 30;
        }
        public override bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot)
        {
            return SlimeAI.CanAttack(npc);
        }
        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (npc.ai[2] <= 0)
            {
                npc.ai[3] = 0;
                npc.netUpdate = true;
            }
        }
        public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            if (npc.ai[2] <= 0)
            {
                npc.ai[3] = 0;
                npc.netUpdate = true;
            }
        }
        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (npc.ai[2] > 0)
                modifiers.DisableKnockback();
        }
        public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        {
            if (npc.ai[2] <= 0)
                return;
            modifiers.Knockback *= 2f;
            modifiers.SourceDamage *= 2.5f;
        }
        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
        {
            if (target.GetModPlayer<BlockingPlayer>().Blocking)
                return;
            target.AddBuff(BuffID.OnFire3, 60 * 4);
            if (npc.ai[2] > 0)
                target.AddBuff(BuffID.BrokenArmor, 60 * 8);
        }
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            //spriteBatch.Draw(TextureAssets.Npc[NPCID.LavaSlime].Value, npc.Bottom - screenPos + new Vector2(0, 4 * npc.scale), npc.frame, npc.GetNPCColorTintedByBuffs(npc.GetAlpha(Color.White)) * npc.Opacity, npc.rotation, new Vector2(npc.frame.Width / 2, npc.frame.Height), npc.scale, SpriteEffects.None, 0);
            float slamPercent = MathHelper.Clamp(npc.ai[2] / 40f, 0f, 1f);
            if (npc.ai[2] > 0 && npc.velocity.Y > 0)
            {
                for (int i = 0; i < 10 * slamPercent; i++)
                {
                    float percent = 1f - i / 10f;
                    percent *= slamPercent;
                    spriteBatch.Draw(Glow.Value, npc.Bottom - screenPos + new Vector2(MathF.Sin(i * 0.6f + (float)Main.timeForVisualEffects * 0.2f) * 8 * (1f - percent), 4 * npc.scale) - (npc.velocity * i * slamPercent), npc.frame with { X = npc.frame.Width }, npc.GetNPCColorTintedByBuffs(new Color(1f, percent, percent * percent * percent, 0.5f)) * percent * npc.Opacity, npc.rotation, new Vector2(npc.frame.Width / 2, npc.frame.Height), (0.6f + percent * 0.7f) * npc.scale * slamPercent, SpriteEffects.None, 0);
                }
            }

            spriteBatch.Draw(TextureAssets.Npc[NPCID.LavaSlime].Value, npc.Bottom - screenPos + new Vector2(0, 4 * npc.scale), npc.frame, npc.GetNPCColorTintedByBuffs(drawColor) * npc.Opacity, npc.rotation, new Vector2(npc.frame.Width / 2, npc.frame.Height), npc.scale, SpriteEffects.None, 0);
            spriteBatch.Draw(Glow.Value, npc.Bottom - screenPos + new Vector2(0, 4 * npc.scale), npc.frame, npc.GetNPCColorTintedByBuffs(new Color(1f, 1f, 1f, (float)Math.Sin((Main.timeForVisualEffects + (npc.whoAmI * 10)) * 0.1f) * 0.5f + 0.5f)) * npc.Opacity, npc.rotation, new Vector2(npc.frame.Width / 2, npc.frame.Height), npc.scale, SpriteEffects.None, 0);

            spriteBatch.Draw(Glow.Value, npc.Bottom - screenPos + new Vector2(0, 4 * npc.scale), npc.frame with { X = npc.frame.Width }, npc.GetNPCColorTintedByBuffs(new Color(slamPercent, slamPercent * 0.5f, 0f, 0f)) * npc.Opacity, npc.rotation, new Vector2(npc.frame.Width / 2, npc.frame.Height), npc.scale, SpriteEffects.None, 0);
            spriteBatch.Draw(Glow.Value, npc.Bottom - screenPos + new Vector2(0, 4 * npc.scale), npc.frame with { X = npc.frame.Width }, (new Color(60, 22, 8, 255) * slamPercent * slamPercent * slamPercent * 3) * npc.Opacity * 0.2f, npc.rotation, new Vector2(npc.frame.Width / 2, npc.frame.Height), npc.scale, SpriteEffects.None, 0);
            spriteBatch.Draw(Glow.Value, npc.Bottom - screenPos + new Vector2(0, 4 * npc.scale), npc.frame with { X = npc.frame.Width }, (new Color(60, 22, 8, 255) * slamPercent * slamPercent * slamPercent * 3) * npc.Opacity, npc.rotation, new Vector2(npc.frame.Width / 2, npc.frame.Height), npc.scale * 0.8f, SpriteEffects.None, 0);
            npc.DrawConfusedQuestionMark(spriteBatch, screenPos);
            return false;
        }
        public override void AI(NPC npc)
        {
            int bigSquishTime = -260;
            NPCStats stats = npc.NPCStats();
            float dist = Math.Abs(npc.Center.X - npc.targetRect.Center.X);
            if ((npc.ai[2] > 0 || (npc.ai[3] == 1 && npc.position.Y < npc.targetRect.Y - 60 && dist < 60 && npc.velocity.Y < 4)) && npc.ai[0] > 0)
            {
                npc.ai[2] += stats.AttackSpeed;
                if (npc.wet || npc.velocity.Y == 0)
                {
                    npc.ai[2] = 0;
                    npc.ai[3] = 0;
                    npc.ai[0] = bigSquishTime;
                    SoundEngine.PlaySound(SoundID.Item14, npc.position);
                    for (int i = 0; i < 15; i++)
                    {
                        ParticleSystem.NewParticle(new Smoke(Main.rand.NextVector2Circular(2, 1f) - Vector2.UnitY, Color.DarkGray * 0.25f, Color.Gray * 0.15f, Main.rand.NextFloat(0.8f, 1.2f)), npc.Bottom);
                    }
                    for (int i = 0; i < 30; i++)
                    {
                        Dust d2 = Dust.NewDustPerfect(npc.Bottom - new Vector2(0, 10), ModContent.DustType<SimpleColorableGlowierDust>());
                        d2.color = new Color(1f, Main.rand.NextFloat(0.1f, 0.5f), 0f, 0f);
                        d2.velocity = new Vector2(Main.rand.NextFloat(-4, 4), Main.rand.NextFloat(-10, -2));
                        d2.customData = Main.rand.Next(3);
                        //d2.noGravity = Main.rand.NextBool();
                        d2.scale *= Main.rand.NextFloat(1f, 2f);
                    }
                    DecalsSystem.NewDecal(new CrackDecal(1.5f), npc.Bottom);

                    int type = ModContent.ProjectileType<LavaSlimeSpike>();
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        for (int i = 1; i < 12; i++)
                        {
                            Vector2 location = FindSpotForSpike(npc.Bottom + new Vector2(i * 32, -16));
                            if (location == Vector2.Zero)
                            {
                                break;
                            }
                            Projectile.NewProjectile(npc.GetSource_FromThis(), FindSpotForSpike(npc.Bottom + new Vector2(i * 32, -16)), Vector2.Zero, type, 30, 3, ai2: i * i, ai1: npc.whoAmI);
                        }
                        for (int i = 1; i < 12; i++)
                        {
                            Vector2 location = FindSpotForSpike(npc.Bottom + new Vector2(i * -32, -16));
                            if (location == Vector2.Zero)
                            {
                                break;
                            }
                            Projectile.NewProjectile(npc.GetSource_FromThis(), FindSpotForSpike(npc.Bottom + new Vector2(i * -32, -16)), Vector2.Zero, type, 30, 3, ai2: i * i, ai1: npc.whoAmI);
                        }
                    }
                }
                npc.velocity.X *= 0.9f;
                npc.velocity.Y += (npc.ai[2] - 10) * 0.02f;
                if (npc.velocity.Y > 14)
                    npc.velocity.Y = 14;
                npc.MaxFallSpeedMultiplier *= 3;

                if (npc.velocity.Y > 0)
                    frame = 6;
            }
            else
            {
                if (npc.ai[0] < bigSquishTime + 60)
                    frame = 7;
                else if (npc.ai[0] < bigSquishTime + 80)
                    frame = 0;
                else if (npc.ai[0] < bigSquishTime + 110)
                    frame = 3;
            }

            stats.MoveSpeed *= 2f;
            stats.AttackSpeed *= 2.5f;
            SlimeAI.AI(npc, stats, ref frameCounter, -17, dist < 300 ? -17 : -6);

            if (npc.wet)
            {
                npc.velocity.Y -= 6f;
                npc.velocity.X += npc.direction * 0.5f * stats.MoveSpeed;
            }
            //if (npc.ai[0] <= 0 && Main.rand.NextBool(3))
            //{
            //    ParticleSystem.NewParticle(new Smoke(npc.velocity * 0.2f + Main.rand.NextVector2Circular(0.25f,0.25f), Color.Gray * 0.4f, Color.DarkGray * 0.25f, Main.rand.NextFloat(0.3f, 1.2f)), Main.rand.NextVector2FromRectangle(npc.Hitbox));
            //}
            if (stats.NoParticles)
                return;
            if (npc.ai[3] == 1)
            {
                Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.height, ModContent.DustType<SimpleColorableGlowierDust>());
                d.color = new Color(1f, Main.rand.NextFloat(0.1f, 0.5f), 0f, 0f);
                d.velocity *= 0.5f;
                d.velocity += npc.velocity * 0.4f;
                d.customData = 1;
                d.noGravity = Main.rand.NextBool();
                d.scale *= Main.rand.NextFloat(0.5f, 1.5f);
            }
            else if (Main.rand.NextBool(3))
            {
                Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.height, ModContent.DustType<SimpleColorableGlowierDust>());
                d.color = new Color(1f, Main.rand.NextFloat(0.1f, 0.5f), 0f, 0f);
                d.velocity = -Vector2.UnitY;
                d.customData = 1;
                d.noGravity = true;
            }
        }
        private Vector2 FindSpotForSpike(Vector2 point)
        {
            Point pointInWorld = point.ToTileCoordinates();
            if (!WorldGen.InWorld(pointInWorld.X, pointInWorld.Y))
                return Vector2.Zero;
            for (int i = 0; i < 16; i++)
            {
                Point PointToCheck = pointInWorld + new Point(0, i % 2 == 0? i / 2 : (-i +1) / 2);
                Vector2 worldCoords = PointToCheck.ToWorldCoordinates();
                //Dust d = Dust.NewDustPerfect(worldCoords, ModContent.DustType<SimpleColorableGlowyDust>());
                //d.noGravity = true;
                //d.velocity = Vector2.Zero;
                //d.fadeIn = 2;
                //d.color = new Color(1f, i / 16f, 1f, 0f);
                if (!Collision.IsWorldPointSolid(worldCoords) && (Main.tile[PointToCheck.X, PointToCheck.Y + 1].LiquidAmount > 0 || Collision.IsWorldPointSolid(new Vector2(worldCoords.X, worldCoords.Y + 16))))
                    return worldCoords + new Vector2(0,-4);
            }
            return Vector2.Zero;
        }
        public override bool? CanFallThroughPlatforms(NPC npc)
        {
            return SlimeAI.CanFallThrough(npc);
        }
    }
    public class LavaSlimeSpike : ModProjectile, ICustomBlockBehavior
    {
        public override void SetDefaults()
        {
            Projectile.QuickDefaults(true);
            Projectile.width = 16;
            Projectile.height = 40;
            Projectile.tileCollide = false;
            Projectile.hide = true;
        }
        public override void AI()
        {
            if (Projectile.localAI[0] == 0)
            {
                Projectile.localAI[0] = Main.rand.Next(1, 3);
                Projectile.rotation = Main.rand.NextFloat(-0.15f, 0.15f);
                Projectile.scale = Main.rand.NextFloat(0.8f, 1.2f);
            }
            if (Projectile.ai[2] == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    Dust d2 = Dust.NewDustPerfect(Projectile.Bottom - new Vector2(0,13), ModContent.DustType<SimpleColorableGlowierDust>());
                    d2.color = new Color(1f, Main.rand.NextFloat(0.1f, 0.5f), 0f, 0f) * Main.rand.NextFloat();
                    d2.velocity = new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-6, -2));
                    d2.customData = 0;
                }
            }
            Projectile.ai[2]--;


            if (Projectile.ai[2] > 0)
            {
                return;
            }
            Projectile.localAI[1]++;
            Projectile.frameCounter++;
            if (Projectile.frame < 4 && Projectile.frameCounter > 2)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
            }
            else if (Projectile.ai[2] < -80)
            {
                Projectile.Opacity -= 1 / 10f;
            }
            if (Projectile.ai[2] < -90)
                Projectile.Kill();
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Type].Value;
            Rectangle frame = tex.Frame(2, 5, 0, Projectile.frame);
            Rectangle glowFrame = frame with { X = frame.Width };
            var effect = Projectile.localAI[0] == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            float outlineGlow = 1f - Math.Min(Projectile.localAI[1] * 0.01f, 1);
            float solidColor = 1f - Math.Min(Projectile.localAI[1] * 0.03f, 1);

            Color glowColor = new Color(1f, 0.3f + (float)Math.Sin(Main.timeForVisualEffects * 0.1f + MathF.Sin(Projectile.identity) * 10) * 0.3f, 0f, 1f);
            for (int i = 0; i < 4; i++)
            {
                Main.EntitySpriteDraw(tex, Projectile.Bottom - Main.screenPosition + new Vector2(2 * Projectile.scale, 0).RotatedBy(Projectile.rotation + (i * MathHelper.PiOver2)), glowFrame, glowColor * Projectile.Opacity * outlineGlow, Projectile.rotation, new Vector2(12, 40), Projectile.scale, effect);
            }
            Main.EntitySpriteDraw(tex, Projectile.Bottom - Main.screenPosition, frame, lightColor * Projectile.Opacity, Projectile.rotation, new Vector2(12, 40), Projectile.scale, effect);
            Main.EntitySpriteDraw(tex, Projectile.Bottom - Main.screenPosition, glowFrame, glowColor with { A = 0 } * Projectile.Opacity * solidColor, Projectile.rotation, new Vector2(12, 40), Projectile.scale, effect);
            Main.EntitySpriteDraw(tex, Projectile.Bottom - Main.screenPosition, glowFrame, new Color(1f, 0.7f * solidColor * solidColor, 0.5f * solidColor * solidColor * solidColor, 0f) * Projectile.Opacity * solidColor * solidColor, Projectile.rotation, new Vector2(12, 40), Projectile.scale, effect);
            return false;
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            if(Projectile.ai[2] <= 0)
                behindNPCsAndTiles.Add(index);
        }
        public override bool CanHitPlayer(Player target)
        {
            return Projectile.frame > 3;
        }
        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            modifiers.HitDirectionOverride = Math.Sign(target.Center.X - Main.npc[(int)Projectile.ai[1]].Center.X);
            modifiers.Knockback *= 2f;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (!target.GetModPlayer<BlockingPlayer>().Blocking)
                target.AddBuff(BuffID.BrokenArmor, 60 * 4);
        }

        public void OnBlocked(Player player, float Power, NPC npc = null)
        {
            Projectile.Kill();
        }
    }
}
