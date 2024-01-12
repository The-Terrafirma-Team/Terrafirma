using TerrafirmaRedux.Global;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;

namespace TerrafirmaRedux.Items.Equipment.Ranged
{
    public class BoxOfHighPiercingRounds : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory(32, 30);
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.buyPrice(0, 5, 0, 0);
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<TerrafirmaGlobalPlayer>().BoxOfHighPiercingRounds = true;
        }
        public class BoxOfHighPiercingRoundsProjectile : GlobalProjectile
        {
            int maindamage = -1;
            public override bool InstancePerEntity => true;
            public override void OnSpawn(Projectile projectile, IEntitySource source)
            {
                if (source is IEntitySource_WithStatsFromItem parent && parent.Player.GetModPlayer<TerrafirmaGlobalPlayer>().BoxOfHighPiercingRounds && parent.Item.useAmmo == AmmoID.Bullet)
                {
                    projectile.penetrate += 3;
                    projectile.usesLocalNPCImmunity = true;
                    projectile.localNPCHitCooldown = -1;
                    maindamage = projectile.damage;
                }
            }

            public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
            {
                if (maindamage != -1) { projectile.damage -= (int)(maindamage * 0.15); }
            }

        }
    }

}