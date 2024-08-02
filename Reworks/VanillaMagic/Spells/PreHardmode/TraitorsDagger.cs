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

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode
{
    internal class TraitorsDagger : Spell
    {
        public override int UseAnimation => -1;
        public override int UseTime => -1;
        public override int ManaCost => -1;
        public override int ReuseDelay => -1;
        public override int[] SpellItem => [ItemID.DemonScythe];

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity * 60, ModContent.ProjectileType<TraitorsDaggerProj>(), damage / 3, knockback, player.whoAmI, 0, 0, 0);

            if (Main.rand.NextBool(3))
            {
                Projectile.NewProjectile(source, position, velocity.RotatedByRandom(0.2f) * 40, ModContent.ProjectileType<TraitorsDaggerProj>(), damage / 2, knockback, player.whoAmI, 0, 0, 0);
            }

            return false;
        }
    }
    public class TraitorsDaggerProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.QuickDefaults();
            Projectile.timeLeft = 60;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            if (Main.rand.NextBool(3))
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Snow);
                d.noGravity = true;
                d.velocity *= 0.2f;
            }
            if (Main.rand.NextBool(3))
            {
                Dust d2 = Dust.NewDustPerfect(Projectile.Center, DustID.Ice_Purple);
                d2.noGravity = true;
                d2.velocity *= 0.2f;
                d2.color = new Color(1f, 1f, 1f, 0f);
                d2.alpha = 128;
            }

            if (Projectile.timeLeft < 40)
            {
                Projectile.alpha += (255 / 40);
                Projectile.velocity.Y += 0.2f;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<ChilledForEnemies>(), 60 * (Main.rand.NextBool(9) ? 20 : 6));
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Chilled, 60 * (Main.rand.NextBool(9)? 20 : 6));
        }
    }
}
