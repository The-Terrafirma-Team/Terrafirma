using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terrafirma.Common;
using Terrafirma.Common.Interfaces;
using Terrafirma.Content.Buffs.Debuffs;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Content.NPCs.Vanilla
{
    public class Slime : GlobalNPC
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<ServerConfig>().CombatReworkEnabled;
        }
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.BlueSlime;
        }
        public override void Unload()
        {
            TextureAssets.Npc[NPCID.BlueSlime] = ModContent.Request<Texture2D>($"Terraria/Images/NPC_{NPCID.BlueSlime}");
            Main.npcFrameCount[NPCID.BlueSlime] = 2;
        }
        private static Asset<Texture2D> Extra;
        public override void SetStaticDefaults()
        {
            TextureAssets.Npc[NPCID.BlueSlime] = Mod.Assets.Request<Texture2D>("Assets/Resprites/NPCs/Slime");
            Extra = Mod.Assets.Request<Texture2D>("Assets/Resprites/NPCs/SlimeExtra");
            Main.npcFrameCount[NPCID.BlueSlime] = 10;
        }
        private float frameCounter;
        private int frame;

        public void OnBlocked(NPC npc, Player player, float Power)
        {
            npc.AddBuff(ModContent.BuffType<Stunned>(), (int)(60 * 2 * Power));
        }
        public override void FindFrame(NPC npc, int frameHeight)
        {
            npc.frame.Y = frame * frameHeight;
            if (npc.NPCStats().NoAnimation)
                return;
            if (npc.ai[2] == 0)
            {
                frameCounter += npc.NPCStats().MoveSpeed;
                if (frameCounter > 8)
                {
                    frameCounter = 0;
                    frame++;
                }

                if (frame > 3)
                    frame = 0;
                if (npc.velocity.Y < 0)
                    frame = 5;
                else if (npc.velocity.Y > 0)
                    frame = 4;
            }
        }
        public override void SetDefaults(NPC npc)
        {
            npc.alpha = 0;
            npc.aiStyle = -1;
            npc.height = 22;
            npc.width = 24;
            if (!npc.IsABestiaryIconDummy)
                npc.color = Color.Lerp(new Color(0, 80, 255, 130), new Color(0, 200, 255, 130), Main.rand.NextFloat());
        }
        public override void SetDefaultsFromNetId(NPC npc)
        {
            byte alpha = 130;
            if (!npc.IsABestiaryIconDummy)
                switch (npc.netID)
                {
                    case NPCID.RedSlime:
                        npc.color = Color.Lerp(new Color(255, 60, 0, alpha), new Color(255, 0, 60, alpha), Main.rand.NextFloat());
                        break;
                    case NPCID.YellowSlime:
                        npc.color = Color.Lerp(new Color(255, 255, 0, alpha), new Color(255, 175, 100, alpha), Main.rand.NextFloat());
                        break;
                    case NPCID.GreenSlime:
                        npc.color = Color.Lerp(new Color(0, 220, 40, alpha), new Color(160, 255, 128, alpha), Main.rand.NextFloat());
                        break;
                    case NPCID.PurpleSlime:
                        npc.color = Color.Lerp(new Color(128, 0, 255, alpha), new Color(255, 0, 255, alpha), Main.rand.NextFloat());
                        break;
                    case NPCID.JungleSlime:
                        npc.color = Color.Lerp(new Color(143, 215, 93, alpha), new Color(128, 64, 0, 255), Main.rand.NextFloat(0.75f));
                        break;
                }
        }
        public override bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot)
        {
            return CanAttack(npc);
        }
        private static bool CanAttack(NPC npc)
        {
            return npc.velocity.Y != 0 && npc.ai[3] == 1 && !npc.NPCStats().Immobile;
        }
        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            npc.ai[3] = 0;
            npc.netUpdate = true;
        }
        public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            npc.ai[3] = 0;
            npc.netUpdate = true;
        }
        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (npc.ai[2] > 0)
                modifiers.DisableKnockback();
        }
        public override void AI(NPC npc)
        {
            npc.rotation = npc.velocity.X * 0.1f * npc.velocity.Y * -0.1f;

            if (npc.direction == 0)
            {
                npc.direction = Main.rand.NextBool() ? 1 : -1;
            }
            if (npc.ai[0] < -500)
            {
                npc.ai[0] = Main.rand.Next(20);
                npc.netUpdate = true;
            }
            if (npc.wet)
            {

                npc.velocity.X += npc.direction * 0.1f * npc.NPCStats().MoveSpeed;
                if (npc.velocity.Y == 0)
                {
                    npc.velocity.Y = -2f;
                }
                if (npc.velocity.Y > 2f)
                {
                    npc.velocity.Y *= 0.9f;
                }
                npc.velocity.Y -= 0.5f;
                if (npc.velocity.Y < -4f)
                {
                    npc.velocity.Y = -4f;
                }
                npc.ai[3] = 1;
            }
            if (npc.NPCStats().Immobile)
            {
                npc.ai[3] = 0;
                npc.ai[2] = 0;
                npc.ai[0] = 0;
                frameCounter += 2;
                if (npc.velocity.Y == 0)
                {
                    npc.velocity.X *= 0.8f;
                }
                return;
            }

            if (npc.velocity.Y == 0)
            {
                npc.velocity.X *= 0.8f;
                npc.ai[0]+= npc.NPCStats().AttackSpeed;
                npc.ai[3] = 0;
            }
            else if (npc.ai[3] == 1)
            {
                if ((npc.direction == 1 && npc.velocity.X < 0.1f) || (npc.direction == -1 && npc.velocity.X > -0.1f) && CanAttack(npc))
                    npc.velocity.X += npc.direction * 0.6f * npc.NPCStats().MoveSpeed;
            }
            if (npc.ai[0] > 50)
                frameCounter += npc.NPCStats().MoveSpeed;
            if (npc.ai[0] > 75)
                frameCounter += npc.NPCStats().MoveSpeed;

            if (!npc.HasValidTarget)
            {
                if (npc.life < npc.lifeMax || !Main.dayTime || npc.position.Y > Main.worldSurface * 16)
                {
                    npc.TargetClosest(false);
                }

                npc.ai[2] = 0;
                if (npc.ai[0] > 100)
                {
                    if (Main.rand.NextBool(3))
                    {
                        npc.direction = Main.rand.NextBool() ? -1 : 1;
                    }
                    npc.ai[3] = 1;
                    npc.ai[0] = Main.rand.Next(-20, 0);
                    npc.velocity.X += npc.direction * Main.rand.NextFloat(2, 4) * npc.NPCStats().MoveSpeed;
                    npc.velocity.Y += Main.rand.NextFloat(-8, -5);
                    npc.netUpdate = true;
                }
            }
            else
            {
                Player p = Main.player[npc.target];

                if (npc.ai[0] > 100)
                {
                    npc.direction = p.Center.X < npc.Center.X ? -1 : 1;
                    if (npc.confused)
                        npc.direction *= -1;
                    npc.ai[3] = 1;
                    npc.ai[0] = Main.rand.Next(40);
                    npc.velocity.X += MathHelper.Clamp((p.Center.X - npc.Center.X) * 0.04f, -4, 4) * (npc.confused ? -1 : 1) * npc.NPCStats().MoveSpeed;
                    npc.velocity.Y += MathHelper.Clamp((p.Center.Y - npc.Center.Y) * 0.06f, -12, Main.rand.NextFloat(-8, -5));
                    npc.netUpdate = true;
                }
                else if ((((p.Center + p.velocity * 5).Distance(npc.Hitbox.ClosestPointInRect(p.Center)) < 32 * npc.scale && npc.ai[0] > 20) || npc.ai[2] > 0) && npc.ai[3] == 0) // Spike Attack
                {
                    npc.ai[0] = 0;
                    npc.ai[2]++;
                    switch ((int)npc.ai[2] + 7)
                    {
                        case 10:
                            frame = 6;
                            break;
                        case 30:
                            frame = 7;
                            break;
                        case 38:
                            frame = 8;
                            break;
                        case 40:
                            frame = 9;

                            if (npc.netID == NPCID.JungleSlime)
                            {
                                for (int i = 0; i < 7; i++)
                                {
                                    Dust.NewDustPerfect(npc.Center, DustID.Mud, (Main.rand.NextVector2CircularEdge(2, 1) * Main.rand.NextFloat(1, 2f)) + new Vector2(0, -2f), Main.rand.Next(128));
                                    Dust.NewDustPerfect(npc.Center, DustID.JungleGrass, (Main.rand.NextVector2CircularEdge(2, 1) * Main.rand.NextFloat(1, 2f)) + new Vector2(0, -2f));
                                }
                            }

                            else
                            {
                                for (int i = 0; i < 15; i++)
                                {
                                    Dust.NewDustPerfect(npc.Center, DustID.t_Slime, (Main.rand.NextVector2CircularEdge(2, 1) * Main.rand.NextFloat(1, 2f)) + new Vector2(0, -2f), Main.rand.Next(255), npc.color);
                                }
                            }
                            SoundEngine.PlaySound(SoundID.NPCDeath1, npc.position);
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                                Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Top, Vector2.Zero, ModContent.ProjectileType<SlimeStab>(), 15, 1, -1, npc.whoAmI);
                            break;
                        case 46:
                            frame = 8;
                            break;
                        case 50:
                            npc.ai[2] = 0;
                            break;
                    }
                }
            }
        }
        private static void DrawSlime(NPC npc, Vector2 position, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor, float Opacity)
        {
            Color highlightColor = Color.Lerp(npc.color, drawColor, 0.8f);

            switch (npc.netID)
            {
                case NPCID.BlackSlime:
                case NPCID.BabySlime:
                    highlightColor *= 0.2f;
                    break;
                case NPCID.JungleSlime:
                    highlightColor = highlightColor.MultiplyRGB(Color.Lerp(Color.Yellow, Color.RosyBrown, (npc.color.A - 130) / 125f)) * 0.6f;
                    break;
            }

            spriteBatch.Draw(Extra.Value, position - screenPos + new Vector2(0, 4 * npc.scale), npc.frame with { Width = 132 / 2 }, Color.Lerp(npc.GetColor(npc.GetNPCColorTintedByBuffs(drawColor)), Color.Black, 0.85f) * 0.4f * npc.Opacity * Opacity, npc.rotation, new Vector2(npc.frame.Width / 2, npc.frame.Height), npc.scale, SpriteEffects.None, 0);
            spriteBatch.Draw(TextureAssets.Npc[NPCID.BlueSlime].Value, position - screenPos + new Vector2(0, 4 * npc.scale), npc.frame, npc.GetColor(npc.GetNPCColorTintedByBuffs(drawColor)) * Opacity, npc.rotation, new Vector2(npc.frame.Width / 2, npc.frame.Height), npc.scale, SpriteEffects.None, 0);
            spriteBatch.Draw(Extra.Value, position - screenPos + new Vector2(0, 4 * npc.scale), npc.frame with { Width = 132 / 2, X = 132 / 2 }, highlightColor with { A = 0 } * npc.Opacity * Opacity, npc.rotation, new Vector2(npc.frame.Width / 2, npc.frame.Height), npc.scale, SpriteEffects.None, 0);
        }
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (npc.ai[1] > 0)
            {
                Main.GetItemDrawFrame((int)npc.ai[1], out var itemTexture, out var rectangle);
                float itemScale = 0.7f;
                spriteBatch.Draw(itemTexture, npc.Center - screenPos + npc.velocity * -0.3f + new Vector2(0, (float)Math.Sin(Main.timeForVisualEffects * 0.05f)), rectangle, drawColor, npc.rotation + ((float)Math.Sin(Main.timeForVisualEffects * 0.1f) * (float)Math.Cos(Main.timeForVisualEffects * 0.03f) * (npc.velocity.Length() + 1) * 0.1f), rectangle.Size() / 2, itemScale * npc.scale, SpriteEffects.None, 0);
            }
            if (CanAttack(npc))
            {
                for (int i = 0; i < 4; i++)
                {
                    DrawSlime(npc, npc.Bottom - (npc.velocity * i), spriteBatch, screenPos, Color.Lerp(drawColor, Color.Black, i * 0.1f),0.2f);
                }
            }
            DrawSlime(npc, npc.Bottom, spriteBatch, screenPos, drawColor, 1f);

            npc.DrawConfusedQuestionMark(spriteBatch, screenPos);
            return false;
        }
    }
    public class SlimeStab : ModProjectile, ICustomBlockBehavior
    {
        public override string Texture => $"Terraria/Images/Projectile_1";
        public override void SetDefaults()
        {
            Projectile.QuickDefaults(true);
            Projectile.timeLeft = 5;
            Projectile.tileCollide = false;
            Projectile.hide = true;
        }
        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            modifiers.Knockback *= 1.5f;
            target.AddBuff(BuffID.Slow, 60 * 15);
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return targetHitbox.ClosestPointInRect(targetHitbox.Center()).Distance(Projectile.Center) < (55 * Main.npc[(int)Projectile.ai[0]].scale);
        }
        public void OnBlocked(Player player, float Power)
        {
            player.ParryStrike(Main.npc[(int)Projectile.ai[0]]);
        }
    }
}
