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

namespace Terrafirma.Content.NPCs.Vanilla.IceSlime
{
    public class IceSlime : GlobalNPC, ICustomBlockBehavior
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<ServerConfig>().CombatReworkEnabled;
        }
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.IceSlime;
        }
        public override void Unload()
        {
            TextureAssets.Npc[NPCID.IceSlime] = ModContent.Request<Texture2D>($"Terraria/Images/NPC_{NPCID.IceSlime}");
            Main.npcFrameCount[NPCID.IceSlime] = 2;
        }
        public override void SetStaticDefaults()
        {
            TextureAssets.Npc[NPCID.IceSlime] = Mod.Assets.Request<Texture2D>("Content/NPCs/Vanilla/IceSlime/IceSlime");
            Main.npcFrameCount[NPCID.IceSlime] = 14;
            DataSets.NPCWhitelistedForStun[NPCID.IceSlime] = true;
        }
        private float frameCounter;
        private int frame;
        public void OnBlocked(Player player, float Power, NPC npc = null)
        {
            npc.ai[2] = 0;
            npc.AddBuff(ModContent.BuffType<Stunned>(), (int)(60 * 1.5f * Power));
        }
        public override void FindFrame(NPC npc, int frameHeight)
        {
            npc.frame.Y = frame * frameHeight;
            if (npc.NPCStats().NoAnimation || npc.ai[2] >= 30)
            {
                frameCounter = 0;
                return;
            }
            SlimeAI.FindFrame(npc, frameHeight, ref frameCounter, ref frame);
        }
        public override Color? GetAlpha(NPC npc, Color drawColor)
        {
            float percent = MathHelper.Clamp((frame - 5) / 4f , 0, 1f);
            return npc.GetNPCColorTintedByBuffs(Color.Lerp(drawColor with { A = 200 } * 0.65f, drawColor with { A = 230},percent));
        }
        public override void SetDefaults(NPC npc)
        {
            npc.alpha = 0;
            npc.aiStyle = -1;
            npc.width = 26;
            npc.height = 22;
        }
        public override bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot)
        {
            return SlimeAI.CanAttack(npc);
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
        }
        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
        {
            if (target.GetModPlayer<BlockingPlayer>().Blocking)
                return;
        }
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (SlimeAI.CanAttack(npc))
            {
                for(int i = 0; i < 4; i++)
                {
                    spriteBatch.Draw(TextureAssets.Npc[NPCID.IceSlime].Value, npc.Bottom - screenPos + new Vector2(0, 4 * npc.scale) - (npc.velocity * i), npc.frame, npc.GetAlpha(drawColor) * npc.Opacity * (0.5f - (i / 8f)), npc.rotation, new Vector2(npc.frame.Width / 2, npc.frame.Height), npc.scale, SpriteEffects.None, 0);
                }
            }
            else if(frame > 6)
            {
                float percent = MathHelper.Clamp((frame - 5) / 4f, 0, 1f);
                for (int i = 0; i < 4; i++)
                {
                    spriteBatch.Draw(TextureAssets.Npc[NPCID.IceSlime].Value, npc.Bottom - screenPos + new Vector2(0, 4 * npc.scale) + new Vector2(2 * npc.scale,0).RotatedBy(i * MathHelper.PiOver2), npc.frame with { Y = npc.frame.Y + npc.frame.Height * 4}, npc.GetNPCColorTintedByBuffs(Color.Lerp(new Color(25,175,255),new Color(120,220,255),Main.masterColor)) * npc.Opacity * percent, npc.rotation, new Vector2(npc.frame.Width / 2, npc.frame.Height), npc.scale, SpriteEffects.None, 0);
                }
            }
            spriteBatch.Draw(TextureAssets.Npc[NPCID.IceSlime].Value, npc.Bottom - screenPos + new Vector2(0, 4 * npc.scale), npc.frame, npc.GetAlpha(drawColor) * npc.Opacity, npc.rotation, new Vector2(npc.frame.Width / 2, npc.frame.Height), npc.scale, SpriteEffects.None, 0);

            npc.DrawConfusedQuestionMark(spriteBatch, screenPos);
            return false;
        }
        public override void AI(NPC npc)
        {
            npc.reflectsProjectiles = frame > 7;
            NPCStats stats = npc.NPCStats();
            if(!stats.Immobile && npc.HasValidTarget && npc.velocity.Y == 0)
                npc.ai[2]+= stats.AttackSpeed;
            if (npc.ai[2] < 30)
                SlimeAI.AI(npc, stats, ref frameCounter, npc.life < npc.lifeMax || !Main.dayTime || npc.position.Y > Main.worldSurface * 16, maxHorizontalSpeed: 5, JumpInterval: 60);
            else
            {
                npc.rotation = 0;
                float endTime = 140 * stats.AttackSpeed;
                if (npc.ai[2] < 40)
                    frame = 6;
                else if (npc.ai[2] < 50)
                    frame = 7;
                else if (npc.ai[2] < 60)
                    frame = 8;
                else if (npc.ai[2] < endTime - 21)
                    frame = 9;
                else if (npc.ai[2] < endTime - 14)
                    frame = 8;
                else if (npc.ai[2] < endTime - 7)
                    frame = 7;
                else if (npc.ai[2] < endTime)
                    frame = 6;

                else if (npc.ai[2] > endTime)
                    npc.ai[2] = -60;

                if (npc.velocity.Y == 0)
                {
                    npc.velocity.X *= 0.8f;
                }
            }
            if (Main.rand.Next(10) == 0)
            {
                int num7 = Dust.NewDust(npc.position, npc.width, npc.height, DustID.Snow);
                Main.dust[num7].noGravity = true;
                Main.dust[num7].velocity *= 0.1f;
            }
        }
        public override bool? CanFallThroughPlatforms(NPC npc)
        {
            return SlimeAI.CanFallThrough(npc);
        }
    }
}