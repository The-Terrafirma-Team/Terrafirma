using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Templates;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.NPCs.Miniboss
{
    public partial class YinYangSlime : TfirmaNPC
    {
        byte phase = 0;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 7;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Terrafirma.Bestiary.YinYangSlime"))
            });
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f;
        }
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.RainbowSlime);
            NPC.alpha = 0;
            NPC.aiStyle = -1;
            NPC.defense = 10;
            NPC.damage = 20;
            phase = 0;
            NPC.Size = new Vector2(36);
            NPC.knockBackResist = 0f;
        }
        public override bool? CanFallThroughPlatforms()
        {
            switch (phase)
            {
                case 1:
                    return target.position.Y > NPC.Bottom.Y;
            }
            return base.CanFallThroughPlatforms();
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life > 0)
            {
                for (int num322 = 0; (double)num322 < hit.Damage / 3; num322++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, Main.rand.NextBool()? DustID.Ghost : DustID.Wraith, hit.HitDirection, -1f, NPC.alpha, Color.White);
                }
            }
            else
            {
                for (int num323 = 0; num323 < 50; num323++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, Main.rand.NextBool() ? DustID.Ghost : DustID.Wraith, 2 * hit.HitDirection, -2f, NPC.alpha, Color.White);
                }
            }
        }
        public override void AI()
        {
            switch (phase)
            {
                case 0:
                    Phase0_Sleepy();
                    break;
                case 1:
                    Phase1();
                    break;
                case 2:
                    Phase2();
                    break;
                case 3:
                    Phase3();
                    break;
            }
        }
        public override void OnKill()
        {

        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (phase == 0)
            {
                switch (NPC.frameCounter)
                {
                    case 32:
                    case 64:
                    case 96:
                        NPC.frame.Y += frameHeight;
                        break;
                    case 128:
                    case 136:
                        NPC.frame.Y -= frameHeight;
                        break;
                    case 144:
                        NPC.frame.Y -= frameHeight;
                        NPC.frameCounter = -32;
                        break;
                }
            }
            else
            {
                NPC.frame.Y = frameHeight * (int)(Main.timeForVisualEffects / 6 % 4);
                if (NPC.velocity.Y > 0)
                {
                    NPC.frame.Y = frameHeight * 5;
                }
                else if (NPC.velocity.Y < 0)
                {
                    NPC.frame.Y = frameHeight * 4;
                }
            }
        }
    }
}
