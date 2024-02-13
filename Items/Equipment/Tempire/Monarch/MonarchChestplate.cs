using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment.Tempire.Monarch
{
    [AutoloadEquip(EquipType.Body)]
    public class MonarchChestplate : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 20;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(silver: 75);
            Item.vanity = true;
            Item.maxStack = 1;
        }
    }

    [AutoloadEquip(EquipType.Head)]
    public class MonarchHelmet : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 20;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(silver: 75);
            Item.vanity = true;
            Item.maxStack = 1;
        }
    }

    [AutoloadEquip(EquipType.Legs)]
    public class MonarchLeggings : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(silver: 75);
            Item.vanity = true;
            Item.maxStack = 1;
        }
    }
}
