using Microsoft.Xna.Framework;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.GemStaves
{
    internal class DiamondTurret : Spell
    {
        public override int UseAnimation => 80;
        public override int UseTime => 80;
        public override int ManaCost => 40;
        public override string TexurePath => "Terrafirma/Systems/MageClass/SpellIcons/PreHardmode/GemStaff/DiamondTurret";
        public override int[] SpellItem => new int[] { ItemID.DiamondStaff };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<DiamondTurretProj>();

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<DiamondTurretProj>()] < 1) return player.ownedProjectileCounts[ModContent.ProjectileType<DiamondTurretProj>()] < 1;
            return false;
        }
    }


    public class DiamondTurretProj : ModProjectile
    {
        public override string Texture => $"Terrafirma/Reworks/VanillaMagic/Spells/GemStaves/DiamondTurret";
        public override void SetDefaults()
        {
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            Projectile.Size = new Vector2(16);
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override void AI()
        {
            Projectile.ai[0]++;
            Projectile.velocity *= 0.98f;
            if (Projectile.ai[0] % 90 == 0 && TFUtils.FindClosestNPC(600f, Projectile.position) != null)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.Center.DirectionTo(TFUtils.FindClosestNPC(600f, Projectile.position).Center) * 10f, ProjectileID.DiamondBolt, Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 0, 0);
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Tink, Projectile.Center);
            for (int i = 0; i < 15; i++)
            {
                Dust newdust = Dust.NewDustPerfect(Projectile.Center, DustID.GemDiamond, new Vector2(Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f)), 0, Color.White, Main.rand.NextFloat(1f, 1.3f));
                newdust.noGravity = true;
            }
        }
    }
}
