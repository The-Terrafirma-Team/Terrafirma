using Microsoft.Xna.Framework;
using Terrafirma.Projectiles.Melee;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Melee.Swords
{
    public class Fireaxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToSword(30, 35, 4);
            Item.useTime = 60;
            Item.shoot = ModContent.ProjectileType<FireaxeBlob>();
            Item.shootSpeed = 10;
            Item.rare = ItemRarityID.Yellow;
            Item.scale = 1.5f;
            Item.GetElementItem().elementData.Fire = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for(int i = 0; i < 3; i++)
            {
                Projectile.NewProjectile(source,position, velocity.RotatedByRandom(0.2f) * Main.rand.NextFloat(0.8f,1f) + new Vector2(0,-3), type, damage, knockback,player.whoAmI);
            }
            return false;
        }
    }
}
