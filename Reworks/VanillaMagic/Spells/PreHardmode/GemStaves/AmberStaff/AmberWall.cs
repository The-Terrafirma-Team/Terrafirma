using Microsoft.Xna.Framework;
using System;
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
        public override int UseAnimation => 50;
        public override int UseTime => 50;
        public override int ManaCost => 14;
        public override int[] SpellItem => new int[] { ItemID.AmberStaff };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<AmberWallCrystal>();

            foreach (Projectile p in Main.ActiveProjectiles)
            {
                if (p.owner == player.whoAmI && p.type == type || p.type == ModContent.ProjectileType<AmberWallProj>())
                    p.Kill();
            }

            Projectile.NewProjectile(source, position, velocity, type, (int)(damage * 0.5f), knockback, player.whoAmI, 0, 0, 0);

            return false;
        }
    }

    public class AmberWallCrystal : ModProjectile
    {
        public override string Texture => "Terrafirma/Reworks/VanillaMagic/Spells/PreHardmode/GemStaves/AmberStaff/AmberWallProj";
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(24);
            Projectile.timeLeft = 90;
            Projectile.friendly = true;
        }

        public override void AI()
        {
            Projectile.ai[0]++;
            Projectile.velocity *= 0.98f;
            Projectile.rotation += Projectile.direction * (MathHelper.TwoPi * 4 / 90f);
            if (Projectile.ai[0] % 2 == 0)
            {
                Dust newdust = Dust.NewDustPerfect(Projectile.Center, DustID.AmberBolt, Vector2.Zero, 0, default, Main.rand.NextFloat(0.9f, 1.4f));
                newdust.noGravity = true;
            }

            if(Projectile.timeLeft == 1)
            {
                Vector2 length = Main.player[Projectile.owner].MountedCenter - Projectile.Center;
                for (int i = -3; i < 4; i++)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Main.player[Projectile.owner].MountedCenter - length.RotatedBy(0.05f * i), Vector2.Normalize(Projectile.velocity.RotatedBy(0.05f * i)), ModContent.ProjectileType<AmberWallProj>(), Projectile.damage / 8, Projectile.knockBack, Projectile.owner, Main.rand.NextFloat());
                }
                Projectile.Kill();
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.HitDirectionOverride = Projectile.direction;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.CommonBounceLogic(oldVelocity);
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Vector2 length = Main.player[Projectile.owner].MountedCenter - Projectile.Center;
            for (int i = -3; i < 4; i++)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Main.player[Projectile.owner].MountedCenter - length.RotatedBy(0.05f * i), Vector2.Normalize(Projectile.velocity.RotatedBy(0.05f * i)), ModContent.ProjectileType<AmberWallProj>(), Projectile.damage / 2, Projectile.knockBack, Projectile.owner, Main.rand.NextFloat());
            }
            Projectile.Kill();
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
    public class AmberWallProj : ModProjectile
    {
        float randfall = 0f;
        public override void SetDefaults()
        {
            Projectile.penetrate = 4;
            Projectile.tileCollide = false;
            DrawOffsetX = -5;
            DrawOriginOffsetY = -5;
            Projectile.Size = new Vector2(24);
            Projectile.timeLeft = 400;
            Projectile.friendly = true;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 40;
        }

        public override void OnSpawn(IEntitySource source)
        {
            //randfall = Main.rand.NextFloat(0.05f, 0.15f);
            //for (int j = 0; j < 5; j++)
            //{
            //    Dust newdust = Dust.NewDustPerfect(Projectile.Center, DustID.GemAmber, new Vector2(Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f)), 0, Color.White, Main.rand.NextFloat(1f, 1.3f));
            //    newdust.noGravity = true;
            //}

            //Projectile.scale = Main.rand.NextFloat(0.7f, 1.3f);
        }

        public override void AI()
        {
            Projectile.velocity *= 0.95f;
            if (Projectile.ai[1] == 0)
            {
                Projectile.ai[1]++;
                for (int j = 0; j < 5; j++)
                {
                    Dust newdust = Dust.NewDustPerfect(Projectile.Center, DustID.GemAmber, new Vector2(Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f)), 0, default, Main.rand.NextFloat(1f, 1.3f));
                    newdust.noGravity = true;
                    newdust.velocity += Projectile.velocity * 3;
                }
                for (int j = 0; j < 5; j++)
                {
                    Dust newdust = Dust.NewDustPerfect(Projectile.Center, DustID.AmberBolt, new Vector2(Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f)), 0, default, Main.rand.NextFloat(1f, 1.3f));
                    newdust.noGravity = true;
                    newdust.velocity += Projectile.velocity * 3;
                }
            }
            if (Projectile.timeLeft < (30 + MathF.Sin(Projectile.ai[0] * MathHelper.TwoPi) * 20))
            {
                Projectile.rotation += MathF.Sin(Projectile.ai[0] * MathHelper.TwoPi) * 0.02f * Projectile.velocity.Length();
                Projectile.velocity.Y += Projectile.ai[0] * 0.1f + 0.04f;
                Projectile.alpha++;
            }
            if(Projectile.timeLeft < MathF.Sin(Projectile.ai[0] * MathHelper.TwoPi) * 20)
            {
                Projectile.Kill();
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
