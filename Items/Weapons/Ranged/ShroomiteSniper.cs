using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrafirmaRedux.Items.Weapons.Ranged
{
    internal class ShroomiteSniper: ModItem
    {

        public override void SetDefaults()
        {
            Item.damage = 195;
            Item.knockBack = 8f;
            Item.crit = 26;

            Item.width = 52;
            Item.height = 28;

            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.autoReuse = true;

            Item.UseSound = SoundID.Item40;

            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;
            Item.shootSpeed = 15;

            Item.rare = ItemRarityID.Yellow;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.DamageType = DamageClass.Ranged;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12, 0);
        }

        public override void HoldItem(Player player)
        {
            player.scope = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.ShroomiteBar, 20)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
