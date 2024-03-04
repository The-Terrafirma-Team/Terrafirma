using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Items.Weapons.Magic;
using Terrafirma.Items.Weapons.Magic.Tempire;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Systems.MageClass
{
    //
    // Summary:
    //     Indexes all Weapon Spells and Spell Types

    public class SpellIndex : ModSystem
    {
        public Dictionary<int, int[]> ItemCatalogue = new Dictionary<int, int[]>();
        /// <summary>
        /// Spell Dictionary. Item1-SpellID. Item2-TexturePath. Item3-SpellName. Item4-SpellDescription. Item5-UseMana. Item6-UseTime. Item7-UseAnimation. 
        /// 
        /// </summary>
        public Dictionary<int, Tuple<int, string, string, string, float, int, int> > SpellCatalogue = new Dictionary<int, Tuple<int, string, string, string, float, int, int> >();
        const string AssetPath = "Terrafirma/Systems/MageClass/SpellIcons/";
        public override void OnWorldLoad()
        {
            ItemCatalogue.Clear();
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
            ItemCatalogue.Add(ItemID.CursedFlames, new[] { 29, 30 });
            ItemCatalogue.Add(ItemID.SkyFracture, new[] { 34, 35, 36 });
            //Dungeon Weapons
            ItemCatalogue.Add(ItemID.InfernoFork, new[] { 14, 15, 16 });
            ItemCatalogue.Add(ItemID.WaterBolt, new[] { 17, 18, 19 });
            ItemCatalogue.Add(ItemID.BookofSkulls, new[] { 22, 23,28 });
            ItemCatalogue.Add(ItemID.AquaScepter, new[] { 26, 27 });
            //Other
            ItemCatalogue.Add(ItemID.RainbowGun, new[] { 24, 25 });
            //Tempire
            ItemCatalogue.Add(ModContent.ItemType<Majesty>(), new[] { 32, 33 });
            //TEST
            //ItemCatalogue.Add(ItemID.WandofFrosting, new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 });



            //// Accessories
            ItemCatalogue.Add(ItemID.ManaFlower, new [] { 31 });
            ItemCatalogue.Add(ItemID.ArcaneFlower, new[] { 31 });
            ItemCatalogue.Add(ItemID.MagnetFlower, new[] { 31 });
            ItemCatalogue.Add(ItemID.ManaCloak, new[] { 31 });


            SpellCatalogue.Clear();
            //// Spells
            /// Gem Staves
            //Amethyst Staff
            SpellCatalogue.Add(0, new Tuple<int, string, string, string, float, int, int>(0, AssetPath + "PreHardmode/GemStaff/AmethystBolt", "Amethyst Bolt", "Shoots a Bolt of Mana", 5, 37, 37) );
            SpellCatalogue.Add(1, new Tuple<int, string, string, string, float, int, int>(1, AssetPath + "PreHardmode/GemStaff/AmethystHomingShot", "Homing Amethyst Bolt", "Shoots a bolt of homing mana", 6, 43, 43) );
            //Topaz Staff
            SpellCatalogue.Add(2, new Tuple<int, string, string, string, float, int, int>(2, AssetPath + "PreHardmode/GemStaff/TopazBolt", "Topaz Bolt", "Shoots a Bolt of Mana", 5, 36, 36));
            SpellCatalogue.Add(3, new Tuple<int, string, string, string, float, int, int>(3, AssetPath + "PreHardmode/GemStaff/TopazSplitShot", "Splitting Topaz Bolt", "Shoots a bolt of splitting mana", 6, 43, 43));
            //Emerald Staff
            SpellCatalogue.Add(4, new Tuple<int, string, string, string, float, int, int>(4, AssetPath + "PreHardmode/GemStaff/EmeraldBolt", "Emerald Bolt", "Shoots a Bolt of Mana", 6, 32, 32));
            SpellCatalogue.Add(5, new Tuple<int, string, string, string, float, int, int>(5, AssetPath + "PreHardmode/GemStaff/EmeraldPenetrationShot", "Piercing Emerald Bolt", "Shoots a bolt of piercing mana", 8, 32, 32));
            //Ruby Staff
            SpellCatalogue.Add(6, new Tuple<int, string, string, string, float, int, int>(6, AssetPath + "PreHardmode/GemStaff/RubyBolt", "Ruby Bolt", "Shoots a Bolt of Mana", 7, 28, 28));
            SpellCatalogue.Add(7, new Tuple<int, string, string, string, float, int, int>(7, AssetPath + "PreHardmode/GemStaff/RubyExplosiveShot", "Explosive Ruby Bolt", "Shoots a bolt of exploding mana", 9, 60, 60));
            //Diamond Staff
            SpellCatalogue.Add(8, new Tuple<int, string, string, string, float, int, int>(8, AssetPath + "PreHardmode/GemStaff/DiamondBolt", "Diamond Bolt", "Shoots a Bolt of mana", 8, 26, 26));
            SpellCatalogue.Add(9, new Tuple<int, string, string, string, float, int, int>(9, AssetPath + "PreHardmode/GemStaff/DiamondTurret", "Crystal Turret", "Shoots out a turret", 40, 80, 80));
            //Sapphire Staff
            SpellCatalogue.Add(10, new Tuple<int, string, string, string, float, int, int>(10, AssetPath + "PreHardmode/GemStaff/SapphireBolt", "Sapphire Bolt", "Shoots a Bolt of Mana", 6, 34, 34));
            SpellCatalogue.Add(11, new Tuple<int, string, string, string, float, int, int>(11, AssetPath + "PreHardmode/GemStaff/SapphireQuickShot", "Quick Sapphire Bolt", "Shoots a quick bolt of mana", 7, 34, 34));
            //Amber Staff
            SpellCatalogue.Add(12, new Tuple<int, string, string, string, float, int, int>(12, AssetPath + "PreHardmode/GemStaff/AmberBolt", "Amber Bolt", "Shoots a bolt of mana", 7, 28, 28));
            SpellCatalogue.Add(13, new Tuple<int, string, string, string, float, int, int>(13, AssetPath + "PreHardmode/GemStaff/AmberWall", "Amber Wall", "Shoots a bolt of mana that expands into a wall", 14, 40, 40));

            /// Evil Weapons
            //Golden Shower
            SpellCatalogue.Add(20, new Tuple<int, string, string, string, float, int, int>(20, AssetPath + "Hardmode/GoldenShower", "Golden Shower", "Shoots Sprays a shower of ichor, Decreases target's defense", 7, 6, 18));
            SpellCatalogue.Add(21, new Tuple<int, string, string, string, float, int, int>(21, AssetPath + "Hardmode/IchorBubble", "Ichor Bubble", "Shoots a big explosive bubble of boiling Ichor, Decreases target's defense and lights them on fire", 7, 40, 40));
            //Cursed Flames
            SpellCatalogue.Add(29, new Tuple<int, string, string, string, float, int, int>(29, AssetPath + "Hardmode/CursedFlame", "Cursed Flame", "Summons unholy fire balls", 9, 15, 15));
            SpellCatalogue.Add(30, new Tuple<int, string, string, string, float, int, int>(30, AssetPath + "Hardmode/CursedFlamethrower", "Cursed Flamethrower", "Summons a wall of cursed fire", 4, 4, 20));
            //Sky Fracture
            SpellCatalogue.Add(34, new Tuple<int, string, string, string, float, int, int>(34, AssetPath + "Hardmode/DanceOfBlades", "Dance of Blades", "Shoots magic blades", 1, 5, 5));
            SpellCatalogue.Add(35, new Tuple<int, string, string, string, float, int, int>(35, AssetPath + "Hardmode/SkyFracture", "Sky Fracture", "Summons magic spears from the sky", 1, 4, 4));
            SpellCatalogue.Add(36, new Tuple<int, string, string, string, float, int, int>(36, AssetPath + "Hardmode/AeonsEternity", "The Aeon's Eternity", "Summons a giant magic blade that follows your mouse", 1, 5, 5));

            /// Dungeon Weapons
            //Inferno Fork
            SpellCatalogue.Add(14, new Tuple<int, string, string, string, float, int, int>(14, AssetPath + "Hardmode/InfernoFork", "Inferno Fork", "Shoots an inferno fork that explodes into a lingering ball of fire", 18, 30, 30));
            SpellCatalogue.Add(15, new Tuple<int, string, string, string, float, int, int>(15, AssetPath + "Hardmode/InfernoFlamethrower", "Hellfire Breath", "Shoots a blast of flame", 16, 5, 20));
            SpellCatalogue.Add(16, new Tuple<int, string, string, string, float, int, int>(16, AssetPath + "Hardmode/InfernoWall", "Diabolical Firewall", "Summons a wall of fire", 24, 17, 51));
            //Water Bolt
            SpellCatalogue.Add(17, new Tuple<int, string, string, string, float, int, int>(17, AssetPath + "PreHardmode/SpellBooks/WaterBolt", "Water Bolt", "Shoots a bolt of bouncy water", 10, 17, 17));
            SpellCatalogue.Add(18, new Tuple<int, string, string, string, float, int, int>(18, AssetPath + "PreHardmode/SpellBooks/WaterGeyser", "Water Geyser", "Summons a water geyser", 16, 26, 26));
            SpellCatalogue.Add(19, new Tuple<int, string, string, string, float, int, int>(19, AssetPath + "PreHardmode/SpellBooks/WaterAura", "Aura Wave", "Shoots a wavee to splash enemies away", 12, 19, 19));
            //Book of Skull
            SpellCatalogue.Add(22, new Tuple<int, string, string, string, float, int, int>(22, AssetPath + "PreHardmode/SpellBooks/FlyingSkull", "Flaming Skull", "Shoots a flying flaming skull", 18, 26, 26));
            SpellCatalogue.Add(23, new Tuple<int, string, string, string, float, int, int>(23, AssetPath + "PreHardmode/SpellBooks/BoneFragmentStorm", "Bone Fragment Storm", "Shoots multiple bone fragments", 2, 5, 5));
            SpellCatalogue.Add(28, new Tuple<int, string, string, string, float, int, int>(28, AssetPath + "PreHardmode/SpellBooks/SkeletonHand", "Master's Hand", "Summons a skeletal hand that roasts all enemies in its vicinity", 40, 60, 60));
            //Aqua Scepter
            SpellCatalogue.Add(26, new Tuple<int, string, string, string, float, int, int>(26, AssetPath + "PreHardmode/WaterStream", "Water Stream", "Sprays out a shower of water", 7, 8, 16));
            SpellCatalogue.Add(27, new Tuple<int, string, string, string, float, int, int>(27, AssetPath + "PreHardmode/WaterHealing", "Water Treatment", "Shoots out a bubble of healing water", 12, 30, 30));
            //SpellCatalogue.Add(31, new Tuple<byte, string, string, string, float>(0, AssetPath + "PlaceholderSpellIcon", "TBA", "This Spell is not here yet! Tell Fred to push those changes already!!", 5, 1, 1));

            /// Other Weapons
            //Rainbow Gun
            SpellCatalogue.Add(24, new Tuple<int, string, string, string, float, int, int>(24, AssetPath + "Hardmode/PiercingRainbow", "Piercing Rainbow", "Shoots a rainbow that does continuous damage", 20, 40, 40));
            SpellCatalogue.Add(25, new Tuple<int, string, string, string, float, int, int>(25, AssetPath + "Hardmode/PrismRain", "Prism Rain", "Shoots a barrage of colored glass prisms", 4, 6, 6));

            /// Tempire
            //Majesty
            SpellCatalogue.Add(32, new Tuple<int, string, string, string, float, int, int>(32, AssetPath + "Tempire/FantasticalDoubleHelix", "Fantastical Double Helix", "Shoots a double helix bolt", 5, 22, 22));
            SpellCatalogue.Add(33, new Tuple<int, string, string, string, float, int, int>(33, AssetPath + "Tempire/GlitterBomb", "Glitter Bomb", "Shoots a bomb of glitter that explodes on contact", 20, 80, 80));

            /// Accessories
            SpellCatalogue.Add(31, new Tuple<int, string, string, string, float, int, int>(31, AssetPath + "Accessories/ManaBloom", "Mana Bloom", "Regenerates 10 mana but slows the player down", 0, 45, 45));

            base.OnWorldLoad();
        }
    }
}
