using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Items.Vanity.Dedicated
{
    [AutoloadEquip(EquipType.Legs)]
    public class KinMechanicalLegs : ModItem
    {
		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 16;

            Item.rare = ItemRarityID.Red;
            Item.value = Item.sellPrice(silver: 75);
			Item.vanity = true;
			Item.maxStack = 1;
		}

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }
    }
}
