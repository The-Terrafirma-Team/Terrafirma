using Microsoft.Xna.Framework;
using Terrafirma.Projectiles.Summons;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.Hardmode
{
    internal class CursedFlamethrower : Spell
    {
        public override int UseAnimation => 20;
        public override int UseTime => 4;
        public override int ManaCost => 4;
        public override string TexurePath => "Terrafirma/Systems/MageClass/SpellIcons/Hardmode/CursedFlamethrower";
        public override int[] SpellItem => new int[] { ItemID.CursedFlames };

        public override void SetDefaults(Item entity)
        {
            entity.UseSound = null;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<CursedFlames>();
            velocity *= 0.8f;
            damage = (int)(damage * 0.5f);

            SoundEngine.PlaySound(SoundID.Item34, player.position);

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
}
