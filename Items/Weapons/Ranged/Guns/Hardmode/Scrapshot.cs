using Microsoft.Xna.Framework;
using Terrafirma.Global.Items;
using Terrafirma.Particles.LegacyParticles;
using Terrafirma.Projectiles.Ranged;
using Terrafirma.Systems.Elements;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Ranged.Guns.Hardmode
{
    internal class Scrapshot : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 90;
            Item.knockBack = 6f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 32;
            Item.useTime = 32;
            Item.width = 56;
            Item.height = 34;
            Item.UseSound = SoundID.Item61;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(0, 1, 90, 0);

            Item.shoot = ModContent.ProjectileType<HotCoal>();
            Item.shootSpeed = 24f;
            Item.scale = 0.9f;
            Item.GetElementItem().elementData.Fire = true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, -2);
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            UseStyles.gunStyle(player,0.05f,6,2);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode,player.position);
            position.Y += player.gravDir * -8;
            for (int i = 0; i < Main.rand.Next(3,6); i++)
            {
                type = !Main.rand.NextBool(3) ? ModContent.ProjectileType<HotCoal>() : ProjectileID.ExplosiveBullet;
                Projectile p = Projectile.NewProjectileDirect(source, position, velocity.RotatedByRandom(0.2f) * Main.rand.NextFloat(0.5f,1f), type, damage, knockback, player.whoAmI);
                if(type != ProjectileID.ExplosiveBullet)
                p.scale = Main.rand.NextFloat(0.5f, 1);
            }
            int[] Grenades = new int[] {ProjectileID.Grenade, ProjectileID.StickyGrenade,ProjectileID.BouncyGrenade,ProjectileID.ClusterGrenadeI, ProjectileID.GrenadeIII,ProjectileID.MiniNukeGrenadeI,ProjectileID.MolotovCocktail,ProjectileID.Stynger };
            if (Main.rand.NextBool(5))
            {
                Projectile.NewProjectileDirect(source, position, velocity * 0.8f, Grenades[Main.rand.Next(Grenades.Length)], damage * 3, knockback, player.whoAmI);
            }
            LegacyParticleSystem.AddParticle(new BigSparkle(), position + Vector2.Normalize(velocity) * 60,default,new Color(255,Main.rand.Next(150),24,0),1,10,10,1,Main.rand.NextFloat(-1,1));
            return false;
            //return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}
