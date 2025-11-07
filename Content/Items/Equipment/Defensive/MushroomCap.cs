using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Content.Items.Equipment.Defensive
{

    [AutoloadEquip(EquipType.Head)]
    public class MushroomCap : ModItem
    { 
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 20);
            Item.defense = 1;
        }
        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 25;
        }

        public override void AddRecipes()
        {
            Recipe.Create(Type).AddIngredient(ItemID.Mushroom, 7).AddTile(TileID.WorkBenches).Register();
            base.AddRecipes();
        }
    }
}
