using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Projectiles.Ranged;
using Terrafirma.Systems.Elements;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Ranged.Guns.PreHardmode
{
    internal class PumpCalamari : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 9;
            Item.knockBack = 4f;

            Item.width = 60;
            Item.height = 24;

            Item.autoReuse = true;
            Item.useTime = 36;
            Item.useAnimation = 36;

            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;
            Item.shootSpeed = 6f;
            Item.UseSound = SoundID.Item36;
            Item.value = Item.sellPrice(gold: 1);

            Item.rare = ItemRarityID.Green;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.DamageType = DamageClass.Ranged;


        }
        public override void SetStaticDefaults()
        {
            AddElementsToVanillaContent.waterItem.Add(Type);
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-15, -4);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (type == ProjectileID.Bullet)
            {
                type = ModContent.ProjectileType<InkProjectile>();
                SoundEngine.PlaySound(SoundID.SplashWeak, position);
            }

            for (int i = 0; i < 4; i++)
            {
                Projectile.NewProjectileDirect(player.GetSource_FromThis(), position, velocity.RotatedByRandom(0.3f) * Main.rand.NextFloat(0.95f,1.05f), type, damage, knockback, player.whoAmI, 0, 0, 0);
                if (type == ModContent.ProjectileType<InkProjectile>()) Dust.NewDustPerfect(position + Vector2.Normalize(velocity) * 40, DustID.Poop, Vector2.Normalize(velocity) * 4f + Main.rand.NextVector2Circular(1f, 1f), 1, Color.Black, 1f);
            }
            return false;
        }
    }
}
