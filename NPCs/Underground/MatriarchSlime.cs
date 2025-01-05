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

namespace Terrafirma.NPCs.Underground
{
    public class MatriarchSlime : TfirmaNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 11;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Terrafirma.Bestiary.MatriarchSlime"))
            });
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.ZoneRockLayerHeight && Main.hardMode ? 0.1f : 0f;
        }
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.RainbowSlime);
            AnimationType = NPCID.BlueSlime;
            NPC.alpha = ContentSamples.NpcsByNetId[NPCID.MotherSlime].alpha;
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life > 0)
            {
                for (int num322 = 0; (double)num322 < hit.Damage / (double)NPC.lifeMax * 100.0; num322++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.TintableDust, hit.HitDirection, -1f, NPC.alpha, Color.DarkGray);
                }
            }
            else
            {
                for (int num323 = 0; num323 < 50; num323++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.TintableDust, 2 * hit.HitDirection, -2f, NPC.alpha, Color.DarkGray);
                }
            }
        }
        public override void AI()
        {
            NPC.ai[0] -= 0.25f;
        }
        public override void OnKill()
        {
            int maxSlime = Main.rand.Next(3,7);
            for (int i = 0; i < maxSlime; i++)
            {
                NPC n = NPC.NewNPCDirect(NPC.GetSource_OnHurt(null), (int)NPC.Center.X, (int)NPC.Center.Y, 1);
                n.SetDefaults(NPCID.MotherSlime);
                n.velocity.X = NPC.velocity.X * 2f;
                n.velocity.Y = NPC.velocity.Y;
                n.velocity += Main.rand.NextVector2Circular(5, 5);
                n.ai[0] = -1000 * Main.rand.Next(3);
                if (Main.netMode == NetmodeID.Server && n.whoAmI < 200)
                {
                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n.whoAmI);
                }
            }
        }
        public override void FindFrame(int frameHeight)
        {
            if (NPC.frame.Y > frameHeight * 3)
                NPC.frame.Y = 0;
            if ((NPC.ai[0] is > -20 and <= -10) || (NPC.ai[0] is >= -1020 and <= -1010) || (NPC.ai[0] is >= -2020 and <= -2010))
            {
                NPC.frame.Y = frameHeight * 4;
            }
            else if ((NPC.ai[0] is >= -10 and <= 0) || (NPC.ai[0] is >= -1010 and <= -1000) || (NPC.ai[0] is >= -2010 and <= -2000))
            {
                NPC.frame.Y = frameHeight * 5;
            }
            if (NPC.velocity.Y != 0)
            {
                NPC.frame.Y = frameHeight * (8 + (int)MathHelper.Clamp(NPC.velocity.Y, -2, 2));
            }
        }
    }
}
