using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Projectiles.Hostile;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.GameContent.Animations.Actions.NPCs;

namespace Terrafirma.NPCs.Graveyard
{
    public class TragicUmbrellaSlime : ModNPC
    {
        private static Asset<Texture2D> Umbrella;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 6;
            Umbrella = TextureAssets.Gore[Mod.Find<ModGore>("TragicUmbrella").Type];
        }
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.UmbrellaSlime);
            AnimationType = NPCID.UmbrellaSlime;
            NPC.scale = 1.1f;
            NPC.lifeMax = 50;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Terrafirma.Bestiary.TragicUmbrellaSlime"))
            ]);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.ZoneGraveyard && Main.raining ? 0.1f : 0f;
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life > 0)
            {
                for (int num275 = 0; (double)num275 < hit.Damage / (double)NPC.lifeMax * 100.0; num275++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.TintableDust, hit.HitDirection, -1f, 100, new Color(0, 80, 255, 100));
                }
            }
            else
            {
                for (int num276 = 0; num276 < 50; num276++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.TintableDust, 2 * hit.HitDirection, -2f, 100, new Color(0, 80, 255, 100));
                }
                Gore.NewGore(NPC.GetSource_Death(),NPC.position, NPC.velocity, Mod.Find<ModGore>("TragicUmbrella").Type, 1f);
            }
        }
        float UmbrellaRotation = 0f;
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            UmbrellaRotation = MathHelper.Lerp(UmbrellaRotation, NPC.velocity.X * -0.1f, 0.1f);
            spriteBatch.Draw(Umbrella.Value, NPC.Center - screenPos, null, drawColor, NPC.rotation + UmbrellaRotation, new Vector2(25, 49), 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(TextureAssets.Npc[Type].Value, NPC.Bottom - screenPos + new Vector2(0, 2), NPC.frame, drawColor, NPC.rotation, new Vector2(23, 32), NPC.scale, SpriteEffects.None, 0);
            return false;
        }
        public override void AI()
        {
            if (NPC.velocity.Y > 0)
            {
                NPC.GravityMultiplier /= 6;
                if (NPC.velocity.Y > 1)
                {
                    NPC.velocity.Y = 1;
                }
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += (1f - (NPC.life / (float)NPC.lifeMax)) * 2;
            if (NPC.frame.Y > frameHeight * 3)
                NPC.frame.Y = 0;
            if (NPC.velocity.Y < 0)
                NPC.frame.Y = frameHeight * 5;
            else if (NPC.velocity.Y > 0)
                NPC.frame.Y = frameHeight * 4;
        }
    }
}
