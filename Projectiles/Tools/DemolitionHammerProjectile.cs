using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace Terrafirma.Projectiles.Tools
{
    public class DemolitionHammerProjectile: ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.MythrilDrill);

           
            Projectile.scale = 1f;

            Projectile.width = 30;
            Projectile.height = 30;
        }

        public override void AI()
        {
            Projectile.gfxOffY = Main.player[Projectile.owner].gfxOffY;
            DrawOriginOffsetX = -1 * Projectile.direction;
            DrawOriginOffsetY = Projectile.spriteDirection == 1? 5 : 12;

            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.MouseWorld.Distance(Main.projectile[i].Center) < 20f &&
                    Main.MouseWorld.Distance(Main.player[Projectile.owner].Center) < 16 * 10 &&
                    Main.projectile[i].active && 
                    Main.projectile[i].WipableTurret && 
                    Main.projectile[i].owner == Main.player[Projectile.owner].whoAmI)
                {
                    Main.projectile[i].Kill();
                    SoundEngine.PlaySound(SoundID.Tink, Projectile.Center);
                    for (int j = 0; j < 10; j++)
                    {
                        Dust dust = Dust.NewDustDirect(Main.MouseWorld, 4, 4, DustID.Stone, Main.rand.NextFloat(-1.5f, 1.5f), Main.rand.NextFloat(-2f, -0.5f), 0, Color.White, 1f);
                    }
                }
            }
        }
    }
}
