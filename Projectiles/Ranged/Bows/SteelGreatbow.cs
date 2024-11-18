using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Templates;
using Terraria;
using Terraria.DataStructures;

namespace Terrafirma.Projectiles.Ranged.Bows
{
    public class SteelGreatbow : DrawnBowTemplate
    {
        public override Vector2 TopString => new Vector2(-7,-24);
        public override Vector2 BottomString => new Vector2(-7,24);
        public override Color StringColor => Color.Gray;
        public override void Shoot(IEntitySource source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, float power)
        {
            base.Shoot(source, position, velocity, type, damage, knockback, power);
        }
    }
}
