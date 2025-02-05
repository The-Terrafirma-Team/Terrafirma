using Microsoft.Xna.Framework;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.Hardmode.InfernoSpork
{
    internal class InfernoWall : Spell
    {
        public override int UseAnimation => 51;
        public override int UseTime => 17;
        public override int ManaCost => 24;
        public override int[] SpellItem => new int[] { ItemID.InfernoFork };

        public override bool OverrideSoundstyle => true;
        public override SoundStyle? UseSound => null;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<Firewall>();
            velocity = Vector2.Normalize(velocity) * 0.01f;
            damage = (int)(damage * 0.5f);
            position = Main.MouseWorld;

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }

    public class Firewall : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.Size = new Vector2(16);
            Projectile.aiStyle = -1;
            Projectile.hide = true;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(1f, 1f, 1f, 0f) * Projectile.Opacity;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override void AI()
        {
            if (Projectile.ai[0] == 0)
                SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
            Projectile.ai[0]++;
            Player player = Main.player[Projectile.owner];

            if (player.ItemAnimationEndingOrEnded && Projectile.timeLeft > 60)
                Projectile.timeLeft = 60;
            Vector2 rand = Main.rand.NextVector2Circular(20, 20);
            Dust d = Dust.NewDustPerfect(Projectile.Center + rand, DustID.InfernoFork, -rand * 0.1f);
            d.noGravity = true;


        }

        public override void OnKill(int timeLeft)
        {
            Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ProjectileID.InfernoFriendlyBlast, Projectile.damage, Projectile.owner);
        }
    }
}
