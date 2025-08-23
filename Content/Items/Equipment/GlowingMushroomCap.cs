
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Content.Items.Equipment
{

    [AutoloadEquip(EquipType.Head)]
    public class GlowingMushroomCap : ModItem
    {
        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.IsTallHat[Item.headSlot] = true;
            ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 26;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 2);
            Item.defense = 1;
        }
        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 40;
            Lighting.AddLight(player.Top,TorchID.Mushroom);
        }
        public override void AddRecipes()
        {
            Recipe.Create(Type).AddIngredient(ItemID.GlowingMushroom, 25).AddTile(TileID.WorkBenches).Register();
            base.AddRecipes();
        }
    }
}
