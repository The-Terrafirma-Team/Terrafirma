using Terrafirma.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Content.Items.Equipment.Accessories.Combat.MysticHourglass
{
    public class MysticHourglass : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory();
            Item.rare = ItemRarityID.Orange;
        }
        public override void UpdateEquip(Player player)
        {
            player.PlayerStats().SkillCooldown -= 0.1f;
        }
    }
}
