using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terrafirma.Data;
using Terrafirma.Projectiles.Summon.Swarm;

namespace Terrafirma.Items.Weapons.Summoner.Swarm
{
    internal class ZombiemancyStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
            ItemSets.isSwarmSummonItem[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.damage = 8;
            Item.knockBack = 4f;

            Item.useAnimation = 80;
            Item.useTime = 80;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item8;

            Item.crit = 15;
            Item.width = 40;
            Item.height = 46;
            Item.DamageType = DamageClass.Summon;

            Item.autoReuse = true;
            Item.noMelee = true;
            Item.mana = 10;
            Item.shoot = ModContent.ProjectileType<ZombiemancedZombie>();
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 30, 0);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld;
            velocity *= 0.01f;
        }
    }
}
