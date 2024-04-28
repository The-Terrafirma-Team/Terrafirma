using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Templates.Melee;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Melee.Swords
{
    public class TestSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToSword(25, 25, 4);
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<TestSwordBlade>();
        }
    }
}
public class TestSwordBlade : BroadswordSwing
{
    public override string Texture => "Terrafirma/Items/Weapons/Melee/Swords/TestSword";
}
