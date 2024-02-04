using Microsoft.Xna.Framework;
using Terrafirma.Projectiles.Ranged.Bullets;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Ranged.Guns.Hardmode
{
    internal class TheFurnace : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 65;
            Item.crit = 20;
            Item.knockBack = 6f;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 8;
            Item.useTime = 8;
            Item.width = 56;
            Item.height = 34;
            Item.UseSound = SoundID.Item34;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(0, 1, 90, 0);

            Item.useAmmo = AmmoID.Bullet;
            Item.shoot = ModContent.ProjectileType<MoltenBulletProjectile>();
            Item.shootSpeed = 4f;

            Item.scale = 0.85f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-18, -10);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust newdust = Dust.NewDustDirect(position, 2, 2, DustID.Torch, velocity.X * Main.rand.NextFloat(0.1f,1.5f), velocity.Y * Main.rand.NextFloat(0.1f, 0.5f), 1, default, 1.5f);
            }

            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(0.05f);
            type = ModContent.ProjectileType<MoltenBulletProjectile>();
            position += new Vector2(40, (-5 * player.direction) * Main.rand.Next(1,4)).RotatedBy(velocity.ToRotation());
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
    }
}
