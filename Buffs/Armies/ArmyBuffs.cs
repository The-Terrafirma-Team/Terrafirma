using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terrafirma.Common.Players;
using Terrafirma.Projectiles.Summon.Summons;

namespace Terrafirma.Buffs.Armies
{
    public class MeteorSaucerArmy : ModBuff
    {
        public override void SetStaticDefaults()
        {
            //Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Airship>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}

