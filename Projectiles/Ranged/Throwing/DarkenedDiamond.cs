using Microsoft.Xna.Framework;
using Terrafirma.Buffs.Debuffs.Throwing;
using Terrafirma.Data;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Ranged.Throwing
{
    public class DarkenedDiamond : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileSets.CanBeReflected[Type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.QuickDefaults(size: 16);
        }
        public override void AI()
        {
            Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(8, 8), DustID.Corruption, Projectile.velocity * 0.2f);
            d.noGravity = true;
            d.alpha = 128;
            d.fadeIn = 1.3f;

            Projectile.rotation += Projectile.velocity.X * 0.06f;
            Projectile.ai[0]++;
            if (Projectile.ai[0] > 30)
            {
                Projectile.velocity.X *= 0.98f;
                Projectile.velocity.Y += 0.3f;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            return base.OnTileCollide(oldVelocity);
        }
        public override void OnKill(int timeLeft)
        {
            Projectile.ThrowerSpawnDroppedItem(Projectile.owner, ModContent.ItemType<Items.Weapons.Ranged.Thrower.DarkenedDiamond>());
        }
        public override bool PreDraw(ref Color lightColor)
        {
            TFUtils.EasyCenteredProjectileDraw(TextureAssets.Projectile[Type],Projectile,lightColor);
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuffThrower(ModContent.BuffType<Corrupting>(), 120, Main.player[Projectile.owner],hit,damageDone);
        }
    }
}
