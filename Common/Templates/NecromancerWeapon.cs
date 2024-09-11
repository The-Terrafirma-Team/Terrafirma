using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terrafirma.Common.Players;
using Terrafirma.Common.Templates.Melee;
using Terrafirma.Particles;
using Terrafirma.Projectiles.Melee.Knight;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common.Templates
{
    public abstract class NecromancerWeapon : ModItem
    {
        public int DamageDealt = 0;
        public virtual string SoulName => "";
        public virtual int DamagePerSoul => 50;

        public Color summonColor = Color.White;
        public virtual int FirstSummon => ModContent.ProjectileType<EruptionFloatProjectile>();
        public virtual int SecondarySummon => ModContent.ProjectileType<HeroSwordShot>();

        public static int getDamageValueToAdd(Player player, NecromancerWeapon item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            return (int)Math.Clamp(item.DamageDealt + (damageDone * player.PlayerStats().NecromancerChargeBonus), 0, item.DamagePerSoul * 6);
        }
    }
    public abstract class NecromancerScythe : NecromancerWeapon
    {
        public override bool MeleePrefix()
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Item.noUseGraphic = true;
                Projectile.NewProjectile(source, player.Center, Vector2.Zero, ModContent.ProjectileType<NecromancerSummonAnimation>(), 0, 0, player.whoAmI, ai1: DamageDealt / (float)DamagePerSoul);
                DamageDealt = 0;
            }
            else
            {
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, ai2: MathF.Sqrt(MathF.Pow(TextureAssets.Item[Type].Width(),2) + MathF.Pow(TextureAssets.Item[Type].Height(), 2)));
            }
            return false;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
    }
    public class ScytheSwing : UpDownSwing
    {
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return targetHitbox.IntersectsConeSlowMoreAccurate(player.MountedCenter, Projectile.ai[2] * Projectile.scale, Projectile.rotation - MathHelper.PiOver4, 0.2f);
        }
        public override string Texture => "Terrafirma/Assets/Bullet";
        public override bool PreAI()
        {
            if (Projectile.ai[0] == 0)
                Projectile.spriteDirection = 1 * player.direction;
            else
                Projectile.spriteDirection = -1 * player.direction;
            return base.PreAI();
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            NecromancerWeapon scythe = player.HeldItem.ModItem as NecromancerWeapon;
            scythe.DamageDealt = NecromancerWeapon.getDamageValueToAdd(player,scythe,target,hit,damageDone);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            commonDiagonalItemDraw(lightColor, TextureAssets.Item[player.HeldItem.type], Projectile.scale);
            return false;
        }
    }
    public class NecromancerSummonAnimation : ModProjectile
    {
        public override string Texture => "Terrafirma/Assets/Bullet";
        public override void SetDefaults()
        {
            Projectile.aiStyle = -1;
            Projectile.hide = true;
            Projectile.timeLeft = (15 * 6) + 5;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.spriteDirection = player.direction;
            player.bodyFrame.Y = 56;
            Projectile.Center = (player.getFrontArmPosition() + player.Center).ToPoint().ToVector2();
            player.heldProj = Projectile.whoAmI;
            player.SetDummyItemTime(Projectile.timeLeft);
            Projectile.ai[0]++;
            Projectile.rotation = MathF.Sin(Projectile.ai[1] / 10f) * 0.1f;

            if (Projectile.ai[0] % 15 == 0 && Projectile.ai[0] <= Projectile.ai[1] * 15)
            {
                SoundEngine.PlaySound(SoundID.Item8, Projectile.Center);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Main.rand.NextVector2CircularEdge(5, 5), ModContent.ProjectileType<NecromancySummon>(), Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.ai[0] % 30 == 0? (player.HeldItem.ModItem as NecromancerScythe).SecondarySummon : (player.HeldItem.ModItem as NecromancerScythe).FirstSummon);
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Item item = Main.player[Projectile.owner].HeldItem;
            Asset<Texture2D> tex = TextureAssets.Item[item.type];

            for (int i = 0; i < 8; i++)
            {
                Main.EntitySpriteDraw(tex.Value, Projectile.Center - Main.screenPosition + new Vector2(Main.rand.NextFloat(2), 0).RotatedBy(i * MathHelper.PiOver4 + (Main.timeForVisualEffects * 0.1f)), null, new Color(1f, 1f, 1f, 0), (Projectile.spriteDirection == 1 ? Projectile.rotation : Projectile.rotation - MathHelper.PiOver2) - MathHelper.PiOver4, new Vector2(0, Projectile.spriteDirection == -1 ? 0 : tex.Height()), item.scale, Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically);
            }

            Main.EntitySpriteDraw(tex.Value, Projectile.Center - Main.screenPosition, null, lightColor, (Projectile.spriteDirection == 1 ? Projectile.rotation : Projectile.rotation - MathHelper.PiOver2) - MathHelper.PiOver4, new Vector2(0, Projectile.spriteDirection == -1 ? 0 : tex.Height()), item.scale, Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically);

            return false;
        }
    }
    public class NecromancySummon : ModProjectile
    {
        public override string Texture => "Terrafirma/Assets/Bullet";
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(8);
            Projectile.aiStyle = -1;
            Projectile.extraUpdates = 30;
            Projectile.timeLeft = Projectile.extraUpdates;
            Projectile.hide = true;
        }
        NecromancerScythe scythe;
        public override void OnSpawn(IEntitySource source)
        {
            scythe = Main.player[Projectile.owner].HeldItem.ModItem as NecromancerScythe;
        }
        public override void AI()
        {
            ParticleSystem.AddParticle(new PixelCircle() {outlineColor = Color.Lerp(scythe.summonColor,Color.Black,0.6f),scale = Main.rand.NextFloat(1,2) }, Projectile.Center, Main.rand.NextVector2Circular(2, 2), scythe.summonColor);
            Projectile.velocity.Y += 0.1f;
        }
        public override void OnKill(int timeLeft)
        {
            //SoundEngine.PlaySound(SoundID.DD2_DarkMageSummonSkeleton,Projectile.Center);
            int iterations = 30;
            for(int i = 0; i < iterations; i++)
            {
                ParticleSystem.AddParticle(new PixelCircle() {deceleration = 0.93f, scaleDecreaseOverTime = Main.rand.NextFloat(0.1f,0.3f), outlineColor = Color.Lerp(scythe.summonColor, Color.Black, 0.6f), scale = Main.rand.NextFloat(4, 8) }, Projectile.Center, new Vector2(Main.rand.NextFloat(3,4),0).RotatedBy(i * MathHelper.TwoPi/iterations), scythe.summonColor);
            }
            if(Projectile.owner == Main.myPlayer)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity * 0.2f, (int)Projectile.ai[0], Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
            base.OnKill(timeLeft);
        }
    }
}
