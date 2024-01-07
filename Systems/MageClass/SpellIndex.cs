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
        public Dictionary<int, Tuple<int, string> > SpellCatalogue = new Dictionary<int, Tuple<int, string> >();
        public Dictionary<int, Tuple<string, string> > SpellDescription = new Dictionary<int, Tuple<string, string> >();

        public override void OnModLoad()
        {
            ItemCatalogue.Add(ItemID.AmethystStaff, new [] { 0,1 });
            ItemCatalogue.Add(ItemID.SpectreStaff, new[] { 2, 2, 2, 2, 2 });
            ItemCatalogue.Add(ItemID.WandofFrosting, new[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, });

            SpellCatalogue.Add(0, new Tuple<int, string>(ProjectileID.AmethystBolt, "TerrafirmaRedux/Systems/MageClass/SpellIcons/Pre-Hardmode/GemStaff/AmethystBolt") );
            SpellCatalogue.Add(1, new Tuple<int, string>(ProjectileID.BeeArrow, "TerrafirmaRedux/Systems/MageClass/SpellIcons/Pre-Hardmode/GemStaff/AmethystHomingShot") );
            SpellCatalogue.Add(2, new Tuple<int, string>(ProjectileID.LunarFlare, "TerrafirmaRedux/Systems/MageClass/SpellIcons/PlaceholderSpellIcon"));

            SpellDescription.Add(0, new Tuple<string, string>( "Amethyst Bolt", "Shoots a Bolt of Mana" ));
            SpellDescription.Add(1, new Tuple<string, string>( "Homing Amethyst Bolt", "Shoots a Bolt of Homing Mana"));
            SpellDescription.Add(2, new Tuple<string, string>("TBA", "This Spell is not here yet! Tell Fred to push those changes already!!"));
        }


    }
}
