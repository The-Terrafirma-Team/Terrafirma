using Microsoft.Xna.Framework;
using System;
using Terrafirma.Common;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment.Healing
{
    class MonsterHeart : ModItem
    {
		public override void SetDefaults()
		{
			Item.DefaultToAccessory();
			Item.rare = ItemRarityID.LightRed;
			Item.value = Item.buyPrice(0, 5, 0, 0);
		}

		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<MonsterHeartPlayer>().Active = true;
		}
        public override void AddRecipes()
        {
            CreateRecipe().AddTile(TileID.DemonAltar).AddIngredient(ItemID.SoulofNight,15).AddIngredient(ItemID.RottenChunk,15).AddIngredient(ItemID.LifeCrystal).Register();
            CreateRecipe().AddTile(TileID.DemonAltar).AddIngredient(ItemID.SoulofNight, 15).AddIngredient(ItemID.Vertebrae, 15).AddIngredient(ItemID.LifeCrystal).Register();
        }
    }
	public class MonsterHeartPlayer : ModPlayer
	{
		public bool Active = false;
        public override void ResetEffects()
        {
            Active = false;
        }
        public override void PostUpdateEquips()
        {
			if (Active)
			{
				Player.statLifeMax2 += Player.aggro / (MathF.Sign(Player.aggro) == 1 ? 10 : 5);
				Player.GetDamage(DamageClass.Generic) -= MathHelper.Clamp( Player.aggro / 4500f,-0.2f,0.2f);
            }
        }
    }
}
