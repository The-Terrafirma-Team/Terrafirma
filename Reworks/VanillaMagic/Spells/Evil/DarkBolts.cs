using Microsoft.Xna.Framework;
using System;
using Terrafirma.Common;
using Terrafirma.Common.Templates;
using Terrafirma.Particles;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.Evil
{
    public class DarkBolts : Spell
    {
        public override int UseAnimation => 19;
        public override int UseTime => 19;
        public override int ManaCost => 12;
        public override int[] SpellItem => new int[] { ItemID.Vilethorn };
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<VileStaffDark>(), damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
    public class VileStaffDark : HeldProjectile
    {
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            return false;
        }
        public override void AI()
        {
            commonHeldLogic(25);
            positionSelf(new Vector2(0,0));
            PlayerAnimation.ArmPointToDirectionWithoutUpOrDown(Projectile.rotation - MathHelper.PiOver4, player);
            Projectile.position += player.getFrontArmPosition();

            if (player.channel && !stoppedChanneling)
            {
                Projectile.ai[0]++;
                Vector2 vel = Main.rand.NextVector2CircularEdge(3, 3);
                Dust d = Dust.NewDustPerfect(Projectile.Center + new Vector2(30,-30).RotatedBy(Projectile.rotation) + (vel * 8),DustID.Corruption,-vel + player.velocity,128);
                d.noGravity = true;
                if (Projectile.ai[0] % 15 == 0 || Projectile.ai[0] == 1)
                    SoundEngine.PlaySound(SoundID.Item24, Projectile.position);
                if (Projectile.ai[0] % 30 == 0)
                {
                    BigSparkle p = new BigSparkle();
                    p.Scale = 1f;
                    p.fadeInTime = 5;
                    p.smallestSize = 0.001f;
                    p.secondaryColor = new Color(255, 0, 255, 0);
                    ParticleSystem.AddParticle(p, Projectile.Center + new Vector2(30, -30).RotatedBy(Projectile.rotation) + Main.rand.NextVector2Circular(4, 4), null, new Color(128, 0, 255, 0));
                    if (!player.CheckMana(3, true))
                    {
                        stoppedChanneling = true;
                    }
                    Projectile.ai[1]++;
                }
                if (Projectile.ai[0] > 60 * 2)
                    stoppedChanneling = true;
            }
            else
            {
                if (Projectile.ai[1] > 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        BigSparkle p = new BigSparkle();
                        p.Scale = 3f - i;
                        p.fadeInTime = 5;
                        p.smallestSize = 0.001f;
                        p.secondaryColor = new Color(255,0,255,0);
                        ParticleSystem.AddParticle(p, Projectile.Center + new Vector2(30, -30).RotatedBy(Projectile.rotation) + Main.rand.NextVector2Circular(4,4), null, new Color(128, 0, 255, 0));
                    }
                    for (int i = 0; i < 6; i++)
                    {
                        ColorDot p = new ColorDot();
                        p.Size = 0.4f;
                        ParticleSystem.AddParticle(p, Projectile.Center + new Vector2(30, -30).RotatedBy(Projectile.rotation), Main.rand.NextVector2Circular(6,6), new Color(255, 0, 255, 0));
                    }
                    for (int i = 0; i < Projectile.ai[1]; i++)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center + new Vector2(30, -30).RotatedBy(Projectile.rotation), new Vector2(1, 0).RotatedBy(Projectile.rotation - MathHelper.PiOver4 + Main.rand.NextFloat(-0.1f, 0.1f)) * Projectile.velocity.Length() * Main.rand.NextFloat(0.9f,1.1f), ProjectileID.AmethystBolt, Projectile.damage, Projectile.knockBack, Projectile.owner);
                    }
                    SoundEngine.PlaySound(SoundID.Item8, Projectile.position);
                }
                Projectile.ai[1] = 0;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            commonDiagonalItemDraw(lightColor, TextureAssets.Projectile[Type]);
            return false;
        }
    }
}
