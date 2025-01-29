using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terrafirma.Buffs.Debuffs.Throwing;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.Hardmode.MagicDagger
{
    internal class DaggerSpread : Spell
    {
        public override int UseAnimation => 12;
        public override int UseTime => 12;
        public override int ManaCost => -1;
        public override int[] SpellItem => new int[] { ItemID.MagicDagger };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (float i = -2.5f; i < 2.5f; i++)
            {
                Projectile.NewProjectile(source, player.Center, velocity.RotatedBy(i * 0.3f), ModContent.ProjectileType<MagicDaggerSpread>(), (int)(damage * 0.8f), knockback, player.whoAmI, 0);
            }
            return false;
        }
    }

    public class MagicDaggerSpread : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.QuickDefaults(size: 10);
            Projectile.alpha = 100;
        }
        public override void AI()
        {
            if (Projectile.ai[0] % 4 == 0)
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 57, Projectile.velocity.X * 0.6f + Projectile.direction, Projectile.velocity.Y * 0.2f, 100);
                d.noGravity = true;
                d.fadeIn = 1f;
                d.velocity.X *= 0.3f;
                d.velocity.Y *= 0.3f;
            }

            if (Projectile.ai[0] == 0)
            {
                Projectile.spriteDirection = Main.player[Projectile.owner].PlayerStats().TimesHeldWeaponHasBeenSwung % 2 == 0 ? 1 : -1;
            }

            Projectile.ai[0]++;

            if (Projectile.ai[0] > 5)
            {
                Projectile.rotation += Projectile.spriteDirection * 0.4f;
                Projectile.velocity *= 0.95f;
                Projectile.alpha += 5;
            }
            else
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            }

            if (Projectile.alpha > 250)
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
            TFUtils.EasyCenteredProjectileDraw(TextureAssets.Projectile[Type], Projectile, new Color(lightColor.R, lightColor.G, lightColor.B, 0f));
            return false;
        }
    }

}
