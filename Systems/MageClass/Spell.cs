using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace Terrafirma.Systems.MageClass
{
    public abstract class Spell
    {
        public static string TexurePath;
        public static int ManaCost;
        public static float UseTimeMultiplier;
        public static float UseAnimationMultiplier;
        public string GetSpellName()
        {
            return Language.GetTextValue("Mods.Terrafirma.Spells.Name." + $"{this.GetType().Name}");
        }
        public string GetSpellDesc()
        {
            return Language.GetTextValue("Mods.Terrafirma.Spells.Desc." + $"{this.GetType().Name}");
        }
        public virtual void Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

        }
    }
}
