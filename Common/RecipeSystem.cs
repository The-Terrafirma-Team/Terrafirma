using System.Collections.Generic;
using Terrafirma.Items.Equipment.Movement;
using Terrafirma.Items.Materials;
using Terrafirma.Systems.Cooking;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Common
{
    public class RecipeSystem : ModSystem
    {
        public override void AddRecipeGroups()
        {
            RecipeGroup GoldBar = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.GoldBar),
            [
                ItemID.GoldBar,
                ItemID.PlatinumBar
            ]);

            RecipeGroup CopperBar = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.CopperBar),
            [
                ItemID.CopperBar,
                ItemID.TinBar
            ]);
            RecipeGroup.RegisterGroup("Terrafirma:CopperBar", CopperBar);
            base.AddRecipeGroups();
        }
        public override void AddRecipes()
        {
            Recipe.Create(ItemID.SlimeStaff).AddTile(TileID.WorkBenches).AddIngredient(ItemID.Gel, 25).AddIngredient(ItemID.Wood, 25).AddIngredient(ItemID.Daybloom).Register();
            
            Recipe.Create(ItemID.Terragrim).AddTile(TileID.DemonAltar).AddIngredient(ItemID.GoldShortsword).AddIngredient(ModContent.ItemType<AncientSpiritEssence>(),8).Register();
            
            Recipe.Create(ItemID.LightningBoots).AddTile(TileID.TinkerersWorkbench).AddIngredient(ModContent.ItemType<SpringBoots>()).AddIngredient(ItemID.Aglet).AddIngredient(ItemID.RocketBoots).AddIngredient(ItemID.AnkletoftheWind).Register();
            AddCookingRecipes();
        }
        public override void PostAddRecipes()
        {
            for(int i = 0; i < Recipe.numRecipes; i++) 
            {
                Recipe recipe = Main.recipe[i];
                // add souls to hallowed armor
                //if (recipe.HasResult(ItemID.HallowedGreaves) || recipe.HasResult(ItemID.HallowedPlateMail) || recipe.HasResult(ItemID.HallowedHelmet) || recipe.HasResult(ItemID.HallowedHeadgear) || recipe.HasResult(ItemID.HallowedHood) || recipe.HasResult(ItemID.HallowedMask) ||
                //    recipe.HasResult(ItemID.AncientHallowedGreaves) || recipe.HasResult(ItemID.AncientHallowedPlateMail) || recipe.HasResult(ItemID.AncientHallowedHelmet) || recipe.HasResult(ItemID.AncientHallowedHeadgear) || recipe.HasResult(ItemID.AncientHallowedHood) || recipe.HasResult(ItemID.AncientHallowedMask))
                //{
                //    recipe.AddIngredient(ItemID.SoulofFright, 5);
                //    recipe.AddIngredient(ItemID.SoulofSight, 5);
                //    recipe.AddIngredient(ItemID.SoulofMight, 5);

                //}
                // Add majestic gel to adamantite forges
                if (recipe.HasResult(ItemID.AdamantiteForge) || recipe.HasResult(ItemID.TitaniumForge))
                {
                    recipe.AddIngredient(ModContent.ItemType<MajesticGel>(), 10);
                }
                else if (recipe.HasResult(ItemID.Leather) && recipe.Mod == null)
                {
                    recipe.RemoveIngredient(ItemID.RottenChunk);
                    recipe.AddIngredient(ItemID.RottenChunk, 3);
                }
                else if (recipe.HasResult(ItemID.NightsEdge))
                {
                    recipe.AddIngredient(ItemID.Terragrim);
                }
            }

        }
        public void AddCookingRecipes()
        {
            CookingRecipe AppleJuice = CookingRecipe.createCookingRecipe(ItemID.AppleJuice);
            AppleJuice.RegisterVariant(new List<int> { ItemID.Apple, ItemID.Bottle });

            CookingRecipe BloodyMoscato = CookingRecipe.createCookingRecipe(ItemID.BloodyMoscato);
            BloodyMoscato.RegisterVariant(new List<int> { ItemID.Rambutan, ItemID.Bottle, ItemID.BloodOrange });

            CookingRecipe BowlOfSoup = CookingRecipe.createCookingRecipe(ItemID.BowlofSoup);
            BowlOfSoup.RegisterVariant(new List<int> { ItemID.Mushroom, ItemID.Goldfish });

            CookingRecipe BunnyStew = CookingRecipe.createCookingRecipe(ItemID.BunnyStew);
            BunnyStew.RegisterVariant(ItemID.Bunny);

            CookingRecipe CookedFish = CookingRecipe.createCookingRecipe(ItemID.CookedFish);
            CookedFish.RegisterVariant(new List<int> { ItemID.Bass });
            CookedFish.RegisterVariant(new List<int> { ItemID.Trout });
            CookedFish.RegisterVariant(new List<int> { ItemID.AtlanticCod });

            CookingRecipe CookedShrimp = CookingRecipe.createCookingRecipe(ItemID.CookedShrimp);
            CookedShrimp.RegisterVariant(new List<int> { ItemID.Shrimp });

            CookingRecipe Escargot = CookingRecipe.createCookingRecipe(ItemID.Escargot);
            Escargot.RegisterVariant(new List<int> { ItemID.Snail });

            CookingRecipe FroggleBunwich = CookingRecipe.createCookingRecipe(ItemID.FroggleBunwich);
            FroggleBunwich.RegisterVariant(new List<int> { ItemID.Frog, ItemID.Frog });

            CookingRecipe BananaDaiquiri = CookingRecipe.createCookingRecipe(ItemID.BananaDaiquiri);
            BananaDaiquiri.RegisterVariant(new List<int> { ItemID.Banana, ItemID.SnowBlock, ItemID.Bottle });

            CookingRecipe FruitJuice = CookingRecipe.createCookingRecipe(ItemID.FruitJuice);
            FruitJuice.AddIngredient(ItemID.Bottle);
            FruitJuice.AddRecipeGroup(RecipeGroupID.Fruit);
            FruitJuice.AddRecipeGroup(RecipeGroupID.Fruit);
            FruitJuice.Register();

            CookingRecipe FruitSalad = CookingRecipe.createCookingRecipe(ItemID.FruitSalad);
            FruitSalad.AddIngredient(ItemID.Bowl);
            FruitSalad.AddRecipeGroup(RecipeGroupID.Fruit);
            FruitSalad.AddRecipeGroup(RecipeGroupID.Fruit);
            FruitSalad.Register();

            CookingRecipe GoldenDelight = CookingRecipe.createCookingRecipe(ItemID.GoldenDelight);
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

            CookingRecipe GrapeJuice = CookingRecipe.createCookingRecipe(ItemID.GrapeJuice);
            GrapeJuice.RegisterVariant(new List<int> { ItemID.Grapes, ItemID.Grapes, ItemID.Bottle });

            CookingRecipe GrilledSquirrel = CookingRecipe.createCookingRecipe(ItemID.GrilledSquirrel);
            GrilledSquirrel.RegisterVariant(new List<int> { ItemID.Squirrel });
            GrilledSquirrel.RegisterVariant(new List<int> { ItemID.SquirrelRed });

            CookingRecipe GrubSoup = CookingRecipe.createCookingRecipe(ItemID.GrubSoup);
            GrubSoup.RegisterVariant(new List<int> { ItemID.Grubby, ItemID.Sluggy, ItemID.Buggy });

            CookingRecipe Lemonade = CookingRecipe.createCookingRecipe(ItemID.Lemonade);
            Lemonade.RegisterVariant(new List<int> { ItemID.Lemon, ItemID.Bottle });

            CookingRecipe LobsterTail = CookingRecipe.createCookingRecipe(ItemID.LobsterTail);
            LobsterTail.RegisterVariant(new List<int> { ItemID.RockLobster });

            CookingRecipe MonsterLasagna = CookingRecipe.createCookingRecipe(ItemID.MonsterLasagna);
            MonsterLasagna.RegisterVariant(new List<int> { ItemID.Vertebrae, ItemID.Vertebrae, ItemID.Vertebrae });
            MonsterLasagna.RegisterVariant(new List<int> { ItemID.RottenChunk, ItemID.RottenChunk, ItemID.RottenChunk });

            CookingRecipe peachsangria = CookingRecipe.createCookingRecipe(ItemID.PeachSangria);
            peachsangria.RegisterVariant(new List<int> { ItemID.Peach, ItemID.Bottle });

            CookingRecipe PinaColada = CookingRecipe.createCookingRecipe(ItemID.PinaColada);
            PinaColada.RegisterVariant(new List<int> { ItemID.Pineapple, ItemID.Coconut, ItemID.Bottle });

            CookingRecipe PrismaticPunch = CookingRecipe.createCookingRecipe(ItemID.PrismaticPunch);
            PrismaticPunch.RegisterVariant(new List<int> { ItemID.Starfruit, ItemID.Dragonfruit, ItemID.Bottle });

            CookingRecipe RoastedBird = CookingRecipe.createCookingRecipe(ItemID.RoastedBird);
            RoastedBird.RegisterVariant(new List<int> { ItemID.Bird });
            RoastedBird.RegisterVariant(new List<int> { ItemID.Cardinal });
            RoastedBird.RegisterVariant(new List<int> { ItemID.BlueJay });
            RoastedBird.RegisterVariant(new List<int> { ItemID.Penguin });
            RoastedBird.RegisterVariant(new List<int> { ItemID.Seagull });

            CookingRecipe RoastedDuck = CookingRecipe.createCookingRecipe(ItemID.RoastedDuck);
            RoastedDuck.RegisterVariant(new List<int> { ItemID.Grebe });
            RoastedDuck.RegisterVariant(new List<int> { ItemID.MallardDuck });
            RoastedDuck.RegisterVariant(new List<int> { ItemID.Duck });

            CookingRecipe SauteedFrogLegs = CookingRecipe.createCookingRecipe(ItemID.SauteedFrogLegs);
            SauteedFrogLegs.RegisterVariant(new List<int> { ItemID.Frog });

            CookingRecipe SeafoodDinner = CookingRecipe.createCookingRecipe(ItemID.SeafoodDinner);
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

            CookingRecipe SmoothieofDarkness = CookingRecipe.createCookingRecipe(ItemID.SmoothieofDarkness);
            SmoothieofDarkness.RegisterVariant(new List<int> { ItemID.BlackCurrant, ItemID.Elderberry, ItemID.Bottle });

            CookingRecipe TropicalSmoothie = CookingRecipe.createCookingRecipe(ItemID.TropicalSmoothie);
            TropicalSmoothie.RegisterVariant(new List<int> { ItemID.Mango, ItemID.Pineapple, ItemID.Bottle });

            CookingRecipe PumpkinPie = CookingRecipe.createCookingRecipe(ItemID.PumpkinPie);
            PumpkinPie.RegisterVariant(new List<int> { ItemID.Pumpkin, ItemID.Pumpkin, ItemID.Pumpkin });

            CookingRecipe Teacup = CookingRecipe.createCookingRecipe(ItemID.Teacup);
            Teacup.RegisterVariant(new List<int> { ItemID.BottledWater });

            CookingRecipe Sashimi = CookingRecipe.createCookingRecipe(ItemID.Sashimi);
            Sashimi.RegisterVariant(new List<int> { ItemID.Flounder });
            Sashimi.RegisterVariant(new List<int> { ItemID.RedSnapper });
            Sashimi.RegisterVariant(new List<int> { ItemID.Salmon });
            Sashimi.RegisterVariant(new List<int> { ItemID.Tuna });
        }
    }
}
