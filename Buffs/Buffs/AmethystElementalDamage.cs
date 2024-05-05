using Terraria.ModLoader;
using Terraria;

namespace Terrafirma.Buffs.Buffs
{
    internal class AmethystElementalDamage: ModBuff
    { 
        public override void Update(Player player, ref int buffIndex)
        {
            Main.NewText("FIND A NEW PURPOSE", 255, 0, 0);
        }
    }
}

