using Microsoft.Xna.Framework;
using Terrafirma.Common.Templates.Melee;
using Terrafirma.Items.Materials;
using Terrafirma.Projectiles.Melee;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Melee
{
    public class TestSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToSword(45, 30, 4);
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<TestSwordProj>();
            Item.UseSound = new SoundStyle("Terrafirma/Sounds/SwordSound2") { PitchVariance = 0.3f, Pitch = -0.45f, MaxInstances = 10 };
            Item.shootSpeed = 8;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 0, 45, 0);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool MeleePrefix()
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            return false;
        }
    }
    public class TestSwordProj : MeleeSlice
    {
        public override string Texture => "Terrafirma/Items/Weapons/Melee/TestSword";
        public override Color DarkSlashColor => new Color(2, 0, 0, 0);
        public override Color LightSlashColor => new Color(255, 0, 0, 128);
        public override float slashSize => 1.2f;
    }
}
