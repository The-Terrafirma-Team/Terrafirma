using Microsoft.Xna.Framework;
using Terrafirma.Common;
using Terrafirma.Projectiles.Misc;
using Terrafirma.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.OtherReworks
{
    internal class CloudBottleRework : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.CloudinaBottle || entity.type == ItemID.Bottle;
        }
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.CloudinaBottle)
            {
                item.useStyle = ItemUseStyleID.Swing;
                item.useTime = 10;
                item.useAnimation = 10;
            }
        }

        public override bool? UseItem(Item item, Player player)
        {
            if (item.type == ItemID.CloudinaBottle && player.itemAnimation == new Item(ItemID.CloudinaBottle).useAnimation)
            {
                for (int i = 0; i < 10; i++) Dust.NewDust(player.itemLocation, 4, 4 / 2, DustID.Cloud, player.Center.DirectionTo(Main.MouseWorld).X, player.Center.DirectionTo(Main.MouseWorld).Y);
                Projectile.NewProjectile(item.GetSource_FromThis(), player.itemLocation, player.Center.DirectionTo(Main.MouseWorld) * new Vector2(5f,8f), ModContent.ProjectileType<CloudInABottleProj>(), 0, 0);
                item.SetDefaults(ItemID.Bottle);
            }
            if (item.type == ItemID.Bottle && player.itemAnimation == new Item(ItemID.CloudinaBottle).useAnimation - 2)
            {
                for (int i = 0; i < SolidProjectiles.Projectiles.Count; i++)
                {
                    Projectile proj = SolidProjectiles.Projectiles[i].Projectile;
                    if (Collision.CheckAABBvAABBCollision(Main.MouseWorld, new Vector2(2,2), proj.position, new Vector2(proj.width, proj.height)) && player.Center.Distance(Main.MouseWorld) <= 128)
                    {
                        if (item.stack > 1) player.QuickSpawnItem(item.GetSource_FromThis(), item.type, item.stack - 1);
                        item.SetDefaults(ItemID.CloudinaBottle);
                        SolidProjectiles.Projectiles[i].Projectile.Kill();
                        break;
                    }
                }
            }

            return base.UseItem(item, player);
        }

        public override bool ConsumeItem(Item item, Player player)
        {
            return base.ConsumeItem(item, player);
        }
    }
}
