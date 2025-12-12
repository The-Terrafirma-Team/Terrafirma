using Terrafirma.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Content.Items.Equipment.Accessories.Combat.CueCards
{
    public class CueCards : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory();
            Item.rare = ItemRarityID.Green;
        }
        public override void UpdateEquip(Player player)
        {
            player.PlayerStats().SkillCastTime -= 0.2f;
        }
    }
}
