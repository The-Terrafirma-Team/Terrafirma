using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Templates.Melee;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.ModLoader;

namespace Terrafirma.Common.Templates
{
    public abstract class NecromancerScythe : ModItem
    {
        public override bool MeleePrefix()
        {
            return true;
        }
        public int DamageDealt = 0;
        public virtual string SoulName => "";
        public virtual int DamagePerSoul => 100;
        //public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        //{
        //    DamageDealt = Math.Clamp(DamageDealt + damageDone,0,DamagePerSoul * 6);
        //    Main.NewText(DamageDealt);
        //}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Item.noUseGraphic = true;
                Projectile.NewProjectile(source, player.Center, Vector2.Zero, ModContent.ProjectileType<ScytheCast>(), 0, 0, player.whoAmI);
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
            NecromancerScythe scythe = player.HeldItem.ModItem as NecromancerScythe;
            scythe.DamageDealt = (int)Math.Clamp(scythe.DamageDealt + (damageDone * player.PlayerStats().NecromancerChargeBonus), 0, scythe.DamagePerSoul * 6);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            commonDiagonalItemDraw(lightColor, TextureAssets.Item[player.HeldItem.type], Projectile.scale);
            return false;
        }
    }
    public class ScytheCast : ModProjectile
    {
        public override string Texture => "Terrafirma/Assets/Bullet";
        public override void SetDefaults()
        {
            Projectile.aiStyle = -1;
            Projectile.hide = true;
            Projectile.timeLeft = 40;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.spriteDirection = player.direction;
            player.bodyFrame.Y = 56;
            Projectile.Center = (player.getFrontArmPosition() + player.Center).ToPoint().ToVector2();
            player.heldProj = Projectile.whoAmI;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Item item = Main.player[Projectile.owner].HeldItem;
            Asset<Texture2D> tex = TextureAssets.Item[item.type];

            for (int i = 0; i < 4; i++)
            {
                Main.EntitySpriteDraw(tex.Value, Projectile.Center - Main.screenPosition + new Vector2(2, 0).RotatedBy(i * MathHelper.PiOver2), null, new Color(1f, 1f, 1f, 0), (Projectile.spriteDirection == 1 ? 0 : -MathHelper.PiOver2) - MathHelper.PiOver4, new Vector2(0, Projectile.spriteDirection == -1 ? 0 : tex.Height()), item.scale, Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically);
            }

            Main.EntitySpriteDraw(tex.Value, Projectile.Center - Main.screenPosition, null, lightColor, (Projectile.spriteDirection == 1 ? 0 : -MathHelper.PiOver2) - MathHelper.PiOver4, new Vector2(0, Projectile.spriteDirection == -1 ? 0 : tex.Height()), item.scale, Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically);

            return false;
        }
    }
}
