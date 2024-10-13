using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Items.Materials;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.NPCs.Boss.Terragrim
{
    public partial class Terragrim : ModNPC
    {
        public byte phase;
        public bool canHitPlayer;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 4;
            NPCID.Sets.TrailCacheLength[NPC.type] = 5;
            NPCID.Sets.TrailingMode[NPC.type] = 7;

            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);

            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        }
        public override bool? CanFallThroughPlatforms()
        {
            return true;
        }
        public override void SetDefaults()
        {
            NPC.lifeMax = 1750;
            NPC.defense = 15;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath52;
            NPC.boss = true;
            NPC.npcSlots = ContentSamples.NpcsByNetId[NPCID.EyeofCthulhu].npcSlots;
            NPC.noTileCollide = true;
            NPC.width = 32;
            NPC.height = 32;
            NPC.noGravity = true;
            NPC.knockBackResist = 0;
            NPC.damage = 25;

            Music = MusicID.Boss1;

            phase = 0;
            canHitPlayer = false;
            spinnyMode = false;
        }
        Player target;
        bool spinnyMode;

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.LesserHealingPotion;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Terragrim));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientSpiritEssence>(),1,10,20));
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (!canHitPlayer)
                return false;
            return base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (Main.rand.NextBool(3))
                target.AddBuff(BuffID.Darkness, 60 * 5);
            if (Main.rand.NextBool(6))
                target.AddBuff(BuffID.BrokenArmor, 60 * 5);
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if(NPC.life <= 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    Gore g = Gore.NewGoreDirect(NPC.GetSource_FromThis(), NPC.Bottom, Main.rand.NextVector2Circular(2, 2), Main.rand.Next(61, 64));
                    g.scale = Main.rand.NextFloat(0.5f, 0.7f);
                    g.rotation = Main.rand.NextFloat(MathHelper.TwoPi);
                }
                for (int i = 0; i < 20; i++)
                {
                    Dust d = Dust.NewDustPerfect(NPC.Center, DustID.Terragrim, Main.rand.NextVector2Circular(6, 6));
                    d.noGravity = true;
                }
            }
        }
        public override void AI()
        {
            target = Main.player[NPC.target];

            if (target.dead)
                NPC.TargetClosest();

            if(phase > 0 && phase != 3)
            {
                if (Main.rand.NextBool(3))
                {
                    Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Terragrim);
                    d.velocity *= 0.5f;
                    d.scale = 1.3f;
                    d.noGravity = true;
                    d.noLight = true;
                }
                Lighting.AddLight(NPC.Center, 0.1f, 0.4f + MathF.Sin((float)Main.timeForVisualEffects * 0.01f) * 0.1f, 0.3f + MathF.Sin((float)Main.timeForVisualEffects * 0.03f) * 0.1f);
            }

            if(NPC.life < (int)(NPC.lifeMax * 0.6f) && phase <= 2)
            {
                NPC.ai[0] = -60;
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.localAI[0] = 0;
                NPC.knockBackResist = 0;
                phase = 3;

                NPC.height = 16;

                NPC.width = 8;

                canHitPlayer = false;
                spinnyMode = false;

                for (int i = 0; i < 24; i++)
                {
                    Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Terragrim);
                    d.velocity = new Vector2(3).RotatedBy(i * MathHelper.TwoPi / 24);
                    d.scale = 2f;
                    d.noGravity = true;
                }
            }

            switch (phase)
            {
                case 0:
                    Phase0_Intro();
                    break;
                case 1:
                    Phase1_Dash();
                    break;
                case 2:
                    Phase2_SpinFireball();
                    break;
                case 3:
                    Phase3_PhaseTwoIntro();
                    break;
                case 4:
                    Phase4_Dash();
                    break;
                case 5:
                    Phase5_BladeRing();
                    break;
                case 6:
                    Phase6_Spin();
                    break;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Asset<Texture2D> tex = TextureAssets.Npc[Type];
            if (phase is > 0 and < 4)
            {
                for (int i = NPCID.Sets.TrailCacheLength[Type] - 1; i >= 0; i--)
                {
                    spriteBatch.Draw(tex.Value, NPC.oldPos[i] - screenPos + NPC.Size / 2, new Rectangle(0, NPC.frame.Height * 2, NPC.frame.Width, NPC.frame.Height), new Color(0, 255, 200, 0) * (NPCID.Sets.TrailCacheLength[Type] - i) * 0.2f, NPC.oldRot[i], NPC.frame.Size() / 2, 1f, SpriteEffects.None, 0);
                }
            }
            else if(phase > 0)
            {
                for (int i = NPCID.Sets.TrailCacheLength[Type] - 1; i >= 0; i--)
                {
                    spriteBatch.Draw(tex.Value, NPC.oldPos[i] - screenPos + NPC.Size / 2 + Main.rand.NextVector2Circular(1,1), new Rectangle(0, 0, NPC.frame.Width, NPC.frame.Height), new Color(128, 255, 200, 0) * (NPCID.Sets.TrailCacheLength[Type] - i) * 0.2f, NPC.oldRot[i] + Main.rand.NextFloat(-0.05f,0.05f), NPC.frame.Size() / 2, 1.3f + (float)(Math.Sin((Main.timeForVisualEffects + i * 10) * 0.1f) * 0.2f), SpriteEffects.None, 0);
                }
            }
                
            spriteBatch.Draw(tex.Value, NPC.Center - screenPos, NPC.frame, (canHitPlayer || phase == 0) ? Color.White : Color.Gray, NPC.rotation, NPC.frame.Size() / 2, 1f, SpriteEffects.None, 0);

            if (spinnyMode)
            {
                for (int i = NPCID.Sets.TrailCacheLength[Type] - 1; i >= 0; i--)
                {
                    spriteBatch.Draw(tex.Value, NPC.oldPos[i] - screenPos + NPC.Size / 2, new Rectangle(0, NPC.frame.Height * 3, NPC.frame.Width, NPC.frame.Height), new Color(0, 255, 100, 0) * (NPCID.Sets.TrailCacheLength[Type] - i) * 0.1f, NPC.oldRot[i], NPC.frame.Size() / 2, 1.3f - ((NPCID.Sets.TrailCacheLength[Type] - i) * 0.1f), SpriteEffects.None, 0);
                }
            }

            return false;
        }
    }
}
