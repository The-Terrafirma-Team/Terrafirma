using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Systems.MageClass;
using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terrafirma.Buffs.Debuffs;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.Audio;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.ShadowCodex
{
    internal class PenetratingSigil : Spell
    {
        public override int UseAnimation => 12;
        public override int UseTime => 12;
        public override int ManaCost => 22;
        public override int ReuseDelay => -1;
        public override int[] SpellItem => [ItemID.DemonScythe];

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<PenetratingSigilProj>()] > 0)
            {
                foreach (Projectile proj in Main.ActiveProjectiles)
                {
                    if (proj.type == ModContent.ProjectileType<PenetratingSigilProj>() && proj.owner == player.whoAmI) proj.Kill();
                }
            }
            Projectile.NewProjectile(source, Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<PenetratingSigilProj>(), damage, knockback, player.whoAmI, 0, 0, 0);

            return false;
        }

        public override bool CanAutoReuse()
        {
            return false;
        }
    }
    public class PenetratingSigilProj : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.QuickDefaults();
            Projectile.timeLeft = 420;
            Projectile.Size = new Vector2(32, 32);
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.Opacity = 0f;
            Projectile.scale = 1.2f;
        }

        NPC.HitInfo hitinfo = new NPC.HitInfo();
        public override void AI()
        {

            if (Projectile.timeLeft <= 40) Projectile.Opacity -= 0.03f;
            if (Projectile.ai[0] < 10) Projectile.Opacity += 0.05f;

            if (Projectile.ai[0] % 4 == 0)
            {
                for (int i = 0; i < 12; i++)
                {
                    Dust d = Dust.NewDustPerfect(Projectile.Center + new Vector2(80 * Projectile.scale, 0).RotatedBy((MathHelper.TwoPi / 12) * i + (Projectile.ai[0] / 40f)), DustID.CorruptTorch, Vector2.Zero, 0, Scale: 4f * Projectile.Opacity);
                    d.noGravity = true;
                }
            }

            if (Projectile.ai[0] % 14 == 0)
            {
                SoundEngine.PlaySound(SoundID.Item20 with { Volume = Projectile.Opacity * 0.75f, MaxInstances = 0, PitchVariance = 0.3f }, Projectile.Center);
            }

            Projectile.ai[0]++;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override bool CanHitPvp(Player target)
        {
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> tex = TextureAssets.Projectile[Type];
            Main.EntitySpriteDraw(tex.Value,
                Projectile.Center - Main.screenPosition,
                tex.Frame(),
                new Color(1f, 1f, 1f, 0.5f) * Projectile.Opacity * 0.2f,
                (float)Main.timeForVisualEffects / 20f,
                tex.Size() / 2,
                Projectile.scale,
                SpriteEffects.None);
            Main.EntitySpriteDraw(tex.Value,
                Projectile.Center - Main.screenPosition,
                tex.Frame(),
                new Color(1f, 1f, 1f, 0.5f) * Projectile.Opacity,
                Projectile.rotation,
                tex.Size() / 2,
                Projectile.scale,
                SpriteEffects.None);
            Main.EntitySpriteDraw(tex.Value,
                Projectile.Center - Main.screenPosition,
                tex.Frame(),
                new Color(1f, 1f, 1f, 0.5f) * Projectile.Opacity * 0.4f,
                Projectile.rotation + (float)Math.Sin(Main.timeForVisualEffects / 10f) / 24f,
                tex.Size() / 2,
                Projectile.scale + (float)Math.Sin(Main.timeForVisualEffects / 20f) * 0.02f,
                SpriteEffects.None);
            Main.EntitySpriteDraw(tex.Value,
                Projectile.Center - Main.screenPosition,
                tex.Frame(),
                new Color(1f, 1f, 1f, 0.5f) * Projectile.Opacity * 0.2f,
                Projectile.rotation + (float)Math.Sin(Main.timeForVisualEffects / 25f) / 12f,
                tex.Size() / 2,
                Projectile.scale,
                SpriteEffects.None);
            return false;
        }
    }

    public class PenetratingSigilGlobalProj : GlobalProjectile
    {
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            foreach (Projectile sigil in Main.ActiveProjectiles)
            {
                if (projectile.penetrate >= 0 &&
                    sigil.type == ModContent.ProjectileType<PenetratingSigilProj>() &&
                    target.Hitbox.ClosestPointInRect(sigil.Center).Distance(sigil.Center) <= 80 * sigil.scale)
                {
                    projectile.penetrate += 1;
                    break;
                }
            }
            base.OnHitNPC(projectile, target, hit, damageDone);
        }

        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
        {
            foreach (Projectile sigil in Main.ActiveProjectiles)
            {
                if (projectile.penetrate >= 0 &&
                    sigil.type == ModContent.ProjectileType<PenetratingSigilProj>() &&
                    target.Hitbox.ClosestPointInRect(sigil.Center).Distance(sigil.Center) <= 100 * sigil.scale)
                {
                    projectile.penetrate += 1;
                    break;
                }
            }
            base.OnHitPlayer(projectile, target, info);
        }
    }
}