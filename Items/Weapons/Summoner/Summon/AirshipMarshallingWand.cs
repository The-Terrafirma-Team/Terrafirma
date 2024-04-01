using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terrafirma.Projectiles.Summon.Sentry;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Input;
using Terrafirma.Projectiles.Summon.Summons;
using Terrafirma.Buffs.Minions;
using Terrafirma.Systems;

namespace Terrafirma.Items.Weapons.Summoner.Summon
{
    internal class AirshipMarshallingWand : ModItem
    {
        public override void SetStaticDefaults()
        {
            Elements.airItem.Add(Type);
            Elements.electricItem.Add(Type);

            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;

            ItemID.Sets.StaffMinionSlotsRequired[Type] = 1f;
        }
        public override void SetDefaults()
        {
            Item.damage = 25;
            Item.knockBack = 1f;
            Item.DamageType = DamageClass.Summon;
            Item.sentry = true;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.UseSound = SoundID.Item46;

            Item.width = 16;
            Item.height = 16;

            Item.autoReuse = true;
            Item.noMelee = true;
            Item.mana = 20;

            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.shoot = ModContent.ProjectileType<Airship>();
            Item.buffType = ModContent.BuffType<AirshipBuff>();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2);
            position = Main.MouseWorld;

            var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
            projectile.originalDamage = Item.damage;

            return false;   
        }
    }
}
