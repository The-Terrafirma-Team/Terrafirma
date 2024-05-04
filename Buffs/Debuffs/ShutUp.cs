using Terrafirma.Items.Equipment;
using Terraria;
using Terraria.ModLoader;

namespace Terrafirma.Buffs.Debuffs
{
    public class ShutUp : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SteelSetBonus>().ShutUp = true;
        }
    }
}
