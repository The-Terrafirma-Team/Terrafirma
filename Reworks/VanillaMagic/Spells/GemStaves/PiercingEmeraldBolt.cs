using Microsoft.Xna.Framework;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.GemStaves
{
    internal class PiercingEmeraldBolt : Spell
    {
        public override int UseAnimation => 32;
        public override int UseTime => 32;
        public override int ManaCost => 8;
        public override string TexurePath => "Terrafirma/Systems/MageClass/SpellIcons/PreHardmode/GemStaff/EmeraldPenetrationShot";
        public override int[] SpellItem => new int[] { ItemID.EmeraldStaff };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<PiercingEmerald>();
            velocity *= 0.9f;

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }

    public class PiercingEmerald : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.EmeraldBolt}";
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.EmeraldBolt);
            AIType = ProjectileID.EmeraldBolt;
            Projectile.penetrate = 6;
            Projectile.tileCollide = false;
            Projectile.Size = new Vector2(16);
        }
    }

}
