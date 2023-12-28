using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Global
{
    public class TerrafirmaGlobalPlayer : ModPlayer
    {
        public bool Foregrip = false;
        public bool DrumMag = false;
        public override void ResetEffects()
        {
            Foregrip = false;
            DrumMag = false;
        }
        public override bool CanConsumeAmmo(Item weapon, Item ammo)
        {
            if(ammo.ammo == AmmoID.Bullet && Main.rand.NextBool(2,3) && DrumMag)
            {
                return false;
            }
            return base.CanConsumeAmmo(weapon, ammo);
        }
    }
}
