using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terrafirma.Projectiles.Melee.Paladin;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.GameInput;
using Terraria.Localization;

namespace Terrafirma.Items.Weapons.Melee.Paladin
{
    internal class LeadThrowingHammer : ModItem
    {
        public override bool MeleePrefix()
        {
            return true;
        }
        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.knockBack = 4;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.DamageType = DamageClass.Melee;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.rare = ContentSamples.ItemsByType[ItemID.LeadHammer].rare;
            Item.value = ContentSamples.ItemsByType[ItemID.LeadHammer].value;

            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.Paladin.LeadThrowingHammer>();
            Item.shootSpeed = 7;
            Item.channel = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            for (int i = 0; i < tooltips.Count; i++)
            {
                if (tooltips[i].Mod.Equals("Terraria") && tooltips[i].Name.Equals("Tooltip0"))
                {
                    string Right = TFUtils.NicenUpKeybindNameIfApplicable(PlayerInput.GenerateInputTag_ForCurrentGamemode(tagForGameplay: true, "MouseRight"));
                    tooltips.Insert(i + 1, new TooltipLine(Mod, "Tooltip", Language.GetText("Mods.Terrafirma.Misc.RightClickToParry").WithFormatArgs([Right, Main.LocalPlayer.ApplyTensionBonusScaling(20, true)]).Value));
                    break;
                }
            }
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<LeadThrowingHammerParry>(), damage, knockback, player.whoAmI);
                return false;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.Anvils).AddIngredient(ItemID.LeadBar, 8).Register();
        }
    }
}
