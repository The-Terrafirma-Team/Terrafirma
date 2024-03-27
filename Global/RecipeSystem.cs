using System.Collections.Generic;
using Terrafirma.Items.Materials;
using Terrafirma.Systems.Cooking;
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
                // add souls to hallowed armor
                Recipe recipe = Main.recipe[i];
                if (recipe.HasResult(ItemID.HallowedGreaves) || recipe.HasResult(ItemID.HallowedPlateMail) || recipe.HasResult(ItemID.HallowedHelmet) || recipe.HasResult(ItemID.HallowedHeadgear) || recipe.HasResult(ItemID.HallowedHood) || recipe.HasResult(ItemID.HallowedMask) ||
                    recipe.HasResult(ItemID.AncientHallowedGreaves) || recipe.HasResult(ItemID.AncientHallowedPlateMail) || recipe.HasResult(ItemID.AncientHallowedHelmet) || recipe.HasResult(ItemID.AncientHallowedHeadgear) || recipe.HasResult(ItemID.AncientHallowedHood) || recipe.HasResult(ItemID.AncientHallowedMask))
                {
                    recipe.AddIngredient(ItemID.SoulofFright, 5);
                    recipe.AddIngredient(ItemID.SoulofSight, 5);
                    recipe.AddIngredient(ItemID.SoulofMight, 5);

                }
                // Add majestic gel to adamantite forges
                else if (recipe.HasResult(ItemID.AdamantiteForge) || recipe.HasResult(ItemID.TitaniumForge))
                {
                    recipe.AddIngredient(ModContent.ItemType<MajesticGel>(), 10);
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

            #region Cooking Recipes

            CookingRecipe AppleJuice = new CookingRecipe(ItemID.AppleJuice);
            AppleJuice.RegisterVariant( new List<int> { ItemID.Apple, ItemID.Bottle });

            CookingRecipe BloodyMoscato = new CookingRecipe(ItemID.BloodyMoscato);
            BloodyMoscato.RegisterVariant(new List<int> { ItemID.Rambutan, ItemID.Bottle, ItemID.BloodOrange });

            CookingRecipe BowlOfSoup = new CookingRecipe(ItemID.BowlofSoup);
            BowlOfSoup.RegisterVariant(new List<int> { ItemID.Mushroom, ItemID.Goldfish });

            CookingRecipe BunnyStew = new CookingRecipe(ItemID.BunnyStew);
            BunnyStew.RegisterVariant( ItemID.Bunny );

            CookingRecipe CookedFish = new CookingRecipe(ItemID.CookedFish);
            CookedFish.RegisterVariant(new List<int> { ItemID.Bass });
            CookedFish.RegisterVariant(new List<int> { ItemID.Trout });
            CookedFish.RegisterVariant(new List<int> { ItemID.AtlanticCod });

            CookingRecipe CookedShrimp = new CookingRecipe(ItemID.CookedShrimp);
            CookedShrimp.RegisterVariant(new List<int> { ItemID.Shrimp });

            CookingRecipe Escargot = new CookingRecipe(ItemID.Escargot);
            Escargot.RegisterVariant(new List<int> { ItemID.Snail });

            CookingRecipe FroggleBunwich = new CookingRecipe(ItemID.FroggleBunwich);
            FroggleBunwich.RegisterVariant(new List<int> { ItemID.Frog, ItemID.Frog });

            CookingRecipe BananaDaiquiri = new CookingRecipe(ItemID.BananaDaiquiri);
            BananaDaiquiri.RegisterVariant(new List<int> { ItemID.Banana, ItemID.SnowBlock, ItemID.Bottle });

            CookingRecipe FruitJuice = new CookingRecipe(ItemID.FruitJuice);
            FruitJuice.AddIngredient(ItemID.Bottle);
            FruitJuice.AddRecipeGroup(RecipeGroupID.Fruit);
            FruitJuice.AddRecipeGroup(RecipeGroupID.Fruit);
            FruitJuice.Register();

            CookingRecipe FruitSalad = new CookingRecipe(ItemID.FruitSalad);
            FruitSalad.AddIngredient(ItemID.Bowl);
            FruitSalad.AddRecipeGroup(RecipeGroupID.Fruit);
            FruitSalad.AddRecipeGroup(RecipeGroupID.Fruit);
            FruitSalad.Register();

            CookingRecipe GoldenDelight = new CookingRecipe(ItemID.GoldenDelight);
            GoldenDelight.RegisterVariant(new List<int> { ItemID.GoldWaterStrider });
            GoldenDelight.RegisterVariant(new List<int> { ItemID.GoldSeahorse });
            GoldenDelight.RegisterVariant(new List<int> { ItemID.GoldFrog });
            GoldenDelight.RegisterVariant(new List<int> { ItemID.GoldLadyBug });
            GoldenDelight.RegisterVariant(new List<int> { ItemID.GoldDragonfly });
            GoldenDelight.RegisterVariant(new List<int> { ItemID.GoldWorm });
            GoldenDelight.RegisterVariant(new List<int> { ItemID.GoldGrasshopper });
            GoldenDelight.RegisterVariant(new List<int> { ItemID.SquirrelGold });
            GoldenDelight.RegisterVariant(new List<int> { ItemID.GoldMouse });
            GoldenDelight.RegisterVariant(new List<int> { ItemID.GoldBunny });
            GoldenDelight.RegisterVariant(new List<int> { ItemID.GoldBird });

            CookingRecipe GrapeJuice = new CookingRecipe(ItemID.GrapeJuice);
            GrapeJuice.RegisterVariant(new List<int> { ItemID.Grapes, ItemID.Grapes, ItemID.Bottle });

            CookingRecipe GrilledSquirrel = new CookingRecipe(ItemID.GrilledSquirrel);
            GrilledSquirrel.RegisterVariant(new List<int> { ItemID.Squirrel});
            GrilledSquirrel.RegisterVariant(new List<int> { ItemID.SquirrelRed });

            CookingRecipe GrubSoup = new CookingRecipe(ItemID.GrubSoup);
            GrubSoup.RegisterVariant(new List<int> { ItemID.Grubby, ItemID.Sluggy, ItemID.Buggy });

            CookingRecipe Lemonade = new CookingRecipe(ItemID.Lemonade);
            Lemonade.RegisterVariant(new List<int> { ItemID.Lemon, ItemID.Bottle });

            CookingRecipe LobsterTail = new CookingRecipe(ItemID.LobsterTail);
            LobsterTail.RegisterVariant(new List<int> { ItemID.RockLobster });

            CookingRecipe MonsterLasagna = new CookingRecipe(ItemID.MonsterLasagna);
            MonsterLasagna.RegisterVariant(new List<int> { ItemID.Vertebrae, ItemID.Vertebrae, ItemID.Vertebrae });
            MonsterLasagna.RegisterVariant(new List<int> { ItemID.RottenChunk, ItemID.RottenChunk, ItemID.RottenChunk });

            CookingRecipe peachsangria = new CookingRecipe(ItemID.PeachSangria);
            peachsangria.RegisterVariant(new List<int> { ItemID.Peach, ItemID.Bottle });

            CookingRecipe PinaColada = new CookingRecipe(ItemID.PinaColada);
            PinaColada.RegisterVariant(new List<int> { ItemID.Pineapple, ItemID.Coconut, ItemID.Bottle });

            CookingRecipe PrismaticPunch = new CookingRecipe(ItemID.PrismaticPunch);
            PrismaticPunch.RegisterVariant(new List<int> { ItemID.Starfruit, ItemID.Dragonfruit, ItemID.Bottle });

            CookingRecipe RoastedBird = new CookingRecipe(ItemID.RoastedBird);
            RoastedBird.RegisterVariant(new List<int> { ItemID.Bird });
            RoastedBird.RegisterVariant(new List<int> { ItemID.Cardinal });
            RoastedBird.RegisterVariant(new List<int> { ItemID.BlueJay });
            RoastedBird.RegisterVariant(new List<int> { ItemID.Penguin });
            RoastedBird.RegisterVariant(new List<int> { ItemID.Seagull });

            CookingRecipe RoastedDuck = new CookingRecipe(ItemID.RoastedDuck);
            RoastedDuck.RegisterVariant(new List<int> { ItemID.Grebe });
            RoastedDuck.RegisterVariant(new List<int> { ItemID.MallardDuck });
            RoastedDuck.RegisterVariant(new List<int> { ItemID.Duck });

            CookingRecipe SauteedFrogLegs = new CookingRecipe(ItemID.SauteedFrogLegs);
            SauteedFrogLegs.RegisterVariant(new List<int> { ItemID.Frog });

            CookingRecipe SeafoodDinner = new CookingRecipe(ItemID.SeafoodDinner);
            SeafoodDinner.RegisterVariant(new List<int> { ItemID.ArmoredCavefish, ItemID.ArmoredCavefish });
            SeafoodDinner.RegisterVariant(new List<int> { ItemID.Stinkfish, ItemID.Stinkfish });
            SeafoodDinner.RegisterVariant(new List<int> { ItemID.SpecularFish, ItemID.SpecularFish });
            SeafoodDinner.RegisterVariant(new List<int> { ItemID.Prismite, ItemID.Prismite });
            SeafoodDinner.RegisterVariant(new List<int> { ItemID.PrincessFish, ItemID.PrincessFish });
            SeafoodDinner.RegisterVariant(new List<int> { ItemID.Obsidifish, ItemID.Obsidifish });
            SeafoodDinner.RegisterVariant(new List<int> { ItemID.VariegatedLardfish, ItemID.VariegatedLardfish });
            SeafoodDinner.RegisterVariant(new List<int> { ItemID.NeonTetra, ItemID.NeonTetra });
            SeafoodDinner.RegisterVariant(new List<int> { ItemID.Honeyfin, ItemID.Honeyfin });
            SeafoodDinner.RegisterVariant(new List<int> { ItemID.Hemopiranha, ItemID.Hemopiranha });
            SeafoodDinner.RegisterVariant(new List<int> { ItemID.FrostMinnow, ItemID.FrostMinnow });
            SeafoodDinner.RegisterVariant(new List<int> { ItemID.FlarefinKoi, ItemID.FlarefinKoi });
            SeafoodDinner.RegisterVariant(new List<int> { ItemID.Ebonkoi, ItemID.Ebonkoi });
            SeafoodDinner.RegisterVariant(new List<int> { ItemID.DoubleCod, ItemID.DoubleCod });
            SeafoodDinner.RegisterVariant(new List<int> { ItemID.Damselfish, ItemID.Damselfish });
            SeafoodDinner.RegisterVariant(new List<int> { ItemID.CrimsonTigerfish, ItemID.CrimsonTigerfish });
            SeafoodDinner.RegisterVariant(new List<int> { ItemID.ChaosFish, ItemID.ChaosFish });

            CookingRecipe SmoothieofDarkness = new CookingRecipe(ItemID.SmoothieofDarkness);
            SmoothieofDarkness.RegisterVariant(new List<int> { ItemID.BlackCurrant, ItemID.Elderberry, ItemID.Bottle });

            CookingRecipe TropicalSmoothie = new CookingRecipe(ItemID.TropicalSmoothie);
            TropicalSmoothie.RegisterVariant(new List<int> { ItemID.Mango, ItemID.Pineapple, ItemID.Bottle });

            CookingRecipe PumpkinPie = new CookingRecipe(ItemID.PumpkinPie);
            PumpkinPie.RegisterVariant(new List<int> { ItemID.Pumpkin, ItemID.Pumpkin, ItemID.Pumpkin });

            CookingRecipe Teacup = new CookingRecipe(ItemID.Teacup);
            Teacup.RegisterVariant(new List<int> { ItemID.BottledWater});

            CookingRecipe Sashimi = new CookingRecipe(ItemID.Sashimi);
            Sashimi.RegisterVariant(new List<int> { ItemID.Flounder});
            Sashimi.RegisterVariant(new List<int> { ItemID.RedSnapper });
            Sashimi.RegisterVariant(new List<int> { ItemID.Salmon });
            Sashimi.RegisterVariant(new List<int> { ItemID.Tuna });


            #endregion
        }
    }
}
