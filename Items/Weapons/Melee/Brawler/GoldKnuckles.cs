using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Linq;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common;
using Terrafirma.Common.Interfaces;
using Terrafirma.Common.Players;
using Terrafirma.Data;
using Terrafirma.Projectiles.Melee.Brawler;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Melee.Brawler
{
    public class GoldKnuckles : ModItem, IItemThatDrawsOnHandsWhenHeld, IHasTertriaryFunction
    {
        private static Asset<Texture2D> ArmTex;
        public override void SetStaticDefaults()
        {
            ItemSets.AltFireDoesNotConsumeFeralCharge[Type] = true;
        }
        const int SuperCost = 30;
        public override void SetDefaults()
        {
            Item.shoot = ModContent.ProjectileType<GoldKnucklesPunch>();
            Item.shootSpeed = 1;
            Item.damage = 13;
            Item.useAnimation = Item.useTime = 20;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 2.5f;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.sellPrice(0, 0, 14, 0);
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.Anvils).AddIngredient(ItemID.GoldBar,6).Register();
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            for (int i = 0; i < tooltips.Count; i++)
            {
                if (tooltips[i].Mod.Equals("Terraria") && tooltips[i].Name.Equals("Tooltip0"))
                {
                    string Right = TFUtils.NicenUpKeybindNameIfApplicable(PlayerInput.GenerateInputTag_ForCurrentGamemode(tagForGameplay: true, "MouseRight"));
                    string Middle = TFUtils.NicenUpKeybindNameIfApplicable(Keybinds.tertiaryAttack.GetAssignedKeys().FirstOrDefault());
                    tooltips[i].Text = Tooltip.WithFormatArgs([Right, Main.LocalPlayer.ApplyTensionBonusScaling(20,true), Middle, Main.LocalPlayer.ApplyTensionBonusScaling(SuperCost,false,true)]).Value;
                    tooltips.RemoveAt(i + 1);
                    break;
                }
            }
        }
        public override void Load()
        {
            ArmTex = ModContent.Request<Texture2D>(Texture + "_Hand");
        }
        public void DrawFrontHand(ref PlayerDrawSet drawInfo)
        {
            if (!drawInfo.drawPlayer.compositeFrontArm.enabled)
            {
                DrawData item = new DrawData(ArmTex.Value, new Vector2((int)(drawInfo.Position.X - Main.screenPosition.X - (float)(drawInfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawInfo.drawPlayer.width / 2)), (int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawInfo.drawPlayer.height - (float)drawInfo.drawPlayer.bodyFrame.Height + 4f)) + drawInfo.drawPlayer.bodyPosition + new Vector2(drawInfo.drawPlayer.bodyFrame.Width / 2, drawInfo.drawPlayer.bodyFrame.Height / 2), drawInfo.compFrontArmFrame, drawInfo.colorArmorBody, drawInfo.drawPlayer.bodyRotation, drawInfo.bodyVect, 1f, drawInfo.playerEffect, 0);
                if (drawInfo.drawPlayer.CurrentLegFrameIsOneThatRaisesTheBody())
                {
                    item.position.Y -= 2 * drawInfo.drawPlayer.gravDir;
                }
                drawInfo.DrawDataCache.Add(item);
            }
            else
            {
                Vector2 vector = new Vector2((int)(drawInfo.Position.X - Main.screenPosition.X - (float)(drawInfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawInfo.drawPlayer.width / 2)), (int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawInfo.drawPlayer.height - (float)drawInfo.drawPlayer.bodyFrame.Height + 4f)) + drawInfo.drawPlayer.bodyPosition + new Vector2(drawInfo.drawPlayer.bodyFrame.Width / 2, drawInfo.drawPlayer.bodyFrame.Height / 2);
                Vector2 value = Main.OffsetsPlayerHeadgear[drawInfo.drawPlayer.bodyFrame.Y / drawInfo.drawPlayer.bodyFrame.Height];
                vector += value * -drawInfo.playerEffect.HasFlag(SpriteEffects.FlipVertically).ToDirectionInt();
                float rotation = drawInfo.drawPlayer.bodyRotation + drawInfo.compositeFrontArmRotation;
                Vector2 bodyVect = drawInfo.bodyVect;
                Vector2 compositeOffset_FrontArm = new Vector2(-5 * ((!drawInfo.playerEffect.HasFlag(SpriteEffects.FlipHorizontally)) ? 1 : (-1)), 0f);
                bodyVect += compositeOffset_FrontArm;
                vector += compositeOffset_FrontArm;
                DrawData drawData2 = new DrawData(ArmTex.Value, vector, drawInfo.compFrontArmFrame, drawInfo.colorArmorBody, rotation, bodyVect, 1f, drawInfo.playerEffect, 0);
                PlayerDrawLayers.DrawCompositeArmorPiece(ref drawInfo, CompositePlayerDrawContext.FrontArmAccessory, drawData2);
            }
        }
        public void DrawOffHand(ref PlayerDrawSet drawInfo)
        {
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<GoldKnucklesParry>(), damage, knockback, player.whoAmI);
            else
            {
                Projectile.NewProjectile(source, position + Vector2.Normalize(velocity) * 10, velocity * 4, type, damage, knockback, player.whoAmI, player.PlayerStats().TimesHeldWeaponHasBeenSwung % 2 == 0 ? 1 : 0);
            }
            return false;
        }
        public void TertriaryShoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<GoldKnucklesDash>(), damage * 2, knockback * 2, player.whoAmI, -30);
        }
        public bool canUseTertriary(Player player)
        {
            return player.CheckTension(player.ApplyTensionBonusScaling(SuperCost, false, true));
        }
        public override bool MeleePrefix() => true;
    }
}
