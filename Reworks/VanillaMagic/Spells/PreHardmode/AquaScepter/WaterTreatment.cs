using Microsoft.Xna.Framework;
using System;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.AquaScepter
{
    internal class WaterTreatment : Spell
    {
        public override int UseAnimation => 30;
        public override int UseTime => 30;
        public override int ManaCost => 12;
        public override int[] SpellItem => new int[] { ItemID.AquaScepter };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<HealingBubble>();
            damage = (int)(damage * 0.1f);

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
    public class HealingBubble : ModProjectile
    {       
        public override void SetDefaults()
        {
            Projectile.penetrate = 1;
            Projectile.Size = new Vector2(22, 22);
            Projectile.timeLeft = 200;
            Projectile.friendly = false;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.velocity.Y *= 0.2f;
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            Projectile.velocity.Y -= 0.05f;
            Projectile.velocity.X *= 0.98f;

            if (Main.rand.NextBool())
            {
                Dust newdust = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(12,12), Main.rand.NextBool(3) ? DustID.GreenTorch : DustID.DungeonWater, Vector2.Zero, 0, Color.White, 1f);
                newdust.noGravity = true;
                newdust.noLightEmittence = true;
            }
            Lighting.AddLight(Projectile.Center, new Vector3(0.4f, 0.5f, 0.6f));

            Projectile.rotation = Projectile.velocity.X * 0.1f;
            Projectile.scale = 0.9f + MathF.Sin(Projectile.timeLeft * 0.1f) * 0.1f;

            for (int i = 0; i < Main.player.Length; i++)
            {
                if (Projectile.Hitbox.Intersects(Main.player[i].Hitbox) &&
                    Main.player[i].team == Main.player[Projectile.owner].team &&
                    Main.player[i] != Main.player[Projectile.owner])
                {
                    Main.player[i].Heal(8);
                    Projectile.Kill();
                }

            }


        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust newdust = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(10f, 10f), DustID.DungeonWater, Main.rand.NextVector2Circular(5f, 5f), 0, Color.White, 1.25f);
            }
        }

    }
}
