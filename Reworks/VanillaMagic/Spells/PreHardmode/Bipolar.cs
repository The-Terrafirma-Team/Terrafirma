using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Linq;
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
    internal class Bipolar : Spell
    {
        public override int UseAnimation => 34;
        public override int UseTime => 34;
        public override int ManaCost => 4;
        public override int ReuseDelay => -1;
        public override int[] SpellItem => new int[] { ItemID.WandofFrosting };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = -2; i < 4; i++)
            {
                Projectile.NewProjectile(source, position, velocity.RotatedBy(0.3f * i) / 2f, ModContent.ProjectileType<BipolarProjectile>(), (int)(damage / 1.5f), knockback, player.whoAmI, 0, i % 2);
            }
            return false;
        }
    }
    
    public class BipolarProjectile : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.WandOfSparkingSpark}";
        public override void SetDefaults()
        {
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.penetrate = 1;

            Projectile.timeLeft = 50;
            Projectile.Opacity = 0f;
            Projectile.Size = new Vector2(16);
            Projectile.extraUpdates = 0;
        }

        public override void AI()
        {
            Dust d = Dust.NewDustPerfect(Projectile.Center, Projectile.ai[1] == 0 ? DustID.Torch : DustID.IceTorch, Vector2.Zero, 1, Scale: 1.5f);
            d.noGravity = true;

            if (Projectile.ai[0] % 4 == 0)
            {
                Dust f = Dust.NewDustPerfect(Projectile.Center, Projectile.ai[1] == 0 ? DustID.Torch : DustID.IceTorch, Main.rand.NextVector2Circular(2f,2f), 1, Scale: 1f);
                f.noGravity = true;
            }

            int invert = Projectile.ai[1] == 0 ? 1 : -1;            
            Projectile.position += new Vector2(0, (float)Math.Sin(Projectile.ai[0] / 3f) * 2f * invert).RotatedBy(Projectile.velocity.ToRotation());

            Projectile.ai[0]++;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; ++i)
            {
                Vector2 randvel = Main.rand.NextVector2Circular(2f, 2f);
                Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, Projectile.ai[1] == 0 ? DustID.Torch : DustID.IceTorch, randvel.X, randvel.Y, 0, Scale: 1.5f);
            }
            base.OnKill(timeLeft);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(4)) target.AddBuff(Projectile.ai[1] == 0 ? BuffID.OnFire : BuffID.Frostburn, 60);
        }
    }
}
