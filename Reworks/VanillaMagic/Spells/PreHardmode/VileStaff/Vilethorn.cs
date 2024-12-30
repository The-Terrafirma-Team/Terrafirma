using Terrafirma.Systems.MageClass;
using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terrafirma.Particles;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria.GameContent;
using Terrafirma.Common;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.VileStaff
{
    public class Vilethorn : Spell
    {
        public override int UseAnimation => 19;
        public override int UseTime => 19;
        public override int ManaCost => 12;
        public override int[] SpellItem => new int[] { ItemID.Vilethorn };
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<VileStaffThorn>(), damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }
    }
    public class VileStaffThorn : VileStaffDark
    {
        public override void AI()
        {
            commonHeldLogic(25);
            positionSelf(new Vector2(0, 0));
            PlayerAnimation.ArmPointToDirectionLegacy(Projectile.rotation - MathHelper.PiOver4, player);
            Projectile.position += player.getFrontArmPosition();

            if (player.channel && !stoppedChanneling)
            {
                Projectile.ai[0]++;
                Vector2 vel = Main.rand.NextVector2CircularEdge(3, 3);
                Dust d = Dust.NewDustPerfect(Projectile.Center + new Vector2(30, -30).RotatedBy(Projectile.rotation) + vel * 8, DustID.CorruptGibs, -vel + player.velocity, 128);
                d.noGravity = true;
                if (Projectile.ai[0] % 30 == 0)
                {
                    SoundEngine.PlaySound(new SoundStyle("Terrafirma/Sounds/VileCharge") { PitchVariance = 0.2f, MaxInstances = 10, Volume = 0.06f + Projectile.ai[1] * 0.06f }, Projectile.position);
                    BigSparkle p = new BigSparkle();
                    p.Scale = 2.5f;
                    p.fadeInTime = 5;
                    p.smallestSize = 0.001f;
                    p.secondaryColor = Color.Black * 0.5f;
                    ParticleSystem.AddParticle(p, Projectile.Center + new Vector2(30, -30).RotatedBy(Projectile.rotation) + Main.rand.NextVector2Circular(4, 4), null, new Color(45, 128, 65, 255));
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
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center + new Vector2(30, -30).RotatedBy(Projectile.rotation), new Vector2(32, 0).RotatedBy(Projectile.rotation - MathHelper.PiOver4), ModContent.ProjectileType<VileThornProj>(), Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.ai[1] * 3);
                    SoundEngine.PlaySound(new SoundStyle("Terrafirma/Sounds/VileThorn") { PitchVariance = 0.1f, MaxInstances = 10, Volume = 0.3f }, Projectile.position);
                }
                Projectile.ai[1] = 0;
            }
        }
    }
    public class VileThornProj : ModProjectile
    {
        int maxTimeLeft = 125;
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.aiStyle = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.tileCollide = false;
            Projectile.Size = new Vector2(30);
            Projectile.timeLeft = maxTimeLeft;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            if (Main.rand.NextBool(Projectile.timeLeft > maxTimeLeft - 10 || Projectile.timeLeft < 10 ? 3 : 100))
                Dust.NewDustDirect(Projectile.position, 30, 30, DustID.Corruption, 0, 0, 200);

            if (Projectile.timeLeft > maxTimeLeft - 10)
                Projectile.alpha -= 20;
            else if (Projectile.timeLeft < 10)
                Projectile.alpha += 20;

            if (Projectile.timeLeft == maxTimeLeft - 12 + Projectile.ai[0] && Projectile.ai[0] > 0)
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center + Projectile.velocity, Projectile.velocity, Type, Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.ai[0] - 1);
            if (Projectile.ai[0] == 0)
                Projectile.frame = 1;
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        public override void OnKill(int timeLeft)
        {
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> tex = TextureAssets.Projectile[Type];
            Main.EntitySpriteDraw(tex.Value, Projectile.Center - Main.screenPosition, new Rectangle(0, Projectile.frame * (tex.Height() / 2), tex.Width(), tex.Height() / 2), lightColor * Projectile.Opacity, Projectile.rotation, new Vector2(tex.Width() / 2, tex.Height() / 2), 1, SpriteEffects.None);
            return false;
        }
    }
}
