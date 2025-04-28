using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrafirma.Common.Interfaces;
using Terrafirma.Projectiles.Summon.Commander;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrafirma.Items.Weapons.Summoner.Commander
{
    public class MeteorBaton : ModItem, IHasTertriaryFunction
    {
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Summon;
            Item.damage = 15;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.knockBack = 1;
            Item.shoot = ModContent.ProjectileType<MeteorSaucer>();
            Item.value = ContentSamples.ItemsByType[ItemID.SpaceGun].value;
            Item.width = 16;
            Item.height = 16;
            Item.useStyle = 15;
            Item.noMelee = true;
        }
        public override void UseItemFrame(Player player)
        {
            base.UseItemFrame(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if(player.altFunctionUse == 2)
                player.UpdateCommanderArmy(1, damage, knockback);
            else
                player.UpdateCommanderArmy(0,damage,knockback);
            
            return false;
        }
        void IHasTertriaryFunction.TertriaryShoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.UpdateCommanderArmy(2, damage, knockback);
        }
        bool IHasTertriaryFunction.canUseTertriary(Player player)
        {
            return true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
    }
}
