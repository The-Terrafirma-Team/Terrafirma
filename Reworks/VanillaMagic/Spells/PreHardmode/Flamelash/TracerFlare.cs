using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Linq;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.Flamelash
{
    internal class TracerFlare : Spell
    {
        public override int UseAnimation => -1;
        public override int UseTime => -1;
        public override int ManaCost => -1;
        public override int ReuseDelay => -1;
        public override int[] SpellItem => new int[] { ItemID.Flamelash };

        public override bool Channeled => true;
        public override void SetDefaults(Item item)
        {
            item.autoReuse = false;
            item.useStyle = ItemUseStyleID.Swing;
        }
    }
}
