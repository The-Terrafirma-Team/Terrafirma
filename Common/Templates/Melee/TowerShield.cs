using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Data;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Common.Templates.Melee
{
    public class ShieldLayer : PlayerDrawLayer
    {
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            return true;
        }
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            foreach (Projectile proj in Main.ActiveProjectiles)
            {
                if(proj.owner == drawInfo.drawPlayer.whoAmI && proj.ModProjectile is TowerShieldProjectile shield)
                {
                    shield.DrawShield(drawInfo);
                    break;
                }
            }
        }
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.LastVanillaLayer);
    }
    public abstract class TowerShield : ModItem
    {
        public virtual int MaxDamageAbsorbed => 15;
        public int getMaxDamageAbsorption(Player player)
        {
            int dmg = MaxDamageAbsorbed;

            return dmg;
        }
        public override bool MeleePrefix()
        {
            return true;
        }
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Melee;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.useTime = Item.useAnimation = 10;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int dmg = getMaxDamageAbsorption(Main.LocalPlayer);
            dmg *= 2;
            if (Main.expertMode)
                dmg *= 2;
            if (Main.masterMode)
                dmg *= 2;
            int index = tooltips.FindLastIndex(tt => tt.Mod.Equals("Terraria") && tt.Name.Equals("Knockback"));
            if (index != -1)
            {
                tooltips.Insert(index + 1, new TooltipLine(Mod, "Tooltip1", Language.GetText("Mods.Terrafirma.Misc.TowerShield").WithFormatArgs(dmg).ToString()));
            }
        }
    }
    public abstract class TowerShieldProjectile : HeldProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileSets.AutomaticallyGivenMeleeScaling[Type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(22,44);
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.hide = true;
        }
        public override bool? CanDamage()
        {
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public virtual void DrawShield(PlayerDrawSet drawInfo)
        {
            Asset<Texture2D> tex = TextureAssets.Projectile[Type];
            drawInfo.DrawDataCache.Add(new DrawData(tex.Value, Projectile.Center - Main.screenPosition, tex.Frame(1, Main.projFrames[Type],0,Projectile.frame),drawInfo.itemColor,Projectile.rotation,new Vector2(tex.Width() / 2, tex.Height() / Main.projFrames[Type] / 2),Projectile.scale, Projectile.spriteDirection == 1? SpriteEffects.None : SpriteEffects.FlipVertically));
        }
        public override void AI()
        {
            player = Main.player[Projectile.owner];
            player.PlayerStats().FeralCharge = 0;
            if (player.channel && !stoppedChanneling)
            {
                Projectile.timeLeft = 2;
                player.SetDummyItemTime(2);
            }
            if (Projectile.ai[0] == 0)
            {
                float scale = player.GetAdjustedItemScale(player.HeldItem);
                Projectile.Resize((int)(player.width * scale), (int)(player.height * scale));
                Projectile.scale = scale;

                Projectile.rotation = player.Center.DirectionTo(player.PlayerStats().MouseWorld).ToRotation();
            }
            Projectile.ai[0]++;

            player.direction = MathF.Sign(player.PlayerStats().MouseWorld.X - player.Center.X);

            Projectile.spriteDirection = player.direction;
            float lerp = 0.1f * player.GetAdjustedWeaponSpeedPercent(player.HeldItem);
            Projectile.rotation = Utils.AngleLerp(Projectile.rotation, player.Center.DirectionTo(player.PlayerStats().MouseWorld).ToRotation(), lerp);
            Projectile.Center = player.Center + Projectile.rotation.ToRotationVector2() * (15 - (5 * Easing.SineOut(Projectile.localAI[0] / 20)));
            Projectile.position.X = (int)Projectile.position.X;
            Projectile.position.Y = (int)Projectile.position.Y + player.gfxOffY;

            if (Projectile.localAI[0] > 0)
            {
                Projectile.localAI[0]--;
            }
            player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation - MathHelper.PiOver2);
            player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation - MathHelper.PiOver2);

            foreach (Projectile proj in Main.ActiveProjectiles)
            {
                if(player.HeldItem.ModItem is TowerShield shield) {
                    if (proj.hostile && proj.damage <= shield.getMaxDamageAbsorption(player))
                    {
                        Vector2 adjustedProPos = proj.Center.RotatedBy(Projectile.rotation, Projectile.Center);
                        adjustedProPos -= proj.Size / 2;
                        Rectangle projRect = new Rectangle((int)adjustedProPos.X, (int)adjustedProPos.Y, proj.width, proj.height);
                        if (projRect.Intersects(Projectile.Hitbox) && player.immuneTime <= 0)
                        {
                            AbsorbProjectile(proj);
                        }
                    }
                }
            }
        }
        public virtual void AbsorbProjectile(Projectile proj)
        {
            SoundEngine.PlaySound(SoundID.Dig);
            proj.Kill();
            player.PlayerStats().StoredMeleeCharge += 0.1f;
            Projectile.localAI[0] = 20;
            if (!player.noKnockback)
                player.velocity += proj.velocity * 0.3f * player.PlayerStats().KnockbackResist;
        }
    }
}
