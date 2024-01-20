using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrafirmaRedux.Items.Weapons.Magic;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Systems.MageClass
{
    //
    // Summary:
    //     Indexes all Weapon Spells and Spell Types

    public class SpellIndex : ModSystem
    {
        public Dictionary<int, int[]> ItemCatalogue = new Dictionary<int, int[]>();
        public Dictionary<int, Tuple<byte, string, string, string, float> > SpellCatalogue = new Dictionary<int, Tuple<byte, string, string, string, float> >();
        const string AssetPath = "TerrafirmaRedux/Systems/MageClass/SpellIcons/";
        public override void OnModLoad()
        {
            //// Weapons
            //Gem Staves
            ItemCatalogue.Add(ItemID.AmethystStaff, new [] { 0 , 1 });
            ItemCatalogue.Add(ItemID.TopazStaff, new[] { 2, 3 });
            ItemCatalogue.Add(ItemID.EmeraldStaff, new[] { 4, 5 });
            ItemCatalogue.Add(ItemID.RubyStaff, new[] { 6, 7 });
            ItemCatalogue.Add(ItemID.DiamondStaff, new[] { 8, 9 });
            ItemCatalogue.Add(ItemID.SapphireStaff, new[] { 10, 11 });
            ItemCatalogue.Add(ItemID.AmberStaff, new[] { 12, 13 });
            //Evil Weapons
            ItemCatalogue.Add(ItemID.GoldenShower, new[] { 20, 21 });
            //Dungeon Weapons
            ItemCatalogue.Add(ItemID.InfernoFork, new[] { 14, 15, 16 });
            ItemCatalogue.Add(ItemID.WaterBolt, new[] { 17, 18, 19 });
            ItemCatalogue.Add(ItemID.BookofSkulls, new[] { 22, 23,28 });
            ItemCatalogue.Add(ItemID.AquaScepter, new[] { 26, 27 });
            //Other
            ItemCatalogue.Add(ItemID.RainbowGun, new[] { 24, 25 });
            //TEST
            //ItemCatalogue.Add(ItemID.WandofFrosting, new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 });


            //// Spells
            /// Gem Staves
            //Amethyst Staff
            SpellCatalogue.Add(0, new Tuple<byte, string, string, string, float>(0, AssetPath + "PreHardmode/GemStaff/AmethystBolt", "Amethyst Bolt", "Shoots a Bolt of Mana", 5) );
            SpellCatalogue.Add(1, new Tuple<byte, string, string, string, float>(1, AssetPath + "PreHardmode/GemStaff/AmethystHomingShot", "Homing Amethyst Bolt", "Shoots a bolt of homing mana", 6) );
            //Topaz Staff
            SpellCatalogue.Add(2, new Tuple<byte, string, string, string, float>(2, AssetPath + "PreHardmode/GemStaff/TopazBolt", "Topaz Bolt", "Shoots a Bolt of Mana", 5));
            SpellCatalogue.Add(3, new Tuple<byte, string, string, string, float>(3, AssetPath + "PreHardmode/GemStaff/TopazSplitShot", "Splitting Topaz Bolt", "Shoots a bolt of splitting mana", 6));
            //Emerald Staff
            SpellCatalogue.Add(4, new Tuple<byte, string, string, string, float>(4, AssetPath + "PreHardmode/GemStaff/EmeraldBolt", "Emerald Bolt", "Shoots a Bolt of Mana", 6));
            SpellCatalogue.Add(5, new Tuple<byte, string, string, string, float>(5, AssetPath + "PreHardmode/GemStaff/EmeraldPenetrationShot", "Piercing Emerald Bolt", "Shoots a bolt of piercing mana", 8));
            //Ruby Staff
            SpellCatalogue.Add(6, new Tuple<byte, string, string, string, float>(6, AssetPath + "PreHardmode/GemStaff/RubyBolt", "Ruby Bolt", "Shoots a Bolt of Mana", 7));
            SpellCatalogue.Add(7, new Tuple<byte, string, string, string, float>(7, AssetPath + "PreHardmode/GemStaff/RubyExplosiveShot", "Explosive Ruby Bolt", "Shoots a bolt of exploding mana", 9));
            //Diamond Staff
            SpellCatalogue.Add(8, new Tuple<byte, string, string, string, float>(8, AssetPath + "PreHardmode/GemStaff/DiamondBolt", "Diamond Bolt", "Shoots a Bolt of mana", 8));
            SpellCatalogue.Add(9, new Tuple<byte, string, string, string, float>(9, AssetPath + "PreHardmode/GemStaff/DiamondTurret", "Crystal Turret", "Shoots out a turret", 40));
            //Sapphire Staff
            SpellCatalogue.Add(10, new Tuple<byte, string, string, string, float>(10, AssetPath + "PreHardmode/GemStaff/SapphireBolt", "Sapphire Bolt", "Shoots a Bolt of Mana", 6));
            SpellCatalogue.Add(11, new Tuple<byte, string, string, string, float>(11, AssetPath + "PreHardmode/GemStaff/SapphireQuickShot", "Quick Sapphire Bolt", "Shoots a quick bolt of mana", 7));
            //Amber Staff
            SpellCatalogue.Add(12, new Tuple<byte, string, string, string, float>(12, AssetPath + "PreHardmode/GemStaff/AmberBolt", "Amber Bolt", "Shoots a bolt of mana", 7));
            SpellCatalogue.Add(13, new Tuple<byte, string, string, string, float>(13, AssetPath + "PreHardmode/GemStaff/AmberWall", "Amber Wall", "Shoots a bolt of mana that expands into a wall", 14));

            /// Evil Weapons
            //Golden Shower
            SpellCatalogue.Add(20, new Tuple<byte, string, string, string, float>(0, AssetPath + "Hardmode/GoldenShower", "Golden Shower", "Shoots Sprays a shower of ichor, Decreases target's defense", 7));
            SpellCatalogue.Add(21, new Tuple<byte, string, string, string, float>(1, AssetPath + "Hardmode/IchorBubble", "Ichor Bubble", "Shoots a big explosive bubble of boiling Ichor, Decreases target's defense and lights them on fire", 7));
            
            /// Dungeon Weapons
            //Inferno Fork
            SpellCatalogue.Add(14, new Tuple<byte, string, string, string, float>(0, AssetPath + "Hardmode/InfernoFork", "Inferno Fork", "Shoots an inferno fork that explodes into a lingering ball of fire", 18));
            SpellCatalogue.Add(15, new Tuple<byte, string, string, string, float>(1, AssetPath + "Hardmode/InfernoFlamethrower", "Hellfire Breath", "Shoots a blast of flame", 16));
            SpellCatalogue.Add(16, new Tuple<byte, string, string, string, float>(2, AssetPath + "Hardmode/InfernoWall", "Diabolical Firewall", "Summons a wall of fire", 24));
            //Water Bolt
            SpellCatalogue.Add(17, new Tuple<byte, string, string, string, float>(3, AssetPath + "PreHardmode/SpellBooks/WaterBolt", "Water Bolt", "Shoots a bolt of bouncy water", 10));
            SpellCatalogue.Add(18, new Tuple<byte, string, string, string, float>(4, AssetPath + "PreHardmode/SpellBooks/WaterGeyser", "Water Geyser", "Summons a water geyser", 16));
            SpellCatalogue.Add(19, new Tuple<byte, string, string, string, float>(5, AssetPath + "PreHardmode/SpellBooks/WaterAura", "Aura Wave", "Shoots a wavee to splash enemies away", 12));
            //Book of Skull
            SpellCatalogue.Add(22, new Tuple<byte, string, string, string, float>(6, AssetPath + "PreHardmode/SpellBooks/FlyingSkull", "Flaming Skull", "Shoots a flying flaming skull", 18));
            SpellCatalogue.Add(23, new Tuple<byte, string, string, string, float>(7, AssetPath + "PreHardmode/SpellBooks/BoneFragmentStorm", "Bone Fragment Storm", "Shoots multiple bone fragments", 2));
            SpellCatalogue.Add(28, new Tuple<byte, string, string, string, float>(10, AssetPath + "PreHardmode/SpellBooks/SkeletonHand", "Master's Hand", "Summons a skeletal hand that roasts all enemies in its vicinity", 40));
            //Aqua Scepter
            SpellCatalogue.Add(26, new Tuple<byte, string, string, string, float>(8, AssetPath + "PreHardmode/WaterStream", "Water Stream", "Sprays out a shower of water", 7));
            SpellCatalogue.Add(27, new Tuple<byte, string, string, string, float>(9, AssetPath + "PreHardmode/WaterHealing", "Water Treatment", "Shoots out a bubble of healing water", 12));
            //SpellCatalogue.Add(14, new Tuple<byte, string, string, string, string>(0, AssetPath + "PlaceholderSpellIcon", "TBA", "This Spell is not here yet! Tell Fred to push those changes already!!", "Uses 5 Mana"));

            /// Other Weapons
            //Rainbow Gun
            SpellCatalogue.Add(24, new Tuple<byte, string, string, string, float>(0, AssetPath + "Hardmode/PiercingRainbow", "Piercing Rainbow", "Shoots a rainbow that does continuous damage", 20));
            SpellCatalogue.Add(25, new Tuple<byte, string, string, string, float>(1, AssetPath + "Hardmode/PrismRain", "Prism Rain", "Shoots a barrage of colored glass prisms", 4));
        }
    }
}
