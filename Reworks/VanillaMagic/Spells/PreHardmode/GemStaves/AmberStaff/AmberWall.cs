using Microsoft.Xna.Framework;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.GemStaves.AmberStaff
{
    internal class AmberWall : Spell
    {
        public override int UseAnimation => 40;
        public override int UseTime => 40;
        public override int ManaCost => 14;
        public override int[] SpellItem => new int[] { ItemID.AmberStaff };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<AmberWallCrystal>();

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<AmberWallProj>()] < 4 && player.ownedProjectileCounts[ModContent.ProjectileType<AmberWallCrystal>()] < 1) return true;
            return false;
        }
    }

    public class AmberWallCrystal : ModProjectile
    {
        public override string Texture => $"Terrafirma/Reworks/VanillaMagic/Spells/GemStaves/AmberCrystal";
        public override void SetDefaults()
        {
            Projectile.tileCollide = false;
            Projectile.Size = new Vector2(16);
            Projectile.timeLeft = 90;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
        }

        public override void AI()
        {
            Projectile.ai[0]++;
            Projectile.velocity *= 0.98f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if (Projectile.ai[0] % 2 == 0)
            {
                Dust newdust = Dust.NewDustPerfect(Projectile.Center, DustID.AmberBolt, Vector2.Zero, 0, default, Main.rand.NextFloat(0.9f, 1.4f));
                newdust.noGravity = true;
            }


        }

        public override void OnKill(int timeLeft)
        {
            Vector2 length = Main.player[Projectile.owner].MountedCenter - Projectile.Center;
            for (int i = -3; i < 4; i++)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Main.player[Projectile.owner].MountedCenter - length.RotatedBy(0.05f * i), Vector2.Zero, ModContent.ProjectileType<AmberWallProj>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 0, 1);
            }
            SoundEngine.PlaySound(SoundID.Tink, Projectile.Center);
        }
    }
    public class AmberWallProj : ModProjectile
    {
        float randfall = 0f;
        public override void SetDefaults()
        {
            Projectile.penetrate = 12;
            Projectile.tileCollide = false;
            DrawOffsetX = -5;
            DrawOriginOffsetY = -5;
            Projectile.Size = new Vector2(16);
            Projectile.timeLeft = 400;
            Projectile.friendly = true;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 40;
        }

        public override void OnSpawn(IEntitySource source)
        {
            randfall = Main.rand.NextFloat(0.05f, 0.15f);
            for (int j = 0; j < 5; j++)
            {
                Dust newdust = Dust.NewDustPerfect(Projectile.Center, DustID.GemAmber, new Vector2(Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f)), 0, Color.White, Main.rand.NextFloat(1f, 1.3f));
                newdust.noGravity = true;
            }

            Projectile.scale = Main.rand.NextFloat(0.7f, 1.3f);
        }

        public override void AI()
        {
            if (Projectile.timeLeft < 30)
            {
                Projectile.velocity.Y += randfall;
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Tink, Projectile.Center);
            for (int j = 0; j < 5; j++)
            {
                Dust newdust = Dust.NewDustPerfect(Projectile.Center, DustID.GemAmber, new Vector2(Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f)), 0, Color.White, Main.rand.NextFloat(1f, 1.3f));
                newdust.noGravity = true;
            }
        }
    }

}
