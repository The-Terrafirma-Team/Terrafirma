using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Items.Vanity.Dedicated
{
    [AutoloadEquip(EquipType.Legs)]
    public class KinMechanicalLegs : ModItem
    {
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Kin's Mechanical Legs");
			//Tooltip.SetDefault("Pantless?\n[c/A58CFF:Dedicated to Patchouli]");
		}
		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 16;

            Item.rare = ItemRarityID.Red;
            Item.value = Item.sellPrice(silver: 75);
			Item.vanity = true;
			Item.maxStack = 1;
		}
	}
}
