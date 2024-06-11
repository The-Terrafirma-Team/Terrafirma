using Terraria.ModLoader;
using Terraria;

namespace Terrafirma.Buffs.Buffs
{
    internal class AmethystCriticalBuff: ModBuff
    { 
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetCritChance(DamageClass.Generic) += 100;
            player.PlayerStats().GenericCritDamage += 1f;
        }
    }
}

