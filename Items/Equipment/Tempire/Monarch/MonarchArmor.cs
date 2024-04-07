using Terrafirma.Items.Equipment.Wings;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
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
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            if (head.type == ModContent.ItemType<MonarchHelmet>() && body.type == ModContent.ItemType<MonarchChestplate>() && legs.type == ModContent.ItemType<MonarchLeggings>())
            {
                return true;
            }
            return false;
        }

        public override void UpdateArmorSet(Player player)
        {
            int WingId = new Item(ModContent.ItemType<MonarchWings>()).wingSlot;
            player.setBonus = Language.GetTextValue("Mods.Terrafirma.Items.MonarchHelmet.SetBonus");
            if (!TFUtils.AnyWingsEquipped(player))
            {
                player.wings = WingId;
                player.wingTimeMax = player.GetWingStats(WingId).FlyTime;
                player.equippedWings = new Item(ModContent.ItemType<MonarchWings>());
                player.wingsLogic = WingId;
            }
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
        }
    }
}
