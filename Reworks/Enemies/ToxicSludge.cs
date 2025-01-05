using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Projectiles.Hostile;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Terrafirma.Reworks.Enemies
{
    public class ToxicSludge : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.ToxicSludge;
        }
        public override bool InstancePerEntity => true;
        public override void Load()
        {
            TextureAssets.Npc[NPCID.ToxicSludge] = ModContent.Request<Texture2D>("Terrafirma/Assets/Resprites/NPCs/ToxicSludge");
        }
        public override void Unload()
        {
            TextureAssets.Npc[NPCID.ToxicSludge] = ModContent.Request<Texture2D>($"Terraria/Images/NPC_{NPCID.ToxicSludge}");
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPCID.ToxicSludge] = 7;
        }
        public override void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
        {
            binaryWriter.Write(npc.localAI[2]);
        }
        public override void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
        {
            npc.localAI[2] = binaryReader.ReadSingle();
        }
        public override void SetDefaults(NPC entity)
        {
            entity.scale = 1f;
        }
        public override void FindFrame(NPC npc, int frameHeight)
        {
            npc.localAI[2] += 0.1f;
            if (npc.localAI[2] > 4)
                npc.localAI[2] = 0;
            npc.frame.Y = frameHeight * (int)npc.localAI[2];
            if(npc.velocity.Y != 0)
            {
                npc.frame.Y = frameHeight * 4;
            }
            if (npc.localAI[1] is > 215 and < 230)
            {
                npc.frame.Y = frameHeight * 5;
            }
            else if (npc.localAI[1] > 200)
            {
                npc.frame.Y = frameHeight * 6;
            }
        }
        public override void AI(NPC npc)
        {
            Player target = Main.player[npc.target];
            npc.localAI[1]--;
            if (TFUtils.IfTheSlimesAI0WasThisNumberItWouldJump((int)npc.ai[0]))
            {
                npc.TargetClosest();
            }
            if (npc.HasValidTarget)
            {
                if (npc.localAI[1] <= 75 && target.Center.Distance(npc.Center) < 400 && Collision.CanHitLine(npc.Center,1,1,target.Center,1,1) && npc.collideY && !target.npcTypeNoAggro[NPCID.ToxicSludge])
                {
                    npc.TargetClosest();
                    npc.localAI[1] = 230;
                }
            }
            if (npc.localAI[1] > 200)
            {
                npc.ai[0] -= 1;
                if (npc.localAI[1] == 215)
                {
                    if(Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Vector2 dir = npc.Center.DirectionTo(target.Center) * 7;
                        dir.Y = Math.Min(dir.Y, -3);
                        Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center + new Vector2(npc.direction * 4,6), dir, ModContent.ProjectileType<ToxicSludgeBall>(),30,2);
                    }
                    SoundEngine.PlaySound(SoundID.NPCDeath13, npc.position);
                }
            }
        }
    }
}
