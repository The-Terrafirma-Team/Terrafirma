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

namespace Terrafirma.Reworks.VanillaMagic.Spells.PreHardmode.SpaceGun
{
    internal class QuantumRay : Spell
    {
        public override int UseAnimation => -1;
        public override int UseTime => -1;
        public override int ManaCost => 8;
        public override int ReuseDelay => -1;
        public override int[] SpellItem => new int[] { ItemID.SpaceGun };

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<QuantumRayProj>();
            damage = (int)(damage * 0.8f);
            velocity *= 0.2f;
            Projectile.NewProjectile(source, position + new Vector2(20,0).RotatedBy(velocity.ToRotation()), velocity, type, damage, knockback, player.whoAmI);
            return false;
        }
    }

    public class QuantumRayProj : ModProjectile
    {
        NPC targetnpc = null;
        NPC[] hittargets = new NPC[]{};
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.DeathLaser}";
        public override void SetDefaults()
        {
            Projectile.QuickDefaults();
            Projectile.hide = true;
            Projectile.penetrate = 3;
            Projectile.extraUpdates = 20;
        }
        public override void AI()
        {
            Dust d = Dust.NewDustPerfect(Projectile.Center + new Vector2(0, ((float)Math.Sin(Projectile.ai[0]) * 5f) * (Projectile.whoAmI % 2 == 0? 1 : -1)).RotatedBy(Projectile.velocity.ToRotation()), DustID.CursedTorch, Vector2.Zero);
            d.noGravity = true;

            if (targetnpc == null || !targetnpc.active)
            {
                targetnpc = TFUtils.FindClosestNPC(300f, Projectile.Center, excludedNPCs: hittargets, TargetThroughWalls: false);
            }
            else
            {
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.Center.DirectionTo(targetnpc.Center) * 3f, 0.01f);
            }

            Projectile.ai[0] += 0.3f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            hittargets = hittargets.Append(target).ToArray();
            targetnpc = null;
            Main.player[Projectile.owner].statMana += 2;
            Main.player[Projectile.owner].ManaEffect(2);
            base.OnHitNPC(target, hit, damageDone);
        }
    }
}
