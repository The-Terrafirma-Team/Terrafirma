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

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HallowedBar, 22)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            Recipe.Create(ItemID.HallowedPlateMail,1)
                .AddIngredient(ModContent.ItemType<SacredPlateMail>(),1)
                .AddIngredient(ItemID.SoulofFright, 5)
                .AddIngredient(ItemID.SoulofSight, 5)
                .AddIngredient(ItemID.SoulofMight, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();

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

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HallowedBar, 16)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            Recipe.Create(ItemID.HallowedPlateMail, 1)
                .AddIngredient(ModContent.ItemType<SacredGreaves>(), 1)
                .AddIngredient(ItemID.SoulofFright, 5)
                .AddIngredient(ItemID.SoulofSight, 5)
                .AddIngredient(ItemID.SoulofMight, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
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

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HallowedBar, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            Recipe.Create(ItemID.HallowedPlateMail, 1)
                .AddIngredient(ModContent.ItemType<SacredHeadgear>(), 1)
                .AddIngredient(ItemID.SoulofFright, 5)
                .AddIngredient(ItemID.SoulofSight, 5)
                .AddIngredient(ItemID.SoulofMight, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
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

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HallowedBar, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            Recipe.Create(ItemID.HallowedPlateMail, 1)
                .AddIngredient(ModContent.ItemType<SacredHelmet>(), 1)
                .AddIngredient(ItemID.SoulofFright, 5)
                .AddIngredient(ItemID.SoulofSight, 5)
                .AddIngredient(ItemID.SoulofMight, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
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

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HallowedBar, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            Recipe.Create(ItemID.HallowedPlateMail, 1)
                .AddIngredient(ModContent.ItemType<SacredMask>(), 1)
                .AddIngredient(ItemID.SoulofFright, 5)
                .AddIngredient(ItemID.SoulofSight, 5)
                .AddIngredient(ItemID.SoulofMight, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
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

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HallowedBar, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            Recipe.Create(ItemID.HallowedPlateMail, 1)
                .AddIngredient(ModContent.ItemType<SacredHood>(), 1)
                .AddIngredient(ItemID.SoulofFright, 5)
                .AddIngredient(ItemID.SoulofSight, 5)
                .AddIngredient(ItemID.SoulofMight, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
