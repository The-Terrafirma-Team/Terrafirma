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
        public Dictionary<int, Tuple<byte, string, string, string, string> > SpellCatalogue = new Dictionary<int, Tuple<byte, string, string, string, string> >();
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
            //TEST
            ItemCatalogue.Add(ItemID.WandofFrosting, new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 });


            //// Spells
            //Amethyst Staff
            SpellCatalogue.Add(0, new Tuple<byte, string, string, string, string>(0, AssetPath + "Pre-Hardmode/GemStaff/AmethystBolt", "Amethyst Bolt", "Shoots a Bolt of Mana", "Uses 5 Mana") );
            SpellCatalogue.Add(1, new Tuple<byte, string, string, string, string>(1, AssetPath + "Pre-Hardmode/GemStaff/AmethystHomingShot", "Homing Amethyst Bolt", "Shoots a Bolt of Homing Mana", "Uses 6 Mana") );
            //Topaz Staff
            SpellCatalogue.Add(2, new Tuple<byte, string, string, string, string>(2, AssetPath + "Pre-Hardmode/GemStaff/TopazBolt", "Topaz Bolt", "Shoots a Bolt of Mana", "Uses 5 Mana"));
            SpellCatalogue.Add(3, new Tuple<byte, string, string, string, string>(3, AssetPath + "Pre-Hardmode/GemStaff/TopazSplitShot", "Splitting Topaz Bolt", "Shoots a Bolt of Splitting Mana", "Uses 6 Mana"));
            //Emerald Staff
            SpellCatalogue.Add(4, new Tuple<byte, string, string, string, string>(4, AssetPath + "Pre-Hardmode/GemStaff/EmeraldBolt", "Emerald Bolt", "Shoots a Bolt of Mana", "Uses 6 Mana"));
            SpellCatalogue.Add(5, new Tuple<byte, string, string, string, string>(5, AssetPath + "Pre-Hardmode/GemStaff/EmeraldPenetrationShot", "Piercing Emerald Bolt", "Shoots a Bolt of Piercing Mana", "Uses 8 Mana"));
            //Ruby Staff
            SpellCatalogue.Add(6, new Tuple<byte, string, string, string, string>(6, AssetPath + "Pre-Hardmode/GemStaff/RubyBolt", "Ruby Bolt", "Shoots a Bolt of Mana", "Uses 7 Mana"));
            SpellCatalogue.Add(7, new Tuple<byte, string, string, string, string>(7, AssetPath + "Pre-Hardmode/GemStaff/RubyExplosiveShot", "Explosive Ruby Bolt", "Shoots a Bolt of Exploding Mana", "Uses 9 Mana"));
            //Diamond Staff
            SpellCatalogue.Add(8, new Tuple<byte, string, string, string, string>(8, AssetPath + "Pre-Hardmode/GemStaff/AmethystBolt", "Diamond Bolt", "Shoots a Bolt of Mana", "Uses 5 Mana"));
            SpellCatalogue.Add(9, new Tuple<byte, string, string, string, string>(9, AssetPath + "Pre-Hardmode/GemStaff/AmethystHomingShot", "Homing Amethyst Bolt", "Shoots a Bolt of Homing Mana", "Uses 6 Mana"));
            //Sapphire Staff
            SpellCatalogue.Add(10, new Tuple<byte, string, string, string, string>(10, AssetPath + "Pre-Hardmode/GemStaff/SapphireBolt", "Sapphire Bolt", "Shoots a Bolt of Mana", "Uses 6 Mana"));
            SpellCatalogue.Add(11, new Tuple<byte, string, string, string, string>(11, AssetPath + "Pre-Hardmode/GemStaff/SapphireQuickShot", "Quick Amethyst Bolt", "Shoots a quick Bolt of Mana", "Uses 7 Mana"));
            //Amber Staff
            SpellCatalogue.Add(12, new Tuple<byte, string, string, string, string>(12, AssetPath + "Pre-Hardmode/GemStaff/AmethystBolt", "Amber Bolt", "Shoots a Bolt of Mana", "Uses 7 Mana"));
            SpellCatalogue.Add(13, new Tuple<byte, string, string, string, string>(13, AssetPath + "Pre-Hardmode/GemStaff/AmethystHomingShot", "Homing Amethyst Bolt", "Shoots a Bolt of Homing Mana", "Uses 6 Mana"));

            //TEST
            SpellCatalogue.Add(14, new Tuple<byte, string, string, string, string>(0, AssetPath + "PlaceholderSpellIcon", "TBA", "This Spell is not here yet! Tell Fred to push those changes already!!", "Uses 5 Mana"));
        }
    }
}
