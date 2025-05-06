using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Templates;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terrafirma.Items.Weapons.Ranged.Bows;

namespace Terrafirma.Projectiles.Ranged.Bows
{
    public class SteelGreatbowProj : DrawnBowTemplate
    {
        public override Vector2 TopString => new Vector2(3,8);
        public override Vector2 BottomString => new Vector2(3,54);
        public override Color StringColor => Color.Gray;
        public override int UseTime => 60;
        public override void Shoot(IEntitySource source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, float power)
        {
            base.Shoot(source, position, velocity, type, damage, knockback, power);
        } 
    }
}
