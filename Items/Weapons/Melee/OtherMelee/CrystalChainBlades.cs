using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terrafirma.Projectiles.Melee.Other;

namespace Terrafirma.Items.Weapons.Melee.OtherMelee
{
    internal class CrystalChainBlades : ModItem
    {
        int ShootDirection = 0;
        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.knockBack = 4f;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 14;
            Item.useTime = 14;
            Item.crit = 5;
            Item.width = 44;
            Item.height = 44;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;

            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(0, 5, 0, 0);

            Item.shoot = ModContent.ProjectileType<CrystalChainBladesProjectile>();
            Item.shootSpeed = 20f;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override bool MeleePrefix()
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            // Ensures no more than one spear can be thrown out, use this when using autoReuse
            return player.ownedProjectileCounts[Item.shoot] < 2;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            ShootDirection++;
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, ShootDirection % 2);
            return false;
        }
    }
}
