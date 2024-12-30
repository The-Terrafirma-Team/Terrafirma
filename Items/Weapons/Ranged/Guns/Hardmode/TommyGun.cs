using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terrafirma.Common;

namespace Terrafirma.Items.Weapons.Ranged.Guns.Hardmode
{
    internal class TommyGun : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 17;
            Item.knockBack = 2f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 5;
            Item.crit = 5;
            Item.useTime = 5;
            Item.width = 38;
            Item.height = 26;
            Item.UseSound = new SoundStyle("Terrafirma/Sounds/TommyGun") { PitchVariance = 0.1f, Pitch = 0.1f, MaxInstances = 3 };
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(0, 0, 20, 0);

            Item.useAmmo = AmmoID.Bullet;
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 16f;
            Item.scale = 0.85f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-14, -1);
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            PlayerAnimation.gunStyle(player, Main.rand.NextFloat(0.01f,0.02f), 2, 0);
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            Vector2 muzzleoff = new Vector2(Item.width - 20, -5 * player.direction).RotatedBy(Math.Atan2(velocity.Y, velocity.X));
            position = player.Center + muzzleoff;
            velocity = velocity.RotatedByRandom(0.05f);
        }
    }
}
