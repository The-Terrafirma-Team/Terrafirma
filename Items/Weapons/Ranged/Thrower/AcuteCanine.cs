using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Players;
using Terrafirma.Data;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Ranged.Thrower
{
    public class AcuteCanine : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemSets.ThrowerWeapon[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.DefaultToThrownWeapon(ModContent.ProjectileType<Projectiles.Ranged.Throwing.AcuteCanine>(),20,14,true);
            Item.damage = 12;
            Item.UseSound = SoundID.Item1;
            Item.noUseGraphic = true;
            Item.value = ContentSamples.ItemsByType[ItemID.CrimtaneBar].value / 255;
            Item.rare = ItemRarityID.Blue;
        }
        public override void UseItemFrame(Player player)
        {
            PlayerAnimation.AlternatingSwingStyle(player);
        }
        public override void AddRecipes()
        {
            CreateRecipe(125).AddTile(TileID.Anvils).AddIngredient(ItemID.CrimtaneBar, 2).Register();
        }
    }
}
