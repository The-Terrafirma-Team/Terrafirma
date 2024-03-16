using Terrafirma.Projectiles.Summon.Summons;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Buffs.Minions
{
    public class AirshipBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
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
