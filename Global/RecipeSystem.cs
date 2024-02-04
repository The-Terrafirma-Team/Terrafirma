using Terrafirma.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Global
{
    public class RecipeSystem : ModSystem
    {
        public override void AddRecipes()
        {
            Recipe.Create(ItemID.SlimeStaff).AddTile(TileID.WorkBenches).AddIngredient(ItemID.Gel, 25).AddIngredient(ItemID.Wood, 25).AddIngredient(ItemID.Daybloom).Register();
        }
        public override void PostAddRecipes()
        {
            for(int i = 0; i < Recipe.numRecipes; i++) 
            {
                Recipe recipe = Main.recipe[i];
                if(recipe.HasResult(ItemID.HallowedGreaves) || recipe.HasResult(ItemID.HallowedPlateMail) || recipe.HasResult(ItemID.HallowedHelmet) || recipe.HasResult(ItemID.HallowedHeadgear) || recipe.HasResult(ItemID.HallowedHood) || recipe.HasResult(ItemID.HallowedMask) ||
                    recipe.HasResult(ItemID.AncientHallowedGreaves) || recipe.HasResult(ItemID.AncientHallowedPlateMail) || recipe.HasResult(ItemID.AncientHallowedHelmet) || recipe.HasResult(ItemID.AncientHallowedHeadgear) || recipe.HasResult(ItemID.AncientHallowedHood) || recipe.HasResult(ItemID.AncientHallowedMask))
                {
                    recipe.AddIngredient(ItemID.SoulofFright,5);
                    recipe.AddIngredient(ItemID.SoulofSight, 5);
                    recipe.AddIngredient(ItemID.SoulofMight, 5);

                }
                else if(recipe.HasResult(ItemID.AdamantiteForge) || recipe.HasResult(ItemID.TitaniumForge))
                {
                    recipe.AddIngredient(ModContent.ItemType<MajesticGel>(),10);
                }
            }
            #region Uncraftable to Craftable
            Recipe HermesBoots = Recipe.Create(ItemID.HermesBoots);
            HermesBoots.AddIngredient(ItemID.Leather, 5);
            HermesBoots.AddIngredient(ItemID.Silk, 5);
            HermesBoots.AddIngredient(ItemID.SwiftnessPotion, 3);
            HermesBoots.AddTile(TileID.Anvils);
            HermesBoots.Register();

            Recipe WaterWalkingBoots = Recipe.Create(ItemID.WaterWalkingBoots);
            WaterWalkingBoots.AddIngredient(ItemID.Leather, 3);
            WaterWalkingBoots.AddIngredient(ItemID.Silk, 3);
            WaterWalkingBoots.AddIngredient(ItemID.ShellPileBlock, 10);
            WaterWalkingBoots.AddIngredient(ItemID.WaterWalkingPotion, 1);
            WaterWalkingBoots.AddTile(TileID.Anvils);
            WaterWalkingBoots.Register();

            Recipe FlurryBoots = Recipe.Create(ItemID.FlurryBoots);
            FlurryBoots.AddIngredient(ItemID.Leather, 3);
            FlurryBoots.AddIngredient(ItemID.Silk, 3);
            FlurryBoots.AddIngredient(ItemID.IceBlock, 25);
            FlurryBoots.AddIngredient(ItemID.SwiftnessPotion, 3);
            FlurryBoots.AddTile(TileID.Anvils);
            FlurryBoots.Register();

            Recipe SailfishBoots = Recipe.Create(ItemID.SailfishBoots);
            SailfishBoots.AddIngredient(ItemID.Leather, 3);
            SailfishBoots.AddIngredient(ItemID.Silk, 3);
            SailfishBoots.AddIngredient(ItemID.SharkFin, 2);
            SailfishBoots.AddIngredient(ItemID.Coral, 5);
            SailfishBoots.AddIngredient(ItemID.SwiftnessPotion, 3);
            SailfishBoots.AddTile(TileID.Anvils);
            SailfishBoots.Register();

            Recipe SandBoots = Recipe.Create(ItemID.SandBoots);
            SandBoots.AddIngredient(ItemID.Leather, 3);
            SandBoots.AddIngredient(ItemID.Silk, 3);
            //SandBoots.AddIngredient(ItemID.AncientCloth, 5);
            SandBoots.AddIngredient(ItemID.SandBlock, 35);
            SandBoots.AddTile(TileID.Anvils);
            SandBoots.Register();

            Recipe IceSkates = Recipe.Create(ItemID.IceSkates);
            IceSkates.AddIngredient(ItemID.Leather, 5);
            IceSkates.AddIngredient(ItemID.Silk, 3);
            IceSkates.AddIngredient(ItemID.IceBlock, 10);
            IceSkates.AddRecipeGroup(RecipeGroupID.IronBar, 5);
            IceSkates.AddTile(TileID.Anvils);
            IceSkates.Register();

            Recipe FlowerBoots = Recipe.Create(ItemID.FlowerBoots);
            FlowerBoots.AddIngredient(ItemID.Leather, 3);
            FlowerBoots.AddIngredient(ItemID.Silk, 3);
            FlowerBoots.AddIngredient(ItemID.JungleRose, 1);
            FlowerBoots.AddTile(TileID.Anvils);
            FlowerBoots.Register();

            Recipe LuckyHorseshoeGold = Recipe.Create(ItemID.LuckyHorseshoe);
            LuckyHorseshoeGold.AddIngredient(ItemID.GoldBar, 5);
            LuckyHorseshoeGold.AddIngredient(ItemID.Cloud, 15);
            LuckyHorseshoeGold.AddTile(TileID.SkyMill);
            LuckyHorseshoeGold.Register();

            Recipe LuckyHorseshoePlatinum = Recipe.Create(ItemID.LuckyHorseshoe);
            LuckyHorseshoePlatinum.AddIngredient(ItemID.PlatinumBar, 5);
            LuckyHorseshoePlatinum.AddIngredient(ItemID.Cloud, 15);
            LuckyHorseshoePlatinum.AddTile(TileID.SkyMill);
            LuckyHorseshoePlatinum.Register();

            Recipe AgletCopper = Recipe.Create(ItemID.Aglet);
            AgletCopper.AddIngredient(ItemID.CopperBar, 5);
            AgletCopper.AddIngredient(ItemID.Sunflower, 1);
            AgletCopper.AddTile(TileID.WorkBenches);
            AgletCopper.Register();

            Recipe AgletTin = Recipe.Create(ItemID.Aglet);
            AgletTin.AddIngredient(ItemID.TinBar, 5);
            AgletTin.AddIngredient(ItemID.Sunflower, 1);
            AgletTin.AddTile(TileID.WorkBenches);
            AgletTin.Register();

            Recipe AnkletoftheWind = Recipe.Create(ItemID.AnkletoftheWind);
            AnkletoftheWind.AddIngredient(ItemID.JungleRose, 1);
            AnkletoftheWind.AddIngredient(ItemID.BambooBlock, 25);
            AnkletoftheWind.AddIngredient(ItemID.Sunflower, 2);
            AnkletoftheWind.AddTile(TileID.WorkBenches);
            AnkletoftheWind.Register();

            Recipe BandofStarpower = Recipe.Create(ItemID.BandofStarpower);
            BandofStarpower.AddIngredient(ItemID.Sapphire, 1);
            BandofStarpower.AddIngredient(ItemID.Star, 5);
            BandofStarpower.AddRecipeGroup(RecipeGroupID.IronBar, 5);
            BandofStarpower.AddTile(TileID.Anvils);
            BandofStarpower.Register();

            Recipe BandofRegeneration = Recipe.Create(ItemID.BandofRegeneration);
            BandofRegeneration.AddIngredient(ItemID.Ruby, 1);
            BandofRegeneration.AddIngredient(ItemID.Star, 5);
            BandofRegeneration.AddRecipeGroup(RecipeGroupID.IronBar, 5);
            BandofRegeneration.AddTile(TileID.Anvils);
            BandofRegeneration.Register();

            Recipe PocketMirror = Recipe.Create(ItemID.PocketMirror);
            PocketMirror.AddIngredient(ItemID.GoldBar, 5);
            PocketMirror.AddIngredient(ItemID.SilverBar, 2);
            PocketMirror.AddTile(TileID.Anvils);
            PocketMirror.Register();

            Recipe EncumberingStone = Recipe.Create(ItemID.EncumberingStone);
            EncumberingStone.AddIngredient(ItemID.StoneBlock, 250);
            EncumberingStone.Register();
            #endregion
        }
    }
}
