using Microsoft.Xna.Framework;
using Terrafirma.Buffs.Debuffs.Throwing;
using Terrafirma.Data;
using Terrafirma.Items.Weapons.Ranged.Thrower;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Projectiles.Ranged.Throwing
{
    public class AcuteCanine : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileSets.CanBeReflected[Type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.QuickDefaults(size: 16);
        }
        public override void OnKill(int timeLeft)
        {
            Projectile.ThrowerSpawnDroppedItem(Projectile.owner, ModContent.ItemType<Items.Weapons.Ranged.Thrower.AcuteCanine>());
        }
        public override void AI()
        {
            Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Crimson, Projectile.velocity + Main.rand.NextVector2Circular(2,2));
            d.noGravity = true;
            d.scale *= 1.2f;
            d.alpha = Projectile.alpha;

            if (Projectile.ai[0] == 0)
            {
                Projectile.spriteDirection = Main.player[Projectile.owner].PlayerStats().TimesHeldWeaponHasBeenSwung % 2 == 0 ? 1 : -1;
            }
            Projectile.ai[0]++;
            if (Projectile.ai[0] > 60)
            {
                Projectile.rotation += Projectile.velocity.X * 0.05f * ((Projectile.ai[0] - 60) / 60);
                Projectile.velocity.X *= 0.98f;
                Projectile.velocity.Y += 0.3f;
                Projectile.alpha += 5;
            }
            else
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            }
            if(Projectile.alpha > 250)
            {
                Projectile.Kill();
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            return base.OnTileCollide(oldVelocity);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            TFUtils.EasyCenteredProjectileDraw(TextureAssets.Projectile[Type],Projectile,lightColor);
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuffThrower(ModContent.BuffType<ThrowerBleeding>(), 60 * 2, Main.player[Projectile.owner],hit,damageDone);
        }
    }
}
