using Microsoft.Xna.Framework;
using Terrafirma.Buffs.Debuffs;
using Terrafirma.Particles;
using Terrafirma.Projectiles.Hostile;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common.NPCs
{
    public class TerrafirmaGlobalNPC : GlobalNPC
    {
        public override void Load()
        {
            On_NPC.SlimeRainSpawns += On_NPC_SlimeRainSpawns;
        }
        private void On_NPC_SlimeRainSpawns(On_NPC.orig_SlimeRainSpawns orig, int plr)
        {
            int logicCheckScreenHeight = Main.LogicCheckScreenHeight;
            int logicCheckScreenWidth = Main.LogicCheckScreenWidth;
            float num = 15f;
            Player player = Main.player[plr];
            if ((double)player.position.Y > Main.worldSurface * 16.0 + (double)(logicCheckScreenHeight / 2) || player.nearbyActiveNPCs > num)
            {
                return;
            }
            float num2 = player.nearbyActiveNPCs / num;
            int num3 = 45 + (int)(450f * num2);
            if (Main.expertMode)
            {
                num3 = (int)((double)num3 * 0.85);
            }
            if (Main.GameModeInfo.IsJourneyMode)
            {
                CreativePowers.SpawnRateSliderPerPlayerPower power = CreativePowerManager.Instance.GetPower<CreativePowers.SpawnRateSliderPerPlayerPower>();
                if (power != null && power.GetIsUnlocked())
                {
                    if (power.GetShouldDisableSpawnsFor(plr))
                    {
                        return;
                    }
                    if (power.GetRemappedSliderValueFor(plr, out var num4))
                    {
                        num3 = (int)((float)num3 / num4);
                    }
                }
            }
            if (!Main.rand.NextBool(num3))
            {
                return;
            }
            int num5 = (int)(player.Center.X - (float)logicCheckScreenWidth);
            int maxValue = num5 + logicCheckScreenWidth * 2;
            int minValue = (int)((double)player.Center.Y - (double)logicCheckScreenHeight * 1.5);
            int maxValue2 = (int)((double)player.Center.Y - (double)logicCheckScreenHeight * 0.75);
            int num6 = Main.rand.Next(num5, maxValue);
            int num7 = Main.rand.Next(minValue, maxValue2);
            num6 /= 16;
            num7 /= 16;
            if (num6 < 10 || num6 > Main.maxTilesX + 10 || (double)num7 < Main.worldSurface * 0.3 || (double)num7 > Main.worldSurface || Collision.SolidTiles(num6 - 3, num6 + 3, num7 - 5, num7 + 2) || Main.wallHouse[Main.tile[num6, num7].WallType])
            {
                return;
            }

            // Regular slime spawns
            if (!Main.rand.NextBool(40))
            {
                if (Main.player[plr].ZoneHallow && Main.rand.NextBool(40))
                {
                    NPC.NewNPC(NPC.GetSource_NaturalSpawn(), num6 * 16 + 8, num7 * 16, NPCID.RainbowSlime);
                }
                int num8 = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), num6 * 16 + 8, num7 * 16, 1);
                if (Main.rand.NextBool(200))
                {
                    Main.npc[num8].SetDefaults(NPCID.Pinky);
                }
                else if (Main.expertMode)
                {
                    if (Main.rand.NextBool(7))
                    {
                        Main.npc[num8].SetDefaults(NPCID.PurpleSlime);
                    }
                    else if (Main.rand.NextBool(3))
                    {
                        Main.npc[num8].SetDefaults(NPCID.GreenSlime);
                    }
                }
                else if (Main.rand.NextBool(10))
                {
                    Main.npc[num8].SetDefaults(NPCID.PurpleSlime);
                }
                else if (Main.rand.Next(5) < 2)
                {
                    Main.npc[num8].SetDefaults(NPCID.GreenSlime);
                }
            }
            else // Special Slimes
            {
                int rand = Main.rand.Next(2);
                switch (rand)
                {
                    case 0:
                        NPC.NewNPC(NPC.GetSource_NaturalSpawn(), num6 * 16 + 8, num7 * 16, NPCID.MotherSlime);
                        break;
                    case 1:
                        NPC.NewNPC(NPC.GetSource_NaturalSpawn(), num6 * 16 + 8, num7 * 16, NPCID.MotherSlime);
                        break;
                }
            }
        }

        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            spawnRate = (int)(spawnRate * player.PlayerStats().EnemySpawnRateMultiplier);
            maxSpawns = (int)(maxSpawns * player.PlayerStats().MaxEnemySpawnMultiplier);
        }
        public override void SetStaticDefaults()
        {
            for (int i = -65; i < ContentSamples.NpcsByNetId.Count - 65; i++)
            {
                if (ContentSamples.NpcsByNetId[i].knockBackResist == 0)
                {
                    NPCID.Sets.SpecificDebuffImmunity[i][ModContent.BuffType<Inked>()] = true;
                    NPCID.Sets.SpecificDebuffImmunity[i][ModContent.BuffType<ChilledForEnemies>()] = true;
                }
            }
        }
        public override void AI(NPC npc)
        {
            switch (npc.type)
            {
                case NPCID.UmbrellaSlime:
                    if(npc.velocity.Y > 0)
                    {
                        npc.GravityMultiplier /= 6;
                        if (npc.velocity.Y > 1)
                        {
                            npc.velocity.Y = 1;
                        }
                    }
                    break;
                case NPCID.RainbowSlime:
                    npc.ai[0]-= 2;
                    if (Main.rand.NextBool(10))
                    {
                        ParticleSystem.AddParticle(new BigSparkle() {Rotation = Main.rand.NextFloat(-0.2f,0.2f), Scale = 0.3f, fadeInTime = 10 }, Main.rand.NextVector2FromRectangle(npc.Hitbox),npc.velocity * 0.1f,Main.DiscoColor with { A = 0});
                    }
                    break;
            }
        }
        public override void OnKill(NPC npc)
        {
            if(npc.type == NPCID.Crimslime)
            {
                Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center, new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-3, -1)), ModContent.ProjectileType<CrimslimeBomb>(), 0, 0, -1);
                return;
            }
            if (NPC.downedBoss2)
                return;
            if (npc.type is >= NPCID.EaterofWorldsHead and <= NPCID.EaterofWorldsTail && npc.boss)
            {
                TFUtils.SendImportantStatusMessage("Mods.Terrafirma.Misc.EbonstoneWeak", new Color(50, 255, 130));
            }
            else if (npc.type == NPCID.BrainofCthulhu)
            {
                TFUtils.SendImportantStatusMessage("Mods.Terrafirma.Misc.CrimstoneWeak", new Color(50, 255, 130));
            }
        }
    }
}
