using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terrafirma.Common;
using Terraria.Audio;
using Terrafirma.Projectiles.Ranged;
using Terrafirma.Items.Materials;

namespace Terrafirma.Items.Weapons.Ranged.Guns.PreHardmode
{
    public class TheBlunderbuss : ModItem
    {
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.Anvils).AddIngredient(ItemID.Boomstick).AddIngredient(ModContent.ItemType<SteelBar>(), 8).Register();
        }
        public override void SetDefaults()
        {
            Item.DefaultToRangedWeapon(10, AmmoID.Bullet, 50, 8, true);
            Item.value = Item.sellPrice(0, 0, 75, 0);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = new SoundStyle("Terrafirma/Sounds/Shotgun") { PitchVariance = 0.2f };
            Item.damage = 20;
            Item.scale = 0.8f;
            Item.knockBack = 2;
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            PlayerAnimation.gunStyle(player, 0.1f, 4f, 2f);
            if (player.itemAnimation == player.itemAnimationMax - 1)
            {
                for(int i = 0; i < 5; i++)
                {
                    Dust d = Dust.NewDustPerfect(player.itemLocation + new Vector2(-35 * Math.Sign(player.Center.X - Main.MouseWorld.X),4).RotatedBy(player.itemRotation),DustID.Torch,Main.rand.NextVector2Circular(3,3));
                    d.noGravity = true;
                    d.scale *= 2;
                    d.velocity += player.MountedCenter.DirectionTo(Main.MouseWorld) * 6;
                }
            }
            if (Main.rand.NextBool(6))
            {
                Dust d2 = Dust.NewDustPerfect(player.itemLocation + new Vector2(40 * player.direction, 8).RotatedBy(player.itemRotation), DustID.Smoke, new Vector2(player.direction * 1,-2) + Main.rand.NextVector2CircularEdge(1,1));
                d2.alpha = 200;
                d2.velocity *= 0.4f;
            }
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for(int i = 0; i < 4; i++)
            {
                Projectile.NewProjectile(source,position,velocity.RotatedByRandom(0.2f) * Main.rand.NextFloat(0.8f,1f),type,damage,knockback,player.whoAmI);
            }
            Projectile.NewProjectile(source, position, velocity * 1.5f, ModContent.ProjectileType<Blunderball>(), damage, knockback * 5, player.whoAmI);
            player.velocity -= velocity * new Vector2(0.3f,0.7f);

            return false;
        }
    }
}
