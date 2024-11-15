using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using SteelSeries.GameSense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Buffs.Debuffs;
using Terrafirma.Common.Templates;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.NPCs.Dungeon
{
    public class NefariousSkull : TfirmaNPC
    {
        Asset<Texture2D> glowtex;
        int targetmode = 0;
        float colorgradient = 0f;
        int[] allowedBuffs = new int[] 
        { 
            BuffID.Slow, BuffID.OnFire, BuffID.Darkness, BuffID.Confused
        };
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 4;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Terrafirma.Bestiary.NefariousSkull"))
            });
        }

        public override void Load()
        {
            glowtex = ModContent.Request<Texture2D>("Terrafirma/NPCs/Dungeon/NefariousSkullGlow");
            base.Load();
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.ZoneDungeon? 0.07f : 0f;
        }

        public override void OnSpawn(IEntitySource source)
        {
            NPC.ai[2] = allowedBuffs[Main.rand.Next(allowedBuffs.Length)];

            base.OnSpawn(source);
        }
        public override void SetDefaults()
        {
            NPC.lifeMax = 100;
            NPC.defense = 2;
            NPC.knockBackResist = 0.2f;
            NPC.noGravity = true;
            NPC.Size = new Vector2(24,32);
            NPC.damage = 25;
            NPC.HitSound = SoundID.NPCHit2;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.noTileCollide = true;
            //NPC.ApplyManaStats(300);
        }
        public override void AI()
        {
            float mindist = 700f;
            NPC.target = -1;
            switch (NPC.ai[2])
            {
                case 0:
                    NPC.color = Color.White;
                    break;
                case BuffID.OnFire:
                    NPC.color = Color.Orange;
                    break;
                case BuffID.Slow:
                    NPC.color = Color.RosyBrown;
                    break;
                case BuffID.Darkness:
                    NPC.color = Color.Purple;
                    break;
                case BuffID.Confused:
                    NPC.color = Color.CadetBlue;
                    break;
            }

            for (int i = 0; i < Main.player.Length; i++)
            {
                if (NPC.Center.Distance(Main.player[i].Center) < mindist)
                {
                    NPC.target = Main.player[i].whoAmI;
                    mindist = NPC.Center.Distance(Main.player[i].Center);
                }
            }

            if (NPC.target != -1)
            {
                if (Main.player[NPC.target].Center.Distance(NPC.Center) <= 150 && !Main.tile[(NPC.Center / 16).ToPoint()].HasTile)
                {
                    NPC.velocity *= 0.9f;
                    targetmode = 1;
                    NPC.ai[1] = 0;
                }
                else if (targetmode != 1 || NPC.ai[1] > 60)
                {
                    NPC.velocity = Vector2.Lerp(NPC.velocity, NPC.Center.DirectionTo(Main.player[NPC.target].Center) * 2f, 0.05f);
                    targetmode = 0;
                }
                else
                {
                    NPC.ai[1]++;
                }
            }
            else
            {
                targetmode = 0;
                NPC.velocity *= 0.95f;
            }

            if (targetmode == 1)
            {
                colorgradient = Math.Clamp(colorgradient += 0.05f, 0f, 1f);
                if (NPC.ai[0] % 8 == 0)
                {
                    for (int i = 0; i < 15; i++)
                    {
                        Dust d = Dust.NewDustPerfect(NPC.Center + new Vector2(200, 0).RotatedBy((MathHelper.TwoPi / 15) * i + (NPC.ai[0] / 10f)), 
                            DustID.FireworksRGB, 
                            Vector2.Zero, 
                            0, 
                            NPC.color, 
                            1f);
                        d.noGravity = true;
                    }

                    Vector2 randpos = NPC.Center + Main.rand.NextVector2Circular(200f, 200f);
                    Dust f = Dust.NewDustPerfect(randpos, DustID.FireworksRGB, NPC.Center.DirectionTo(randpos) * 5f, newColor: NPC.color);
                    f.noGravity = true;
                }

                if (NPC.ai[0] % 14 == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item20 with { MaxInstances = 0, PitchVariance = 0.3f, Volume = 0.6f }, NPC.Center);
                }
            }
            else colorgradient = Math.Clamp(colorgradient -= 0.05f, 0f, 1f);

            Lighting.AddLight(NPC.Center, NPC.color.ToVector3() * colorgradient);

            NPC.rotation = float.Lerp(NPC.rotation, NPC.velocity.X / 10f, 0.05f);
            NPC.ai[0]++;
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (target.Center.Distance(NPC.Center) <= 200 && targetmode == 1 && NPC.ai[2] != 0)
            {
                target.AddBuff((int)NPC.ai[2], 10);
            }
            return base.CanHitPlayer(target, ref cooldownSlot);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            glowtex = ModContent.Request<Texture2D>("Terrafirma/NPCs/Dungeon/NefariousSkullGlow");
            Asset<Texture2D> tex = TextureAssets.Npc[Type];
            spriteBatch.Draw(tex.Value,
                NPC.Center - Main.screenPosition,
                NPC.frame,
                drawColor,
                NPC.rotation,
                NPC.frame.Size()/2,
                NPC.scale,
                SpriteEffects.None,
                1f);
            spriteBatch.Draw(glowtex.Value,
                NPC.Center - Main.screenPosition,
                NPC.frame,
                new Color(NPC.color.R, NPC.color.G, NPC.color.B ,0) * (float)((Math.Sin(Main.timeForVisualEffects / 10f) + 1f) * 0.5f),
                NPC.rotation,
                NPC.frame.Size() / 2,
                NPC.scale,
                SpriteEffects.None,
                1f);
            return false;
        }
        public override void FindFrame(int frameHeight)
        {
            if (NPC.ai[0] % 8 == 0)
            {
                if (targetmode == 1)
                {
                    if (NPC.frameCounter < 2) NPC.frameCounter++;
                    else if (NPC.frameCounter == 3) NPC.frameCounter = 2;
                    else NPC.frameCounter++;
                }
                else
                {
                    NPC.frameCounter = Math.Clamp(NPC.frameCounter - 1, 0, 3);
                }
            }

            NPC.frame.Y = (int)(NPC.frameCounter * frameHeight);
        }
    }
}
