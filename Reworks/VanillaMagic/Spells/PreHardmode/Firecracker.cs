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
    internal class Firecracker : Spell
    {
        public override int UseAnimation => -1;
        public override int UseTime => -1;
        public override int ManaCost => 3;
        public override int ReuseDelay => -1;
        public override int[] SpellItem => new int[] { ItemID.WandofSparking };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = -2; i < 2; i++)
            {
                Projectile.NewProjectile(source, position, velocity.RotatedBy(0.1f * i), ModContent.ProjectileType<FireCrackerSpark>(), (int)(damage / 1.5f), knockback, player.whoAmI);
            }
            return false;
        }
    }

    public class FireCrackerSpark : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.WandOfSparkingSpark}";
        public override void SetDefaults()
        {
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.penetrate = 1;

            Projectile.timeLeft = 40;
            Projectile.Opacity = 0f;
            Projectile.Size = new Vector2(16);
        }

        public override void AI()
        {
            Projectile.velocity.X *= 0.98f;
            Projectile.velocity.Y += 0.1f;

            if (Projectile.ai[0] % 2 == 0)
            {
                Dust d = Dust.NewDustDirect(Projectile.Center - new Vector2(2,2), 4, 4, DustID.Torch, Projectile.velocity.X, Projectile.velocity.Y + Main.rand.NextFloat(-1f,1f), Scale: Projectile.timeLeft / 20f + 0.3f);
                d.noGravity = false;
                d.customData = 0;
            }

            Projectile.ai[0]++;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(5)) target.AddBuff(BuffID.OnFire, 60);
            base.OnHitNPC(target, hit, damageDone);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (Main.rand.NextBool(5)) target.AddBuff(BuffID.OnFire, 60);
            base.OnHitPlayer(target, info);
        }
    }
}
