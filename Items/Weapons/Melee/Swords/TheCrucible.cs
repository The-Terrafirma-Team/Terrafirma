using Microsoft.Xna.Framework;
using System;
using Terrafirma.Particles;
using Terrafirma.Projectiles.Melee;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Melee.Swords
{
    public class TheCrucible : ModItem
    {
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void SetDefaults()
        {
            Item.DefaultToSword(85, 45,8);
            Item.useTime *= 2;
            Item.shoot = ModContent.ProjectileType<CrucibleBeam>();
            Item.shootSpeed = 10;
            Item.rare = ItemRarityID.Yellow;
        }
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for(int i = 0; i < 5; i++)
                Projectile.NewProjectile(source, position, velocity.RotatedByRandom(0.2f) * Main.rand.NextFloat(0.8f,1.2f), type, (damage / 3) * 2, knockback / 2, player.whoAmI,Main.rand.NextFloat(-20,40));
            return false;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (player.ItemAnimationJustStarted)
            {
                SoundEngine.PlaySound(SoundID.Item20, player.position);
            }

            TFUtils.GetPointOnSwungItemPath(72, 72, 0.2f + 0.8f * Main.rand.NextFloat(), Item.scale, out var location2, out var outwardDirection2, player);
            Vector2 vector2 = outwardDirection2.RotatedBy((float)Math.PI / 2f * (float)player.direction * player.gravDir);
            ParticleSystem.AddParticle(new HiResFlame(),
                player.Center + vector2.RotatedBy(player.direction * -MathHelper.PiOver2 * Main.rand.NextFloat(0.8f, 1.2f)) * Main.rand.Next(24, 90),
                new Vector2(player.velocity.X * 0.2f + (float)(player.direction * 3), player.velocity.Y * 0.2f) + vector2 * Main.rand.NextFloat(2, 5),
                TFUtils.getAgnomalumFlameColor() * Main.rand.NextFloat(0.8f, 1f), 3);
            ParticleSystem.AddParticle(new HiResFlame(),
                player.Center + vector2.RotatedBy(player.direction * -MathHelper.PiOver2 * Main.rand.NextFloat(0.8f, 1.2f)) * Main.rand.Next(24, 90),
                new Vector2(player.velocity.X * 0.2f + (float)(player.direction * 3), player.velocity.Y * 0.2f) + vector2 * Main.rand.NextFloat(2, 5),
                TFUtils.getAgnomalumFlameColor() * Main.rand.NextFloat(0.8f, 1f), 4);
            if (Main.rand.NextBool(5))
            {
                ParticleSystem.AddParticle(new ColorDot(),
                    player.Center + vector2.RotatedBy(player.direction * -MathHelper.PiOver2 * Main.rand.NextFloat(0.8f, 1.2f)) * Main.rand.Next(24, 90),
                    new Vector2(player.velocity.X * 0.2f + (float)(player.direction * 3), player.velocity.Y * 0.2f) + vector2 * Main.rand.NextFloat(2, 5),
                    TFUtils.getAgnomalumFlameColor() * Main.rand.NextFloat(0.8f, 1f), 0.2f);
            }
            //int num15 = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.GemDiamond, player.velocity.X * 0.2f + (float)(player.direction * 3), player.velocity.Y * 0.2f, 140, default(Color), 0.7f);
        }
    }
}
