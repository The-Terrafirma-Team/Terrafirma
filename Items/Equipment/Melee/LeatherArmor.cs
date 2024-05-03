using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment.Melee
{
    [AutoloadEquip(EquipType.Legs)]
    public class LeatherPants : ModItem
    {
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ItemID.Leather, 8).AddRecipeGroup(RecipeGroupID.IronBar, 4).AddTile(TileID.WorkBenches).Register();
        }
        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(0,0,10,0);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 2;
        }
        public override void UpdateEquip(Player player)
        {
            player.PlayerStats().MeleeFlatDamage += 1;
        }
    }
    [AutoloadEquip(EquipType.Body)]
    public class LeatherChestplate : ModItem
    {
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ItemID.Leather, 12).AddRecipeGroup(RecipeGroupID.IronBar, 6).AddTile(TileID.WorkBenches).Register();
        }
        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 3;
        }
        public override void UpdateEquip(Player player)
        {
            player.PlayerStats().MeleeFlatDamage += 2;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return legs.type == ModContent.ItemType<LeatherPants>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.Terrafirma.Items.LeatherChestplate.SetBonus");

            player.PlayerStats().MeleeFlatDamage += 2;

            if (player.dashType == DashID.None && !player.mount.Active)
                player.GetModPlayer<LeatherArmorRoll>().CanRoll = true;
        }
    }
    public class LeatherArmorRoll : ModPlayer
    {
        public bool CanRoll;
        bool isRolling;
        int RollTimer = -80;
        int RollDirection = 0;

        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {
            if (isRolling)
            {
                modifiers.HitDirectionOverride = 0;
                modifiers.FinalDamage *= 0.25f;
            }
        }
        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            if (isRolling)
            {
                modifiers.HitDirectionOverride = 0;
                modifiers.FinalDamage *= 0.25f;
            }
        }
        public override void ResetEffects()
        {
            if (CanRoll && RollTimer < -80)
            {
                if (Player.controlRight && Player.releaseRight && Player.doubleTapCardinalTimer[2] < 15)
                {
                    isRolling = true;
                    RollDirection = 1;
                    RollTimer = 20;
                }
                else if (Player.controlLeft && Player.releaseLeft && Player.doubleTapCardinalTimer[3] < 15)
                {
                    isRolling = true;
                    RollDirection = -1;
                    RollTimer = 20;
                }
            }
            else if(RollTimer == -79)
            {
                SoundEngine.PlaySound(SoundID.MaxMana);
            }
            RollTimer--;
            CanRoll = false;
        }
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            if (isRolling)
                Player.legFrame = Player.bodyFrame = new Rectangle(0, 280, Player.legFrame.Width, Player.legFrame.Height);
        }
        public override bool CanUseItem(Item item)
        {
            if (isRolling)
                return false;
            return base.CanUseItem(item);
        }
        public override void PreUpdateMovement()
        {
            if (RollDirection != 0 && isRolling)
            {
                Player.direction = RollDirection;
                Player.fullRotation += (MathHelper.TwoPi / 20f) * RollDirection;
                Player.velocity.X = RollDirection * 10;
                Player.fullRotationOrigin = new Vector2(Player.width / 2, Player.height / 2);
            }
            if(RollTimer == 0)
            {
                Player.velocity.X = Player.maxRunSpeed * RollDirection;
                isRolling = false;
                Player.fullRotation = 0;
                Player.fullRotationOrigin = Vector2.Zero;
            }
        }
    }
}
