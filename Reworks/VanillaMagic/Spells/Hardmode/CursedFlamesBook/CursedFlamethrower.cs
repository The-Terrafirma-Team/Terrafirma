using Microsoft.Xna.Framework;
using Terrafirma.Projectiles.Summon.Sentry.Hardmode;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.Hardmode.CursedFlamesBook
{
    internal class CursedFlamethrower : Spell
    {
        public override int UseAnimation => 20;
        public override int UseTime => 8;
        public override int ManaCost => 8;
        public override int[] SpellItem => new int[] { ItemID.CursedFlames };

        public override bool OverrideSoundstyle => true;
        public override SoundStyle? UseSound => SoundID.DD2_BetsyFireballShot;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<Projectiles.Summon.Sentry.Hardmode.CursedFlames>();
            velocity *= 1.5f;

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
}
