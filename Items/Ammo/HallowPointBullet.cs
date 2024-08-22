using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terrafirma.Projectiles.Ranged.Bullets;
using Terrafirma.Common.Interfaces;
using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Terrafirma.Items.Ammo
{
    internal class HallowPointBullet : ModItem, ITFBullet
    {
        Asset<Texture2D> ITFBullet.casingTexture { get => ModContent.Request<Texture2D>("Terrafirma/Assets/BulletCasings/HallowPointBulletCasing"); }
        public override void SetDefaults()
        {
            Item.damage = 9;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 10;
            Item.height = 16;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.knockBack = 3f;
            Item.value = Item.sellPrice(0, 0, 0, 40);
            Item.rare = ItemRarityID.Pink;
            Item.shoot = ModContent.ProjectileType<HallowPointBulletProjectile>();
            Item.shootSpeed = 2f;
            Item.ammo = AmmoID.Bullet;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

        public override void AddRecipes()
        {
            CreateRecipe(100)
            .AddIngredient(ItemID.HallowedBar, 1)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }

    }
}
