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
using Terraria.ID;

namespace Terrafirma.Reworks.VanillaRanger.Bows.PreHardmode
{
    public class WoodenBowProj : DrawnBowTemplate
    {
        public override Vector2 TopString => new Vector2(4,4);
        public override Vector2 BottomString => new Vector2(4,28);
        public override Color StringColor => new Color(220, 220, 200);
        public override int UseTime => (int)(ContentSamples.ItemsByType[ItemID.WoodenBow].useTime * 2.5f);
        public override void Shoot(IEntitySource source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, float power)
        {
            Projectile p = Projectile.NewProjectileDirect(
                source, 
                position,
                Vector2.Lerp(velocity, velocity * 3, (float)Math.Pow(power, 3)), 
                type, 
                (int)MathHelper.Lerp(damage, damage * 3, (float)Math.Pow(power,3)), 
                knockback, 
                player.whoAmI);
            p.penetrate++;
        } 
    }
}
