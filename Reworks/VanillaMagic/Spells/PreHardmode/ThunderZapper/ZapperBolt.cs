using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.ThunderZapper
{
    internal class ZapperBolt : Spell
    {
        public override int UseAnimation => 17;
        public override int UseTime => 17;
        public override int ManaCost => 7;
        public override int ReuseDelay => 0;
        public override int[] SpellItem => new int[] { ItemID.ThunderStaff };

    }
}
