using Terraria.ModLoader;
using Terrafirma.Common.Templates.Melee;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terrafirma.Common.Players;

namespace Terrafirma.Projectiles.Melee.Paladin
{
    public class EaterOfSkulls : PaladinHammer
    {
        public override void PostAI()
        {
            if (Main.rand.NextBool(5))
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center + new Vector2(Main.rand.NextFloat(46), Main.rand.NextFloat(-46,0)).RotatedBy(Projectile.rotation) * Projectile.scale, DustID.Corruption);
                d.noGravity = !Main.rand.NextBool(5);
                d.alpha = 128;
            }
        }
        public override int ThrownProjectile => ModContent.ProjectileType<EaterOfSkullsThrown>();
    }
    public class EaterOfSkullsThrown : PaladinHammerThrown
    {
        public override void PostAI()
        {
            if (Main.rand.NextBool(3))
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Corruption);
                d.noGravity = !Main.rand.NextBool(5);
                d.alpha = 128;
            }
        }
        public override string Texture => "Terrafirma/Projectiles/Melee/Paladin/EaterOfSkulls";
    }
}
