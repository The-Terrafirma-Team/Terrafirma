using Microsoft.Xna.Framework;
using TerrafirmaRedux.Global;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Items.Equipment.Movement
{
    class SpringBoots : ModItem
    {
		public override void SetDefaults()
		{
			Item.DefaultToAccessory(26, 36);
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(0, 0, 75, 0);
		}
	}
}
