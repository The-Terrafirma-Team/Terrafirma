using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terrafirma.Data;
using Terrafirma.Projectiles.Summon.Swarm;
using Terrafirma.Systems.Elements;

namespace Terrafirma.Items.Weapons.Summoner.Swarm
{
    internal class VultureStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            Elements.airItem.Add(Type);
            Item.staff[Item.type] = true;
            ItemSets.isSwarmSummonItem[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.knockBack = 4f;
            Item.mana = 15;
            Item.DamageType = DamageClass.Summon;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 80;
            Item.useTime = 80;
            Item.UseSound = SoundID.Item8;

            Item.width = 38;
            Item.height = 46;

            Item.autoReuse = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<SummonedVulture>();
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 30, 0);

        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld;
            velocity *= 0.01f;
        }
    }
}
