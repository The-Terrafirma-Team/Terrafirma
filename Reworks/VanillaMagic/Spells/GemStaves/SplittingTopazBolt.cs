using Microsoft.Xna.Framework;
using System;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.GemStaves
{
    internal class SplittingTopazBolt : Spell
    {
        public override int UseAnimation => 43;
        public override int UseTime => 43;
        public override int ManaCost => 6;
        public override string TexurePath => "Terrafirma/Systems/MageClass/SpellIcons/PreHardmode/GemStaff/TopazSplitShot";
        public override int[] SpellItem => new int[] { ItemID.TopazStaff };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<SplittingTopaz>();
            damage = (int)(damage * 0.8f);

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
    public class SplittingTopaz : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.TopazBolt}";
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.TopazBolt);
            AIType = ProjectileID.TopazBolt;
            Projectile.ai[2] = 0;
            Projectile.Size = new Vector2(16);
        }

        public override void AI()
        {
            Projectile.ai[0]++;

            if (Projectile.ai[0] > 20 && Projectile.ai[2] == 0)
            {
                for (int i = -1; i < 2; i++)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity.RotatedBy(20 * (Math.PI / 180) * i), Projectile.type, (int)(Projectile.damage * 0.4f), Projectile.knockBack, Projectile.owner, 0, 0, 2);

                }
                Projectile.Kill();
            }
        }
    }
}
