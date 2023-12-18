using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Items.Equipment.Sacred
{
    [AutoloadEquip(EquipType.Body)]
    public class SacredPlateMail : ModItem
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(16);
            Item.defense = 12;
            Item.rare = ItemRarityID.Pink;
            Item.sellPrice(3);
        }
    }
    [AutoloadEquip(EquipType.Legs)]
    public class SacredGreaves : ModItem
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(16);
            Item.defense = 10;
            Item.rare = ItemRarityID.Pink;
            Item.sellPrice(3);
        }
    }
    [AutoloadEquip(EquipType.Head)]
    public class SacredHeadgear : ModItem
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(16);
            Item.defense = 5;
            Item.rare = ItemRarityID.Pink;
            Item.sellPrice(3);
        }
    }
    [AutoloadEquip(EquipType.Head)]
    public class SacredHelmet : ModItem
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(16);
            Item.defense = 9;
            Item.rare = ItemRarityID.Pink;
            Item.sellPrice(3);
        }
    }
    [AutoloadEquip(EquipType.Head)]
    public class SacredMask : ModItem
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(16);
            Item.defense = 24;
            Item.rare = ItemRarityID.Pink;
            Item.sellPrice(3);
        }
    }
    [AutoloadEquip(EquipType.Head)]
    public class SacredHood : ModItem
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(16);
            Item.defense = 1;
            Item.rare = ItemRarityID.Pink;
            Item.sellPrice(3);
        }
    }
}
