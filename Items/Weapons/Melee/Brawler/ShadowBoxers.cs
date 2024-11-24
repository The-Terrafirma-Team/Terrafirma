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
    public class ShadowBoxers : ModItem, IItemThatDrawsOnHandsWhenHeld, IHasTertriaryFunction
    {
        private static Asset<Texture2D> ArmTex;
        public override void SetStaticDefaults()
        {
            ItemSets.AltFireDoesNotConsumeFeralCharge[Type] = true;
        }
        const int SuperCost = 25;
        public override void SetDefaults()
        {
            Item.shoot = ModContent.ProjectileType<ShadowBoxersPunch>();
            Item.shootSpeed = 1;
            Item.damage = 20;
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
            CreateRecipe().AddTile(TileID.Anvils).AddIngredient(ItemID.DemoniteBar,6).Register();
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            for (int i = 0; i < tooltips.Count; i++)
            {
                if (tooltips[i].Mod.Equals("Terraria") && tooltips[i].Name.Equals("Tooltip0"))
                {
                    string Right = TFUtils.NicenUpKeybindNameIfApplicable(PlayerInput.GenerateInputTag_ForCurrentGamemode(tagForGameplay: true, "MouseRight"));
                    string Middle = TFUtils.NicenUpKeybindNameIfApplicable(Keybinds.tertiaryAttack.GetAssignedKeys().FirstOrDefault());
                    tooltips[i].Text = Language.GetText("Mods.Terrafirma.Misc.RightClickToParry").WithFormatArgs([Right, Main.LocalPlayer.ApplyTensionBonusScaling(20, true)]).Value;
                    tooltips.Insert(i + 1, new TooltipLine(Mod, "Tooltip", Tooltip.WithFormatArgs([Middle, Main.LocalPlayer.ApplyTensionBonusScaling(SuperCost, false, true)]).Value));
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
            IItemThatDrawsOnHandsWhenHeld.commonFrontHandDrawData(ArmTex, ref drawInfo);
            if (drawInfo.drawPlayer.CheckTension(SuperCost, false))
            {
                for (int i = 0; i < 4; i++)
                {
                    IItemThatDrawsOnHandsWhenHeld.commonFrontHandDrawData(ArmTex, ref drawInfo, new Vector2(0, 2).RotatedBy(MathHelper.PiOver2 * i), new Color(0.7f, 0.2f, 1f, 0f) * (drawInfo.colorArmorBody.A / 255f) * (((float)Math.Sin(Main.timeForVisualEffects * 0.1f) * 0.1f) + 0.5f));
                }
            }

        }
        public void DrawOffHand(ref PlayerDrawSet drawInfo)
        {
        }
        public override bool AltFunctionUse(Player player)
        {
            return !player.PlayerStats().Whiffed;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<ShadowBoxersParry>(), damage, knockback, player.whoAmI);
            else
            {
                Projectile.NewProjectile(source, position + Vector2.Normalize(velocity) * 10, velocity * 4, type, damage, knockback, player.whoAmI, player.PlayerStats().TimesHeldWeaponHasBeenSwung % 2 == 0 ? 1 : 0);
            }
            return false;
        }
        public void TertriaryShoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity * 12, ModContent.ProjectileType<ShadowBoxerBomb>(), damage * 3, knockback * 2, player.whoAmI);
        }
        public bool canUseTertriary(Player player)
        {
            return player.CheckTension(player.ApplyTensionBonusScaling(SuperCost, false, true));
        }
        public override bool MeleePrefix() => true;
    }
}
