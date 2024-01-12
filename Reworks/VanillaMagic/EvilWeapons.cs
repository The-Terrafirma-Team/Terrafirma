using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TerrafirmaRedux.Global;
using Terraria.Audio;
using Terraria.DataStructures;

namespace TerrafirmaRedux.Reworks.VanillaMagic
{
    public class EvilWeapons : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.GoldenShower;
        }
        public override void SetDefaults(Item entity)
        {
            entity.UseSound = null;
        }
        public override float UseAnimationMultiplier(Item item, Player player)
        {
            if (item.GetGlobalItem<GlobalItemInstanced>().Spell == 1)
                return 3;
            return base.UseAnimationMultiplier(item, player);
        }
        public override float UseTimeMultiplier(Item item, Player player)
        {
            if (item.GetGlobalItem<GlobalItemInstanced>().Spell == 1)
                return 9;
            return base.UseTimeMultiplier(item, player);
        }
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            switch(item.GetGlobalItem<GlobalItemInstanced>().Spell)
            {
                case 0:
                    if(player.ItemAnimationJustStarted)
                    SoundEngine.PlaySound(SoundID.Item13,player.position);
                    break;
                case 1:
                    SoundEngine.PlaySound(SoundID.NPCDeath19, player.position);
                    break;
            }
            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }
        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if(item.GetGlobalItem<GlobalItemInstanced>().Spell == 1)
            {  
                type = ModContent.ProjectileType<IchorBubble>();
                velocity *= 0.45f;
                damage *= 3;
            }
            base.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
    }
    public class IchorBubble : ModProjectile
    {
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(1f, 1f + (float)Math.Sin(Projectile.timeLeft * 0.14f) * 0.3f, 1f + (float)Math.Sin(Projectile.timeLeft * 0.12f) * 0.3f, 0.4f) * (0.8f + (float)Math.Sin(Projectile.timeLeft * 0.1f) * 0.2f);
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.ToxicBubble);
            Projectile.timeLeft = 150;
            DrawOffsetX = 5;
            DrawOriginOffsetY = 5;
        }
        public override void AI()
        {
            if (Projectile.ai[0] == 0)
                Projectile.ai[0] = 1;

            Projectile.ai[0] *= 1.02f;
            Projectile.scale = 1 + (float)Math.Sin(Projectile.ai[0] * 0.1f) * 0.1f;

            Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(10,10), DustID.Ichor, Projectile.velocity, 0);
            d.noGravity = true;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14);
            for (int i = 0; i < 24; i++)
            {
                float rot = MathHelper.TwoPi / 24 * Main.rand.NextFloat(0.9f, 1.1f) * i;
                Dust d = Dust.NewDustPerfect(Projectile.Center + new Vector2(0, 10).RotatedBy(rot), DustID.Ichor, new Vector2(0, Main.rand.NextFloat(2,4)).RotatedBy(rot));
                d.noGravity = !Main.rand.NextBool(6);
            }
            for(int i = 0; i < 12; i++)
            {
                Dust d2 = Dust.NewDustPerfect(Projectile.Center,DustID.Torch,Main.rand.NextVector2Circular(6,6));
                d2.noGravity = true;
                d2.fadeIn = 1.2f;
            }
            Projectile.Explode(100);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Ichor, 60 * 17);
            if (Main.rand.NextBool())
            {
                target.AddBuff(BuffID.OnFire3, 60 * 6);
            }
        }
    }
}
