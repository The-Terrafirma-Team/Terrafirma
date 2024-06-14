using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common.Items
{
    internal class PrefixChanges : ModSystem
    {
        public override void Load()
        {
            On_Item.TryGetPrefixStatMultipliersForItem += On_Item_TryGetPrefixStatMultipliersForItem;
        }

        private bool On_Item_TryGetPrefixStatMultipliersForItem(On_Item.orig_TryGetPrefixStatMultipliersForItem orig, Item self, int rolledPrefix, out float dmg, out float kb, out float spd, out float size, out float shtspd, out float mcst, out int crt)
        {
            orig(self,rolledPrefix, out dmg, out kb, out spd, out size, out shtspd, out mcst, out crt);

            switch (rolledPrefix)
            {
                case PrefixID.Hurtful:
                    dmg = 1.2f;
                    crt = -4;
                    break;

                case PrefixID.Massive:
                    size = 1.5f;
                    break;
                case PrefixID.Large:
                    size = 1.2f;
                    break;
                case PrefixID.Heavy:
                    kb = 1.75f;
                    break;
                case PrefixID.Forceful:
                    kb = 1.45f;
                    break;
                case PrefixID.Savage:
                    dmg = 1.25f;
                    spd = 1.1f;
                    size = 1.23f;
                    crt = -4;
                    break;

                case PrefixID.Keen:
                    crt = 10;
                    break;
                case PrefixID.Frenzying:
                    spd = 0.63f;
                    dmg = 0.66f;
                    break;
            }

            return true;
        }
    }
}
