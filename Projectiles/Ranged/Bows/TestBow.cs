using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Templates;
using Terraria;

namespace Terrafirma.Projectiles.Ranged.Bows
{
    public class TestBow : DrawnBowTemplate
    {
        public override Vector2 TopString => new Vector2(-7,-14);
        public override Vector2 BottomString => new Vector2(-7,14);
        public override Color StringColor => Color.Lerp(Color.Red,Color.Blue,Main.masterColor);
    }
}
