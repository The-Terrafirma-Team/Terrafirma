using Microsoft.Xna.Framework;
using Terrafirma.Items.Materials;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Melee.Swords
{
    public class SteelGreatsword : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToSword(25, 30, 4);
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.SteelGreatsword>();
            Item.UseSound = new SoundStyle("Terrafirma/Sounds/SwordSound2") { PitchVariance = 0.3f, Pitch = -0.3f, MaxInstances = 10 };
            Item.shootSpeed = 8;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 0, 45, 0);
        }
        public override bool MeleePrefix()
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int mhm = player.PlayerStats().TimesHeldWeaponHasBeenSwung % 2 == 0 ? 1 : 0;
            Projectile.NewProjectile(source,position,velocity,type, damage, knockback,player.whoAmI, mhm);
            //if(mhm == 1)
                //Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SteelGreatswordSlash>(), (int)(damage * 0.5f), knockback, player.whoAmI);
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.Anvils).AddIngredient(ModContent.ItemType<SteelBar>(), 8).AddRecipeGroup(RecipeGroupID.Wood, 4).Register();
        }
    }
}
