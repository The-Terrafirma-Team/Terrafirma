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
            //Dungeon Weapons
            ItemCatalogue.Add(ItemID.InfernoFork, new[] { 14, 15, 16 });
            ItemCatalogue.Add(ItemID.WaterBolt, new[] { 17, 18, 19 });
            //TEST
            ItemCatalogue.Add(ItemID.WandofFrosting, new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 });


            //// Spells
            /// Gem Staves
            //Amethyst Staff
            SpellCatalogue.Add(0, new Tuple<byte, string, string, string, float>(0, AssetPath + "Pre-Hardmode/GemStaff/AmethystBolt", "Amethyst Bolt", "Shoots a Bolt of Mana", 5) );
            SpellCatalogue.Add(1, new Tuple<byte, string, string, string, float>(1, AssetPath + "Pre-Hardmode/GemStaff/AmethystHomingShot", "Homing Amethyst Bolt", "Shoots a bolt of homing mana", 6) );
            //Topaz Staff
            SpellCatalogue.Add(2, new Tuple<byte, string, string, string, float>(2, AssetPath + "Pre-Hardmode/GemStaff/TopazBolt", "Topaz Bolt", "Shoots a Bolt of Mana", 5));
            SpellCatalogue.Add(3, new Tuple<byte, string, string, string, float>(3, AssetPath + "Pre-Hardmode/GemStaff/TopazSplitShot", "Splitting Topaz Bolt", "Shoots a bolt of splitting mana", 6));
            //Emerald Staff
            SpellCatalogue.Add(4, new Tuple<byte, string, string, string, float>(4, AssetPath + "Pre-Hardmode/GemStaff/EmeraldBolt", "Emerald Bolt", "Shoots a Bolt of Mana", 6));
            SpellCatalogue.Add(5, new Tuple<byte, string, string, string, float>(5, AssetPath + "Pre-Hardmode/GemStaff/EmeraldPenetrationShot", "Piercing Emerald Bolt", "Shoots a bolt of piercing mana", 8));
            //Ruby Staff
            SpellCatalogue.Add(6, new Tuple<byte, string, string, string, float>(6, AssetPath + "Pre-Hardmode/GemStaff/RubyBolt", "Ruby Bolt", "Shoots a Bolt of Mana", 7));
            SpellCatalogue.Add(7, new Tuple<byte, string, string, string, float>(7, AssetPath + "Pre-Hardmode/GemStaff/RubyExplosiveShot", "Explosive Ruby Bolt", "Shoots a bolt of exploding mana", 9));
            //Diamond Staff
            SpellCatalogue.Add(8, new Tuple<byte, string, string, string, float>(8, AssetPath + "Pre-Hardmode/GemStaff/DiamondBolt", "Diamond Bolt", "Shoots a Bolt of mana", 8));
            SpellCatalogue.Add(9, new Tuple<byte, string, string, string, float>(9, AssetPath + "Pre-Hardmode/GemStaff/DiamondTurret", "Crystal Turret", "Shoots out a turret", 40));
            //Sapphire Staff
            SpellCatalogue.Add(10, new Tuple<byte, string, string, string, float>(10, AssetPath + "Pre-Hardmode/GemStaff/SapphireBolt", "Sapphire Bolt", "Shoots a Bolt of Mana", 6));
            SpellCatalogue.Add(11, new Tuple<byte, string, string, string, float>(11, AssetPath + "Pre-Hardmode/GemStaff/SapphireQuickShot", "Quick Amethyst Bolt", "Shoots a quick bolt of mana", 7));
            //Amber Staff
            SpellCatalogue.Add(12, new Tuple<byte, string, string, string, float>(12, AssetPath + "Pre-Hardmode/GemStaff/AmberBolt", "Amber Bolt", "Shoots a bolt of mana", 7));
            SpellCatalogue.Add(13, new Tuple<byte, string, string, string, float>(13, AssetPath + "Pre-Hardmode/GemStaff/AmberWall", "Amber Wall", "Shoots a bolt of mana that expands into a wall", 14));
            
            /// Dungeon Weapons
            //Inferno Fork
            SpellCatalogue.Add(14, new Tuple<byte, string, string, string, float>(0, AssetPath + "Hardmode/InfernoFork", "Inferno Fork", "Shoots an inferno fork that explodes into a lingering ball of fire", 18));
            SpellCatalogue.Add(15, new Tuple<byte, string, string, string, float>(1, AssetPath + "Hardmode/InfernoFlamethrower", "Hellfire Breath", "Shoots a blast of flame", 16));
            SpellCatalogue.Add(16, new Tuple<byte, string, string, string, float>(2, AssetPath + "Hardmode/InfernoWall", "Diabolical Firewall", "Summons a wall of fire", 24));
            //Water Bolt
            SpellCatalogue.Add(17, new Tuple<byte, string, string, string, float>(3, AssetPath + "Pre-Hardmode/SpellBooks/WaterBolt", "Water Bolt", "Shoots a bolt of bouncy water", 10));
            SpellCatalogue.Add(18, new Tuple<byte, string, string, string, float>(4, AssetPath + "Pre-Hardmode/SpellBooks/WaterGeyser", "Water Geyser", "Summons a water geyser", 16));
            SpellCatalogue.Add(19, new Tuple<byte, string, string, string, float>(5, AssetPath + "Pre-Hardmode/SpellBooks/WaterAura", "Aura Wave", "Shoots a wavee to splash enemies away", 12));
            //SpellCatalogue.Add(14, new Tuple<byte, string, string, string, string>(0, AssetPath + "PlaceholderSpellIcon", "TBA", "This Spell is not here yet! Tell Fred to push those changes already!!", "Uses 5 Mana"));
        }
    }
}
