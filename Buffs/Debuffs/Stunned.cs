using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Buffs.Debuffs
{
    internal class Stunned : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.pvpBuff[Type] = true;
            Main.debuff[Type] = true;

            for(int i = 0; i < NPCID.Count; i++)
            {
                if (ContentSamples.NpcsByNetId[i].knockBackResist == 0 || ContentSamples.NpcsByNetId[i].boss)
                NPCID.Sets.SpecificDebuffImmunity[i][Type] = true;
            }
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            //npc.position.X -= npc.velocity.X * (npc.knockBackResist * 0.9f);

            if (npc.direction == 1)
            {
                npc.velocity.X = MathF.Min(npc.velocity.X,npc.velocity.X * MathHelper.Clamp(1f - npc.knockBackResist,0f,1f));
            }
            else
            {
                npc.velocity.X = MathF.Max(npc.velocity.X, npc.velocity.X * MathHelper.Clamp(1f - npc.knockBackResist, -1f, 0f));
            }
            if (npc.noTileCollide)
                return;
            if (npc.noGravity) npc.noGravity = false;
            if (npc.buffTime[buffIndex] == 0)
            {
                NPC dummy = new NPC();
                dummy.SetDefaults(npc.type);
                npc.noGravity = dummy.noGravity;
            }
        }
        public override void Update(Player player, ref int buffIndex)
        {
        }
    }
}
