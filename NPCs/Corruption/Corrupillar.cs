using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Projectiles.Hostile;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.NPCs.Corruption
{
    public class Corrupillar : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 8;
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = -1.4f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }
        public override void SetDefaults()
        {
            NPC.Size = new Vector2(28,50);
            NPC.lifeMax = 70;
            NPC.defense = 2;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.aiStyle = -1;
            NPC.damage = 20;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Terrafirma.Bestiary.Corrupillar"))
            });
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.ZoneCorrupt ? 0.4f : 0f;
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Corrupillar_0").Type);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Corrupillar_1").Type);
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CorruptGibs, hit.HitDirection, -1f, NPC.alpha, NPC.color, NPC.scale);
                }
            }
            else
            {
                for (int i = 0; i < hit.Damage; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CorruptGibs, hit.HitDirection, -1f, NPC.alpha, NPC.color, NPC.scale);
                }
                return;
            }
        }
        public override void AI()
        {
            float walkSpeed = 1.4f;
            float acceleration = 0.15f;
            if (!NPC.HasValidTarget)
            {
                NPC.TargetClosestUpgraded(true);
            }
            else
            {
                Player target = Main.player[NPC.target];
                NPC.direction = Math.Sign(NPC.Center.X - target.Center.X) * (NPC.confused? 1 : -1);
                NPC.spriteDirection = NPC.direction;
            }

            NPC.ai[0]++;
            if (NPC.velocity.Y == 0)
            {
                if (NPC.ai[0] > 200 && NPC.HasValidTarget)
                {
                    Player target = Main.player[NPC.target];
                    NPC.velocity.X *= 0.9f;

                    if (NPC.ai[0] == 240)
                    {
                        if(Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Top + new Vector2(0, 15), (NPC.Top + new Vector2(0, 15)).DirectionTo(target.Center) * (NPC.confused? -4 : 4),ModContent.ProjectileType<CorrupillarInk>(),20,1);
                        }
                    }
                    else if (NPC.ai[0] >= 260)
                    {
                        NPC.ai[0] = 0;
                    }
                    return;
                }
                NPC.localAI[0]++;
                if (NPC.direction == 1)
                {
                    if (NPC.velocity.X < walkSpeed)
                        NPC.velocity.X += acceleration;
                    else
                    {
                        NPC.velocity.X -= acceleration;
                    }
                }
                else
                {
                    if (NPC.velocity.X > -walkSpeed)
                        NPC.velocity.X -= acceleration;
                    else
                    {
                        NPC.velocity.X += acceleration;
                    }
                }
                Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY);
                
                if (NPC.localAI[0] > 30 && (!Main.tileSolid[Main.tile[NPC.Bottom.ToTileCoordinates() + new Point(NPC.direction, 1)].TileType] || !Main.tile[NPC.Bottom.ToTileCoordinates() + new Point(NPC.direction, 1)].HasTile) && (!Main.tileSolid[Main.tile[NPC.Bottom.ToTileCoordinates() + new Point(NPC.direction * 2, 0)].TileType] || !Main.tile[NPC.Bottom.ToTileCoordinates() + new Point(NPC.direction * 2, 0)].HasTile))
                {
                    NPC.localAI[0] = 0;
                    NPC.velocity.Y -= 6;
                    NPC.velocity.X += NPC.direction * 4;
                }
                else if (NPC.collideX && NPC.localAI[0] > 30)
                {
                    NPC.localAI[0] = 0;
                    NPC.ai[1] = 30;
                    NPC.velocity.Y -= 10;
                }
            }
            else if (NPC.ai[1] > 0)
            {
                NPC.velocity.X += NPC.direction * 0.1f;
            }
            NPC.ai[1]--;
        }
        public override void FindFrame(int frameHeight)
        {
            int frame = 0;
            if (NPC.IsABestiaryIconDummy)
            {
                frame = (int)(Main.timeForVisualEffects / 10f) % 4;
                NPC.frame.Y = frameHeight * frame;
                return;
            }
            if (NPC.velocity.Y == 0 && NPC.ai[0] > 10)
            {
                NPC.frameCounter += NPC.velocity.X * NPC.direction;
                frame = (int)(NPC.frameCounter / 10f) % 4;
            }
            else
            {
                frame = 1;
            }

            if (NPC.HasValidTarget && NPC.ai[0] > 200)
            {
                float rot = NPC.confused? MathHelper.ToDegrees((NPC.Top + new Vector2(0, 15)).DirectionFrom(Main.player[NPC.target].Center).RotatedBy(NPC.direction == 1 ? 0 : MathHelper.Pi).ToRotation()) : MathHelper.ToDegrees((NPC.Top + new Vector2(0, 15)).DirectionTo(Main.player[NPC.target].Center).RotatedBy(NPC.direction == 1? 0 : MathHelper.Pi).ToRotation());

                if (NPC.confused)

                rot *= NPC.direction;
                if (Math.Abs(rot) <= 30 && Math.Abs(rot) >= -30)
                    frame = 5;
                else if (Math.Abs(rot) >= 30 && Math.Abs(rot) <= 60)
                    frame = Main.player[NPC.target].Center.Y < NPC.Center.Y ? 6 : 4;
                else
                    frame = Main.player[NPC.target].Center.Y < NPC.Center.Y ? 7 : 4;
            }
            NPC.frame.Y = frameHeight * frame;
        }
    }
}
