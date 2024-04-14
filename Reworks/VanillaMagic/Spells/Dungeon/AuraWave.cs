using Microsoft.Xna.Framework;
using System;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.Dungeon
{
    internal class AuraWave : Spell
    {
        public override int UseAnimation => 19;
        public override int UseTime => 19;
        public override int ManaCost => 12;
        public override int[] SpellItem => new int[] { ItemID.WaterBolt };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<AuraWaveProj>();
            position = Main.MouseWorld;
            velocity = Vector2.Normalize(velocity) * 0.01f;
            knockback *= 2f;

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
    internal class AuraWaveProj : ModProjectile
    {

        Vector2 playerpos = Vector2.Zero;
        public override void SetDefaults()
        {
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.Size = new Vector2(10, 10);
            Projectile.timeLeft = 400;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
            Projectile.ai[0]++;


            if (Projectile.ai[2] == 0)
            {
                for (int i = 0; i < 16; i++)
                {
                    Projectile newproj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Main.player[Projectile.owner].MountedCenter, new Vector2(3f, 0f).RotatedBy(Math.PI / 8f * i), Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 0, 1);
                    newproj.Opacity = 0.5f;
                    newproj.timeLeft = 60;
                }
                Projectile.Kill();

            }
            else
            {
                Projectile.Opacity = Projectile.timeLeft / 60f;
                Projectile.velocity = Projectile.velocity * 0.95f + ((Projectile.Center - playerpos) - (Projectile.Center - playerpos).RotatedBy(0.01f));
                Projectile.velocity *= 0.95f;
                Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;

                //Projectile.velocity = Projectile.velocity.RotatedBy(0.08f);


                if (Projectile.ai[0] % 2 == 0)
                {
                    Dust newdust = Dust.NewDustPerfect(Projectile.Center + new Vector2(5, 0).RotatedBy(Projectile.velocity.ToRotation()), DustID.DungeonWater, Vector2.Zero, Projectile.alpha, Color.White, 1);
                    newdust.noGravity = !Main.rand.NextBool(8);
                }
            }
        }

        public override void OnSpawn(IEntitySource source)
        {
            playerpos = Main.player[Projectile.owner].MountedCenter;
        }
    }
}
