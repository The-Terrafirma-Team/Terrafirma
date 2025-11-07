using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Linq;
using Terrafirma.Common;
using Terrafirma.Common.Interfaces;
using Terrafirma.Content.Buffs.Debuffs;
using Terrafirma.Content.Dusts;
using Terrafirma.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Content.NPCs.Vanilla.DemonEye
{
    public class DemonEye : GlobalNPC, ICustomBlockBehavior, ISpecificBlockConditions
    {
        private static Asset<Texture2D> Glow;
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<ServerConfig>().CombatReworkEnabled;
        }
        public override bool InstancePerEntity => true;

        int[] DemonEyes = { NPCID.DemonEye, NPCID.CataractEye, NPCID.PurpleEye, NPCID.GreenEye, NPCID.DemonEyeOwl, NPCID.DemonEyeSpaceship, NPCID.DialatedEye, NPCID.SleepyEye };
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return DemonEyes.Contains(entity.type);
        }
        public void OnBlocked(Player player, float Power, NPC npc = null)
        {
            if (npc.HasBuff<Flightless>())
            {
                npc.AddBuff(ModContent.BuffType<Stunned>(), (int)(60 * 5 * Power));
            }
            npc.AddBuff(ModContent.BuffType<Flightless>(), (int)(60 * 5 * Power));
        }
        public override void SetDefaults(NPC npc)
        {
            npc.aiStyle = -1;
            npc.noGravity = true;
            npc.DeathSound = SoundID.NPCDeath52;
        }
        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (npc.ai[2] > DashTime)
                npc.ai[2] = 2000;
        }
        public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            if (npc.ai[2] > DashTime)
                npc.ai[2] = 2000;
        }
        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (npc.ai[2] > DashTime)
                modifiers.Knockback *= 6;
        }
        public override void SetStaticDefaults()
        {
            for (int i = 0; i < DemonEyes.Length; i++)
            {
                Main.npcFrameCount[DemonEyes[i]] = 12;
                DataSets.NPCWhitelistedForStun[DemonEyes[i]] = true;
            }
            string path = "Content/NPCs/Vanilla/DemonEye/";
            Glow = Mod.Assets.Request<Texture2D>(path + "DemonEyeGlow");
            TextureAssets.Npc[NPCID.DemonEye] = Mod.Assets.Request<Texture2D>(path + "DemonEye");
            TextureAssets.Npc[NPCID.CataractEye] = Mod.Assets.Request<Texture2D>(path + "DemonEye_Cataract");
            TextureAssets.Npc[NPCID.PurpleEye] = Mod.Assets.Request<Texture2D>(path + "DemonEye_Purple");
            TextureAssets.Npc[NPCID.GreenEye] = Mod.Assets.Request<Texture2D>(path + "DemonEye_Green");
            TextureAssets.Npc[NPCID.DemonEyeOwl] = Mod.Assets.Request<Texture2D>(path + "DemonEye"); // need sprite
            TextureAssets.Npc[NPCID.DemonEyeSpaceship] = Mod.Assets.Request<Texture2D>(path + "DemonEye"); // ditto
            TextureAssets.Npc[NPCID.DialatedEye] = Mod.Assets.Request<Texture2D>(path + "DemonEye_Dilated");
            TextureAssets.Npc[NPCID.SleepyEye] = Mod.Assets.Request<Texture2D>(path + "DemonEye"); // ditto
        }
        public override void Unload()
        {
            for (int i = 0; i < DemonEyes.Length; i++)
            {
                TextureAssets.Npc[NPCID.DemonEye] = ModContent.Request<Texture2D>($"Terraria/Images/NPC_{DemonEyes[i]}");
                Main.npcFrameCount[DemonEyes[i]] = 2;
            }
        }
        public override void FindFrame(NPC npc, int frameHeight)
        {
            // WHY IS THE DEMON EYE CODED THE WAY IT IS
            if (npc.noGravity)
            {
                if (npc.ai[2] is > DashTime || npc.ai[2] < DashTime - 50)
                    npc.localAI[0] = npc.localAI[0].AngleLerp(npc.velocity.ToRotation(), 0.1f * npc.NPCStats().MoveSpeed);
            }
            else
                npc.localAI[0] += npc.velocity.X * 0.1f;
            if (!npc.IsABestiaryIconDummy)
                npc.rotation = npc.localAI[0] + (npc.spriteDirection == 1 ? 0 : MathHelper.Pi);

            npc.frameCounter++;
            if (npc.noGravity)
            {
                if (npc.frame.Y > frameHeight * 5)
                {
                    npc.frame.Y = 0;
                }
            }
            else
            {
                if (npc.frame.Y > frameHeight * 11 || npc.frame.Y < frameHeight * 6)
                {
                    npc.frame.Y = frameHeight * 6;
                }
            }
        }
        const int DashTime = 120;
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Asset<Texture2D> tex = TextureAssets.Npc[npc.type];
            NPCStats stats = npc.NPCStats();
            float GlowOpacity = (npc.ai[2] - DashTime + 35) * 1f / 30;
            if (GlowOpacity > 1)
                GlowOpacity = 1;

            if (GlowOpacity > 0)
                for (int i = 0; i < 4; i++)
                {
                    spriteBatch.Draw(Glow.Value, npc.Center - screenPos + new Vector2(0, 2 * npc.scale).RotatedBy(npc.rotation + (i * MathHelper.PiOver2)), npc.frame, Terrafirma.UnparryableAttackColor * GlowOpacity, npc.rotation, npc.frame.Size() / 2, npc.scale, npc.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
                }

            spriteBatch.Draw(tex.Value, npc.Center - screenPos, npc.frame, Color.Lerp(npc.GetNPCColorTintedByBuffs(drawColor), Color.Black, GlowOpacity), npc.rotation, npc.frame.Size() / 2, npc.scale, npc.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);


            float flashSize = MathF.Sin(MathHelper.Clamp((npc.ai[2] - DashTime + (15 * stats.AttackSpeed)) * 0.06f / stats.AttackSpeed, 0, 1) * MathHelper.Pi) * 1.5f;
            Asset<Texture2D> highlight = TextureAssets.Extra[ExtrasID.ThePerfectGlow];
            for (int i = 0; i < 2; i++)
            {
                spriteBatch.Draw(highlight.Value, npc.Center - screenPos + new Vector2(npc.frame.Width * 0.5f * npc.spriteDirection * npc.scale, 0).RotatedBy(npc.rotation), null, Terrafirma.UnparryableAttackColor with { A = 0}, i * MathHelper.PiOver2 + ((float)Main.timeForVisualEffects * 0.1f), highlight.Size() / 2, npc.scale * new Vector2(0.5f, 1f) * flashSize, SpriteEffects.None, 0);
                spriteBatch.Draw(highlight.Value, npc.Center - screenPos + new Vector2(npc.frame.Width * 0.5f * npc.spriteDirection * npc.scale, 0).RotatedBy(npc.rotation), null, new Color(1f, 1f, 1f, 0f) * 0.5f, i * MathHelper.PiOver2 + ((float)Main.timeForVisualEffects * 0.1f), highlight.Size() / 2, npc.scale * new Vector2(0.4f, 0.4f) * flashSize, SpriteEffects.None, 0);
            }
            return false;
        }
        public override void AI(NPC npc)
        {
            NPCStats stats = npc.NPCStats();

            if (Main.rand.NextBool(40))
            {
                npc.position += npc.netOffset;
                int num4 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + npc.height * 0.25f), npc.width, (int)(npc.height * 0.5f), DustID.Blood, npc.velocity.X, 2f);
                Main.dust[num4].velocity.X *= 0.5f;
                Main.dust[num4].velocity.Y *= 0.1f;
                npc.position -= npc.netOffset;
            }

            if (npc.wet)
            {
                if (npc.velocity.Y > 0f)
                {
                    npc.velocity.Y *= 0.95f;
                }
                npc.velocity.Y -= 0.5f;
                if (npc.velocity.Y < -4f)
                {
                    npc.velocity.Y = -4f;
                }
                npc.TargetClosest();
            }

            if (stats.NoFlight || stats.Immobile)
            {
                npc.ai[2] = 0;
                npc.noGravity = false;
                if (npc.collideY)
                {
                    npc.velocity.X *= 0.98f;
                    if (!stats.Immobile)
                    {
                        npc.velocity.Y -= Main.rand.NextFloat(4, 6);
                        npc.velocity.X += Main.rand.NextFloat(-1f, 1f) * stats.MoveSpeed;
                        npc.netUpdate = true;
                    }
                }
                if (npc.collideX)
                {
                    npc.velocity.X *= -0.8f;
                }
                return;
            }
            else
            {
                npc.noGravity = true;
            }
            if (npc.ai[2] < DashTime) // Regular flight
            {
                npc.ai[2] += stats.AttackSpeed;
                if (npc.ai[2] > DashTime)
                    npc.ai[2] = DashTime;
                if (!npc.noTileCollide)
                {
                    if (npc.collideX)
                    {
                        npc.velocity.X = npc.oldVelocity.X * -0.2f;
                        if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 2f)
                        {
                            npc.velocity.X = 2f;
                        }
                        if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -2f)
                        {
                            npc.velocity.X = -2f;
                        }
                    }
                    if (npc.collideY)
                    {
                        npc.velocity.Y = npc.oldVelocity.Y * -0.2f;
                        if (npc.velocity.Y > 0f && npc.velocity.Y < 1f)
                        {
                            npc.velocity.Y = 1f;
                        }
                        if (npc.velocity.Y < 0f && npc.velocity.Y > -1f)
                        {
                            npc.velocity.Y = -1f;
                        }
                    }
                }
                if (NPC.DespawnEncouragement_AIStyle2_FloatingEye_IsDiscouraged(npc.type, npc.position, npc.target))
                {
                    npc.EncourageDespawn(10);
                    npc.directionY = -1;
                    if (npc.velocity.Y > 0f)
                    {
                        npc.direction = 1;
                    }
                    npc.direction = -1;
                    if (npc.velocity.X > 0f)
                    {
                        npc.direction = 1;
                    }
                }
                else
                {
                    npc.TargetClosest();
                }

                float HorizontalSpeed = 4f * stats.MoveSpeed * npc.scale;
                float VerticalSpeed = 2.5f * stats.MoveSpeed * npc.scale;
                npc.velocity += new Vector2(npc.direction, npc.directionY * 0.6f) * 0.1f * stats.MoveSpeed;
                if (npc.direction == -1 && npc.velocity.X > 0f - HorizontalSpeed)
                {
                    if (npc.velocity.X > HorizontalSpeed)
                    {
                        npc.velocity.X -= 0.1f * stats.MoveSpeed;
                    }
                    else if (npc.velocity.X > 0f)
                    {
                        npc.velocity.X += 0.05f * stats.MoveSpeed;
                    }
                    if (npc.velocity.X < -HorizontalSpeed)
                    {
                        npc.velocity.X = -HorizontalSpeed;
                    }
                }
                else if (npc.direction == 1 && npc.velocity.X < HorizontalSpeed)
                {
                    if (npc.velocity.X < 0f - HorizontalSpeed)
                    {
                        npc.velocity.X += 0.1f * stats.MoveSpeed;
                    }
                    else if (npc.velocity.X < 0f)
                    {
                        npc.velocity.X -= 0.05f * stats.MoveSpeed;
                    }
                    if (npc.velocity.X > HorizontalSpeed)
                    {
                        npc.velocity.X = HorizontalSpeed;
                    }
                }
                if (npc.directionY == -1 && npc.velocity.Y > 0f - VerticalSpeed)
                {
                    if (npc.velocity.Y > VerticalSpeed)
                    {
                        npc.velocity.Y -= 0.05f * stats.MoveSpeed;
                    }
                    else if (npc.velocity.Y > 0f)
                    {
                        npc.velocity.Y += 0.03f * stats.MoveSpeed;
                    }
                    if (npc.velocity.Y < -VerticalSpeed)
                    {
                        npc.velocity.Y = -VerticalSpeed;
                    }
                }
                else if (npc.directionY == 1 && npc.velocity.Y < VerticalSpeed)
                {
                    if (npc.velocity.Y < 0f - VerticalSpeed)
                    {
                        npc.velocity.Y += 0.05f * stats.MoveSpeed;
                    }
                    else if (npc.velocity.Y < 0f)
                    {
                        npc.velocity.Y -= 0.03f * stats.MoveSpeed;
                    }
                    if (npc.velocity.Y > VerticalSpeed)
                    {
                        npc.velocity.Y = VerticalSpeed;
                    }
                }
                if (npc.ai[2] > DashTime - 50 * stats.AttackSpeed)
                {
                    npc.velocity *= 0.95f;
                    npc.localAI[0] = npc.localAI[0].AngleLerp(npc.Center.DirectionTo(Main.player[npc.target].Center).ToRotation(),0.1f);
                }
            }
            else
            {
                npc.ai[2]++;
                int type = ModContent.DustType<SimpleColorableGlowyDust>();
                if (npc.ai[2] == DashTime + 1)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.width, type);
                        d.noGravity = true;
                        d.color = Terrafirma.UnparryableAttackColor with { A = 0 };
                        d.noLight = true;
                        d.velocity *= 2;
                    }
                    SoundEngine.PlaySound(SoundID.DD2_GoblinBomberThrow, npc.Center);
                    npc.velocity = npc.Center.DirectionTo(Main.player[npc.target].Center) * 14 * stats.MoveSpeed;
                }

                if (Main.rand.NextBool(3))
                {
                    Dust d2 = Dust.NewDustDirect(npc.position, npc.width, npc.width, type);
                    d2.noGravity = true;
                    d2.velocity = npc.velocity * Main.rand.NextFloat(0.2f, 1f);
                    d2.noLight = true;
                    d2.color = Terrafirma.UnparryableAttackColor with { A = 0 };
                }

                if (npc.collideX || npc.collideY || npc.ai[2] > DashTime + Math.Min((40 / stats.MoveSpeed),40))
                {
                    npc.ai[2] = Main.rand.Next(-440,0);
                    for (int i = 0; i < 20; i++)
                    {
                        Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.width, type);
                        d.noGravity = true;
                        d.noLight = true;
                        d.color = Terrafirma.UnparryableAttackColor with { A = 0 };
                        if (Main.rand.NextBool())
                            d.velocity += npc.velocity * Main.rand.NextFloat(0.5f, 1.5f);
                    }
                    npc.velocity *= 0.3f;
                    npc.netUpdate = true;
                }
            }
        }
        public bool CanBeBlocked(Player player, NPC? npc)
        {
            return npc.ai[2] < DashTime;
        }
    }
}
