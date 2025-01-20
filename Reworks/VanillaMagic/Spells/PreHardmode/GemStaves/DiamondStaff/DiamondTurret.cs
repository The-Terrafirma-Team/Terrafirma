using Microsoft.Xna.Framework;
using System;
using Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.GemStaves.AmberStaff;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.GemStaves.DiamondStaff
{
    internal class DiamondTurret : Spell
    {
        public override int UseAnimation => 26;
        public override int UseTime => 26;
        public override int ManaCost => 40;
        public override int[] SpellItem => new int[] { ItemID.DiamondStaff };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<DiamondTurretProj>();
            foreach (Projectile p in Main.ActiveProjectiles)
            {
                if (p.owner == player.whoAmI && p.type == type || p.type == type)
                    p.Kill();
            }
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }


    public class DiamondTurretProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            Projectile.Size = new Vector2(24);
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override void AI()
        {
            Projectile.ai[0]++;
            Projectile.velocity *= 0.95f;
            if (Projectile.ai[0] % 90 == 0 && TFUtils.FindClosestNPC(600f, Projectile.position) != null)
            {
                if (Main.myPlayer == Projectile.owner)
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.Center.DirectionTo(TFUtils.FindClosestNPC(600f, Projectile.position).Center) * 13f, ModContent.ProjectileType<DiamondBoltProj>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 1);
                for (int i = 0; i < 8; i++)
                {
                    Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GemDiamond, Main.rand.NextVector2Circular(5, 5));
                    d.noGravity = true;
                }
            }
            if (Main.rand.NextBool(3))
            {
                Dust d2 = Dust.NewDustPerfect(Projectile.Center + new Vector2(Main.rand.NextFloat(-12, 12), 0), DustID.GemDiamond);
                d2.velocity = new Vector2(0, 5);
                d2.noGravity = true;
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
