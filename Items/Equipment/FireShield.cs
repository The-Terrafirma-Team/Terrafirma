using Terrafirma.Global;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment
{
    [AutoloadEquip(EquipType.Shield)]
    public class FireShield : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory();
            Item.defense = 2;
        }
        public override void UpdateEquip(Player player)
        {
            player.buffImmune[BuffID.OnFire] = true;
            player.buffImmune[BuffID.OnFire3] = true;
        }
    }
}
