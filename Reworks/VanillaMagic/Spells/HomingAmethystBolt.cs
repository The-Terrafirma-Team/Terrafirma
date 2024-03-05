using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Reworks.VanillaMagic.Projectiles;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells
{
    internal class HomingAmethystBolt : Spell
    {
        public override int UseAnimation => 43;
        public override int UseTime => 43;
        public override int ManaCost => 6;
        public override string TexurePath => "Terrafirma/Systems/MageClass/SpellIcons/PreHardmode/GemStaff/AmethystHomingShot";
        public override int[] SpellItem => new int[] { ItemID.AmethystStaff };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<HomingAmethyst>();
            damage = (int)(damage * 0.8f);

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }

    public class HomingAmethyst : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.AmethystBolt}";
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.AmethystBolt);
            AIType = ProjectileID.AmethystBolt;
            Projectile.Size = new Vector2(16);
        }
        public override void AI()
        {
            NPC target = TFUtils.FindClosestNPC(200, Projectile.Center);
            if (target != null && target.active)
            {
                Projectile.velocity = Vector2.Lerp(Projectile.velocity += Projectile.Center.DirectionTo(target.Center) * 0.1f, Projectile.Center.DirectionTo(target.Center) * Projectile.velocity.Length(), 0.1f);
            }
            Projectile.velocity = Projectile.velocity.LengthClamp(5);
        }
    }
}
