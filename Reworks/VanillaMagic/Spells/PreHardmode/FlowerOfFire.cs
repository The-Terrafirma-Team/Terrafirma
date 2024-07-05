using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Linq;
using Terrafirma.Particles;
using Terrafirma.Reworks.VanillaMagic.Spells.Hardmode;
using Terrafirma.Systems.MageClass;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode
{
    internal class FlowerOfFire : Spell
    {
        public override int UseAnimation => 24;
        public override int UseTime => 24;
        public override int ManaCost => 40;
        public override int ReuseDelay => -1;
        public override int[] SpellItem => new int[] { ItemID.FlowerofFire };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<FlowerOfFireProj>();
            damage = (int)(damage * 1.1f);
            velocity = Vector2.Normalize(velocity) * 8f;

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0, 0);
            return false;
        }

        public override bool CanUseItem(Item item, Player player)
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<FlowerOfFireProj>()] < 1;
        }
    }

    internal class FlowerOfFireProj : ModProjectile
    {
        NPC target = null;
        public override void SetDefaults()
        {
            Projectile.tileCollide = false;
            Projectile.friendly = true;

            Projectile.timeLeft = 60 * 12;
            Projectile.Opacity = 0f;
            Projectile.Size = new Vector2(14);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D Tex = TextureAssets.Projectile[Type].Value;

            Main.EntitySpriteDraw(Tex,
                Projectile.Center - Main.screenPosition,
                new Rectangle(2, 28, 14, 14),
                new Color(1f, 1f, 1f, 0f) * Projectile.Opacity * 0.5f,
                Projectile.rotation,
                new Vector2(7),
                1.2f + ((float)Math.Sin((Main.timeForVisualEffects + 20f) / 20f) / 8f),
                SpriteEffects.None,
                0);
            Main.EntitySpriteDraw(Tex, 
                Projectile.Center - Main.screenPosition, 
                new Rectangle(2, 28, 14, 14), 
                new Color(1f,1f,1f,0f) * Projectile.Opacity, 
                Projectile.rotation,
                new Vector2(7), 
                1f, 
                SpriteEffects.None, 
                0);

            int PetalAmount = 5;
            for (int i = 0; i < PetalAmount; i++)
            {
                Main.EntitySpriteDraw(Tex,
                    Projectile.Center - Main.screenPosition,
                    new Rectangle(0, 0, 18, 26),
                    new Color(1f, 1f, 1f, 0f) * Projectile.Opacity * 0.3f,
                    i * (MathHelper.TwoPi / (float)PetalAmount) + (float)(Main.timeForVisualEffects / 50f) + (MathHelper.TwoPi / ((float)PetalAmount * 2f)),
                    new Vector2(9, 34 + ((float)Math.Sin((Main.timeForVisualEffects + 30f) / 20f) * 3f + 3f)),
                    1.2f + ((float)Math.Sin((Main.timeForVisualEffects + 20f) / 20f) / 8f),
                    SpriteEffects.None,
                    0);
            }
            for (int i = 0; i < PetalAmount; i++)
            {
                Main.EntitySpriteDraw(Tex,
                    Projectile.Center - Main.screenPosition,
                    new Rectangle(0, 0, 18, 26),
                    new Color(1f, 1f, 1f, 0f) * Projectile.Opacity,
                    i * (MathHelper.TwoPi / (float)PetalAmount) + (float)(Main.timeForVisualEffects / 50f),
                    new Vector2(9, 34 + ((float)Math.Sin(Main.timeForVisualEffects / 20f) * 2f + 2f)),
                    1f + ((float)Math.Sin(Main.timeForVisualEffects / 20f) / 8f),
                    SpriteEffects.None,
                    0);
            }
            return false;
        }
        public override void AI()
        {
            Projectile.velocity *= 0.96f;
            Projectile.rotation = Projectile.velocity.X * 0.2f;

            if (Projectile.timeLeft < 20) Projectile.Opacity -= 0.05f;
            else Projectile.Opacity += 0.02f;

            if (Projectile.timeLeft % 8 == 0) Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(30f, 30f), DustID.Torch, Vector2.Zero, Scale: Main.rand.NextFloat(1f, 1.7f));

            if (target == null || Projectile.Center.Distance(target.Center) > 450f || !target.active) target = TFUtils.FindClosestNPC(400f, Projectile.Center, TargetThroughWalls: false);

            if (Projectile.ai[0] > 60 && target != null)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.DirectionTo(target.Center) * 10f, ProjectileID.BallofFire, Projectile.damage, Projectile.knockBack, Projectile.owner);
                Projectile.ai[0] = 0;
            }
            Projectile.ai[0]++;
        }

        public override bool? CanHitNPC(NPC target) => false;
        public override bool CanHitPlayer(Player target) => false;

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 30; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(40f,40f), DustID.Torch, Vector2.Zero, Scale: Main.rand.NextFloat(1f,1.7f));
            }
        }
    }
}
