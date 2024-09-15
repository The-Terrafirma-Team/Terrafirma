using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment.Mixed
{
    [AutoloadEquip(EquipType.Head)]
    public class PaladinHelmet : ModItem
    {
        public static LocalizedText SetBonusText { get; private set; }
        public override void SetStaticDefaults()
        {
            SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs(Terrafirma.SetBonusKey);
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<SteelChestplate>() && legs.type == ModContent.ItemType<SteelGreaves>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = SetBonusText.Value;
            player.GetModPlayer<SteelSetBonus>().active = true;
        }
        public override void UpdateVanitySet(Player player)
        {
            player.armorEffectDrawShadowLokis = true;
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(gold: 4, silver: 50);
            Item.defense = 2;
        }
    }

    [AutoloadEquip(EquipType.Body)]
    public class PaladinCuirass : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(gold: 4, silver: 50);
            Item.defense = 4;
        }
    }

    [AutoloadEquip(EquipType.Legs)]
    public class PaladinGreaves : ModItem
    {
        public override void SetDefaults()
        {
            Item.defense = 2;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(gold: 4, silver: 50);
        }
    }
}
