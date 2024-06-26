﻿using Microsoft.Xna.Framework;
using Terrafirma.Common.Players;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Equipment.Ranged
{
    public class Foregrip : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory(8, 16);
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.buyPrice(0, 5, 0, 0);
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<TerrafirmaModPlayer>().Foregrip = true;
        }
    }
    public class ForegripProjectile : GlobalProjectile
    {
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (source is IEntitySource_WithStatsFromItem parent && parent.Player.GetModPlayer<TerrafirmaModPlayer>().Foregrip && parent.Item.useAmmo == AmmoID.Bullet)
            {
                projectile.velocity = Vector2.Lerp(projectile.Center.DirectionTo(Main.MouseWorld) * projectile.velocity.Length(), projectile.velocity, 0.2f) * 1.2f;
            }
        }
    }
}
