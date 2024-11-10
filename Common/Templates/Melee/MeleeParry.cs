using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Buffs.Debuffs;
using Terrafirma.Common.Players;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Common.Templates.Melee
{
    public abstract class MeleeParry : HeldProjectile
    {
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        public override bool? CanDamage()
        {
            return false;
        }
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(player.width * 0.8f, player.height + 8);
            Projectile.tileCollide = false;
            Projectile.friendly = true;
        }
        public void UpwardsSwingParryAnimation()
        {
            Projectile.rotation = MathHelper.Lerp(1f, -0.5f, Easing.OutPow(Projectile.ai[0],2));
            PlayerAnimation.ArmPointToDirection(Projectile.rotation + MathHelper.PiOver4, player);
            if (player.direction == -1)
                Projectile.rotation = MathHelper.Pi - Projectile.rotation + MathHelper.PiOver2;
        }
        public virtual void FailParry()
        {
            player.AddBuff(ModContent.BuffType<Whiffed>(),(int)(60 * 5 * player.PlayerStats().ParryDurationMultiplier));
        }
        public override void AI()
        {
            Projectile.ai[0] += (1 / 15f);

            if (Projectile.ai[0] > 1f)
            {
                FailParry();
                Projectile.Kill();
            }

            player.SetDummyItemTime(2);
            player.PlayerStats().ParryProjectile = Projectile.whoAmI;
            Projectile.Center = player.Center + new Vector2(player.direction * Projectile.width / 2, 2);
            Projectile.Center = Projectile.Center.ToPoint().ToVector2();
        }
        public void CommonParryEffects(Player player, Entity e, int immuneTime = 60, int tensionGain = 20)
        {
            player.immune = true;
            player.AddImmuneTime(ImmunityCooldownID.General, (int)(immuneTime * player.PlayerStats().ParryImmunityDurationMultiplier));
            SoundEngine.PlaySound(SoundID.Item37, player.position);
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDustPerfect(e.Hitbox.ClosestPointInRect(player.Center), DustID.Smoke);
                Dust g = Dust.NewDustPerfect(e.Hitbox.ClosestPointInRect(player.Center), DustID.GoldCoin);
                g.noGravity = true;
            }
            player.GiveTension(player.ApplyTensionBonusScaling(tensionGain,true));
        }
        public virtual void OnParryNPC(NPC n, Player player)
        {
            Projectile.Kill();
            CommonParryEffects(player, n);
        }
        public virtual void OnParryProjectile(Projectile p, Player player)
        {
            Projectile.Kill();
            CommonParryEffects(player, p);
        }

        public void PaladinHammerParry(Color color)
        {
            Asset<Texture2D> tex = TextureAssets.Projectile[Type];
            Main.EntitySpriteDraw(tex.Value, player.RotatedRelativePoint(player.MountedCenter + player.getFrontArmPosition()) - Main.screenPosition, new Rectangle(0, tex.Height() / Main.projFrames[Type] * Projectile.frame, tex.Width(), tex.Height() / Main.projFrames[Type]), color, Projectile.rotation, new Vector2(0, tex.Height() / Main.projFrames[Type]), player.GetAdjustedItemScale(player.HeldItem), Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically);
        }
    }
}
