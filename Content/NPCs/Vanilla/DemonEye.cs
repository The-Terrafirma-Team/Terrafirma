using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Terrafirma.Common;
using Terrafirma.Common.Interfaces;
using Terrafirma.Content.Buffs.Debuffs;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Content.NPCs.Vanilla
{
    public class DemonEye : GlobalNPC, ICustomBlockBehavior
    {
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
            npc.AddBuff(ModContent.BuffType<Flightless>(), (int)(60 * 3 * Power));
        }
        public override void SetDefaults(NPC npc)
        {
            npc.aiStyle = -1;
            npc.noGravity = true;
        }

        public override void SetStaticDefaults()
        {
            for (int i = 0; i < DemonEyes.Length; i++)
            {
                Main.npcFrameCount[DemonEyes[i]] = 12;
            }
            TextureAssets.Npc[NPCID.DemonEye] = Mod.Assets.Request<Texture2D>("Assets/Resprites/NPCs/DemonEye");
            TextureAssets.Npc[NPCID.CataractEye] = Mod.Assets.Request<Texture2D>("Assets/Resprites/NPCs/DemonEye_Cataract");
            TextureAssets.Npc[NPCID.PurpleEye] = Mod.Assets.Request<Texture2D>("Assets/Resprites/NPCs/DemonEye_Purple");
            TextureAssets.Npc[NPCID.GreenEye] = Mod.Assets.Request<Texture2D>("Assets/Resprites/NPCs/DemonEye_Green");
            TextureAssets.Npc[NPCID.DemonEyeOwl] = Mod.Assets.Request<Texture2D>("Assets/Resprites/NPCs/DemonEye"); // need sprite
            TextureAssets.Npc[NPCID.DemonEyeSpaceship] = Mod.Assets.Request<Texture2D>("Assets/Resprites/NPCs/DemonEye"); // ditto
            TextureAssets.Npc[NPCID.DialatedEye] = Mod.Assets.Request<Texture2D>("Assets/Resprites/NPCs/DemonEye_Dilated");
            TextureAssets.Npc[NPCID.SleepyEye] = Mod.Assets.Request<Texture2D>("Assets/Resprites/NPCs/DemonEye"); // ditto
        }
        public override void Unload()
        {
            for(int i = 0; i < DemonEyes.Length; i++)
            {
                TextureAssets.Npc[NPCID.DemonEye] = ModContent.Request<Texture2D>($"Terraria/Images/NPC_{DemonEyes[i]}");
                Main.npcFrameCount[DemonEyes[i]] = 2;
            }
        }
        public override void FindFrame(NPC npc, int frameHeight)
        {
            // WHY IS THE DEMON EYE CODED THE WAY IT IS
            if (npc.noGravity)
                npc.localAI[0] = Utils.AngleLerp(npc.localAI[0], npc.velocity.ToRotation(), 0.1f * npc.NPCStats().MoveSpeed);
            else
                npc.localAI[0] += npc.velocity.X * 0.1f;
            if(!npc.IsABestiaryIconDummy)
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
        public override void AI(NPC npc)
        {
            NPCStats stats = npc.NPCStats();

            if (Main.rand.NextBool(40))
            {
                npc.position += npc.netOffset;
                int num4 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + (float)npc.height * 0.25f), npc.width, (int)((float)npc.height * 0.5f), DustID.Blood, npc.velocity.X, 2f);
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
                npc.noGravity = false;
                if (npc.collideY)
                {
                    npc.velocity.X *= 0.98f;
                    if (!stats.Immobile)
                    {
                        npc.velocity.Y -= Main.rand.NextFloat(4,6);
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

            if (!npc.noTileCollide)
            {
                if (npc.collideX)
                {
                    npc.velocity.X = npc.oldVelocity.X * -0.5f;
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
                    npc.velocity.Y = npc.oldVelocity.Y * -0.5f;
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
            if (npc.direction == -1 && npc.velocity.X > 0f - HorizontalSpeed)
            {
                npc.velocity.X -= 0.1f * stats.MoveSpeed;
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
                npc.velocity.X += 0.1f * stats.MoveSpeed;
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
                npc.velocity.Y -= 0.04f * stats.MoveSpeed;
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
                npc.velocity.Y += 0.04f * stats.MoveSpeed;
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
        }
    }
}
