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
        public Dictionary<int, Tuple<byte, string, string, string> > SpellCatalogue = new Dictionary<int, Tuple<byte, string, string, string> >();
        const string AssetPath = "TerrafirmaRedux/Systems/MageClass/SpellIcons/";
        public override void OnModLoad()
        {
            ItemCatalogue.Add(ItemID.AmethystStaff, new [] { 0,1 });
            ItemCatalogue.Add(ItemID.SpectreStaff, new[] { 2, 2, 2, 2, 2 });
            ItemCatalogue.Add(ItemID.WandofFrosting, new[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, });

            SpellCatalogue.Add(0, new Tuple<byte, string, string, string>(0, AssetPath + "Pre-Hardmode/GemStaff/AmethystBolt", "Amethyst Bolt", "Shoots a Bolt of Mana") );
            SpellCatalogue.Add(1, new Tuple<byte, string, string, string>(1, AssetPath + "Pre-Hardmode/GemStaff/AmethystHomingShot", "Homing Amethyst Bolt", "Shoots a Bolt of Homing Mana") );
            SpellCatalogue.Add(2, new Tuple<byte, string, string, string>(0, AssetPath + "PlaceholderSpellIcon", "TBA", "This Spell is not here yet! Tell Fred to push those changes already!!"));
        }
    }
}
