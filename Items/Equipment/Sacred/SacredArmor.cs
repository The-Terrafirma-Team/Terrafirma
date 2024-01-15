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

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Generic) += 4;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            if (head.type == ModContent.ItemType<SacredMask>() || head.type == ModContent.ItemType<SacredHeadgear>() || head.type == ModContent.ItemType<SacredHelmet>() || head.type == ModContent.ItemType<SacredHood>())
            {
                if (body.type == ModContent.ItemType<SacredPlateMail>() && legs.type == ModContent.ItemType<SacredGreaves>())
                {
                    return true;
                }
            }
            return false;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "5% increased damage and increases your max number of minions and sentries by 1";
            player.GetDamage(DamageClass.Generic) += 0.05f;
            player.maxMinions += 1;
            player.maxTurrets += 1;
        }
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
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

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.04f;
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

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 60;
            player.GetDamage(DamageClass.Magic) += 0.06f;
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

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Ranged) += 0.04f;
            player.GetDamage(DamageClass.Ranged) += 0.08f;
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

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetAttackSpeed(DamageClass.Melee) += 0.05f;
            player.GetDamage(DamageClass.Melee) += 0.05f;
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

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void UpdateEquip(Player player)
        {
            player.maxMinions += 1;
            player.GetDamage(DamageClass.Summon) += 0.05f;
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
